// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    CodeNewCommand.
    ///</summary>
    public partial class CodeNewCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "+ neue Markierung";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.BinIchMeinPostIt)
            {
                HyperLink1.NavigateUrl = "~/Sites/CodeSite.aspx?cmd=newC&prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }
        }
    }
}
