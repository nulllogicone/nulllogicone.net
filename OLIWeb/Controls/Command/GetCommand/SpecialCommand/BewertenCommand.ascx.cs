// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Sites.Edit;
using Resources;

namespace OliWeb.Controls.Command.GetCommand.SpecialCommand
{
    ///<summary>
    ///    BewertenCommand.
    ///</summary>
    public partial class BewertenCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = General.RateTopLap;
            //HyperLink1.NavigateUrl = "~/Site/Edit/TollEdit.aspx";
            HyperLink1.ToolTip = "Bewerten sie diese Antwort mit 0 - 100%";
            HyperLink1.Enabled = false;

            // Bewerten
            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.BinIchMeinPostIt &&
                TopLab != null)
            {
                HyperLink1.NavigateUrl = "~/Sites/Edit/TollEdit.aspx";
                HyperLink1.Enabled = true;
            }

            if (Page is TollEdit)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}