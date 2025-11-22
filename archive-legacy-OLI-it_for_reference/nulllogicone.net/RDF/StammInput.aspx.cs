// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung f�r StammInput.
    /// </summary>
    public partial class StammInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einf�gen
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

        protected void GetRdfButton_Click(object sender, System.EventArgs e)
        {
            string result = OliWeb.Klassen.Helper.GetResponse(UrlTextBox.Text);
            StammRdfTextBox.Text = result;
        }

        protected void ParseButton_Click(object sender, System.EventArgs e)
        {
            if (StammRdfTextBox.Text.Length == 0) return;

            OliEngine.OliDataAccess.Stamm s = OliEngine.OliDataAccess.Stamm.ParseRdf(StammRdfTextBox.Text);
            DataGrid1.DataSource = s.Stamm;

            URIHyperLink.Text = "https://nulllogicone.net/Stamm/?" + s.StammRow.StammGuid;
            URIHyperLink.NavigateUrl = "https://nulllogicone.net/Stamm/?" + s.StammRow.StammGuid;

            DataBind();
        }
    }
}
