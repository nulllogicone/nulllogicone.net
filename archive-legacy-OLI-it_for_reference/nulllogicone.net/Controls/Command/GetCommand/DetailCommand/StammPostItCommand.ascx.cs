// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammPostItCommand.
    ///</summary>
    public class StammPostItCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "StammPostIt";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "von diesem Stamm geschriebene Nachrichten";


            if (Stamm != null)
            {
                Text = Stamm.Q.P + " (" + Stamm.MyPostIt.Rows.Count + ")";
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/StammPostItSite.aspx?sguid=" +
                                         Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is Sites.StammPostItSite)
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
            Load += Page_Load;
        }

        #endregion
    }
}
