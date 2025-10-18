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
    ///    AnglerLoecherCommand.
    ///</summary>
    public partial class AnglerLoecherCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = GetGlobalResourceObject("SAPCT", "A").ToString();
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = GetGlobalResourceObject("SAPCT", "A").ToString();

            if (Stamm != null)
            {
                if (Stamm.Angler != null)
                {
                    HyperLink1.NavigateUrl = "~/Sites/AnglerLoecherSite.aspx";
                    HyperLink1.Visible = true;
                }
            }

            if (Page is AnglerLoecherSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}