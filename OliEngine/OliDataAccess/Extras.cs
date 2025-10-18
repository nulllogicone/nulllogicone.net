// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Extras.
    /// </summary>
    public class Extras : ExtrasDataSet
    {
        private SqlDataAdapter ad;

        // Default-Extra
        public Extras()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT TOP 1 * FROM oli.Extras";
            SqlCommand cmd = new SqlCommand(sql, con);

            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder ecb = new SqlCommandBuilder(ad);

            ad.Fill(Extras);
        }

        public Extras(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            string sql = "SELECT * FROM oli.Extras WHERE StammGuid = '" + stammRow.StammGuid + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder ecb = new SqlCommandBuilder(ad);

            ad.Fill(Extras);

            if (Extras.Rows.Count < 1)
            {
                con.Open();
                SqlCommand insCmd =
                    new SqlCommand("INSERT INTO oli.Extras (StammGuid, TxtOnly) VALUES ('" + stammRow.StammGuid + "',0)");
                insCmd.Connection = con;
                int i = insCmd.ExecuteNonQuery();
                con.Close();

                ad.Fill(Extras);
            }
        }

        public bool Werbefrei
        {
            get { return (true); }
        }

        public bool FreakMode
        {
            get { return (true); }
        }

        public new ExtrasRow ExtrasRow
        {
            get
            {
                try
                {
                    return ((ExtrasRow) Extras.Rows[0]);
                }
                catch 
                {
                    return null;
                }
            }
        }

        public int UpdateExtras()
        {
            int r = ad.Update(Extras);
            return (r);
        }
    }
}