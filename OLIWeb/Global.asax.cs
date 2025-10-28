// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.ApplicationInsights;
using OliEngine;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb
{
    /// <summary>
    ///     Die Klasse <b>Global</b> wird vom Framework zur Verf�gung gestellt und gilt
    ///     f�r die gesamte OliWeb-Anwendung. Hier werden Begin- und End- Application, Session- oder Request- Ereignisse
    ///     behandelt. Aufrufe mit der virtuellen Adresse /[SAPCT]/Guid.aspx werden als
    ///     Querystring an die passende Seite weitergeleitet.
    /// </summary>
    /// <example>
    ///     Die folgenden Beispiele zeigen die kanonischen Adressen auf OLI-it.
    ///     Man kann jede Entit�t direkt anspringen:<br /><br/>
    ///     <A href="https://www.oli-it.com/S/b4111e0e-48d9-42c4-a6f6-ec4991264947.aspx">http://www.oli-it.com/S/b4111e0e-48d9-42c4-a6f6-ec4991264947.aspx</A>
    ///     (mein Stamm)<br /><br />
    ///     <a href="https://www.oli-it.com/P/559948f0-42b2-4e00-be17-a10591063dfc.aspx">http://www.oli-it.com/P/559948f0-42b2-4e00-be17-a10591063dfc.aspx</a>
    ///     (eine Nachricht)<br /><br />
    /// </example>
    public class Global : HttpApplication
    {
        private OliUser user;

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }

        /// <summary>
        ///     zum SessionStart wird ein OliUser erstellt und als Gast eingeloggt -
        ///     wenn er nicht ein Cookie zum automatisch einloggen gesetzt hat.
        ///     <b>Oder auch nicht</b> ich habe es gerade wieder ausgestellt, da�
        ///     man automatisch als Gast eingeloggt wird, da ich es verwirrend f�r
        ///     die bestehenden St�mme finde.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Session_Start(object sender, EventArgs e)
        {
            // OliUser Objekt in der Mittelschicht erstellen
            user = SessionManager.Instance().OliUser;

            // ohne Cookie wird man als Gast eingeloggt
            var c = Request.Cookies["oliweb"];
            if (c == null)
            {
                // Die folgende Zeile loggt automatisch den Stamm 'Gast' ein wenn die
                // Seite das erste Mal aufgerufen wird
                // user.ShowStamm("Gast","");
            }
            else
            {
                if (c.Values["autologin"] == "true")
                {
                    user.ShowStamm(HttpUtility.HtmlEncode(c.Values["stamm"]),
                        HttpUtility.HtmlEncode(c.Values["kennwort"]));
                    Response.Redirect("~/Sites/StammSite.aspx");
                }
            }
        }

        /// <summary>
        ///     Bei jedem Request an den Server wird als allererstes
        ///     dieser Handler aufgerufen.
        ///     <p>
        ///         1. Zuerst wird auf einen Bug von Microsoft gepr�ft (siehe)
        ///         Programmatically check for canonicalization issues with ASP.NET
        ///     </p>
        ///     <p>
        ///         2. Dann werden spezielle URL verarbeitet
        ///         (fr�her wurde mal mit path-rewriting gearbeitet - aber das gab Probleme mit Suchmaschinen)
        ///     </p>
        /// </summary>
        /// <example>
        ///     http://localhost/OliWeb/S/b4111e0e-48d9-42c4-a6f6-ec4991264947.aspx
        ///     <p>
        ///         So kann man �ber die [SAPT]-Guid direkt
        ///         Stamm, Angler, PostIt, TopLab anzeigen!
        ///     </p>
        ///     <p>
        ///         www.oli-it.com/[SAPT]/EntsprechendeGuid.aspx > wird weitergeleitet auf
        ///         www.oli-it.com/Sites/PassendeSeite.aspx?[sapt]guid=EntsprechendeGuid
        ///     </p>
        /// </example>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Behandlung bestimmter Requests 
            // (oli-it.com/[SAPCT]/guid.aspx)
            var ziel = "";
            var cult = "";
            var appendCult = "";
            var ctx = HttpContext.Current;
            var raw = ctx.Request.RawUrl;

            // Language
            var exLang = new Regex(@"/([a-zA-Z]{2}-[a-zA-Z]{2}|[a-zA-Z]{2})/");
            var matchLang = exLang.Match(Request.RawUrl);
            if (matchLang.Success)
            {
                cult = matchLang.Value.Replace("/", "");
                appendCult = "&hl=" + cult;
                raw = raw.Replace(matchLang.Value, "/");
            }

            // A/xxx.aspx
            var exa = new Regex(
                @"[aA]/([0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12})\.aspx");
            var matcha = exa.Match(raw);
            if (matcha.Success)
            {
                var aguid = matcha.Value.Substring(2, 36);
                ziel = "~/Sites/AnglerPostItSite.aspx?aguid=" + aguid + appendCult;
            }

            // S/xxx.aspx
            var exs = new Regex(
                @"[sS]/([0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12})\.aspx");
            var matchs = exs.Match(raw);
            if (matchs.Success)
            {
                var sguid = matchs.Value.Substring(2, 36);
                ziel = "~/Sites/StammSite.aspx?sguid=" + sguid + appendCult;
            }

            // P/xxx.aspx
            var exp = new Regex(
                @"[pP]/([0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12})\.aspx");
            var matchp = exp.Match(raw);
            if (matchp.Success)
            {
                var pguid = matchp.Value.Substring(2, 36);
                ziel = "~/Sites/PostItSite.aspx?pguid=" + pguid + appendCult;
            }

            // T/xxx.aspx
            var ext = new Regex(
                @"[tT]/([0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12})\.aspx");
            var matcht = ext.Match(raw);
            if (matcht.Success)
            {
                var tguid = matcht.Value.Substring(2, 36);
                ziel = "~/Sites/TopLabSite.aspx?tguid=" + tguid + appendCult;
            }

            if (cult != "" && raw.Contains("aspx"))
            {
                if (raw.Contains("?"))
                {
                    ziel = raw + "&hl=" + cult;
                }
                else
                {
                    ziel = raw + "?hl=" + cult;
                }
            }

            if (!string.IsNullOrEmpty(ziel))
            {
                if (ziel.Contains("Elemente/~/T/"))
                {
                    var em = new ErrorMail(new Exception("this should not happen"));
                    em.Send();
                }
                System.Diagnostics.Trace.WriteLine($"{raw} -> redirect -> {ziel}");
                ctx.Response.Redirect(ziel);
            }
        }

        /// <summary>
        ///     An das Ende jeder Antwort wird die <b>FussZeile</b> geschrieben.
        ///     Mit (c), Impressum und Home-Link
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //if (Request.Path.ToLower().EndsWith("sitemap_postit.aspx") ||
            //    Request.Path.ToLower().EndsWith("sitemap_toplab.aspx") ||
            //    Request.Path.ToLower().EndsWith("sitemap_stamm.aspx") ||
            //    Request.Path.ToLower().EndsWith("ashx") ||
            //    Response.ContentType == "application/rdf+xml")
            //{
            //    // mach nichts
            //}
            //else
            //{
            //    // Das wird von der XHTMLPage erledigt, da sie den Fuss innerhalb von
            //    // /form und /body /html einf�gt
            //    SchreibFussZeile();
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     wenn ein Fehler auftritt wird eine <see cref="Klassen.ErrorMail" /> an den Supporter geschrieben
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            var telemetry = new TelemetryClient();
            telemetry.TrackException(ex);

            if (ex is HttpUnhandledException)
            {
                var hue = (HttpUnhandledException) ex;
                if (hue.ErrorCode == 404)
                {
                    Server.Transfer("~/NotFound.aspx");
                }
            }
            if (!(ex is HttpUnhandledException))
            {
                var em = new ErrorMail(ex);
                em.Send();
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        private void Global_EndRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     SchreibFussZeile h�ngt an jede Antwort ein Copyright und Impressum dran.
        ///     Wird von <see cref="Application_EndRequest" /> aufgerufen.
        /// </summary>
        [Obsolete("see Footer.ascx control")]
        private void SchreibFussZeile()
        {
            var sb = new StringBuilder();

            // break
            sb.Append("<div align='left' style='clear:both;'>");
            sb.Append("<hr size='1'></hr>");

            // Home
            sb.Append(" <a href='/default.aspx'><img border='0px' alt='home' src='/images/oli-it_36.jpg' /></a>");

            // CopyRight
            sb.Append("<font size='1'> " + OliCommon.CopyRight + "  </font>");

            // Nutzungsbedingungen 
            sb.Append("<font size='2'> | <a href='~/nutzungsbedingungen.aspx'>Nutzungsbedingungen</a>");

            // Impressum
            sb.Append("<font size='2'> | <a href='~/Impressum.aspx'>Impressum</a>");

            //			if(user != null && user.BookMark.Length > 1)
            //			{
            //				sb.Append(" | <span class=bookmark>");
            //				sb.Append("<a href=" + Helper.ScriptName() + user.BookMark);
            //				sb.Append(" target='oliitlinkpage' title='" + Helper.ScriptName() + user.BookMark + "'>page uri</a></span>");
            //			}

            // DateTime
            sb.Append("<font size='1'> | " + DateTime.Now.ToString("s") + "</font>");

            // comments
            sb.Append("<font size='1'> | comments <a href='mailto:info@oli-it.com'>mailto:info@oli-it.com</a></font>");

            // Schlusszeile zu
            sb.Append("</div>");

            // Google analytics
            // TODO kann vor dem Ver�ffentlichen wieder aktiviert werden
            //sb.Append("<script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\">");
            //sb.Append("</script>");
            //sb.Append("<script type=\"text/javascript\">");
            //sb.Append("_uacct = \"UA-3103879-1\";");
            //sb.Append("urchinTracker();");
            //sb.Append("</script>");

            Response.Write(sb.ToString());
        }
    }
}
