// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     StammAnglerSite.
    /// </summary>
    public partial class StammAnglerSite : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mittelschichtfehler Abfanger bei zurück und Objektverweis
            if (OliUser.Stamm == null)
            {
                OliUser.Nachricht = "Stamm hat sich verabschiedet - keine Filterprofile mehr";
                Helper.RedirectToSite();
            }

            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
        }

        #region Web Form Designer generated code

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