// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     TopLabTopLab.
    /// </summary>
    public class TopLabTopLab : TopLabTopLabDataSet
    {
        public TopLabTopLab()
        {
        }

        public TopLabTopLab(TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLabTopLab WHERE TopTopLabGuid = '" + topLabRow.TopLabGuid + "'";
            cmd.Connection = con;

            SqlDataAdapter tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(TopLabTopLab);
        }
    }
}