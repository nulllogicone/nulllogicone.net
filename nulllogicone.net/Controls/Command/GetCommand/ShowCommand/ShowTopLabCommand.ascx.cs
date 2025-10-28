// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowTopLabCommand.
    ///</summary>
    public class ShowTopLabCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "Antwort";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Antwort";


            if (Stamm != null)
            {
                if (Stamm.TopLab != null)
                {
                    string tool = TopLab.TopLabRow.IsTitelNull() ? "" : TopLab.TopLabRow.Titel;
                    tool += TopLab.TopLabRow.TopLab;
                    HyperLink1.ToolTip = tool;
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/TopLabSite.aspx?tguid=" +
                                             TopLab.TopLabRow.TopLabGuid;
                    HyperLink1.Visible = true;
                }
            }

//			if(this.Page is Sites.TopLabSite)
//			{
//				HyperLink1.CssClass = "ButtonSel";
//			}
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
            CssClass = "";
            NavigateUrl = "";
            Text = "HyperLink";
            ToolTip = "";
            Load += Page_Load;
        }

        #endregion
    }
}
