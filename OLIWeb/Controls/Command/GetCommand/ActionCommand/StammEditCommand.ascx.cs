// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Sites.Edit;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    StammEditCommand.
    ///</summary>
    public partial class StammEditCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
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
                HyperLink1.NavigateUrl = "~/Sites/Edit/StammEdit.aspx?prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }

            if (Page is StammEdit)
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
        }

        #endregion
    }
}