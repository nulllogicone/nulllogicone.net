// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    AnglerPostItCommand.
    ///</summary>
    public partial class AnglerPostItCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "Treffer";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "gefundene Treffer (Nachrichten)";

            if (Stamm != null)
            {
                if (Stamm.Angler != null)
                {
                    HyperLink1.Text = string.Format("{0} ({1})", OliUser.Stamm.Q.A_X , OliUser.Stamm.Angler.MyPostIt.Count );
                    HyperLink1.NavigateUrl = "~/Sites/AnglerPostItSite.aspx";
                    HyperLink1.Visible = true;
                }
            }

            if (Page is AnglerPostItSite)
            {
                HyperLink1.CssClass = "PostItButtonSel";
            }
        }
    }
}