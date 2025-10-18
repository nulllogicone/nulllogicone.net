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
    ///     StammPostItSite.
    /// </summary>
    public partial class StammPostItSite : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hilfepanel zeigen/verstecken
            StammLabel.Text = Stamm.StammRow.Stamm;
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
            XmlHyperLink.NavigateUrl = "http://xml.oli-it.com/RSS/StammPostIt.aspx?sguid=" + Stamm.StammRow.StammGuid;
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

        ///// <summary>
        /////   Erforderliche Methode für die Designerunterstützung. 
        /////   Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //}

        //#endregion
    }
}