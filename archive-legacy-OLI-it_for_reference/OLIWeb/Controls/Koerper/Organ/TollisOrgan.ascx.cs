// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.Organ
{
    ///<summary>
    ///    TollisOrgan.
    ///</summary>
    public partial class TollisOrgan : MasterControl
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
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

//		OliUser user;
//		protected System.Web.UI.WebControls.Panel TollPanel;
        protected TollChart TollChart1;

        // Ereignisse
        // ----------

        // Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (OliUser.Stamm.Tollis != null)
                {
                    TollTextBox.Text = OliUser.Stamm.Tollis.TollisRow.Toll.ToString();
                    TollTextTextBox.Text = HttpUtility.HtmlDecode(OliUser.Stamm.Tollis.TollisRow.TollText);
                    TollChart1.TollWert = OliUser.Stamm.Tollis.TollisRow.Toll;
                }
            }
        }

        protected void AbgebenButton_Click(object sender, EventArgs e)
        {
            short toll = short.Parse(TollTextBox.Text);

            if (toll >= 0 && toll <= 100)
            {
                if (Stamm.Tollis == null)
                {
                    OliUser.Stamm.ShowTollis(OliUser.Stamm.StammRow, TopLab.TopLabRow);
                }
                OliUser.Stamm.Tollis.TollisRow.Toll = toll;
                OliUser.Stamm.Tollis.TollisRow.TollText = HttpUtility.HtmlEncode(TollTextTextBox.Text);
                OliUser.Stamm.Tollis.UpdateTollis();

                OliUser.Stamm.InboxGelesen(TopLab.TopLabRow);
                OliUser.Stamm.MyInbox = null;

                OliUser.Stamm.Tollis = null;
//				SichtbaresGrid = Stamm.SichtbaresGrid.TopLabTollis;

                // PostIt.MyTopLab aktualisieren
                if (PostIt != null)
                {
                    PostIt.MyTopLab = null;
                }
                Response.Redirect("~/Sites/TopLabSite.aspx");
            }
        }
    }
}
