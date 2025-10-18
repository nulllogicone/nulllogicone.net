// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.PostIt
{
    /// <summary>
    ///     Zusammenfassung für _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected System.Web.UI.WebControls.Label AnzCodeLabel;

        private OliEngine.OliMiddleTier.OLIs.PostIt p;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string pguidstr = Request.QueryString.ToString();
            if (pguidstr.Length > 0)
            {
                Guid pguid = new Guid(pguidstr);


                OliUser.ShowPostIt(pguid);
                p = OliUser.Stamm.PostIt;

                string uri = "https://nulllogicone.net/PostIt/?" + pguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;
                TitelLabel.Text = p.PostItRow.IsTitelNull() ? "" : p.PostItRow.Titel;
                PostItLabel.Text = p.PostItRow.PostIt;
                DatumLabel.Text = p.PostItRow.Datum.ToString("s");

                AnzUrheberLinkButton.Text = p.MyStamm.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";
                AnzCodeLinkButton.Text = p.MyCode.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";
                AnzEmpfLinkButton.Text = p.MyEmpfaenger.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";
                AnzTopLabLinkButton.Text = p.MyTopLab.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";


                // Link Url
                if (!p.PostItRow.IsURLNull())
                {
                    UrlHyperLink.Text = p.PostItRow.URL;
                    UrlHyperLink.NavigateUrl = p.PostItRow.URL;
                }
                // Bild Datei
                if (p.PostItRow.IsDateiNull() || p.PostItRow.Datei.Length == 0)
                {
                    DateiImage.Visible = false;
                }
                else
                {
                    DateiImage.ImageUrl = OliEngine.OliUtil.MakeImageSrc(p.PostItRow.Datei);
                    DateiImage.Visible = true;
                }
                OLIitHyperLink.NavigateUrl = "https://www.oli-it.com/P/" + pguidstr + ".aspx";
                RdfHyperLink.Text = "https://nulllogicone.net/PostIt/" + pguidstr + ".rdf";
                RdfHyperLink.NavigateUrl = "https://nulllogicone.net/PostIt/" + pguidstr + ".rdf";

                OLIHyperLink.Text = "https://www.oli-it.com/P/" + pguidstr + ".aspx";
                OLIHyperLink.NavigateUrl = "https://www.oli-it.com/P/" + pguidstr + ".aspx";
            }
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

        protected void RdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            p = OliUser.Stamm.PostIt;
            if (p != null)
            {
                Response.Expires = 0;
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/rdf+xml";
                Response.AddHeader("content-disposition", "attachment; filename=\"PostIt.rdf\"");
                Response.Write(p.MakePostItRDF());
                Response.End();
            }
        }

        protected void AnzUrheberLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle Urheber";
            StammRepeater.DataSource = p.MyStamm;
            DataBind();
        }

        protected void AnzEmpfLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle Empfänger";
            AnglerRepeater.DataSource = p.MyEmpfaenger;
            DataBind();
        }

        protected void AnzTopLabLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle Antworten";
            TopLabRepeater.DataSource = p.MyTopLab;
            DataBind();
        }

        protected void AnzCodeLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "die Codes (Markierungen)";
            CodeRepeater.DataSource = p.MyCode;
            DataBind();
        }
    }
}