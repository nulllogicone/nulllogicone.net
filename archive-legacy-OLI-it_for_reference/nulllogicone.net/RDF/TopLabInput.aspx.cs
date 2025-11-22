// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung f�r TopLabInput.
    /// </summary>
    public partial class TopLabInput : System.Web.UI.Page
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
            TopLabRdfTextBox.Text = result;
        }

        protected void ParseButton_Click(object sender, System.EventArgs e)
        {
            if (TopLabRdfTextBox.Text.Length == 0) return;

            OliEngine.DataSetTypes.TopLabDataSet t = OliEngine.OliDataAccess.TopLab.ParseTopLabRdf(TopLabRdfTextBox.Text);
            DataGrid1.DataSource = t.TopLab;

//			URIHyperLink.Text = "https://nulllogicone.net/Stamm/?" + s.StammRow.StammGuid.ToString();
//			URIHyperLink.NavigateUrl = "https://nulllogicone.net/Stamm/?" + s.StammRow.StammGuid.ToString();

            DataBind();
        }
    }
}
