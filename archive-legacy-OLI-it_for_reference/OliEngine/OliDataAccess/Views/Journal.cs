// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     Journal.
    /// </summary>
    public class Journal : JournalDataSet
    {
        public Journal()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP 10 * FROM oli.UnionJournale ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "UnionJournale");
        }

        public Journal(string zeichen)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql = "SELECT TOP 10 * FROM oli.UnionJournale ";
            sql += "WHERE Zeichen = '" + zeichen + "' ";
            sql += " ORDER BY Datum DESC";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "UnionJournale");
        }

        public Journal(int anzahlZeilen)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP " + anzahlZeilen + " * FROM oli.UnionJournale ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "UnionJournale");
        }

        public Journal(string zeichen, int anzahlZeilen)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql;
            if (zeichen.Length > 0)
            {
                sql = "SELECT TOP " + anzahlZeilen + " * FROM oli.UnionJournale ";
                sql += "WHERE Zeichen = '" + zeichen + "' ";
                sql += " ORDER BY Datum DESC";
            }
            else
            {
                sql = "SELECT TOP " + anzahlZeilen + " * FROM oli.UnionJournale ";
                sql += " ORDER BY Datum DESC";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "UnionJournale");
        }
    }
}
