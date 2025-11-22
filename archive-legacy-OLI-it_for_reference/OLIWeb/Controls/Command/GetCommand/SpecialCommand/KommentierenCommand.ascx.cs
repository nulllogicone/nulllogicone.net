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
    ///    KommentierenCommand.
    ///</summary>
    public partial class KommentierenCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = General.comment;          
            HyperLink1.ToolTip = "Geben Sie eine Feedback: Eine Antwort auf diese Antwort (thread)!";
            HyperLink1.Enabled = false;

            // Bewerten
            if (Stamm != null &&
                PostIt != null &&
                Stamm.BinIchEingeloggt &&
//				PostIt.BinIchMeinPostIt &&  // man sollte eine Antwort auch kommentieren d�rfen, wenn es nicht das eigene PostIt ist
                TopLab != null)
            {
                HyperLink1.NavigateUrl = "~/Sites/Edit/TopLabAufTopLab.aspx";
                HyperLink1.Enabled = true;
            }

            if (Page is TopLabAufTopLab)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}
