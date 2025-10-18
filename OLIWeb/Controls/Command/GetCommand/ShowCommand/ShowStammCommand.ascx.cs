// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowStammCommand.
    ///</summary>
    public partial class ShowStammCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "Stamm";
            HyperLink1.NavigateUrl = "";
            HyperLink1.ToolTip = "Stamm";

            if (Stamm != null)
            {
                HyperLink1.Text = Stamm.StammRow.Stamm;
                HyperLink1.ToolTip = Stamm.StammRow.Stamm;
                HyperLink1.NavigateUrl = "~/Sites/StammSite.aspx?sguid=" +
                                         Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }


        }
    }
}