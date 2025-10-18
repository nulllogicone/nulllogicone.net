// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    StammEditCommand.
    ///</summary>
    public class StammEditCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "Stamm editieren";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren wenn eingeloggt und *echter* Account
            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                Stamm.StammRow.Stamm.ToLower() != "gast" &&
                Stamm.StammRow.Stamm.ToLower() != "test")
            {
                HyperLink1.Text = Stamm.Q.S + " editieren";
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Edit/StammEdit.aspx?prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }

            if (Page is Sites.Edit.StammEdit)
            {
                HyperLink1.CssClass = "ButtonSel";
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
            CssClass = "button";
            NavigateUrl = "";
            Text = "HyperLink";
            ToolTip = "";
            Load += Page_Load;
        }

        #endregion
    }
}