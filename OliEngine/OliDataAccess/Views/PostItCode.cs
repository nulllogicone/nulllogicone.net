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
    ///     CodeSpiegel.
    /// </summary>
    public class PostItCode : PostItCodeDataSet
    {
        public PostItCode(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostItCode_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", postItRow.PostItGuid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(PostItCode);
        }
    }
}
