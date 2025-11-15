// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     ShortCutsList.
    /// </summary>
    public class ShortCutsList : ShortCutsDataSet
    {
        public ShortCutsList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            con.Open();

            // ShortCuts -Tabelle
            SqlDataAdapter SCad =
                new SqlDataAdapter("Select * From oli.ShortCuts WHERE StammGuid='" + stammRow.StammGuid + "'", con);
            SCad.Fill(ShortCuts);
            SqlCommandBuilder SCcb = new SqlCommandBuilder(SCad);
        }
    }
}
