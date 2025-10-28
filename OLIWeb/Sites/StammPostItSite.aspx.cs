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
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        ///// <summary>
        /////   Erforderliche Methode f�r die Designerunterst�tzung. 
        /////   Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //}

        //#endregion
    }
}
