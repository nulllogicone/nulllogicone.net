// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einf�gen
        }

        #region Vom Web Form-Designer generierter Code

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

        protected void PostItRdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Guid pguid = new Guid(PostItGuidTextBox.Text);
            OliEngine.OliMiddleTier.OLIs.PostIt p = new OliEngine.OliMiddleTier.OLIs.PostIt(pguid);

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"PostIt.rdf\"");
            Response.Write(p.MakePostItRDF());
            Response.End();
        }

        protected void CodeRdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
//			Response.Redirect("https://nulllogicone.net/Code/" + CodeGuidTextBox.Text + ".rdf");
            Guid cguid = new Guid(CodeGuidTextBox.Text);
            OliEngine.OliMiddleTier.OLIs.Code c = new OliEngine.OliMiddleTier.OLIs.Code(cguid);

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Code.rdf\"");
            Response.Write(c.MakeCodeRDF());
            Response.End();
        }

        protected void AnglerRdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
//			Response.Redirect("https://nulllogicone.net/Angler/" + AnglerGuidTextBox.Text + ".rdf");
            Guid aguid = new Guid(AnglerGuidTextBox.Text);
            OliEngine.OliMiddleTier.OLIs.Angler a = new OliEngine.OliMiddleTier.OLIs.Angler(aguid);

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Angler.rdf\"");
            Response.Write(a.MakeAnglerRDF());
            Response.End();
        }

        protected void StammRdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Guid sguid = new Guid(StammGuidTextBox.Text);
            OliEngine.OliMiddleTier.OLIs.Stamm s = new OliEngine.OliMiddleTier.OLIs.Stamm(sguid);

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Stamm.rdf\"");
            Response.Write(s.MakeStammRDF());
            Response.End();
        }

        protected void TopLabRdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Guid tguid = new Guid(TopLabGuidTextBox.Text);
            OliEngine.OliMiddleTier.OLIs.TopLab t = new OliEngine.OliMiddleTier.OLIs.TopLab(tguid);

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"TopLab.rdf\"");
            Response.Write(t.MakeTopLabRDF());
            Response.End();
        }

        protected void NKBZImagebutton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"NKBZ.rdf\"");
            Response.Write(OliEngine.OliMiddleTier.OLIx.NKBZ.Instance().MakeWortraumRDF());
            Response.End();
        }
    }
}
