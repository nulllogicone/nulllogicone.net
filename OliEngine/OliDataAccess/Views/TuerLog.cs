// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     TuerLog.
    /// </summary>
    public class TuerLog : TuerLogDataSet
    {
        private SqlDataAdapter ad;

        /// <summary>
        /// returns top most 1000 TuerLog entries
        /// </summary>
        public TuerLog()
        {
            var con = OliCommon.OLIsConnection;
            var cmd = new SqlCommand("SELECT TOP 1000 * FROM oli.TuerLog ORDER BY datum DESC", con);
            ad = new SqlDataAdapter(cmd);

            ad.Fill(TuerLog);
        }

        public static void InsertEntry(string ip, Guid eglsguid, Guid sguid, Guid aguid, Guid pguid, Guid tguid,
                                       string kommentar)
        {
            var sql =
                "INSERT INTO oli.tblTuerLog (datum, ip, eglsguid, sguid, aguid, pguid, tguid, kommentar) VALUES (@datum, @ip, @eglsguid, @sguid, @aguid, @pguid, @tguid, @kommentar)";
            var con = OliCommon.OLIsConnection;
            var cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@datum", DateTime.Now);
            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@eglsguid", eglsguid);
            cmd.Parameters.AddWithValue("@sguid", sguid);
            cmd.Parameters.AddWithValue("@aguid", aguid);
            cmd.Parameters.AddWithValue("@pguid", pguid);
            cmd.Parameters.AddWithValue("@tguid", tguid);
            cmd.Parameters.AddWithValue("@kommentar", kommentar);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                var w = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        public int UpdateTuerLog()
        {
            return ad.Update(TuerLog);
        }
    }
}