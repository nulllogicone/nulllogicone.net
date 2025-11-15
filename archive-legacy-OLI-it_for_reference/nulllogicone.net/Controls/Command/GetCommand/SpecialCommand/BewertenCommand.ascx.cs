// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.SpecialCommand
{
    ///<summary>
    ///    BewertenCommand.
    ///</summary>
    public class BewertenCommand : CommandBase
    {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = "bewerten";
            HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Site/Edit/TollEdit.aspx";
            HyperLink1.ToolTip = "Bewerten sie diese Antwort mit 0 - 100%";
            HyperLink1.Visible = false;


            // Bewerten
            if (Stamm != null &&
                Stamm.BinIchEingeloggt &&
                PostIt != null &&
                PostIt.BinIchMeinPostIt &&
                TopLab != null)
            {
                HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Edit/TollEdit.aspx";
                HyperLink1.Visible = true;
            }

            if (Page is Sites.Edit.TollEdit)
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
