// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess.Chart
{
    /// <summary>
    ///     TopLabChartAccess.
    /// </summary>
    public abstract class TopLabChartAccess
    {
        // Member 
        private static readonly SqlConnection con = OliCommon.OLIsConnection;
        private static SqlCommand cmd;
        private static SqlDataAdapter ad;

        public static TopLabDataSet.TopLabDataTable Top(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.TopLab ORDER BY Lohn desc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            TopLabDataSet tds = new TopLabDataSet();
            ad.Fill(tds.TopLab);
            return (tds.TopLab);
        }

        public static TopLabDataSet.TopLabDataTable Flop(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.TopLab ORDER BY Lohn asc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            TopLabDataSet tds = new TopLabDataSet();
            ad.Fill(tds.TopLab);
            return (tds.TopLab);
        }
    }
}