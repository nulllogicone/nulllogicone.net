// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowCodeCommand.
    ///</summary>
    public partial class ShowCodeCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "Code";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Markierung der Nachricht";

            if (Stamm != null &&
                Stamm.PostIt != null)
            {
                HyperLink1.NavigateUrl = "~/Sites/CodeSite.aspx";
                HyperLink1.Visible = true;
            }
        }
    }
}