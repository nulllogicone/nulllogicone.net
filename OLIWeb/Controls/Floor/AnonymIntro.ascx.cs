// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OliWeb.Controls.Floor
{
    ///<summary>
    ///    Ein Intro-Control für anonyme Besucher. Es wird auf der
    ///    Startseite angezeigt und soll einen kurze Erklärung von OLI-it
    ///    geben. Außerdem wird eine Zufallsnachricht
    ///    mit ihren Antworten gezeigt und im HilfePanel die wichtigsten
    ///    Links wiederholt.
    ///</summary>
    public partial class AnonymIntro : UserControl
    {
        /// <summary>
        ///     Hilfe zur Intro-Seite (zeigt wo einem geholfen wird)
        /// </summary>
        protected Panel HilfePanel;

        /// <summary>
        ///     ist nur zur Illustration
        /// </summary>
        protected Image Image1;

        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        #endregion

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
        }
    }
}