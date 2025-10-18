// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using OliWeb.Klassen;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    PostItTopLabCommand.
    ///</summary>
    public partial class PostItTopLabCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imgsrc = "<img alt='PostItTopLab' height='10px' width='16px' border='0' src='/images/icons/Symbole/TopLab.png'>";
            HyperLink1.NavigateUrl = "";
            HyperLink1.CssClass = "PostItButton";
            HyperLink1.ToolTip = "Antworten auf diese Nachricht";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    HyperLink1.Text =  string.Format("{0} {1} ({2})",  imgsrc ,GetGlobalResourceObject("SAPCT","T"), PostIt.MyTopLab.Count );
                    HyperLink1.NavigateUrl = "~/Sites/PostItTopLabSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                }
            }

            if (Page is PostItTopLabSite)
            {
                HyperLink1.CssClass = "PostItButtonSel";
            }
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////    Erforderliche Methode für die Designerunterstützung
        /////</summary>
        //private void InitializeComponent()
        //{
        //    CssClass = "button";
        //    NavigateUrl = "";
        //    Text = "HyperLink";
        //    ToolTip = "";
        //}

        //#endregion
    }
}