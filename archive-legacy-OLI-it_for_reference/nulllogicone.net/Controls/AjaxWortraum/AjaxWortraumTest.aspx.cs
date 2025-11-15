// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace nulllogicone.net.Controls.AjaxWortraum
{
    /// <summary>
    ///     Zusammenfassung f�r AjaxWortraumTest.
    /// </summary>
    public partial class AjaxWortraumTest : System.Web.UI.Page
    {
        private OliEngine.OliMiddleTier.OLIs.Code c;
        private OliEngine.OliMiddleTier.OLIs.Angler a;

        protected System.Web.UI.WebControls.CheckBox FreakCheckBox;
        protected OliWeb.Controls.AjaxWortraum.AjaxWortraumControl AjaxWortraumControl1;
        protected AjaxWortraumControlFlip AjaxWortraumControlFlip1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            AjaxWortraumControl1.ShowVgb = VgbCheckBox.Checked;
            AjaxWortraumControl1.Markierbar = EditCheckBox.Checked;
            AjaxWortraumControl1.Werbefrei = WerbefreiCheckBox.Checked;

            AjaxWortraumControlFlip1.ShowVgb = VgbCheckBox.Checked;
            AjaxWortraumControlFlip1.Markierbar = EditCheckBox.Checked;
            AjaxWortraumControlFlip1.Werbefrei = WerbefreiCheckBox.Checked;
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

        protected void CodeButton_Click(object sender, System.EventArgs e)
        {
            c = new OliEngine.OliMiddleTier.OLIs.Code(new Guid(CodeGuidTextBox.Text));
            AjaxWortraumControl1.Markierer = c;

            DataGrid1.DataSource = c.MyRinge;
            DataBind();
        }

        protected void AnglerButton_Click(object sender, System.EventArgs e)
        {
            a = new OliEngine.OliMiddleTier.OLIs.Angler(new Guid(AnglerGuidTextBox.Text));
            AjaxWortraumControlFlip1.Markierer = a;

            DataGrid1.DataSource = a.MyL�cher;
            DataBind();
        }

        protected void ShowButton_Click(object sender, System.EventArgs e)
        {
            EditCheckBox.Checked = false;
            AjaxWortraumControl1.Markierer = null;
            AjaxWortraumControlFlip1.Markierer = null;
        }
    }
}
