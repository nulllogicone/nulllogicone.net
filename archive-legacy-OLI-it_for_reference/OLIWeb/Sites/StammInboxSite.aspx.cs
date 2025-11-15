// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     StammInboxSite.
    /// </summary>
    public partial class StammInboxSite : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;

            string feedUrl = "http://xml.oli-it.com/RSS/StammInbox.aspx?sguid=" + Stamm.StammRow.StammGuid;
            XmlHyperLink.NavigateUrl = feedUrl;
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}
