// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     StammExtras.
    /// </summary>
    public partial class StammExtras : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisBasePage Initialisiert wird, wird 
        ///     auf das vorhandensein eine Stamm geprüft.
        ///     Auf dieser Seite muss er auch noch eingeloggt sein
        ///     und nicht 'gast' oder 'test' heissen.
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            if (OliUser.Stamm.BinIchEingeloggt &&
                Stamm.StammRow.Stamm.ToLower() != "gast" &&
                Stamm.StammRow.Stamm.ToLower() != "test")
            {
                // alles OK (weder gast noch test Account und eingeloggt)
            }
            else
            {
                OliUser.Nachricht = "login required";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
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