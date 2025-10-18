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
    ///    StammPostItCommand.
    ///</summary>
    public partial class StammPostItCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "StammPostIt";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "von diesem Stamm geschriebene Nachrichten";

            if (Stamm != null)
            {
                Text = Stamm.Q.P + " (" + Stamm.MyPostIt.Rows.Count + ")";
                HyperLink1.NavigateUrl = "~/Sites/StammPostItSite.aspx?sguid=" +
                                         Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is StammPostItSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}