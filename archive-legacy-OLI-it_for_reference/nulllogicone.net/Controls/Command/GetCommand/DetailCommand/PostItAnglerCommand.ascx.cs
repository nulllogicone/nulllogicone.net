// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    PostItAnglerCommand.
    ///</summary>
    public class PostItAnglerCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            string imgsrc = "<img alt='PostItAngler' height='10px' width='16px' border='0' src='" +
                            Helper.MakeBaseLink() + "images/icons/Symbole/Angler.jpg'>";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "Empf�nger dieser Nachricht";

            if (Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    HyperLink1.Text = imgsrc + " Empf�nger (" + PostIt.MyEmpfaenger.Count + ")";
                    HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/PostItAnglerSite.aspx?pguid=" +
                                             PostIt.PostItRow.PostItGuid;
                    HyperLink1.Visible = true;
                }
            }

            if (Page is Sites.PostItAnglerSite)
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
