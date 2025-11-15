// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    TopLabNewCommand.
    ///</summary>
    public class TopLabNewCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "neue Antwort";
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
                        HyperLink1.Text = "neue " + Stamm.Q.T;
                        HyperLink1.NavigateUrl = Helper.MakeBaseLink() +
                                                 "Sites/Edit/TopLabEdit.aspx?cmd=newT&prevSessionId=" +
                                                 Session.SessionID;
                        HyperLink1.Visible = true;
                    }
                }
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///</summary>
        private void InitializeComponent()
        {
            CssClass = "button";
            NavigateUrl = "";
            Text = "HyperLink";
            ToolTip = "";
            Load += Page_Load;
        }

        #endregion
    }
}
