// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using System.Web;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     Counter.
    ///     Die Tabelle liegt in der OLIs Datenbank und speichert 
    ///     die Aufrufe der Seiten mit IP und referrer.
    /// </summary>
    public class Counter
    {
        public static void AddVisit(string ip, string site)
        {
            string referrer = "-";
            string rawurl = "-";

            // Counter
            HttpContext ctx = HttpContext.Current;
            if (ctx.Request != null)
            {
                if (ctx.Request.UrlReferrer != null)
                {
                    referrer = ctx.Request.UrlReferrer.ToString();
                }
                rawurl = ctx.Request.RawUrl;
            }
            string sql = "";
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection con = OliEngine.OliCommon.OLIsConnection;
                sql += "INSERT INTO oli.counter (zeit, ip, site, ref, url, OliUser) VALUES(";
                sql += "'" + dt + "', ";
                sql += "'" + ip + "', ";
                sql += "'" + site + "', ";
                sql += "'" + referrer + "', ";
                sql += "'" + rawurl + "', ";
                sql += "'" + SessionManager.Instance().OliUser + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
//				System.Web.HttpContext.Current.Response.Write(ex.Message + "<br>" + sql);
                string s = ex.Message;
            }
        }
    }
}
