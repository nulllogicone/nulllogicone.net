// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    AnglerEditCommand.
    ///</summary>
    public class AnglerEditCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "Angler editieren";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;


            // dann aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                if (Stamm.Angler != null)
                {
                    HyperLink1.Text = Stamm.Q.A + " editieren";
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Edit/AnglerEdit.aspx?prevSessionId=" +
                                             Session.SessionID;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.Edit.AnglerEdit)
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
