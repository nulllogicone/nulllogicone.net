// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    CodeNewCommand.
    ///</summary>
    public class CodeNewCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "neue Markierung";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.BinIchMeinPostIt)
            {
                HyperLink1.Text = "neue Markierung";
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/CodeSite.aspx?cmd=newC&prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung
        ///</summary>
        private void InitializeComponent()
        {
            Load += Page_Load;
        }

        #endregion
    }
}