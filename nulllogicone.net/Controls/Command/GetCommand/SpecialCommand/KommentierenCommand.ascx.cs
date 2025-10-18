// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.SpecialCommand
{
    ///<summary>
    ///    KommentierenCommand.
    ///</summary>
    public class KommentierenCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "kommentieren";
            HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/TopLabAufTopLab.aspx";
            HyperLink1.ToolTip = "Geben Sie eine Feedback auf diese Antwort und kommentieren es!";
            HyperLink1.Visible = false;


            // Bewerten
            if (Stamm != null &&
                PostIt != null &&
                Stamm.BinIchEingeloggt &&
//				PostIt.BinIchMeinPostIt &&  // man sollte eine Antwort auch kommentieren dürfen, wenn es nicht das eigene PostIt ist
                TopLab != null)
            {
                HyperLink1.Visible = true;
            }

            //if(this.Page is Sites.TopLabAufTopLab)
            //{
            //    HyperLink1.CssClass = "ButtonSel";
            //}
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