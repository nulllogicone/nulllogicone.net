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
    ///     Zusammenfassung für StammKonto.
    /// </summary>
    public class StammKonto : StammKontoDataSet
    {
        private Guid sguid;

        public StammKonto(Guid sguid)
        {
            this.sguid = sguid;
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.StammKonto WHERE StammGuid=@sguid ORDER BY Datum DESC";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@sguid", sguid);

            SqlDataAdapter sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            sad.Fill(StammKonto);
        }

        public void AddPosten(decimal betrag, string kommentar)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "INSERT INTO oli.StammKonto (StammGuid, Datum, Kommentar, Betrag) VALUES(@sguid,@datum,@kommentar,@betrag)";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@sguid", sguid);
            cmd.Parameters.AddWithValue("@datum", DateTime.Now);
            cmd.Parameters.AddWithValue("@kommentar", kommentar);
            cmd.Parameters.AddWithValue("@betrag", betrag);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddPosten(decimal betrag, string kommentar, TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =
                "INSERT INTO oli.StammKonto (StammGuid, Datum, Kommentar, Betrag, TopLabGuid) VALUES(@sguid,@datum,@kommentar,@betrag,@tguid)";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@sguid", sguid);
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