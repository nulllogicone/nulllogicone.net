// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using OliWeb.Sites.Edit;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    PostItNewCommand.
    ///</summary>
    public partial class PostItEditCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // alles deaktivieren
            HyperLink1.Text = "Nachricht editieren";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                if (Stamm.PostIt != null && Stamm.PostIt.BinIchMeinPostIt)
                {
                    HyperLink1.Text = Stamm.Q.P + " editieren";
                    HyperLink1.NavigateUrl = "~/Sites/Edit/PostItMaker.aspx?prevSessionId=" +
                                             Session.SessionID;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is PostItMaker)
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
