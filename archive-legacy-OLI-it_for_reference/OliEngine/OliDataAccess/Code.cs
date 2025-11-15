// --------------------------
// (c) frederic@luchting.de
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
    ///     Code.
    /// </summary>
    public class Code : CodeDataSet
    {
        protected SqlDataAdapter cad;
        protected SqlDataAdapter rad;

        public Code()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Code WHERE 1 = 0";
            cmd.Connection = con;

            cad = new SqlDataAdapter();
            cad.SelectCommand = cmd;
            SqlCommandBuilder cb = new SqlCommandBuilder(cad);

            // die folgende Zeilen sind n�tig,
            // falls das DataSet aus xml generiert und dann gespeichert wird
            rad = new SqlDataAdapter("Select * From oli.Ringe", con);
            SqlCommandBuilder rcb = new SqlCommandBuilder(rad);
        }

        public Code(Guid cguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            cad = new SqlDataAdapter("Select * From oli.Code WHERE CodeGuid='" + cguid + "'", con);
            SqlCommandBuilder ccb = new SqlCommandBuilder(cad);
            cad.Fill(Code);

            rad = new SqlDataAdapter("Select * From oli.Ringe WHERE CodeGuid='" + cguid + "'", con);
            SqlCommandBuilder rcb = new SqlCommandBuilder(rad);
            rad.Fill(Ringe);
        }

        public int UpdateCode()
        {
            return (cad.Update(Code));
        }

        // da das this-DataSet immer nur eine Reihe hat
        // wird folgende Eigenschaft neu gesetzt:
        // (von Table auf Row geschrumpft)
        public new CodeRow CodeRow
        {
            get { return ((CodeRow) Code.Rows[0]); }
        }

        /// <summary>
        ///     Updates the ringe.
        /// </summary>
        /// <returns></returns>
        public int UpdateRinge()
        {
            int r = rad.Update(Ringe);
            return (r);
        }

        public static Code parseCodeRDF(string codeInput)
        {
            // Code Felder
            Guid sguid = Guid.Empty;
            Guid pguid = Guid.Empty;
            Guid cguid = Guid.Empty;
            string beschreibung;

            // Ringe Felder
            Guid nguid = Guid.Empty;
            Guid kguid = Guid.Empty;
            Guid bguid = Guid.Empty;
            Guid zguid = Guid.Empty;
            int olis = -1;
            int _get = -1;

            // DataSet
            Code c = new Code();
            CodeRow cr;
            RingeRow rr;

            IRdfParser parser = new RdfParser();
            parser.Load(codeInput);
            Statements st;
            Object obj;
            string str;

            // Das Statement mit dem Predicate #type und Object #Code suchen => CodeGuid
            st = parser.GetStatements(null, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
                                      "http://nulllogicone.net/schema.rdfs#Code");
            if (st.Count != 1)
                throw new Exception("Keiner oder mehrere Codes gefunden. In einer Code.rdf Datei darf nur einer sein.");
            Statement codeStatement = st[0];
            str = st[0].Subject.Value;
            cguid = new Guid(str.Substring(str.IndexOf("?") + 1));

            // Das Statement mit Predicate #codePostIt suchen => PostItGuid
            st = parser.GetStatements(codeStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#codePostIt",
                                      null);
            if (st.Count != 1)
                throw new Exception("Keiner oder mehrere PostIt gefunden. Ein Code kann nur zu einem PostIt geh�ren.");
            str = st[0].Object.Value;
            pguid = new Guid(str.Substring(str.IndexOf("?") + 1));

            // Das Statement mit Predicate #codeStamm suchen => StammGuid
            st = parser.GetStatements(codeStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#codeStamm", null);
            if (st.Count != 1)
                throw new Exception("Keiner oder mehrere St�mme gefunden. Ein Code kann nur von einem Stamm sein");
            str = st[0].Object.Value;
            sguid = new Guid(str.Substring(str.IndexOf("?") + 1));

            // Kommentar suchen
            st = parser.GetStatements(codeStatement.Subject.Value, "http://nulllogicone.net/schema.rdfs#beschreibung",
                                      null);
            if (st.Count != 1)
                throw new Exception(
                    "Keiner oder mehrere Beschreibungen gefunden. Ein Code kann nur eine Beschreibung haben.");
            string bstr = st[0].Object.Value;
            beschreibung = bstr;

            // Neue Code Reihe erstellen
            cr = c.Code.NewCodeRow();
            cr.CodeGuid = cguid;
            cr.PostItGuid = pguid;
            cr.StammGuid = sguid;
            cr.gescannt = false;
            cr.Kommentar = beschreibung;
            c.Code.Rows.Add(cr);

            // alle Ringe suchen also Predicate #type = #Ring und einzeln durchlaufen und anh�ngen
            Statements ringe = parser.GetStatements(null, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
                                                    "http://nulllogicone.net/schema.rdfs#Ring");
            foreach (Statement ring in ringe)
            {
                string ringSubj = ring.Subject.Value;
                // Netz			
                obj = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleNetz")[0];
                str = obj.Value;
                nguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                // Knoten			
                obj = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleKnoten")[0];
                str = obj.Value;
                kguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                // Baum
                bguid = Guid.Empty;
                Objects objs = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleBaum");
                if (objs.Count > 0)
                {
                    if (objs.Count > 1) throw new Exception("Warum gibt es hier mehrere B�ume????");
                    obj = objs[0];
                    str = obj.Value;
                    bguid = new Guid(str.Substring(str.IndexOf("?") + 1));

                    // Zweig
                    obj = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#markierungsStelleZweig")[0];
                    str = obj.Value;
                    zguid = new Guid(str.Substring(str.IndexOf("?") + 1));
                }

                // OLIs
                obj = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#olis")[0];
                obj = parser.GetObjects(obj.Value, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type")[0];
                str = obj.Value;
                switch (str)
                {
                    case "http://nulllogicone.net/schema.rdfs#Muss":
                        olis = 3;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Sollte":
                        olis = 2;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Nicht":
                        olis = 1;
                        break;
                }

                // get
                obj = parser.GetObjects(ringSubj, "http://nulllogicone.net/schema.rdfs#get")[0];
                obj = parser.GetObjects(obj.Value, "http://www.w3.org/1999/02/22-rdf-syntax-ns#type")[0];
                str = obj.Value;
                switch (str)
                {
                    case "http://nulllogicone.net/schema.rdfs#Muss":
                        _get = 3;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Sollte":
                        _get = 2;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Egal":
                        _get = 0;
                        break;
                    case "http://nulllogicone.net/schema.rdfs#Nicht":
                        _get = 1;
                        break;
                }

                // neue RingReihe erstellen und hinzuf�gen
                rr = c.Ringe.NewRingeRow();
                rr.RingGuid = Guid.NewGuid();
                rr.CodeGuid = cguid;
                rr.NetzGuid = nguid;
                rr.KnotenGuid = kguid;
                if (bguid != Guid.Empty)
                {
                    rr.BaumGuid = bguid;
                    rr.ZweigGuid = zguid;
                }
                rr.OLIs = olis;
                rr.Get = _get;
                c.Ringe.Rows.Add(rr);
            }
            return c;
        }
    }
}
