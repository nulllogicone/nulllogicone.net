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
    ///     Zusammenfassung für PostItKonto.
    /// </summary>
    public class PostItKonto : PostItKontoDataSet
    {
        private Guid pguid;

        public PostItKonto(Guid pguid)
        {
            this.pguid = pguid;
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.PostItKonto WHERE PostItGuid=@pguid ORDER BY Datum DESC";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@pguid", pguid);

            SqlDataAdapter pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            pad.Fill(PostItKonto);
        }

        /// <summary>
        /// Simple overload for initial FlowKook only
        /// </summary>
        /// <param name="betrag"></param>
        /// <param name="kommentar"></param>
        public void AddPosten(decimal betrag, string kommentar)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "INSERT INTO oli.PostItKonto (PostItGuid, Datum, Kommentar, Betrag) VALUES(@pguid,@datum,@kommentar,@betrag)";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@pguid", pguid);
            cmd.Parameters.AddWithValue("@datum", DateTime.Now);
            cmd.Parameters.AddWithValue("@kommentar", kommentar);
            cmd.Parameters.AddWithValue("@betrag", betrag);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddPosten(decimal betrag, string kommentar, StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "INSERT INTO oli.PostItKonto (PostItGuid, Datum, Kommentar, Betrag, StammGuid) VALUES(@pguid,@datum,@kommentar,@betrag,@sguid)";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@pguid", pguid);
            cmd.Parameters.AddWithValue("@datum", DateTime.Now);
            cmd.Parameters.AddWithValue("@kommentar", kommentar);
            cmd.Parameters.AddWithValue("@betrag", betrag);
            cmd.Parameters.AddWithValue("@sguid", stammRow.StammGuid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddPosten(decimal betrag, string kommentar, TopLabDataSet.TopLabRow topLabRow)
        {
            using (var con = OliCommon.OLIsConnection)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                    "INSERT INTO oli.PostItKonto (PostItGuid, Datum, Kommentar, Betrag, TopLabGuid) VALUES(@pguid,@datum,@kommentar,@betrag,@tguid)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@pguid", pguid);
                cmd.Parameters.AddWithValue("@datum", DateTime.Now);
                cmd.Parameters.AddWithValue("@kommentar", kommentar);
                cmd.Parameters.AddWithValue("@betrag", betrag);
                cmd.Parameters.AddWithValue("@tguid", topLabRow.TopLabGuid);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}