// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System.Text;
using System.Web.UI;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     stellt davon abgeleiteten Controls das Mittelschicht OliUser Objekt
    ///     zur Verf�gung.
    /// </summary>
    /// <remarks>
    ///     Es wird nicht gepr�ft, ob die jeweiligen Objekte existieren.
    ///     Das muss �ber die Page auf der das Control platziert ist, gew�hrleistet werden.
    /// </remarks>
    public class MasterControl : UserControl
    {
        // Eigenschaften
        // -------------

        protected OliUser OliUser
        {
            get { return SessionManager.Instance().OliUser; }
        }

        protected Stamm Stamm
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     das aktuelle Mittelschicht PostIt.
        /// </summary>
        protected PostIt PostIt
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.PostIt;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                // TODO: eigentlich sollte man das PostIt nur auf null setzen d�rfen.
                // �nderungen an der Mittelschicht werden �ber die Show-Methoden vorgenommen
                OliUser.Stamm.PostIt = value;
            }
        }

        protected TopLab TopLab
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.TopLab;
                }
                else
                {
                    return null;
                }
            }
            set { OliUser.Stamm.TopLab = value; }
        }

        protected Angler Angler
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.Angler;
                }
                else
                {
                    return null;
                }
            }
            set { OliUser.Stamm.Angler = value; }
        }


        // Methoden
        // --------

        //protected void AddVisit(string text)
        //{
        //    // Counter
        //    Counter.AddVisit(Request.UserHostAddress, text);
        //}

        /// <summary>
        ///     ein <b>javasript</b> setzt den Focus auf das angegebene Control.
        ///     So kann man direkt in TextBoxen eingeben oder mit Enter auf Button klicken.
        /// </summary>
        /// <param name="page"> auf welcher Seite soll das Script eingef�gt werden </param>
        /// <param name="ctrlname"> f�r welches Control (den Namen aus der Quelltextansicht) soll der Focus gesetzt werden </param>
        public static void FocusAufControl(Page page, string ctrlname)
        {
            if (!page.ClientScript.IsStartupScriptRegistered("FocusAuf" + ctrlname))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n<script language=\"JavaScript\" type=\"text/javascript\">\n");
                sb.Append("<!-- \n");
                sb.Append("el = document.getElementById(\"" + ctrlname + "\");\n ");
                sb.Append("if(el != null)el.focus();\n");
                sb.Append("// -->\n</script>");

                page.ClientScript.RegisterStartupScript(page.GetType(), "FocusAuf" + ctrlname, sb.ToString());
            }
        }

        protected string RssLink
        {
            get
            {
                var link = "https://xml.oli-it.com/default.aspx";
                var param = "";
                if (Stamm != null)
                {
                    param += "?sguid=" + Stamm.StammRow.StammGuid;
                    if (Stamm.PostIt != null)
                    {
                        param += "&pguid=" + PostIt.PostItRow.PostItGuid;
                    }
                    if (Stamm.Angler != null)
                    {
                        param += "&aguid=" + Angler.AnglerRow.AnglerGuid;
                    }
                }
                return link + param;
            }
        }
    }
}
