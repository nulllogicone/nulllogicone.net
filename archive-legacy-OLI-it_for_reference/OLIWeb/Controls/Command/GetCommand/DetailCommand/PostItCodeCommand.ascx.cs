// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Runtime.InteropServices;
using OliWeb.Klassen;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    PostItCodeCommand.
    ///</summary>
    [ComVisible(false)]
    public partial class PostItCodeCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imgsrc = "<img alt='PostItAngler' height='10px' width='17px' border='0' src='/images/icons/Symbole/MiniCode.gif'>";
            HyperLink1.Text = "PostItCode";
            HyperLink1.NavigateUrl = "";
            HyperLink1.CssClass = "PostItButton";
            HyperLink1.ToolTip = "Markierung dieser Nachricht";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    HyperLink1.Text = string.Format("{0} {1} ({2})", imgsrc , GetGlobalResourceObject("SAPCT","C"), PostIt.MyCode.Rows.Count );
                    HyperLink1.NavigateUrl = "~/Sites/PostItCodeSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                }
            }
            if (Page is PostItCodeSite || Page is CodeSite)
            {
                HyperLink1.CssClass = "PostItButtonSel";
            }
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////    Erforderliche Methode f�r die Designerunterst�tzung
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
