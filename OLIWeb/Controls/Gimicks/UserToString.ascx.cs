// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliEngine;
using OliWeb.Klassen;

namespace OliWeb.Controls.Gimicks
{
    ///<summary>
    ///    Ausgabe der aktuellen Daten des Mittelschicht OliUser Objektes mit
    ///    rekursiver Ausgabe von Stamm, Angler, PostIt und TopLab
    ///</summary>
    public partial class UserToString : MasterControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserLabel.Text = OliUtil.MakeHtmlLineBreak(OliUser.ToString());
        }

        //#region Web Form Designer generated code

        //protected override void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////    Erforderliche Methode für die Designerunterstützung.
        /////    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /////</summary>
        //private void InitializeComponent()
        //{
        //}

        //#endregion
    }
}