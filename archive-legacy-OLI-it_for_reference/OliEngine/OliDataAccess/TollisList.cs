// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     TollisList.
    /// </summary>
    public class TollisList : TollisDataSet
    {
        private SqlDataAdapter tad;

        public TollisList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Tollis";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "Tollis");
        }

        public TollisList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Tollis WHERE StammGuid='" + stammRow.StammGuid + "'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "Tollis");
        }

        public TollisList(TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Tollis WHERE TopLabGuid='" + topLabRow.TopLabGuid + "'";
            cmd.Connection = con;

            tad = new SqlDataAdapter();
            tad.SelectCommand = cmd;

            tad.Fill(this, "Tollis");
        }
    }
}
