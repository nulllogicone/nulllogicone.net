// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung für test.
    /// </summary>
    public class test : BasePage
    {
        protected System.Web.UI.WebControls.Label Label1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
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
            Load += Page_Load;
        }

        #endregion
    }
}