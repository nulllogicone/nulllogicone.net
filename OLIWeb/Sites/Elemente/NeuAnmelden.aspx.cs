// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliEngine.OliDataAccess;
using OliWeb.Controls.Koerper;
using OliWeb.Klassen;
using Stamm = OliEngine.OliMiddleTier.OLIs.Stamm;

namespace OliWeb.Sites.Elemente
{
    public partial class NeuAnmelden : BasePage
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

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        protected Image OLIitImage;
        protected StammKoerper StammKoerper1;

        // Eigenschaften
        // -------------

        // Methoden
        // --------

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            // Counter
//			this.AddVisit("Neu Anmelden angeschaut");

            // BUGFIX 
            // beim ersten anzeigen den Stamm leer machen
            // TODO: - in StammOrgan vor Anzeige der Daten prüfen,
            // ob man eingeloggt ist.
            if (!IsPostBack)
            {
                OliUser.Stamm = null;
            }

            if (OliUser.Stamm == null)
            {
                StammKoerper1.Visible = false;
            }

            if (Request["von"] != null)
            {
                var von = Request["von"];

                // dem user-Objekt wird die Freund Eigenschaft eingestellt
                Guid freundGuid = new Guid(von);
                Stamm freund = new Stamm(OliUser, freundGuid);
                OliUser.Freund = freund;

                FreundLabel.Text = freund.StammRow.Stamm;

                FreundGuidPanel.Visible = true;
                FreundEmailPanel.Visible = false;
            }

            if (OliUser.Freund != null)
            {
                FreundGuidPanel.Visible = true;
                FreundEmailPanel.Visible = false;
            }
            else
            {
                FreundEmailPanel.Visible = true;
                FreundGuidPanel.Visible = false;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = true;
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (OkCheckBox.Checked)
            {
                RechtPanel.Visible = false;
                OliUser.Stamm = OliUser.NewStamm();
                Response.Redirect("../Edit/StammEdit.aspx?cmd=newS");
            }
            else
            {
                OkLabel.Text = "Sie müssen Ihre Zustimmung erklären";
            }
        }

        protected void SuchFreundButton_Click(object sender, EventArgs e)
        {
            DataRow[] dr = OliDb.GiveRows("oli.Stamm", "Email", FreundEmailTextBox.Text, false);
            if (dr != null && dr.Length > 0)
            {
//				FreundGuidTextBox.Text = dr[0]["StammGuid"].ToString();
                Guid freundGuid = new Guid(dr[0]["StammGuid"].ToString());
                Stamm freund = new Stamm(OliUser, freundGuid);
                OliUser.Freund = freund;

                FreundLabel.Text = freund.StammRow.Stamm;

                FreundGuidPanel.Visible = true;
                FreundEmailPanel.Visible = false;
            }
            else
            {
                Label ngl = new Label
                {
                    Text = "<br />Emailadresse nicht gefunden"
                };
                FreundEmailPanel.Controls.Add(ngl);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Helper.RedirectToSite();
        }
    }
}