// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:59
// --------------------------
//  

using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using VicSoft.Rdf;
using Object = VicSoft.Rdf.Object;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Angler.
    /// </summary>
    public class Angler : AnglerDataSet
    {
        protected SqlDataAdapter aad;
        protected SqlDataAdapter lad;

        public Angler()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Angler WHERE 1 = 0";
            cmd.Connection = con;

            aad = new SqlDataAdapter();
            aad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(aad);

            lad = new SqlDataAdapter("SELECT * FROM oli.Löcher", con);
            SqlCommandBuilder lcb = new SqlCommandBuilder(lad);

//			aad.Fill(this.Angler);
        }

        public Angler(Guid aguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            aad = new SqlDataAdapter("SELECT * FROM oli.Angler WHERE AnglerGuid = '" + aguid + "'", con);
            SqlCommandBuilder acb = new SqlCommandBuilder(aad);
            aad.Fill(Angler);

            // wenn kein Angler gefunden wurde => FEHLER werfen
            if (Angler.Rows.Count == 0)
            {
                throw new KeinAnglerException("falsche Angler GUID - nix gefunden");
            }

            lad = new SqlDataAdapter("SELECT * FROM oli.Löcher WHERE AnglerGuid='" + aguid + "'", con);
            SqlCommandBuilder lcb = new SqlCommandBuilder(lad);
            lad.Fill(Löcher);
        }

        public int UpdateAngler()
        {
            return (aad.Update(Angler));
        }

        public int UpdateLöcher()
        {
            return (lad.Update(Löcher));
        }

        public void ClearLoecher()
        {
            if (AnglerRow != null)
            {
                using (var con = OliCommon.OLIsConnection)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM Löcher WHERE AnglerGuid='" + AnglerRow.AnglerGuid + "'";
                    cmd.Connection = con;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public new AnglerRow AnglerRow
        {
            get
            {
                if (Angler.Rows.Count == 0)
                {
                    throw new KeinAnglerException("keine Angler Reihe");
                }
                else
                {
                    AnglerRow ar = (AnglerRow) Angler.Rows[0];
                    return (ar);
                }
            }
        }

        public static Angler parseAnglerRDF(string anglerInput)
        {
            // Angler Felder
            Guid sguid = Guid.Empty;
            Guid aguid = Guid.Empty;
            string beschreibung;

            // Loch Felder
            Guid nguid = Guid.Empty;
            Guid kguid = Guid.Empty;
            Guid bguid = Guid.Empty;
            Guid zguid = Guid.Empty;
            int ilos = -1;
            int fit = -1;

            // DataSet
            Angler a = new Angler();
            AnglerRow ar;
            LöcherRow lr;

            IRdfParser parser = new RdfParser();
            parser.Load(anglerInput);
            Statements st;
            Object obj;
            string str;

            // Das Statement mit dem Predicate #type und Object #Angler suchen => CodeGuid
            st = parser.GetStatements(null, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
                                      "http://nulllogicone.net/schema.rdfs#Angler");
            if (st.Count != 1)
                throw new Exception(
                    "Keiner oder mehrere Angler gefunden. In einer Angler.rdf Datei darf nur einer sein.");
            Statement anglerStatement = st[0];
            str = st[0].Subject.Value;
            aguid = new Guid(str.Substring(str.IndexOf("?") + 1));

            // Das Statement mit Predicate #anglerStamm suchen => StammGuid
            st = parser.GetStatements(anglerStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#anglerStamm",
                                      null);
            if (st.Count != 1)
                throw new Exception("Keiner oder mehrere Stämme gefunden. Ein Angler kann nur von einem Stamm sein");
            str = st[0].Object.Value;
            sguid = new Guid(str.Substring(str.IndexOf("?") + 1));

            // Stamm Name suchen
            st = parser.GetStatements(anglerStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#name", null);
            if (st.Count != 1)
                throw new Exception("Keiner oder mehrere Angler Namen gefunden. Ein Angler kann nur einen haben.");
            string angler = st[0].Object.Value;

            // Kommentar suchen
            st = parser.GetStatements(anglerStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#beschreibung",
                                      null);
            if (st.Count != 1)
                throw new Exception(
                    "Keiner oder mehrere Beschreibungen gefunden. Ein Angler kann nur eine Beschreibung haben.");
            beschreibung = st[0].Object.Value;

            // Neue Angler Reihe erstellen
            ar = a.Angler.NewAnglerRow();
            ar.AnglerGuid = aguid;
            ar.StammGuid = sguid;
            ar.Angler = angler;
            ar.Beschreibung = beschreibung;
            ar.gescannt = false;
            ar.Datum = DateTime.Now;
            a.Angler.Rows.Add(ar);

            // alle Ringe suchen also Predicate #type = #Ring und einzeln durchlaufen und anhängen
            Statements loecher = parser.GetStatements(null, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
                                                      "http://nulllogicone.net/schema.rdfs#Loch");
            foreach (Statement loch in loecher)
            {
                string lochSubj = loch.Subject.Value;
                // Netz			
                obj = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleNetz")[0];
                str = obj.Value;
                nguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                // Knoten			
                obj = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleKnoten")[0];
                str = obj.Value;
                kguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                // Baum
                Objects objs = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleBaum");
                bguid = Guid.Empty;
                if (objs.Count > 0)
                {
                    if (objs.Count > 1) throw new Exception("Warum gibt es hier mehrere Bäume????");
                    obj = objs[0];
                    str = obj.Value;
                    bguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                    // Zweig
                    obj = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleZweig")[0];
                    str = obj.Value;
                    zguid = new Guid(str.Substring(str.IndexOf("?") + 1));
                }

                // ILOs - und davon den #type
                obj = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#ilos")[0];
                obj = parser.GetObjects(obj.Value, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type")[0];
                str = obj.Value;
                switch (str)
                {
                    case "http://nulllogicone.net/schema.rdfs#Muss":
                        ilos = 3;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Sollte":
                        ilos = 2;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Nicht":
                        ilos = 1;
                        break;
                }

                // fit - und davon den #type
                obj = parser.GetObjects(lochSubj, "http://nulllogicone.net/schema.rdfs#fit")[0];
                obj = parser.GetObjects(obj.Value, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type")[0];
                str = obj.Value;
                switch (str)
                {
                    case "http://nulllogicone.net/schema.rdfs#Muss":
                        fit = 3;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Sollte":
                        fit = 2;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Egal":
                        fit = 0;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Nicht":
                        fit = 1;
                        break;
                }

                // neue RingReihe erstellen und hinzufügen
                lr = a.Löcher.NewLöcherRow();
                lr.LochGuid = Guid.NewGuid();
                lr.AnglerGuid = aguid;
                lr.NetzGuid = nguid;
                lr.KnotenGuid = kguid;
                if (bguid != Guid.Empty)
                {
                    lr.BaumGuid = bguid;
                    lr.ZweigGuid = zguid;
                }
                lr.ILOs = ilos;
                lr.Fit = fit;
                a.Löcher.Rows.Add(lr);
            }
            return a;
        }
    }

    /// <summary>
    ///     Kein Angler Exception - weil die Angler gelöscht werden können,
    ///     kommt es immer mal wieder vor, daß einer nicht gefunden wird:
    ///     Deshalb eine eigene Exception
    /// </summary>
    public class KeinAnglerException : Exception
    {
        public KeinAnglerException(string msg) : base(msg)
        {
        }
    }
}