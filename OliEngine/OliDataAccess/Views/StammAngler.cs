// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     StammAngler.
    /// </summary>
    public class StammAngler : StammAnglerDataSet
    {
        public StammAngler()
        {
        }

        public StammAngler(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_StammAngler_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", stammRow.StammGuid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(StammAngler);
        }
    }
}
