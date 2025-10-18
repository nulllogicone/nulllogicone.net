// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess.Chart
{
    /// <summary>
    ///     StammChartAccess.
    /// </summary>
    public abstract class StammChartAccess
    {
        // Member 
        private static readonly SqlConnection con = OliCommon.OLIsConnection;
        private static SqlCommand cmd;
        private static SqlDataAdapter ad;

        public static StammDataSet.StammDataTable Reichste(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.Stamm ORDER BY KooK desc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            StammDataSet sds = new StammDataSet();
            ad.Fill(sds.Stamm);
            return sds.Stamm;
        }

        public static StammDataSet.StammDataTable Aermste(int anz)
        {
            string sql = "SELECT TOP " + anz + " * FROM oli.Stamm ORDER BY KooK asc";
            cmd = new SqlCommand(sql, con);
            ad = new SqlDataAdapter(cmd);
            StammDataSet sds = new StammDataSet();
            ad.Fill(sds.Stamm);
            return sds.Stamm;
        }
    }
}