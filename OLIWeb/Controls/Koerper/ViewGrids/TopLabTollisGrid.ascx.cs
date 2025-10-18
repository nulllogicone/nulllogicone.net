// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    TopLabTollisGrid.
    ///</summary>
    public partial class TopLabTollisGrid : UserControl
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
            this.TollisRepeater.ItemCommand += this.TollisRepeater_ItemCommand;
        }

        #endregion

        // Member
        // ------

        private OliUser user;

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            user = SessionManager.Instance().OliUser;
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (user.Stamm != null)
            {
                if (user.Stamm.TopLab != null)
                {
//					if(user.Stamm.sichtbaresGrid == Stamm.SichtbaresGrid.TopLabTollis ||
//						user.Stamm.sichtbaresGrid == Stamm.SichtbaresGrid.None)
//					{
                    var myTollis = user.Stamm.TopLab.MyTollis;
                    TollisRepeater.DataSource = myTollis;
                    TollisRepeater.DataBind();
                    DivTollis.Visible = myTollis.Count > 0;

                    if (user.Stamm.TopLab.MyTollis.Rows.Count == 0)
                    {
                        Label l = new Label();
                        l.Text =
                            "<div style='font-size:8pt; text-align:center'>Diese Antwort hat noch keine Bewertungen</div>";
                        Controls.Add(l);
                    }
//					}
                }
            }
        }

        // Item_Command
        private void TollisRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // ids merken zum wieder herstellen
            Guid pguid = Guid.Empty;
            Guid tguid = Guid.Empty;
            if (user.Stamm.PostIt != null)
            {
                pguid = user.Stamm.PostIt.PostItRow.PostItGuid;
            }
            if (user.Stamm.TopLab != null)
            {
                tguid = user.Stamm.TopLab.TopLabRow.TopLabGuid;
            }

            // Stamm zeigen
            Label l = (Label) e.Item.FindControl("StammGuidLabel");
            Guid sguid = new Guid(l.Text);
            user.ShowStamm(sguid);

            // wenn vorher was offen war : wieder herstellen
            if (pguid != Guid.Empty)
            {
                user.Stamm.ShowPostIt(pguid);
            }
            if (tguid != Guid.Empty)
            {
                user.Stamm.ShowTopLab(tguid);
            }
            Helper.RedirectToSite();
        }
    }
}