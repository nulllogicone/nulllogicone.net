// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliDataAccess;
using OliWeb.Klassen;

namespace nulllogicone.net
{
    /// <summary>
    ///     Zusammenfassung für Instanzen.
    /// </summary>
    public partial class Instanzen : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            int anz;
            string guid;
            Random rnd = new Random();

            StammList sl = new StammList();
            anz = sl.Stamm.Count;
            guid = sl.Stamm[rnd.Next(anz - 1)]["StammGuid"].ToString();
            StammHyperLink.Text = "https://nulllogicone.net/Stamm/?" + guid;
            StammHyperLink.NavigateUrl = "https://nulllogicone.net/Stamm/?" + guid;

            AnglerList al = new AnglerList();
            anz = al.Angler.Count;
            guid = al.Angler[rnd.Next(anz - 1)]["AnglerGuid"].ToString();
            AnglerHyperLink.Text = "https://nulllogicone.net/Angler/?" + guid;
            AnglerHyperLink.NavigateUrl = "https://nulllogicone.net/Angler/?" + guid;

            PostItList pl = new PostItList();
            anz = pl.PostIt.Count;
            guid = pl.PostIt[rnd.Next(anz - 1)]["PostItGuid"].ToString();
            PostItHyperLink.Text = "https://nulllogicone.net/PostIt/?" + guid;
            PostItHyperLink.NavigateUrl = "https://nulllogicone.net/PostIt/?" + guid;

            OliEngine.OliDataAccess.PostIt p = new OliEngine.OliDataAccess.PostIt(new Guid(guid));

            CodeList cl = new CodeList(p.PostItRow);
            anz = cl.Code.Count;
            if (anz > 0)
            {
                guid = cl.Code[rnd.Next(anz - 1)]["CodeGuid"].ToString();
                CodeHyperLink.Text = "https://nulllogicone.net/Code/?" + guid;
                CodeHyperLink.NavigateUrl = "https://nulllogicone.net/Code/?" + guid;
            }

            TopLabList tl = new TopLabList();
            anz = tl.TopLab.Count;
            guid = tl.TopLab[rnd.Next(anz - 1)]["TopLabGuid"].ToString();
            TopLabHyperLink.Text = "https://nulllogicone.net/TopLab/?" + guid;
            TopLabHyperLink.NavigateUrl = "https://nulllogicone.net/TopLab/?" + guid;
        }

        #region Vom Web Form-Designer generierter Code

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