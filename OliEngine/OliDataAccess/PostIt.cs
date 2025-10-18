// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using OliEngine.DataSetTypes;
using OliEngine.EventGrid;
using VicSoft.Rdf;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     PostIt.
    /// </summary>
    public class PostIt : PostItDataSet
    {
        protected SqlDataAdapter pad;
        protected SqlDataAdapter wad;

        /// <summary>
        ///     gibt ein leeres
        /// </summary>
        public PostIt()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.PostIt WHERE 1=0";
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            wad = new SqlDataAdapter("SELECT * FROM oli.Wurzeln WHERE 1=0", con);
            SqlCommandBuilder wcb = new SqlCommandBuilder(wad);
        }

        public PostIt(Guid pguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand pcmd = new SqlCommand();
            pcmd.CommandType = CommandType.StoredProcedure;
            pcmd.CommandText = "oli.Get_PostIt_PostItGuid";
            pcmd.Parameters.Add(new SqlParameter("@PostItGuid", pguid));
            pcmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = pcmd;
            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(PostIt);
        }

        public PostIt(TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostIt_TopLabGuid";
            cmd.Parameters.AddWithValue("@TopLabGuid", topLabRow.TopLabGuid);
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(this, "PostIt");
        }

        // da das base-DataSet immer nur eine Reihe hat
        // wird folgende Eigenschaft neu gesetzt:
        // (von Table auf Row geschrumpft)

        public new PostItRow PostItRow
        {
            get { return ((PostItRow) PostIt.Rows[0]); }
        }

        /// <summary>
        ///     aktualisiert die PostIt Tabelle mit einem eigenen UPDATE Command,
        ///     da es zu Speicherkonflikten kommen kann wenn im Hintergrund die Hits-Werte
        ///     geändert werden.
        ///     Das Hinzufügen eines neuen PostIt wird über den DataAdapter erledigt
        /// </summary>
        /// <returns></returns>
        public int UpdatePostIt()
        {
            PostItRow pr = this.PostItRow;
            int i;

            if (pr.RowState != DataRowState.Added)
            {
                SqlConnection con = OliCommon.OLIsConnection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@PostItGuid", pr.PostItGuid);
                cmd.Parameters.AddWithValue("@Titel", pr.IsTitelNull() ? "" : pr.Titel);
                cmd.Parameters.AddWithValue("@PostIt", pr.PostIt);
                cmd.Parameters.AddWithValue("@Datum", pr.Datum);
                cmd.Parameters.AddWithValue("@KooK", pr.KooK);
                cmd.Parameters.AddWithValue("@PostItZust", pr.IsPostItZustNull() ? 1 : pr.PostItZust);
                cmd.Parameters.AddWithValue("@URL", pr.IsURLNull() ? "" : pr.URL);
                cmd.Parameters.AddWithValue("@Datei", pr.IsDateiNull() ? "" : pr.Datei);
                cmd.Parameters.AddWithValue("@Typ", pr.Typ);

                cmd.CommandText =
                    "UPDATE oli.PostIt SET PostItGuid=@PostItGuid, Titel=@Titel, PostIt=@PostIt, Datum=@Datum, KooK=@KooK, PostItZust=@PostItZust, URL=@URL, Datei=@Datei, Typ=@Typ WHERE PostItGuid=@PostItGuid";

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                i = pad.Update(PostIt);
            }

            // Here comes the first EventGrid adaption (after 10years)
            // https://dev.azure.com/nulllogicone/OLI-it/_workitems/edit/642
            // just an experiment
            OliEventGrid.SendEvent(EventType.PostItUpdated, pr);

            pad.Fill(PostIt);
            return i;
        }

        /// <summary>
        ///     die Verknüpfungstabelle zwischen Stamm und PostIt aktualisieren
        /// </summary>
        /// <returns></returns>
        public int UpdateWurzeln()
        {
            int i = wad.Update(this, "Wurzeln");
            return (i);
        }

        // HitPostIt
        // Wird von der Mittelschicht aufgerufen wenn
        // ein PostIt angezeigt wird -> schreibt den
        // Hit-Zähler um eins höer
        public void HitPostIt()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.HitPostIt";
            cmd.Connection = con;

            SqlParameter para = new SqlParameter("@PostItGuid", SqlDbType.UniqueIdentifier);
            para.Value = PostItRow.PostItGuid;
            cmd.Parameters.Add(para);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            pad.Fill(PostIt);
        }

        public static PostIt ParsePostItRdf(string postItRdf)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // neues PostIt mit Row erstellen
            PostIt p = new PostIt();
            PostItRow pr = p.PostIt.NewPostItRow();

            Guid pguid = Guid.Empty;

            // Parser erstellen
            IRdfParser parser = new RdfParser();
            parser.Load(postItRdf);

            // Die PostItGuid suchen
            Objects objs = parser.GetObjects(null, "http://nulllogicone.net/schema.rdfs#postItGuid");
            if (objs.Count != 1)
                throw new Exception(
                    "Keiner oder mehrere PostIt gefunden. In einer PostIt.rdf Datei darf nur eines sein.");
            pguid = new Guid(objs[0].Value);

            string pSubj = parser.GetSubjectValue("http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
                                                  "http://nulllogicone.net/schema.rdfs#PostIt");

            pr.PostItGuid = pguid;
            pr.Titel = parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#titel");
            pr.PostIt = parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#text");
            pr.Datum = DateTime.Parse(parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#datum"));
            pr.KooK = decimal.Parse(parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#flowKook"));
            pr.Typ = parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#typ");
            pr.URL = parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#link");
            pr.Datei = parser.GetObjectValue(pSubj, "http://nulllogicone.net/schema.rdfs#datei");
            pr.Hits = 0;

            p.PostIt.AddPostItRow(pr);
            Thread.CurrentThread.CurrentCulture = ci;
            return p;
        }
    }
}