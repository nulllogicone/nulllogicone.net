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
    ///     AnglerSite.
    /// </summary>
    public partial class AnglerSite : MasterStammPage
    {
        // Member
        // ------

        // �berschriebene Methoden
        // ----------------------- 
        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisStammPage Initialisiert wird, wird 
        ///     auf das vorhandensein eines Stamm gepr�ft.
        ///     Auf dieser Seite muss auch noch ein Angler vorhanden sein
        ///     oder im QueryString die PostItGuid �bergeben werden.
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            if (OliUser.Stamm.Angler == null)
            {
                Response.Redirect(NO_ANGLER_REDIRECT);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Mittelschichtfehler Abfanger bei zur�ck und Objektverweis
            if (OliUser.Stamm == null || OliUser.Stamm.Angler == null)
            {
                OliUser.Nachricht = "Stamm und/oder Filterprofil sind in Session verloren gegangen";
                Helper.RedirectToSite();
            }

            RdfHyperLink.NavigateUrl = "http://nulllogicone.net/Angler/?" + Angler.AnglerRow.AnglerGuid;
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
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

        /// <summary>
        ///     MyTitle wird in dieser Klasse mit Stamm und Anglernamen �berschrieben.
        /// </summary>
        protected override string MyTitle
        {
            get
            {
                string s = "[S]" + OliUser.Stamm.StammRow.Stamm;
                s += " [A]" + Angler.AnglerRow.Angler;
                return s;
            }
        }
    }
}
