// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.NKBZ
{
    /// <summary>
    ///     Zusammenfassung für xml.
    /// </summary>
    public class xml : BasePage
    {
        protected System.Web.UI.WebControls.LinkButton LinkButton1;
        protected System.Web.UI.WebControls.TextBox TextBox1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einfügen
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
            LinkButton1.Click += LinkButton1_Click;
            Load += Page_Load;
        }

        #endregion

        private void LinkButton1_Click(object sender, System.EventArgs e)
        {
//			Netz n = new Netz();
//			TextBox1.Text = n.GetXml();
        }
    }
}