// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using OliEngine;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     schickt eine Mail an info@oli-it.com
    ///     <p>Es wird der aktuelle Mittelschicht OliUser, die Client-IP-Adresse,
    ///         die aufgerufene URL und die Uhrzeit gefolgt von dem Fehler und
    ///         inneren Fehler verschickt</p>
    /// </summary>
    /// <example>
    ///     Alle Laufzeitfehler werden in der <see cref="Global.Application_Error" /> 
    ///     Methode aufgefangen. Dort wird diese Mail erstellt und geschickt.
    ///     <code>ErrorMail em = new ErrorMail(Server.GetLastError());
    ///         em.Send();</code>
    /// </example>
    public class ErrorMail
    {
        // Member
        // ------
        private readonly MailMessage mm;

        /// <summary>
        ///     Konstrukor der ErrorMail.
        ///     Diese Klasse ist ziemlich veraltet - wird aber noch genutzt.
        ///     Deshalb verhält sie sich ganz passiv - beim kleinsten Fehler passiert
        ///     einfach nichts.
        /// </summary>
        /// <param name="error"> der geworfene Fehler </param>
        public ErrorMail(Exception error)
        {
            try
            {
                // OliUser-Objekt aus Session holen
                HttpContext ctx = HttpContext.Current;
                SessionManager sm = (SessionManager) ctx.Session["sm"];
                OliUser u = sm.OliUser;

                // IP Adresse aus Request holen
                string ip = ctx.Request.UserHostAddress;
                // Versuchen die ip aufzulösen
                try
                {
                    if (ip != null)
                    {
                        var host = Dns.GetHostEntry(ip);
                        ip = host.HostName;
                    }
                }
                catch
                {
                }

                mm = new MailMessage();
                mm.From = new MailAddress("info@oli-it.com");
                mm.To.Add(new MailAddress("info@oli-it.com"));
                mm.Subject = "Fehler auf OLI-it";
                mm.IsBodyHtml = true;

                StringBuilder sb = new StringBuilder();

                sb.Append("<hr><strong>OliUser</strong>");
                sb.Append("<p>" + OliUtil.MakeHtmlLineBreak(u.ToString()) + "</p>");

                sb.Append("<hr><strong>Context</strong>");
                sb.Append("<p><b>Zeitpunkt: </b>" + DateTime.Now + "</p>");
                sb.Append("<p><b>IP: </b>" + ip + "</p>");
                sb.Append("<p><b>Request: </b>" + HttpContext.Current.Request.Url + "</p>");

                if (error != null)
                {
                    sb.Append("<hr>");
                    sb.Append("<strong>Exception</strong>");
                    sb.Append("<p><b>Source: </b>" + error.Source + "</p>");
                    sb.Append("<p><b>Message: </b>" + error.Message + "</p>");
                    sb.Append("<p><b>TargetSite: </b>" + error.TargetSite + "</p>");
                    sb.Append("<p><b>StackTrace: </b>" + error.StackTrace + "</p>");
                }
                if (error.InnerException != null)
                {
                    sb.Append("<hr>");
                    sb.Append("<strong>InnerException</strong>");
                    sb.Append("<p><b>Source: </b>" + error.InnerException.Source + "</p>");
                    sb.Append("<p><b>Message: </b>" + error.InnerException.Message + "</p>");
                    sb.Append("<p><b>TargetSite: </b>" + error.InnerException.TargetSite + "</p>");
                    sb.Append("<p><b>StackTrace: </b>" + error.InnerException.StackTrace + "</p>");
                }

                Exception objErr = HttpContext.Current.Server.GetLastError().GetBaseException();
                if (objErr != null)
                {
                    sb.Append("<hr>");
                    sb.Append("<strong>BaseException</strong>");
                    sb.Append("<p><b>Source: </b>" + objErr.Source + "</p>");
                    sb.Append("<p><b>Message: </b>" + objErr.Message + "</p>");
                    sb.Append("<p><b>TargetSite: </b>" + objErr.TargetSite + "</p>");
                    sb.Append("<p><b>StackTrace: </b>" + objErr.StackTrace + "</p>");
                }

                mm.Body = sb.ToString();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     versendet die Nachricht
        /// </summary>
        public void Send()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Send(mm);
            }
            catch
            {
            }
        }
    }
}