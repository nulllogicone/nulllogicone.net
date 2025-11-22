// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    PostItNewCommand.
    ///</summary>
    public class PostItEditCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
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
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Edit/PostItMaker.aspx?prevSessionId=" +
                                             Session.SessionID;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.Edit.PostItMaker)
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
            Load += Page_Load;
        }

        #endregion
    }
}
