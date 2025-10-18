// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     StammPostIt
    ///     ----------------
    /// 
    ///     Gibt für einen Stamm seine Nachrichten (PostIt)
    ///     und die Werte aus der Wurzeltabelle (bezahlt, frist)
    /// </summary>
    public class StammPostIt : StammPostItDataSet
    {
        public StammPostIt()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.StammPostIt WHERE 1=0 ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "StammPostIt");
        }

        public StammPostIt(bool closed)
        {
            // in der Abfrage StammPostIt steht wclosed für die jeweilige Wurzel zum stamm
            // und closed ob es insgesamt geschlossen ist
            if (closed)
            {
                SqlConnection con = OliCommon.OLIsConnection;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM oli.StammPostIt WHERE wclosed <> 0 ORDER BY Datum DESC";
                cmd.Connection = con;

                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;

                ad.Fill(this, "StammPostIt");
            }
            else
            {
                SqlConnection con = OliCommon.OLIsConnection;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM oli.StammPostIt WHERE wclosed = 0 ORDER BY Datum DESC";
                cmd.Connection = con;

                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = cmd;

                ad.Fill(this, "StammPostIt");
            }
        }

        public StammPostIt(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_StammPostIt_open_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", stammRow.StammGuid));

            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "StammPostIt");
        }

        public StammPostIt(StammDataSet.StammRow stammRow, bool withClosed)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_StammPostIt_all_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", stammRow.StammGuid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(this, "StammPostIt");
        }
    }
}