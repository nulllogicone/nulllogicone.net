// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     StammSite.
    /// </summary>
    /// <example>
    ///     Meine persönliche <a href="https://www.oli-it.com/S/b4111e0e-48d9-42c4-a6f6-ec4991264947.aspx">StammSite</a>
    /// </example>
    public partial class StammSite : MasterStammPage
    {
        protected HyperLink PostItHyperLink;

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

        // Member
        // ------

        private bool biegl;

        protected void Page_Load(object sender, EventArgs e)
        {
            biegl = OliUser.Stamm.BinIchEingeloggt;
            KennwortPanel.Visible = !biegl;
            EingeloggtPanel.Visible = biegl;

            RdfHyperLink.NavigateUrl = "http://nulllogicone.net/Stamm/?" + Stamm.StammRow.StammGuid;

            StammHyperLink.Text = OliUser.Stamm.StammRow.Stamm;
            StammHyperLink.NavigateUrl = "~/S/" + OliUser.Stamm.StammRow.StammGuid + ".aspx";

            // Konto Informationen
            if (OliUser.Stamm.BinIchEingeloggt)
            {
                KontoLabel.Visible = true;
                KontoDataGrid.DataSource = OliUser.Stamm.MyKonto;
                // TODO Bug - beim Registrieren ist MyKonto NULL
                decimal sum = 0;
                if (OliUser.Stamm.MyKonto.Rows.Count > 0)
                {
                    sum = decimal.Parse(OliUser.Stamm.MyKonto.Compute("SUM(Betrag)", "").ToString());
                }
                SummeLabel.Text = sum.ToString("0.00");
                KontoDataGrid.DataBind();
            }
        }

        // BilderLinkButton()
        protected void BilderLinkButton_Click(object sender, EventArgs e)
        {
            if (biegl)
            {
                Response.Redirect("../Sites/Elemente/BildUpload.aspx");
            }
            else
            {
                OliUser.Nachricht = "login required";
            }
        }

        // FreundLinkButton
        protected void FreundLinkButton_Click(object sender, EventArgs e)
        {
            if (biegl)
            {
                Response.Redirect("../Sites/Elemente/FreundWerben.aspx");
            }
            else
            {
                OliUser.Nachricht = "login required";
            }
        }

        protected void KennwortVergessenLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Sites/Elemente/KennwortVergessen.aspx");
        }

        protected override string MyTitle
        {
            get
            {
                string s = OliUser.Stamm.StammRow.Stamm;
                return s;
            }
        }
    }
}