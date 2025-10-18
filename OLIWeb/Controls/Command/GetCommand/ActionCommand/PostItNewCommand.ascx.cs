// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI.WebControls;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    PostItNewCommand.
    ///</summary>
    public partial class PostItNewCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = string.Format("<b>+ {0}</b>",GetGlobalResourceObject("SAPCT","P"));
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                HyperLink1.NavigateUrl = "~/Sites/Edit/PostItMaker.aspx?cmd=newP&prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////  Erforderliche Methode für die Designerunterstützung
        /////</summary>
        //private void InitializeComponent()
        //{
        //    CssClass = "button";
        //    NavigateUrl = "";
        //    Text = "HyperLink";
        //    ToolTip = "";
        //}

        //#endregion
    }
}