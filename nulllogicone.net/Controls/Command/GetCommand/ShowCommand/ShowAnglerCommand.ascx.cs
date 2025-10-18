// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowAnglerCommand.
    ///</summary>
    public class ShowAnglerCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "Angler";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Filterprofil dieses Stammes";


            if (Stamm != null)
            {
                if (Stamm.Angler != null)
                {
                    Text = Stamm.Angler.AnglerRow.Angler;
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/AnglerSite.aspx?aguid=" +
                                             Angler.AnglerRow.AnglerGuid;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.AnglerSite)
            {
//				HyperLink1.CssClass = "ButtonSel";
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
            CssClass = "";
            NavigateUrl = "";
            Text = "HyperLink";
            ToolTip = "";
            Load += Page_Load;
        }

        #endregion
    }
}