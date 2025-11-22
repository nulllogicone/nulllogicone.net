// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Controls.AjaxWortraum;

namespace nulllogicone.net.Controls.AjaxWortraum
{
    /// <summary>
    ///     Zusammenfassung f�r AjaxWortraumTest.
    /// </summary>
    public partial class AjaxWortraumTest : Page
    {
        private Code c;
        private Angler a;

        protected CheckBox FreakCheckBox;
        protected AjaxWortraumControl AjaxWortraumControl1;
        protected AjaxWortraumControlFlip AjaxWortraumControlFlip1;

        protected void Page_Load(object sender, EventArgs e)
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

        protected void CodeButton_Click(object sender, EventArgs e)
        {
            c = new Code(new Guid(CodeGuidTextBox.Text));
            AjaxWortraumControl1.Markierer = c;

            DataGrid1.DataSource = c.MyRinge;
            DataBind();
        }

        protected void AnglerButton_Click(object sender, EventArgs e)
        {
            a = new Angler(new Guid(AnglerGuidTextBox.Text));
            AjaxWortraumControlFlip1.Markierer = a;

            DataGrid1.DataSource = a.MyL�cher;
            DataBind();
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            EditCheckBox.Checked = false;
            AjaxWortraumControl1.Markierer = null;
            AjaxWortraumControlFlip1.Markierer = null;
        }
    }
}
