// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammShortCutsCommand.
    ///</summary>
    public partial class StammShortCutsCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
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
                HyperLink1.NavigateUrl = "~/Sites/StammShortCutsSite.aspx";
                HyperLink1.Visible = true;
            }

            if (Page is StammShortCutsSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}
