// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    PostItTopLabCommand.
    ///</summary>
    public class PostItTopLabCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            string imgsrc = "<img alt='PostItTopLab' height='10px' width='16px' border='0' src='" +
                            Helper.MakeBaseLink() + "images/icons/Symbole/TopLab.jpg'>";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Antworten auf diese Nachricht";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    HyperLink1.Text = imgsrc + " Antworten (" + PostIt.MyTopLab.Count + ")";
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/PostItTopLabSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.PostItTopLabSite)
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
            CssClass = "button";
            NavigateUrl = "";
            Text = "HyperLink";
            ToolTip = "";
            Load += Page_Load;
        }

        #endregion
    }
}