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
    ///     Code.
    /// </summary>
    public class Inbox : InboxDataSet
    {
        protected SqlDataAdapter iad;

        protected Inbox()
        {
        }

        public Inbox(Guid iguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            iad = new SqlDataAdapter("Select * From oli.INBOX WHERE InboxGuid='" + iguid + "'", con);
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        public Inbox(StammDataSet.StammRow stammRow, TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            iad =
                new SqlDataAdapter(
                    "SELECT * FROM oli.Inbox WHERE StammGuid='" + stammRow.StammGuid + "' AND TopLabGuid = '" +
                    topLabRow.TopLabGuid + "'", con);
            SqlCommandBuilder icb = new SqlCommandBuilder(iad);
            iad.Fill(Inbox);
        }

        public new InboxRow InboxRow
        {
            get
            {
                if (Inbox.Rows.Count == 1)
                {
                    return ((InboxRow) Inbox.Rows[0]);
                }
                else
                {
                    return (null);
                }
            }
        }

        // Methoden
        // --------

        // UpdateInbox
        public int UpdateInbox()
        {
            return (iad.Update(Inbox));
        }
    }
}