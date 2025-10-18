// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammAnglerCommand.
    ///</summary>
    public class StammAnglerCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "StammAngler";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Filterprofile dieses Stammes";

            if (Stamm != null)
            {
                HyperLink1.Text = Stamm.Q.A + " (" + Stamm.MyAngler.Rows.Count + ")";
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/StammAnglerSite.aspx?sguid=" +
                                         Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is Sites.StammAnglerSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung
        ///</summary>
        private void InitializeComponent()
        {
            Load += Page_Load;
        }

        #endregion
    }
}