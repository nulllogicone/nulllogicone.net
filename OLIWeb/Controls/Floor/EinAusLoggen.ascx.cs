// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.ApplicationInsights;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor
{
    /// <summary>
    ///     dieses Control hat zwei Zustände: Wenn man eingeloggt ist, zeigt es den
    ///     Stamm (draufklicken lädt die Daten neu) und Button zum ausloggen - sonst
    ///     zeigt es zwei Textboxen für Name und Kennwort zum einloggen.
    /// </summary>
    public partial class EinAusLoggen : MasterControl
    {
        /// <summary>
        ///     wird angezeigt wenn kein Stamm eingeloggt ist
        /// </summary>
        protected Panel AnonymPanel;

        /// <summary>
        ///     wird angezeigt wenn ein Stamm eingeloggt ist.
        /// </summary>
        protected Panel EingeloggterStammPanel;

        protected KlickBild StammKlickBild;

        protected string GetPrevSessionId
        {
            get { return Session.SessionID; }
        }

        /// <summary>
        ///     <b>javasripct</b> für die Stamm- und Kennwort-Textboxen stellt den Fokus für das Enter-Klicken auf den
        ///     <see cref="ShowStammButton" /> ein.
        /// </summary>
        /// <remarks>
        ///     Die ID der Steuerelemente hängen davon ab wo dieses Control platziert ist. Am einfachsten öffnet man
        ///     eine Seite im Browser und sieht in der Quelltextansicht nach.
        /// </remarks>
        /// <example>
        ///     <code>StammTextBox.Attributes.Add("onKeyDown", 
        ///         "javascript:if(event.keyCode == 13 || event.which == 13 ){event.returnValue=false;event.cancel=true;Kopf1_EinAusLoggen1_ShowStammButton.click();}");
        ///         KennwortTextBox.Attributes.Add("onKeyDown", 
        ///         "javascript:if(event.keyCode == 13 || event.which == 13 ){event.returnValue=false;event.cancel=true;Kopf1_EinAusLoggen1_ShowStammButton.click();}");</code>
        /// </example>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cookie - Prüfung
            var sess = Session.SessionID;
            var prevsess = Request["prevSessionId"];
            if (prevsess != null
                && prevsess.Length > 0
                && prevsess != sess)
            {
                Response.Redirect("~/NoFeature.aspx?nocookie=1");
            }

            // wenn nicht eingeloggt
            if (OliUser.Eingeloggt == false)
            {
                // werden Textfeldereingaben mit Enter abgeschickt
                StammTextBox.Attributes.Add("onKeyDown",
                    "javascript:if(event.keyCode == 13 || event.which == 13 ){event.returnValue=false;event.cancel=true;Kopf1_EinAusLoggen1_ShowStammButton.click();}");
                KennwortTextBox.Attributes.Add("onKeyDown",
                    "javascript:if(event.keyCode == 13 || event.which == 13 ){event.returnValue=false;event.cancel=true;Kopf1_EinAusLoggen1_ShowStammButton.click();}");
            }
        }

        /// <summary>
        ///     abhängig ob jemand eingeloggt ist, wird entweder das eine oder
        ///     andere Panel sichtbar geschaltet.
        ///     Wenn ein Stamm angezeigt wird, wird sein Name in die <see cref="StammTextBox" />
        ///     geschrieben, damit man sich leichter einloggen kann.
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            EingeloggterStammPanel.Visible = false;

            // user.EingeloggterStamm
            if (OliUser.EingeloggterStamm != null)
            {
                EingeloggterStammLinkButton.Text = OliUser.EingeloggterStamm.StammRow.Stamm;
                if (!OliUser.EingeloggterStamm.StammRow.IsDateiNull())
                {
                    StammKlickBild.BildName = OliUser.EingeloggterStamm.StammRow.Datei;
                    StammKlickBild.Breite = 36;
                }
                EingeloggterStammPanel.Visible = true;
                AnonymPanel.Visible = false;
            }
            else
            {
                AnonymPanel.Visible = true;
            }

            if (OliUser.Stamm != null)
            {
                StammTextBox.Text = HttpUtility.HtmlDecode(OliUser.Stamm.StammRow.Stamm);
            }

            // Niemand eingeloggt
            if (OliUser.Stamm == null && OliUser.EingeloggterStamm == null)
            {
                // Beim ersten Aufruf : Focus auf Name zum einloggen
                if (!IsPostBack)
                {
                    FocusAufControl(Page, "Kopf1_EinAusLoggen1_StammTextBox");
                }
            }
        }

        /// <summary>
        ///     <b>POST</b> mit Name und Kennwort zum Einloggen. Schlägt der Versuch
        ///     fehl, wird der Stamm einfach angezeigt.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void ShowStammButton_Click(object sender, EventArgs e)
        {
            showStamm();
        }

        protected void StammZeigenLinkbutton_Click(object sender, EventArgs e)
        {
            showStamm();
        }

        /// <summary>
        ///     durch klick auf den eingeloggten Stammnamen werden die Daten in der
        ///     Mittelschicht neu geladen.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void EingeloggterStammLinkButton_Click(object sender, EventArgs e)
        {
            EingeloggtenStammAnzeigen();
        }

        protected void AnzeigenLinkButton_Click(object sender, EventArgs e)
        {
            EingeloggtenStammAnzeigen();
        }

        private void EingeloggtenStammAnzeigen()
        {
            // merken und ggf. wieder aufrufen
            var pguid = Guid.Empty;
            var tguid = Guid.Empty;

            if (OliUser.Stamm != null)
            {
                if (PostIt != null)
                {
                    pguid = PostIt.PostItRow.PostItGuid;
                }
                if (TopLab != null)
                {
                    tguid = TopLab.TopLabRow.TopLabGuid;
                }
            }

            OliUser.ShowStamm(OliUser.EingeloggterStamm.StammRow.StammGuid);

            // wieder zeigen
            if (pguid != Guid.Empty)
            {
                OliUser.Stamm.ShowPostIt(pguid);
            }
            if (tguid != Guid.Empty)
            {
                OliUser.Stamm.ShowTopLab(tguid);
            }

            Helper.RedirectToSite();
        }

        /// <summary>
        ///     der Stamm wird aus der Mittelschicht ausgeloggt
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void AusloggenButton_Click(object sender, EventArgs e)
        {
            // ein evtl gesetztes Cookie läuft sofort ab!
            var c = Response.Cookies["oliweb"];
            c.Expires = DateTime.Now;
            Response.Cookies.Set(c);

            // user.Nachricht
            if (OliUser.Stamm != null)
            {
                OliUser.Nachricht = OliUser.EingeloggterStamm.StammRow.Stamm + " ausgeloggt";
                //				this.OliUser.Stamm = null;
            }

            OliUser.Ausloggen();

            // Redirect
            Helper.RedirectToSite();
        }

        private void showStamm()
        {
            if (StammTextBox.Text.Length == 0)
            {
                OliUser.Nachricht = "* Name";
                return;
            }

            // ids merken zum wieder herstellen
            var pguid = Guid.Empty;
            var tguid = Guid.Empty;
            var aguid = Guid.Empty;

            if (OliUser.Stamm != null)
            {
                if (PostIt != null)
                {
                    pguid = PostIt.PostItRow.PostItGuid;
                }
                if (TopLab != null)
                {
                    tguid = TopLab.TopLabRow.TopLabGuid;
                }
                if (Angler != null)
                {
                    aguid = Angler.AnglerRow.AnglerGuid;
                }
            }

            // Stamm zeigen (einloggen)
            // TODO: refactor login!
            var success = OliUser.ShowStamm(HttpUtility.HtmlEncode(StammTextBox.Text),
                HttpUtility.HtmlEncode(KennwortTextBox.Text));

            // Application Insights Event tracking
            var telemetry = new TelemetryClient();
            var props = new Dictionary<string, string>
            {
                {"StammTextBox", StammTextBox.Text},
                {"Success", success.ToString()},
                {"Machine", Environment.MachineName }
                
            };
            telemetry.TrackEvent("Login", props);

            // Merken cookie 
            if (MerkenCheckBox.Checked)
            {
                var c = new HttpCookie("oliweb");
                c.Expires = DateTime.Now.AddMonths(1);
                c.Value = "Cookie für automatisch Anmelden bei www.oli-it.com";

                c.Values.Add("autologin", "true");
                c.Values.Add("stamm", StammTextBox.Text);
                c.Values.Add("kennwort", KennwortTextBox.Text);

                Response.SetCookie(c);
            }

            // wenn vorher was offen war : wieder herstellen
            if (pguid != Guid.Empty)
            {
                OliUser.Stamm.ShowPostIt(pguid);
            }
            if (tguid != Guid.Empty)
            {
                OliUser.Stamm.ShowTopLab(tguid);
            }
            //			if (aguid != Guid.Empty)
            //			{
            //				this.OliUser.Stamm.ShowAngler(aguid);
            //			}

            KennwortTextBox.Text = "";

            if (Stamm != null)
            {
                // GO StammSite
                Helper.RedirectToSite();
            }
        }
    }
}