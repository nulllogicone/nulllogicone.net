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
    ///     StammTopLabSite.
    /// </summary>
    public partial class StammTopLabSite : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mittelschichtfehler Abfanger bei zur�ck und Objektverweis
            if (OliUser.Stamm == null)
            {
                OliUser.Nachricht = "Stamm hat sich verabschiedet - keine Antworten mehr";
                Helper.RedirectToSite();
            }

            StammLabel.Text = OliUser.Stamm.StammRow.Stamm;
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;
            XmlHyperLink.NavigateUrl = "http://xml.oli-it.com/RSS/StammTopLab.aspx?sguid=" + Stamm.StammRow.StammGuid;
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
    }
}
