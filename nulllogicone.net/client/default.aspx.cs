// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.client
{
    /// <summary>
    ///     Zusammenfassung für _default.
    /// </summary>
    public class _default : BasePage
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einfügen
        }

        #region Vom Web Form-Designer generierter Code

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
            Load += Page_Load;
        }

        #endregion
    }
}