// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using OliEngine.OliDataAccess;

namespace OliWeb
{
    /// <summary>
    ///     erstellt eine Google Sitemap mit allen Stamm-Seiten Links
    /// </summary>
    public partial class Sitemap_Stamm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StammList sl = new StammList();
            Repeater.DataSource = sl.Stamm;
            DataBind();
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}
