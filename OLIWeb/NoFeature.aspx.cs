// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Drawing;
using System.Web.UI;

namespace OliWeb
{
    /// <summary>
    ///     Zusammenfassung für NoCookie.
    /// </summary>
    public partial class NoFeature : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["nocookie"] != null)
            {
                NoCookieLabel.Text = "Browser unterstützt keine Session- Cookies";
                NoCookieLabel.ForeColor = Color.Red;
            }
            else
            {
                NoCookieLabel.Text = "Cookies werden akzeptiert";
                NoCookieLabel.ForeColor = Color.Green;
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}