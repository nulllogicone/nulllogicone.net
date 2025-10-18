// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammShortCutsCommand.
    ///</summary>
    public class StammShortCutsCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "StammShortCuts";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Favoritenmarkierungen dieses Stammes";


            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                try
                {
                    Text = "ShortCuts (" + Stamm.MyShortCuts.Rows.Count + ")";
                }
                catch
                {
                }
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/StammShortCutsSite.aspx";
                HyperLink1.Visible = true;
            }

            if (Page is Sites.StammShortCutsSite)
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