// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Controls.Command;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    ///<summary>
    ///    wird als oberstes Element auf den Seiten eingebunden. Es enthält
    ///    das <see cref="Floor.LogoControl" />, daneben Platz für <b>Nachrichten</b> und das <see cref="Floor.EinAusLoggen" />-Control.
    ///    Ausserdem enthält es die <see cref="CommandBar" /> als vertikale Menüleiste.
    ///</summary>
    public partial class Kopf : MasterControl
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        #endregion

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            //DivGoogleSkyscraper.Visible = !SessionManager.Instance().OliUser.Eingeloggt;
        }

        /// <summary>
        ///     evtl. vorhandene Nachrichten aus der Mittelschicht werden angezeigt 
        ///     und wenn niemand eingeloggt ist, wird der Fokus in die 
        ///     <see cref="Floor.EinAusLoggen.StammTextBox" /> gesetzt (<b>javascript</b>).
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            // NachrichtLabel
            if (OliUser.Nachricht.Length > 0)
            {
                NachrichtLabel.Text = OliUser.Nachricht;
                OliUser.Nachricht = null;
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
    }
}