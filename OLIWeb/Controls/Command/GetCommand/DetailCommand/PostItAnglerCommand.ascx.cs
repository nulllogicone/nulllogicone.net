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
    ///    PostItAnglerCommand.
    ///</summary>
    [ComVisible(false)]
    public partial class PostItAnglerCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imgsrc = "<img alt='PostItAngler' height='10px' width='16px' border='0' src='/images/icons/Symbole/Angler.png' />";
            HyperLink1.NavigateUrl = "";
            HyperLink1.CssClass = "PostItButton";
            HyperLink1.ToolTip = "Empfänger dieser Nachricht";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    HyperLink1.Text =  string.Format("{0} {1} ({2})",  imgsrc ,GetGlobalResourceObject("SAPCT","P_X"), PostIt.MyEmpfaenger.Count );
                    HyperLink1.NavigateUrl = "~/Sites/PostItAnglerSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                }
            }

            if (Page is PostItAnglerSite)
            {
                HyperLink1.CssClass = "PostItButtonSel";
            }
        }
    }
}