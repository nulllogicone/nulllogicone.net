// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    AnglerPostItCommand.
    ///</summary>
    public class AnglerPostItCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            HyperLink1.Text = "Fische";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "geangelte Fische (Nachrichten)";

            if (Stamm != null)
            {
                if (Stamm.Angler != null)
                {
                    HyperLink1.Text = "Fische (" + OliUser.Stamm.Angler.MyPostIt.Count + ")";
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/AnglerPostItSite.aspx";
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.AnglerPostItSite)
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
            Load += Page_Load;
        }

        #endregion
    }
}