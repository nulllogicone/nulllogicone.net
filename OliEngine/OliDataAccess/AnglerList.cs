// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     AnglerList.
    /// </summary>
    public class AnglerList : Angler
    {
        public AnglerList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Angler";
            cmd.Connection = con;

            aad = new SqlDataAdapter();
            aad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(aad);

            aad.Fill(Angler);
        }

        public AnglerList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Angler WHERE StammGuid = '" + stammRow.StammGuid + "'";
            cmd.Connection = con;

            aad = new SqlDataAdapter();
            aad.SelectCommand = cmd;
            aad.Fill(Angler);

            foreach (AnglerRow ar in Angler.Rows)
            {
                cmd.CommandText = "SELECT * FROM oli.L�cher WHERE AnglerGuid='" + ar.AnglerGuid + "'";
                aad.Fill(L�cher);
            }
        }

        public AnglerList(CodeDataSet.CodeRow codeRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Angler_CodeGuid";
            cmd.Parameters.Add(new SqlParameter("@CodeGuid", codeRow.CodeGuid));
            cmd.Connection = con;

            aad = new SqlDataAdapter();
            aad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(aad);

            aad.Fill(Angler);
        }
    }
}
