// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    public class Helper
    {

        public static string ScriptName()
        {
            return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        }

        #region RedirectToSite() - aus der Mittelschicht an Seite weiterleiten	

        /// <summary>
        /// wenn durch ein POST-Ereignis an der Mittelschicht etwas ge�ndert wird,
        /// soll auf das <see cref="Ergebnis" /> mit einem GET <i>weitergeleitet</i> werden.
        /// <p>Sie leitet anhand der Mittelschicht-Daten auf die passendste Seite weiter</p>
        /// </summary>
        /// <remarks>
        /// Die Methode sollte eigentlich nur als letzter Notanker geworfen werden,
        /// wenn das gew�nschte Ziel nicht sicher bekannt ist.
        /// <p>Sie kann nur nach POST-backs gefeuert werden und soll den Back-Button-Bug abfangen</p>
        /// </remarks>
        public static void RedirectToSite()
        {
            var ctx = HttpContext.Current;
            var mss = MittelschichtSeite();

            // Weiterleiten auf die entsprechende Seite
            ctx.Response.Redirect(mss);
        }

        public static void UploadFileToCloudStorage(Stamm stamm, HttpPostedFile file)
        {
            var sguid = stamm.StammRow.StammGuid.ToString().ToLower();
            var container = GetContainerClient();
            var name = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            var blobName = sguid + "/" + name;
            var blobRef = container.GetBlobClient(blobName);
            blobRef.Upload(file.InputStream);
            blobRef.SetHttpHeaders(new BlobHttpHeaders() { ContentType = Helper.GetContentTypeByFileExtension(ext) });

        }

        public static BlobContainerClient GetContainerClient()
        {
            var conStr = ConfigurationManager.AppSettings["StorageConnectionString"];
            var container = new BlobContainerClient(conStr, "oliupload");
            return container;
        }
        private static string GetContentTypeByFileExtension(string extension)
        {
            extension = extension.Replace(".", "").ToLower();
            var ct = "";
            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    ct = "image/jpeg";
                    break;
                case "png":
                    ct = "image/png";
                    break;
                case "gif":
                    ct = "image/gif";
                    break;
                case "ico":
                    ct = "image/x-icon";
                    break;
                case "txt":
                case "info":
                    ct = "text/plain";
                    break;
                case "htm":
                case "html":
                    ct = "text/html";
                    break;
                default:
                    ct = "application/octet-stream";
                    break;
            }
            return ct;
        }

        /// <summary>
        /// gibt anhand der Mittelschichtdaten die Seite zur�ck, die am meisten
        /// Elemente darstellt.
        /// </summary>
        /// <returns> die Seite, die die Mittelschicht am Besten darstellen kann. </returns>
        /// <example>
        /// Sites/StammSite.aspx
        /// </example>
        private static string MittelschichtSeite()
        {
            var user = SessionManager.Instance().OliUser;
            var site = "";

            if (user.Stamm == null)
            {
                site = "default.aspx";
                return site;
            }
            site = "StammSite.aspx";
            if (user.Stamm.Angler != null) // SA
            {
                site = "AnglerSite.aspx";
                if (user.Stamm.PostIt != null) // SAP
                {
                    site = "AnglerPSite.aspx";
                    if (user.Stamm.TopLab != null) // SAPT
                    {
                        // +A +P +T
                        site = "AnglerTSite.aspx";
                    }
                }
            }
            else // +Stamm -Angler							// S
            {
                if (user.Stamm.PostIt != null) // SP
                {
                    site = "PostItSite.aspx";
                    if (user.Stamm.TopLab != null) // SPT
                    {
                        // +s +p +t
                        site = "TopLabSite.aspx";
                    }
                    else if (user.Stamm.PostIt.Code != null)
                    {
                        site = "PostItCodeSite.aspx";
                    }
                }
                if (user.Stamm.TopLab != null)
                {
                    site = "TopLabSite.aspx";
                }
            }

            // Wenn hier die site noch nicht besetzt wurde => FEHLER werfen!
            if (site == "")
            {
                throw new HttpException("Helper.RedirectToSite() findet keinen g�ltigen Wert");
            }

            return ("~/Sites/" + site);
        }

        #endregion

        #region SetAllButtons, Sortierpfeil, FocusAufControl, LoadCss, LinkInfo

        /// <summary>
        /// kann alle Controls (Button, LinkButton, ImageButton) und alle
        /// UnterControls auf einer Seite oder in einem Bereich aktivieren
        /// bzw. deaktivieren (enabled = true/false)
        /// </summary>
        /// <remarks>
        ///     <b>Diese Methode wird nicht mehr verwendet</b>
        /// </remarks>
        /// <param name="cc"> Eine ControlCollection (entweder die ganze Seite oder nur ein Bereich) </param>
        /// <param name="enabled"> <b>true</b> wenn alle Buttons aktiviert werden sollen - sonst false </param>
        private static void SetAllButtons(ControlCollection cc, bool enabled)
        {
            foreach (Control c in cc)
            {
                if (c is Button)
                {
                    ((Button) c).Enabled = enabled;
                }
                if (c is LinkButton)
                {
                    ((LinkButton) c).Enabled = enabled;
                }
                if (c is ImageButton)
                {
                    ((ImageButton) c).Enabled = enabled;
                }
                if (c.HasControls())
                {
                    SetAllButtons(c.Controls, enabled);
                }
            }
        }

        /// <summary>
        /// f�r Debug Zwecke.
        /// </summary>
        /// <returns> gibt die unterschiedlichsten Context.Request Eigenschaften als zusammenh�ngenden String aus </returns>
        public static string LinkInfo()
        {
            var ctx = HttpContext.Current;
            var req = ctx.Request;

            var url = req.Url.AbsoluteUri;
            var app = req.ApplicationPath;

            var s = "<b>AbsoluteUri</b> " + url + "<br />";
            s += "<b>ApplicationPath</b> " + app + "<br />";
            s += "<b>MapPath(/)</b> " + ctx.Server.MapPath("/") + "<br />";
            s += "<b>MapPath(Application)</b> " + ctx.Server.MapPath(app) + "<br />";

            return s;
        }

        #endregion
    }
}
