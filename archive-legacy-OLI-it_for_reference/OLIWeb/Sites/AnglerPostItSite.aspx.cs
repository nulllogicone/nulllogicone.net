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
            // Mittelschichtfehler Abfanger bei zur�ck und Objektverweis
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
