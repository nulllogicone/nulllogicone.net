// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     TopLabAufTopLab.
    /// </summary>
    public partial class TopLabAufTopLab : BasePage
    {
        // Member
        // ------

//		OliUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Titel von der vorigen Antwort �bernehmen:
            if (!IsPostBack)
            {
                TitelTextBox.Text = TopLab.TopLabRow.IsTitelNull() ? "" : TopLab.TopLabRow.Titel;
            }
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
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
        /////     Erforderliche Methode f�r die Designerunterst�tzung. 
        /////     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //}

        //#endregion

        protected void AddButton_Click(object sender, EventArgs e)
        {
            string titel = HttpUtility.HtmlEncode(TitelTextBox.Text);
            string text = HttpUtility.HtmlEncode(TextBox1.Text);
            OliUser.Stamm.TopLab.AddTopLab(OliUser, titel, text);
            OliUser.Nachricht = "Es wurde eine Antwort auf die Antwort hinzugef�gt";
            Helper.RedirectToSite();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Helper.RedirectToSite();
        }
    }
}
