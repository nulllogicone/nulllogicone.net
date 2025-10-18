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
    ///     StammShortCutsSite.
    /// </summary>
    public partial class StammShortCutsSite : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}