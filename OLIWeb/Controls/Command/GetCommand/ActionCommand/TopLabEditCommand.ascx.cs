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
    ///    TopLabEditCommand.
    ///</summary>
    public partial class TopLabEditCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "TopLab editieren";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                if (Stamm.TopLab != null && Stamm.TopLab.BinIchMeinTopLab)
                {
                    HyperLink1.Text = Stamm.Q.T + " editieren";
                    HyperLink1.NavigateUrl = "~/Sites/Edit/TopLabEdit.aspx?prevSessionId=" +
                                             Session.SessionID;
                    HyperLink1.Visible = true;
                }
            }
            if (Page is TopLabEdit)
            {
                HyperLink1.CssClass = "ButtonSel";
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
        }

        #endregion
    }
}
