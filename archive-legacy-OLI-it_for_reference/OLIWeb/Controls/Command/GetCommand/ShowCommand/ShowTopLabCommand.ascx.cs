// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowTopLabCommand.
    ///</summary>
    public partial class ShowTopLabCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "Antwort";
            HyperLink1.NavigateUrl = "";
            HyperLink1.ToolTip = "TopLab, Feedback, Antwort, ...";

            if (Stamm != null)
            {
                if (Stamm.TopLab != null)
                {
                    string tool = TopLab.TopLabRow.IsTitelNull() ? "" : TopLab.TopLabRow.Titel;
                    tool += TopLab.TopLabRow.TopLab;
                    HyperLink1.ToolTip = tool;
                    HyperLink1.NavigateUrl = "~/Sites/TopLabSite.aspx?tguid=" +
                                             TopLab.TopLabRow.TopLabGuid;
                    HyperLink1.Visible = true;
                }
            }
        }
    }
}
