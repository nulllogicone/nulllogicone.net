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
    ///    StammAnglerCommand.
    ///</summary>
    public partial class StammAnglerCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "StammAngler";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Filterprofile dieses Stammes";

            if (Stamm != null)
            {
                HyperLink1.Text = Stamm.Q.A + " (" + Stamm.MyAngler.Rows.Count + ")";
                HyperLink1.NavigateUrl = "~/Sites/StammAnglerSite.aspx?sguid=" +
                                         Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is StammAnglerSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}
