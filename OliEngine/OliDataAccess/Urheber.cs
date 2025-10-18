// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Urheber.
    /// </summary>
    public class Urheber : StammDataSet
    {
        private SqlDataAdapter sad;

        public Urheber()
        {
        }

        public Urheber(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();

            string sql = " SELECT Stamm.* ";
            sql += " FROM Stamm INNER JOIN Wurzeln ";
            sql += " ON Stamm.StammGuid = Wurzeln.StammGuid ";
            sql += " WHERE Wurzeln.PostItGuid = '" + postItRow.PostItGuid + "'";

            cmd.CommandText = sql;
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            sad.Fill(this, "Stamm");
        }
    }
}