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
    ///     TopLab.
    /// </summary>
    public class TopLab : TopLabDataSet
    {
        private SqlDataAdapter tad;

        public TopLab()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab WHERE 1=0";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            SqlCommandBuilder tcb = new SqlCommandBuilder(tad);

            //			tad.Fill(this,"TopLab");
        }

        public TopLab(Guid tguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab WHERE TopLabGuid='" + tguid + "'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            SqlCommandBuilder tcb = new SqlCommandBuilder(tad);

            tad.Fill(this, "TopLab");
        }

        public int UpdateTopLab()
        {
            return (tad.Update(TopLab));
        }

        // da das this-DataSet immer nur eine Reihe hat
        // wird folgende Eigenschaft neu gesetzt:
        // (von Table auf Row geschrumpft)

        public new TopLabRow TopLabRow
        {
            get
            {
                if (TopLab.Rows.Count > 0)
                {
                    return ((TopLabRow) TopLab.Rows[0]);
                }
                else
                {
                    return (null);
                }
            }
        }

        public static TopLabDataSet ParseTopLabRdf(string toplabRdf)
        {
            TopLabDataSet tds = new TopLabDataSet();
            TopLabRow tr = tds.TopLab.NewTopLabRow();

            Guid tguid;

            IRdfParser parser = new RdfParser();
            parser.Load(toplabRdf);

            // Die TopLabGuid suchen
            Objects objs = parser.GetObjects(null, "http://nulllogicone.net/schema.rdfs#topLabGuid");
            if (objs.Count != 1)
                throw new Exception(
                    "Keine oder mehrere topLabGuid gefunden. In einer TopLab.rdf Datei darf es nur eine geben.");
            tguid = new Guid(objs[0].Value);
            string tSubj = "http://nulllogicone.net/TopLab/?" + tguid;

            tr.TopLabGuid = tguid;

            Object sObj = parser.GetObjects(tSubj, "http://nulllogicone.net/schema.rdfs#topLabStamm")[0];
            string sObjsURI = sObj.Value;
            string sGuid = parser.GetObjectValue(sObjsURI, "http://nulllogicone.net/schema.rdfs#stammGuid");
            tr.StammGuid = new Guid(sGuid);

            Object pObj = parser.GetObjects(tSubj, "http://nulllogicone.net/schema.rdfs#topLabPostIt")[0];
            string pObjsURI = pObj.Value;
            string pGuid = parser.GetObjectValue(pObjsURI, "http://nulllogicone.net/schema.rdfs#postItGuid");
            tr.PostItGuid = new Guid(pGuid);

            tr.TopLab = parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#text");
            tr.Titel = parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#titel");
            tr.Datei = parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#datei");
            tr.Datum = DateTime.Parse(parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#datum"));
            tr.Typ = parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#typ");
            tr.URL = parser.GetObjectValue(tSubj, "http://nulllogicone.net/schema.rdfs#link");
            tr.Lohn = 0;

            tds.TopLab.AddTopLabRow(tr);
            return tds;
        }
    }
}