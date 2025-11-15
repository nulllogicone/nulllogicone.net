// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     Diese Seite zeigt die neuesten St�mme, Angler, Nachrichten und Antworten.
    ///     Sie enth�lt einfach das <see cref="Controls.Floor.Journal.JournalControl" /> ohne weiteren Code.
    /// </summary>
    public partial class Journal : BasePage
    {
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

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
