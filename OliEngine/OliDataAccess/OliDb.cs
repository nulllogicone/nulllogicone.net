// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     OliDb.
    ///     DataAccess - Layer !!
    /// </summary>
    public class OliDb
    {
        // Konstruktor
        private OliDb()
        {
        }

        // Methoden
        // --------

        // GiveTable TODO: change this generic SELECT * FROM Table
        [Obsolete("Try to avoid this method")]
        public static DataTable GiveTable(string tableName)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            // Adapter f�llt DataSet
            ad.Fill(ds);

            return (ds.Tables[0]);
        }

        // GiveRows
        //public static DataRow[] GiveRows(string tableName, string keyField, int id)
        //{
        //    DataTable dt = GiveTable(tableName);
        //    DataRow[] dr = dt.Select(keyField + "=" + id);
        //    return (dr);
        //}

        // GiveRows
        public static DataRow[] GiveRows(string tableName, string keyField, Guid guid)
        {
            DataTable dt = GiveTable(tableName);
            DataRow[] dr = dt.Select(keyField + "='" + guid + "'");
            return (dr);
        }

        // GiveRows
        public static DataRow[] GiveRows(string tableName, string field, string match)
        {
            DataTable dt = GiveTable(tableName);
            DataRow[] dr = dt.Select(field + "='" + match + "'");
            return (dr);
        }

        // GiveRows
        public static DataRow[] GiveRows(string tableName, string field, string match, bool like)
        {
            DataTable dt = GiveTable(tableName);
            if (like)
            {
                DataRow[] dr = dt.Select(field + " LIKE '%" + match + "%'");
                return (dr);
            }
            else
            {
                return (GiveRows(tableName, field, match));
            }
        }

//		// GiveName
//		public static string GiveName(string tableName, int id)
//		{
//			return(null);
//		}
    }
}
