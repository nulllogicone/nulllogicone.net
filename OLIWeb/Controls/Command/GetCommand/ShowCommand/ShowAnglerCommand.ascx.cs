// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowAnglerCommand.
    ///</summary>
    public partial class ShowAnglerCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = GetGlobalResourceObject("SAPCT", "A").ToString();
            HyperLink1.NavigateUrl = "";
            //HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Filterprofil dieses Stammes";

            if (Stamm != null)
            {
                if (Stamm.Angler != null)
                {
                    Text = Stamm.Angler.AnglerRow.Angler;
                    HyperLink1.NavigateUrl = "~/Sites/AnglerSite.aspx?aguid=" +
                                             Angler.AnglerRow.AnglerGuid;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is AnglerSite)
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
        }

        #endregion
    }
}