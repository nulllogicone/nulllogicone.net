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
    ///     AnglerPostItSite.
    /// </summary>
    public partial class AnglerPostItSite : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mittelschichtfehler Abfanger bei zurück und Objektverweis
            if (OliUser.Stamm == null || OliUser.Stamm.Angler == null)
            {
                OliUser.Nachricht = "Stamm und/oder Angler sind in Session verloren gegangen";
                Helper.RedirectToSite();
            }

            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
            XmlHyperLink.NavigateUrl = "http://xml.oli-it.com/RSS/AnglerPostIt.aspx?aguid=" +
                                       Angler.AnglerRow.AnglerGuid;
            RdfHyperLink.NavigateUrl = "http://nulllogicone.net/Angler/?" + Angler.AnglerRow.AnglerGuid;
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