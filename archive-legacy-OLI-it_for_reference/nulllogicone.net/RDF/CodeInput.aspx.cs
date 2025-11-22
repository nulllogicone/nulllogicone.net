// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung f�r CodeInput.
    /// </summary>
    public partial class CodeInput : BasePage
    {
        protected System.Web.UI.WebControls.DataGrid AnglerDataGrid;

        protected OliWeb.Controls.Koerper.ViewGrids.PostItAnglerGrid PostItAnglerGrid1;
        protected OliWeb.Controls.AjaxWortraum.AjaxWortraumControl AjaxWortraumControl1;

        private OliUser user;

        /// <summary>
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            user = SessionManager.Instance().OliUser;

            try
            {
                AjaxWortraumControl1.Markierer = user.Stamm.PostIt.Code;
            }
            catch
            {
            }
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

        protected void ParseButton_Click(object sender, System.EventArgs e)
        {
            MsgLabel.Text = "";
            string rdfstr = InputTextBox.Text;

            if (rdfstr.Length == 0)
            {
                MsgLabel.Text = "f�gen Sie Code.rdf in die TextBox ein";
                return;
            }

            OliEngine.OliDataAccess.Code c = OliEngine.OliDataAccess.Code.parseCodeRDF(rdfstr);

            // ** ERFOLG **
            MsgLabel.Text += "<div style='color:green'>Code parsed successfully</div>";

            // DataGrid Datenbindung
            DataGridPanel.Visible = DataGridCheckBox.Checked;
            if (DataGridCheckBox.Checked)
            {
                CodeDataGrid.DataSource = c.Code;
                RingeDataGrid.DataSource = c.Ringe;
                DataBind();
            }

            // Wortraumcontroller f�llen
            WortraumPanel.Visible = WortraumCheckBox.Checked;
            if (WortraumCheckBox.Checked)
            {
                // Mittelschicht Objekte erstellen
                try
                {
                    CodeDataSet.CodeRow cr = (CodeDataSet.CodeRow) c.Code.Rows[0];
                    user.ShowStamm(cr.StammGuid);
                    user.Stamm.ShowPostIt(cr.PostItGuid);
                    user.Stamm.PostIt.ShowCode(cr.CodeGuid);
                }
                catch (Exception ex)
                {
                    MsgLabel.Text += "Mittelschicht Objekt erstellen fehlgeschlagen :: " + ex.Message;
                    return;
                }
                AjaxWortraumControl1.Markierer = user.Stamm.PostIt.Code;
            }

            // Save in Database
            SavePanel.Visible = SaveCheckBox.Checked;
            if (SaveCheckBox.Checked)
            {
                try
                {
                    c.UpdateCode();
                    c.UpdateRinge();
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
                user.ShowPostIt(c.CodeRow.PostItGuid);
                user.Stamm.PostIt.ShowCode(c.CodeRow.CodeGuid);
                AnglerList al = user.Stamm.PostIt.Code.MyAngler;
            }
        }

        protected void GetRdfButton_Click(object sender, System.EventArgs e)
        {
            string result = OliWeb.Klassen.Helper.GetResponse(UrlTextBox.Text);
            InputTextBox.Text = result;
        }
    }
}
