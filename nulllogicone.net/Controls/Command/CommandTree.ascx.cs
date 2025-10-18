// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace OliWeb.Controls.Command
{
    ///<summary>
    ///    vertikale Menüdarstellung mit allen GET-Commands
    ///</summary>
    public abstract class CommandTree : Klassen.MasterControl
    {
//		protected System.Web.UI.WebControls.HyperLink StammHyperLink;
        protected System.Web.UI.WebControls.HyperLink HyperLink4;
        protected System.Web.UI.WebControls.HyperLink HyperLink7;
        protected System.Web.UI.WebControls.HyperLink TopLabTopLabHyperlink;
        protected System.Web.UI.WebControls.HyperLink TollisHyperlink;
        protected System.Web.UI.HtmlControls.HtmlTable CommandTable;
        protected System.Web.UI.WebControls.Image Image2;
        protected System.Web.UI.WebControls.Image Image3;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected System.Web.UI.WebControls.HyperLink HyperLink3;
        protected System.Web.UI.WebControls.Image CodeImage;
        protected System.Web.UI.WebControls.Image Image4;


        /// <summary>
        ///     Die Hyperlinks werden mit den aktuellen Bezeichnungen und Anzahlen beschriftet
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (OliUser.Stamm != null)
            {
                if (Stamm.PostIt != null)
                {
                    CodeImage.Visible = true;
                }
//				StammLabel.Text = OliUser.Stamm.StammRow.Stamm;

                // Button beschriften
                // meine Q-Row zum beschriften holen
//				OliEngine.DataSetTypes.QDataSet.QRow Q = this.OliUser.Stamm.Q;
                // HyperLink.Text beschriften
//				StammNewsHyperLink.Text = "News (" + this.OliUser.Stamm.MyNews.Count + ")";
//				StammInboxHyperLink.Text = "Inbox (" + this.OliUser.Stamm.MyInbox.Count + ")";
//
//				// NavigateUrl einstellen
//				StammInboxHyperLink.NavigateUrl = Helper.MakeBaseLink() + "Sites/StammInboxSite.aspx";
//				StammNewsHyperLink.NavigateUrl = Helper.MakeBaseLink() + "Sites/StammNewsSite.aspx";


//				if(PostIt != null)
//				{
////					PostItAnglerHyperLink.Text = this.OliUser.Stamm.Q.A + " (" + PostIt.MyEmpfaenger.Count + ")";
////					CodeHyperLink.Text = this.OliUser.Stamm.Q.C + " (" + PostIt.MyCode.Count + ")";
//				
////					PostItAnglerHyperLink.NavigateUrl = Helper.MakeBaseLink() + "Sites/PostItAnglerSite.aspx";
//				}			
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung.
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///</summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}