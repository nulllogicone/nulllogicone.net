// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ShowCommand
{
    ///<summary>
    ///    ShowPostItCommand.
    ///</summary>
    public partial class ShowPostItCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "<strong>Nachricht *</strong>";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "aktuelle Nachricht der Session";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    string tool = PostIt.PostItRow.IsTitelNull() ? "" : PostIt.PostItRow.Titel;
                    tool += PostIt.PostItRow.PostIt;
                    HyperLink1.ToolTip = tool;
                    HyperLink1.NavigateUrl = "~/Sites/PostItSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                    HyperLink1.Visible = true;
                }
            }

//			if(this.Page is Sites.PostItSite)
//			{
//				HyperLink1.CssClass = "ButtonSel";
//			}
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////    Erforderliche Methode f�r die Designerunterst�tzung
        /////</summary>
        //private void InitializeComponent()
        //{
        //    CssClass = "";
        //    NavigateUrl = "";
        //    Text = "HyperLink";
        //    ToolTip = "";
        //}

        //#endregion
    }
}
