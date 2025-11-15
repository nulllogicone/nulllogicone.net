// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;
using VicSoft.Rdf;
using Object = VicSoft.Rdf.Object;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     PostItStamm
    ///     ---------------
    /// 
    ///     Gibt f�r ein PostIt die Urheber zur�ck (St�mme)
    ///     mit den Feldern aus der Wurzeltabelle (frist, bezahlt)
    /// </summary>
    public class PostItStamm : PostItStammDataSet
    {
        public PostItStamm()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.PostItStamm WHERE 1=0 ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "PostItStamm");
        }

        public PostItStamm(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostItStamm_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", postItRow.PostItGuid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "PostItStamm");
        }

        public static PostItStammDataSet ParsePostItStamm(string postItRdf)
        {
            // neues PostIt mit Row erstellen
            PostItStammDataSet postItStammDs = new PostItStammDataSet();

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

            Objects postItStammObjs = parser.GetObjects(pSubj, "http://nulllogicone.net/schema.rdfs#postItStamm");

            foreach (Object postItStammSubj in postItStammObjs)
            {
                PostItStammRow psr = postItStammDs.PostItStamm.NewPostItStammRow();
                psr.PostItGuid = pguid;
                psr.StammGuid =
                    new Guid(parser.GetObjectValue(postItStammSubj.Value,
                                                   "http://nulllogicone.net/schema.rdfs#stammGuid"));
                psr.Stamm = parser.GetObjectValue(postItStammSubj.Value, "http://nulllogicone.net/schema.rdfs#name");
                psr.StammZust =
                    int.Parse(parser.GetObjectValue(postItStammSubj.Value, "http://nulllogicone.net/schema.rdfs#zustand"));
                psr.bezahlt = 0;
                psr.closed = false;

                postItStammDs.PostItStamm.AddPostItStammRow(psr);
            }
            return postItStammDs;
        }
    }
}
