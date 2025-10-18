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
    ///     PostItTopLabSite.
    /// </summary>
    public partial class PostItTopLabSite : MasterPostItPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mittelschichtfehler Abfanger bei zurück und Objektverweis
            if (OliUser.Stamm == null || OliUser.Stamm.PostIt == null)
            {
                OliUser.Nachricht = "Stamm und/oder Nachricht sind in Session verloren gegangen - keine Antworten";
                Helper.RedirectToSite();
            }

            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
            string feedUrl = "http://xml.oli-it.com/RSS/PostItTopLab.aspx?pguid=" + PostIt.PostItRow.PostItGuid;
            XmlHyperLink.NavigateUrl = feedUrl;
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

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}