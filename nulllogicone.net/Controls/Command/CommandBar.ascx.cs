// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;

namespace OliWeb.Controls.Command
{
    ///<summary>
    ///    horizontale Darstellung aller GET-Commands
    ///</summary>
    public abstract class CommandBar : Klassen.MasterControl
    {
        private void Page_Load(object sender, System.EventArgs e)
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
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}