// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     TopLabList.
    /// </summary>
    public class TopLabList : TopLabDataSet
    {
        private SqlDataAdapter tad;

        public TopLabList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "TopLab");
        }

        public TopLabList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab WHERE StammGuid = '" + stammRow.StammGuid + "'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "TopLab");
        }

        public TopLabList(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab WHERE PostItGuid = '" + postItRow.PostItGuid + "'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "TopLab");
        }

        public TopLabList(string suchT)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.TopLab WHERE TopLab LIKE '%" + suchT + "%' OR Titel LIKE '%" + suchT +
                              "%'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "TopLab");
        }

        public int UpdateTopLab()
        {
            return (tad.Update(base.TopLab));
        }
    }
}
