// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    ///<summary>
    ///    UserEinstellungen.
    ///</summary>
    public partial class UserEinstellungen : UserControl
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        #endregion

        // Member
        // ------

        private OliUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //			Helper.SetAllButtons(this.Page.Controls, false);
            UpdateLinkButton.Enabled = true;

            // user
            user = SessionManager.Instance().OliUser;

            if (!IsPostBack)
            {
                // QDropDownList
                QDropDownList.DataSource = user.QList;
                QDropDownList.DataBind();

                if (user.Stamm != null)
                {
                    ExtrasDataSet.ExtrasRow er = user.Stamm.Extras.ExtrasRow;

                    ZeilenZahlTextBox.Text = MakeValidZeilenZahl(er.ZeilenZahl).ToString();
                    HilfeCheckBox.Checked = er.hilfe;
                    ClosedCheckBox.Checked = er.showclosed;
                    WerbefreiCheckBox.Checked = er.werbefrei;
                    FreakModeCheckBox.Checked = er.freakmode;
                    TxtOnlyCheckBox.Checked = er.TxtOnly;
                    boundKookLabel.Text = "bound kook";
                    flowKookLabel.Text = "flow kook";

                    // MailNachrichten
                    GutschriftCheckBox.Checked = er.gutschrift;
                    FristAblaufCheckBox.Checked = er.fristablauf;
                    NeueAntwortenCheckBox.Checked = er.newT;
                    NeueNachrichtenCheckBox.Checked = er.newP;

                    // MailFormat
                    HtmlRadioButton.Checked = er.HtmlMail;
                    TextRadioButton.Checked = !er.HtmlMail;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (user.Stamm != null)
            {
                QDropDownList.SelectedIndex = -1;
                ListItem li = QDropDownList.Items.FindByValue(user.Stamm.Q.QID.ToString());
                li.Selected = true;

                if (user.Stamm.BinIchEingeloggt)
                {
                    UpdateLinkButton.Enabled = true;
                }
                else
                {
                    UpdateLinkButton.Enabled = false;
                }
            }
        }

        protected void UpdateLinkButton_Click(object sender, EventArgs e)
        {
            Guid sguid = user.Stamm.StammRow.StammGuid;

            // Stamm Sprache (Q) einstellen und Update
            user.Stamm.StammRow.zuQID = int.Parse(QDropDownList.SelectedItem.Value);
            user.Stamm.UpdateStamm();

            // Extras Update
            ExtrasDataSet.ExtrasRow er = user.Stamm.Extras.ExtrasRow;

            // ListenAnsichten
            er.hilfe = HilfeCheckBox.Checked;
            er.showclosed = ClosedCheckBox.Checked;
            er.ZeilenZahl = MakeValidZeilenZahl(int.Parse(ZeilenZahlTextBox.Text));
            er.freakmode = FreakModeCheckBox.Checked;
            er.werbefrei = WerbefreiCheckBox.Checked;
            er.TxtOnly = TxtOnlyCheckBox.Checked;

            // EmailVersand
            er.fristablauf = FristAblaufCheckBox.Checked;
            er.gutschrift = GutschriftCheckBox.Checked;
            er.newT = NeueAntwortenCheckBox.Checked;
            er.newP = NeueNachrichtenCheckBox.Checked;

            // EmailFormat
            er.HtmlMail = HtmlRadioButton.Checked;

            user.Stamm.Extras.UpdateExtras();

            // neu anzeigen
            user.ShowStamm(sguid);

            //			// Einstellungen unsichtbar
            //			StammOrgan so = (StammOrgan)this.Parent.Parent ;
            //			so.FindControl("UserEinstellungen1").Visible = false;
            //			so.ShowEdit = false;

            // Alle Buttons enabeln
            //			Helper.SetAllButtons(this.Page.Controls, true);
//			Helper.RedirectToSite();
            Response.Redirect("~/Sites/StammSite.aspx");
        }

        private int MakeValidZeilenZahl(int zahl)
        {
            int z = zahl;
            if (z < 1) z = 1;
            if (z > 100) z = 100;
            return z;
        }
    }
}