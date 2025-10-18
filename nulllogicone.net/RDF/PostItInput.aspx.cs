// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung für PostItInput.
    /// </summary>
    public partial class PostItInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
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
            PostItStammDataGrid.ItemCommand += PostItStammDataGrid_ItemCommand;
        }

        #endregion

        protected void GetRdfButton_Click(object sender, System.EventArgs e)
        {
            string result = OliWeb.Klassen.Helper.GetResponse(UrlTextBox.Text);
            PostItRdfTextBox.Text = result;
        }

        protected void ParseButton_Click(object sender, System.EventArgs e)
        {
            if (PostItRdfTextBox.Text.Length == 0) return;

            OliEngine.OliDataAccess.PostIt p = OliEngine.OliDataAccess.PostIt.ParsePostItRdf(PostItRdfTextBox.Text);
            PostItDataGrid.DataSource = p.PostIt;

            URIHyperLink.Text = "https://nulllogicone.net/PostIt/?" + p.PostItRow.PostItGuid;
            URIHyperLink.NavigateUrl = "https://nulllogicone.net/PostIt/?" + p.PostItRow.PostItGuid;

            OliEngine.DataSetTypes.Views.PostItStammDataSet psds =
                OliEngine.OliDataAccess.Views.PostItStamm.ParsePostItStamm(PostItRdfTextBox.Text);
            PostItStammDataGrid.DataSource = psds;

            DataBind();
        }

        private void PostItStammDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            PostItDataGrid.SelectedIndex = e.Item.ItemIndex;
            StammLabel.Text = PostItStammDataGrid.DataKeys[e.Item.ItemIndex].ToString();
        }

        protected void LoginButton_Click(object sender, System.EventArgs e)
        {
            OliEngine.StammService.StammService ss = new OliEngine.StammService.StammService();
            OliEngine.StammService.StammDataSet sds = ss.GetStammByNameAndPwd(StammLabel.Text, KennwortTextBox.Text);
            if (sds != null)
            {
                StammServiceLabel.Text = "Stamm angemeldet!<br>aktuelle KooK: " + sds.Stamm[0].KooK.ToString("#,##0.00");
            }
            else
            {
                StammServiceLabel.Text = "Fehler";
                return;
            }
        }
    }
}