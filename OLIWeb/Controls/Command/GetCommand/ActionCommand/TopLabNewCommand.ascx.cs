// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    TopLabNewCommand.
    ///</summary>
    public partial class TopLabNewCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = string.Format("<b>+ {0}</b>",GetGlobalResourceObject("SAPCT","T"));
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                if (Stamm.PostIt != null)
                {
                    // jeder eingeloggte darf antworten
                    if (1 == 1)
                    {
                        HyperLink1.Text = string.Format("+ <b>{0}</b>" , Stamm.Q.T);
                        HyperLink1.NavigateUrl = "~/Sites/Edit/TopLabEdit.aspx?cmd=newT&prevSessionId=" +Session.SessionID;
                        HyperLink1.Visible = true;
                    }
                }
            }
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////  Erforderliche Methode f�r die Designerunterst�tzung
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
