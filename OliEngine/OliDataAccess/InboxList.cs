// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    public class InboxList : Inbox
    {
        // Member
        // ------

        // Konstruktor
        // -----------
        public InboxList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            iad = new SqlDataAdapter("Select * From oli.INBOX", con);
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        public InboxList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            iad = new SqlDataAdapter("Select * From oli.INBOX WHERE StammGuid='" + stammRow.StammGuid + "'", con);
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        public InboxList(StammDataSet.StammRow stammRow, bool zuMailen)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            if (zuMailen)
            {
                iad =
                    new SqlDataAdapter(
                        "Select * From oli.INBOX WHERE StammGuid='" + stammRow.StammGuid +
                        "' AND (gelesen IS NULL) AND (gesehen IS NULL) AND (gemailt IS NULL)", con);
            }
            else
            {
                iad = new SqlDataAdapter("Select * From oli.INBOX WHERE StammGuid='" + stammRow.StammGuid + "'", con);
            }
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        public InboxList(bool zuMailen)
        {
            // noch nicht gesehen oder gelesene Inbox Nachrichten
            // zum verMailen !!!
            SqlConnection con = OliCommon.OLIsConnection;

            if (zuMailen)
            {
                iad =
                    new SqlDataAdapter(
                        "SELECT * FROM oli.Inbox WHERE (gelesen IS NULL) AND (gesehen IS NULL) AND (gemailt IS NULL)",
                        con);
            }
            else
            {
                iad = new SqlDataAdapter("SELECT * FROM oli.Inbox", con);
            }
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        // Eigenschaften
        // -------------

        // Methoden
        // --------

//		public InboxList StaemmeToMailInbox()
//		{
//			SqlConnection con = OliCommon.OLIsConnection ;
//			iad = new SqlDataAdapter("SELECT DISTINCT StammGuid FROM Inbox WHERE (gelesen IS NULL) AND (gesehen IS NULL) AND (gemailt IS NULL)",con);
//			iad.Fill(this.Inbox);
//			return this;
//		}

        public new int UpdateInbox()
        {
            return iad.Update(Inbox);
        }
    }
}