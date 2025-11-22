// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     eine Ansammlung von Hilfsfunktionen die an mehreren Orten verwendet werden k�nnen.
    /// </summary>
    /// <remarks>
    ///     Die meisten Methoden k�nnen Statisch aufgerufen werden (eigentlich k�nnte die
    ///     Klasse abstrakt werden). 
    ///     <p>Es sind Seiten-, Control- und Klassen Hilfsfunktionen enthalten sowie
    ///         ganz allgeimeine Hilfen und die sehr spezielle Weiterleitung auf die wohl
    ///         wahrscheinlichste Seite <see cref = "RedirectToSite" /></p>
    /// </remarks>
    public class Helper
    {
        #region MakeBaseLink() - das WurzelVerzeichnis der WebAnwendung/

        /// <summary>
        ///     gibt das AnwendungsRootVerzeichnis aus der Web.config Datei zur�ck.
        /// </summary>
        /// <remarks>
        ///     In der Web.config Datei muss ein Attribut mit dem namen <see cref = "root" />
        ///     und dem Wert des Wurzelverzeichnisses f�r diese Anwendung eingestellt werden.
        ///     Liegt die Anwendung im Root des Webservers reicht ein "/" ansonsten der relative
        ///     Pfad zum Anwendungsverzeichnis mit abschlie�endem /.
        ///     <p>An diese Zeichenfolge kann dann entweder direkt die Resource angeh�ngt werden
        ///         (start.htm) oder mit dem obersten Ordner beginnend bis zur resource f�hren
        ///         (images/bild.jpg).</p>
        ///     <p>Es braucht keinen f�hrenden Backslash!</p>
        /// </remarks>
        /// <example>
        ///     <code>
        ///         &lt;configuration> 
        ///         &lt;appSettings>
        ///         &lt;add key="root" value="/OliWeb/" /> </code>
        /// </example>
        /// <returns>Auf dem lokalen 
        ///     Entwicklungsrechner ist es ein Unterverzeichnis /OliWeb/.
        ///     Auf dem <a href = "https://www.oli-it.com">Produktivserver</a> ist es das Root-Verzeichnis (/).
        /// </returns>
        public static string MakeBaseLink()
        {
            string r = ConfigurationSettings.AppSettings["root"];
            return r;
        }

        #endregion

        public static string ScriptName()
        {
            return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        }

        public static string GetResponse(string url)
        {
            Uri uri = new Uri(url);
            WebRequest req = WebRequest.Create(uri);
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            string result = reader.ReadToEnd();
            return result;
        }

        #region RedirectToSite() - aus der Mittelschicht an Seite weiterleiten	

        ///<summary>
        ///    wenn durch ein POST-Ereignis an der Mittelschicht etwas ge�ndert wird,
        ///    soll auf das <see cref = "Ergebnis" /> mit einem GET <i>weitergeleitet</i> werden.
        ///
        ///
        ///    <p>Sie leitet anhand der Mittelschicht-Daten auf die passendste Seite weiter
        ///    </p>
        ///</summary>
        ///<remarks>
        ///    Die Methode sollte eigentlich nur als letzter Notanker geworfen werden,
        ///    wenn das gew�nschte Ziel nicht sicher bekannt ist. 
        ///    <p>Sie kann nur nach POST-backs gefeuert werden und soll den Back-Button-Bug abfangen</p>
        ///</remarks>
        public static void RedirectToSite()
        {
            System.Web.HttpContext ctx = System.Web.HttpContext.Current;
            string mss = MittelschichtSeite();

            // Weiterleiten auf die entsprechende Seite
            ctx.Response.Redirect(Helper.MakeBaseLink() + mss);
        }


        /// <summary>
        ///     gibt anhand der Mittelschichtdaten die Seite zur�ck, die am meisten
        ///     Elemente darstellt.
        /// </summary>
        /// <returns>die Seite, die die Mittelschicht am Besten darstellen kann.
        /// </returns>
        /// <example>
        ///     Sites/StammSite.aspx
        /// </example>
        private static string MittelschichtSeite()
        {
            OliUser user = SessionManager.Instance().OliUser;
            string site = "";

            if (user.Stamm == null)
            {
                site = "default.aspx";
                return site;
            }
            else // +Stamm
            {
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
            }

            // Wenn hier die site noch nicht besetzt wurde => FEHLER werfen!
            if (site == "")
            {
                throw new System.Web.HttpException("Helper.RedirectToSite() findet keinen g�ltigen Wert");
            }


            return ("Sites/" + site);
        }

        #endregion

        #region SetAllButtons, Sortierpfeil, FocusAufControl, LoadCss, LinkInfo

        /// <summary>
        ///     kann alle Controls (Button, LinkButton, ImageButton) und alle 
        ///     UnterControls auf einer Seite oder in einem Bereich aktivieren
        ///     bzw. deaktivieren (enabled = true/false)
        /// </summary>
        /// <remarks>
        ///     <b>Diese Methode wird nicht mehr verwendet</b>
        /// </remarks>
        /// <param name = "cc">Eine ControlCollection (entweder die ganze Seite 
        ///     oder nur ein Bereich)</param>
        /// <param name = "enabled"><b>true</b> wenn alle Buttons aktiviert werden sollen - sonst false</param>
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


//		/// <summary>
//		/// f�gt in ein DataGrid in der Spalte mit entsprechender SortExpression
//		/// einen hoch/runter Pfeil ein
//		/// </summary>
//		/// <remarks>Diese Methode funktioniert nur in <see cref="Controls.Koerper.ViewGrids.ViewGridControl"/> Elementen
//		/// (Deshalb sollte es auch dorthin oder verallgemeinert werden)
//		/// Aber die Richtung des Pfeiles steckt halt in diesen abgeleiteten Controls ...</remarks>
//		/// <param name="dataGrid">das DataGrid f�r das der Sortierpfeil in der Spalten�berschrift eingestellt werden soll</param>
//		/// <param name="item">die Kopfzeilenelemente (??? glaub ich .???)</param>
//		public static void SortierPfeil(DataGrid dataGrid, DataGridItem item)
//		{
//			string sort = ((OliWeb.Controls.Koerper.ViewGrids.ViewGridControl)dataGrid.Parent).sortString ;
//			bool desc = ((OliWeb.Controls.Koerper.ViewGrids.ViewGridControl)dataGrid.Parent).desc;
//			int i = 0;
//			foreach(DataGridColumn dgc in dataGrid.Columns)
//			{
//				if (dgc.SortExpression == sort && sort.Length > 0)
//				{
//					HtmlImage img = new HtmlImage();
//					if(desc)
//					{
//						img.Src = OliEngine.OliCommon.IconOrdner + "Ecken/eck_runter_16_sw.gif"; 
//					}
//					else
//					{
//						img.Src = OliEngine.OliCommon.IconOrdner + "Ecken/eck_rauf_16_sw.gif"; 
//					}
//					TableCell tc = item.Cells[i];
//
//					tc.Controls.Add(img);
//					break;
//				}
//				i++;
//			}
//		}


//		/// <summary>
//		/// ein <b>javasript</b> setzt den Focus auf das angegebene Control.
//		/// So kann man direkt in TextBoxen eingeben oder mit Enter auf Button klicken.
//		/// </summary>
//		/// <param name="page">auf welcher Seite soll das Script eingef�gt werden</param>
//		/// <param name="ctrlname">f�r welches Control (den Namen aus der Quelltextansicht) soll der Focus gesetzt werden</param>
//		public static void FocusAufControl(Page page, string ctrlname)
//		{
//			if(!page.IsStartupScriptRegistered("FocusAuf" + ctrlname) )
//			{
//				System.Text.StringBuilder sb = new System.Text.StringBuilder();
//				sb.Append("\n<script language=\"JavaScript\">\n");
//				sb.Append("<!-- \n");
//				sb.Append("el = document.getElementById(\"" + ctrlname + "\");\n ");
//				sb.Append("if(el != null)el.focus();\n");
//				sb.Append("// -->\n</script>");
//
//				page.RegisterStartupScript("FocusAuf" + ctrlname, sb.ToString());
//			}
//		}

//		/// <summary>
//		///  Diese Funktion kann im Page_Load einer Seite eingesetzt werden,
//		///  wenn das verlinkte Stylesheet nicht richtig angezeigt wird.
//		///  Sie erzeugt eine java-Funktion auf der Seite. Dann muss
//		///  noch im Html-Quelltext der Seite in das body Tag ein Attribut:
//		/// </summary>
//		/// <example>
//		/// <code>
//		/// &lt;body onload="LoadCSS()">
//		/// </code>
//		/// </example>
//		/// <param name="page"></param>
//		public static void LoadCSS(Page page)
//		{
//			if(!page.IsClientScriptBlockRegistered("LoadCSS"))
//			{
//				System.Text.StringBuilder sb = new System.Text.StringBuilder();
//				sb.Append("\n<script language=\"JavaScript\">\n");
//				sb.Append("<!-- \n");
//				sb.Append("function LoadCSS() \n");
//				sb.Append("{ document.createStyleSheet(href=\"/OliWeb/OliWeb.css\");}\n");
//				sb.Append("// -->\n</script>");
//
//				page.RegisterClientScriptBlock("LoadCSS",sb.ToString());
//			}
//		}


        /// <summary>
        ///     f�r Debug Zwecke.
        /// </summary>
        /// <returns>gibt die unterschiedlichsten Context.Request Eigenschaften als
        ///     zusammenh�ngenden String aus</returns>
        public static string LinkInfo()
        {
            System.Web.HttpContext ctx = System.Web.HttpContext.Current;
            HttpRequest req = ctx.Request;

            string url = req.Url.AbsoluteUri;
            string app = req.ApplicationPath;

            string s = "<b>AbsoluteUri</b> " + url + "<br>";
            s += "<b>ApplicationPath</b> " + app + "<br>";
            s += "<b>MakeBaseLink</b> " + MakeBaseLink() + "<br>";
            s += "<b>MapPath(/)</b> " + ctx.Server.MapPath("/") + "<br>";
            s += "<b>MapPath(Application)</b> " + ctx.Server.MapPath(app) + "<br>";

            return s;
        }

        #endregion
    }
}
