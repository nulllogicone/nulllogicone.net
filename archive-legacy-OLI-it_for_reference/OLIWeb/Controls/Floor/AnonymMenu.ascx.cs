// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using System.Web.UI.WebControls;
using OliWeb.Klassen;
using OliWeb.Sites.Elemente;

namespace OliWeb.Controls.Floor
{
    ///<summary>
    ///    Das Hauptmen� f�r anonyme (noch nicht eingeloggte) Besucher.
    ///    Es enth�lt Links zu Seiten, die eine �bersicht oder Ableitung
    ///    von OLI-it darstellen oder alles was sonst noch damit zu tun hat.
    ///    Ausserdem ist ein Textfeld f�r die Suche einer Zeichenfolge in
    ///    den Stamm, PostIt und TopLab Tabellen vorhanden.
    ///</summary>
    ///<remarks>
    ///    Weil noch Platz war und ich die Nachrichten von Telepolis
    ///    gut finde, ist noch ein kleiner Ausschnitt (5 zuf�llige Nachrichten)
    ///    von Telepolis-Artikeln angezeigt. (wurde l�ngst wieder eingestellt)
    ///</remarks>
    public partial class AnonymMenu : MasterControl
    {
        protected Image Image1;

        /// <summary>
        ///     Die Enter-Taste soll den <see cref="SuchButton_Click" /> ausl�sen (javascript)
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SuchTextBox.Attributes.Add("onKeyDown",
                                       "javascript:if(event.keyCode == 13 || event.which == 13 )" +
                                       "{event.returnValue=false; " +
                                       "event.cancel=true;" +
                                       "AnonymMenu1_SuchButton.click();}");

            if (Page is NeuAnmelden)
            {
                DivNewStamm.Style.Add("background-color", "white");
            }
            if (Page is Sites.Elemente.Journal)
            {
                DivJournal.Style.Add("background-color", "white");
            }
            if (Page is ChartPage)
            {
                DivChart.Style.Add("background-color", "white");
            }
            if (Page is SuchList)
            {
                DivSearch.Style.Add("background-color", "white");
            }
            if (Page is Nutzungsbedingungen || Page is Impressum)
            {
                DivImprint.Style.Add("background-color", "white");
            }
        }

        /// <summary>
        ///     Wenn mehr als drei Zeichen eingegeben wurden, wird auf die <see cref="SuchList" /> mit
        ///     Querystring weitergeleitet.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void SuchButton_Click(object sender, EventArgs e)
        {
            // Counter
            Counter.AddVisit(Request.UserHostAddress, "<strong>SUCHE: " + SuchTextBox.Text + "</strong>");
            // TODO: remove the Counter class, replace with Application Insights #626


            if (SuchTextBox.Text.Length < 3)
            {
                OliUser.Nachricht = "> 3 char";
            }

            Response.Redirect("~/Sites/Elemente/SuchSite.aspx?such=" +
                              HttpUtility.UrlEncode(SuchTextBox.Text));
        }

        protected void GoogleLinkButton_Click(object sender, EventArgs e)
        {
            string target = "https://www.google.de/search?q=site%3Awww.oli-it.com+" +
                            HttpUtility.UrlEncode(SuchTextBox.Text);
            Response.Redirect(target);
        }
    }
}
