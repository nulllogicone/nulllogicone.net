// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.DataSetTypes;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;
using nulllogicone.net.Controls.AjaxWortraum;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung für AnglerInput.
    /// </summary>
    public partial class AnglerInput : BasePage
    {
        protected System.Web.UI.WebControls.DataGrid PostItDatagrid;

        protected OliWeb.Controls.Koerper.ViewGrids.AnglerPostItGrid AnglerPostItGrid1;
        protected AjaxWortraumControlFlip AjaxWortraumControlFlip1;

        private OliUser user;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            user = SessionManager.Instance().OliUser;

            try
            {
                AjaxWortraumControlFlip1.Markierer = user.Stamm.Angler;
            }
            catch
            {
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

        protected void ParseButton_Click(object sender, System.EventArgs e)
        {
            MsgLabel.Text = "";
            string rdfstr = InputTextBox.Text;

            if (rdfstr.Length == 0)
            {
                MsgLabel.Text = "fügen Sie Angler.rdf in die TextBox ein";
                return;
            }

            OliEngine.OliDataAccess.Angler a = OliEngine.OliDataAccess.Angler.parseAnglerRDF(rdfstr);

            // ** ERFOLG **
            MsgLabel.Text += "<div style='color:green'>Angler parsed successfully</div>";

            // DataGrid Datenbindung
            DataGridPanel.Visible = DataGridCheckBox.Checked;
            if (DataGridCheckBox.Checked)
            {
                AnglerDataGrid.DataSource = a.Angler;
                LöcherDataGrid.DataSource = a.Löcher;
                DataBind();
            }

            // Wortraumcontroller füllen
            WortraumPanel.Visible = WortraumCheckBox.Checked;
            if (WortraumCheckBox.Checked)
            {
                // Mittelschicht Objekte erstellen
                try
                {
                    AnglerDataSet.AnglerRow ar = (AnglerDataSet.AnglerRow) a.Angler.Rows[0];
                    user.ShowStamm(ar.StammGuid);
                    user.Stamm.ShowAngler(ar.AnglerGuid);
                }
                catch (Exception ex)
                {
                    MsgLabel.Text += "Mittelschicht Objekt erstellen fehlgeschlagen :: " + ex.Message;
                    return;
                }
                AjaxWortraumControlFlip1.Markierer = user.Stamm.Angler;
            }

            // Save in Database
            SavePanel.Visible = SaveCheckBox.Checked;
            if (SaveCheckBox.Checked)
            {
                try
                {
                    a.UpdateAngler();
                    a.UpdateLöcher();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 547) // INSERT conflicted FOREIGN KEY constraint
                    {
                        MsgLabel.Text += "Fehler 547";
                    }
                    MsgLabel.Text += ex.Message;
                }
            }

            // Matching
            MatchPanel.Visible = MatchCheckBox.Checked;
            if (MatchCheckBox.Checked)
            {
                user.ShowAngler(a.AnglerRow.AnglerGuid);
            }
        }

        protected void GetRdfButton_Click(object sender, System.EventArgs e)
        {
            string result = OliWeb.Klassen.Helper.GetResponse(UrlTextBox.Text);
            InputTextBox.Text = result;
        }
    }
}