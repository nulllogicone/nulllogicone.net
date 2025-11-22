// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess.Chart
{
    /// <summary>
    ///     PostItChartAccess.
    /// </summary>
    public abstract class PostItChartAccess
    {
        // Member 
        private static readonly SqlConnection con = OliCommon.OLIsConnection;
        private static SqlCommand cmd;
        private static SqlDataAdapter ad;

        public static PostItDataSet.PostItDataTable Teuerste(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.PostIt ORDER BY KooK desc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            PostItDataSet pds = new PostItDataSet();
            ad.Fill(pds.PostIt);
            return (pds.PostIt);
        }

        public static PostItDataSet.PostItDataTable Billigste(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.PostIt ORDER BY KooK asc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            PostItDataSet pds = new PostItDataSet();
            ad.Fill(pds.PostIt);
            return (pds.PostIt);
        }

        public static PostItDataSet.PostItDataTable VielBeachtet(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.PostIt ORDER BY Hits desc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            PostItDataSet pds = new PostItDataSet();
            ad.Fill(pds.PostIt);
            return (pds.PostIt);
        }

        public static PostItDataSet.PostItDataTable UnBeachtet(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.PostIt ORDER BY Hits asc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            PostItDataSet pds = new PostItDataSet();
            ad.Fill(pds.PostIt);
            return (pds.PostIt);
        }
    }
}
