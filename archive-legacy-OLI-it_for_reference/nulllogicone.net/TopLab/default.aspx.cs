// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.TopLab
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string tguidstr = Request.QueryString.ToString();
            if (tguidstr.Length > 0)
            {
                Guid tguid = new Guid(tguidstr);


                OliUser.ShowTopLab(tguid);
                OliEngine.OliMiddleTier.OLIs.TopLab t = OliUser.Stamm.TopLab;

                string uri = "https://nulllogicone.net/TopLab/?" + tguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;
                TitelLabel.Text = t.TopLabRow.IsTitelNull() ? "" : t.TopLabRow.Titel;
                TopLabLabel.Text = t.TopLabRow.TopLab;
                DatumLabel.Text = t.TopLabRow.Datum.ToString("s");
                // Link Url
                UrlHyperLink.NavigateUrl = t.TopLabRow.IsURLNull() ? "" : t.TopLabRow.URL;
                UrlHyperLink.Text = t.TopLabRow.IsURLNull() ? "" : t.TopLabRow.URL;
                // Stamm
                StammHyperLink.NavigateUrl = "https://nulllogicone.net/Stamm/?" + t.TopLabRow.StammGuid;
                StammHyperLink.Text = "https://nulllogicone.net/Stamm/?" + t.TopLabRow.StammGuid +
                                      "<img border=0 src='../images/eck_rauf_16_sw.gif'>";
                // PostIt
                PostItHyperLink.NavigateUrl = "https://nulllogicone.net/PostIt/?" + t.TopLabRow.PostItGuid;
                PostItHyperLink.Text = "https://nulllogicone.net/PostIt/?" + t.TopLabRow.PostItGuid +
                                       "<img border=0 src='../images/eck_rauf_16_sw.gif'>";

                // Bild Datei
                if (t.TopLabRow.IsDateiNull() || t.TopLabRow.Datei.Length == 0)
                {
                    DateiImage.Visible = false;
                }
                else
                {
                    DateiImage.ImageUrl = OliEngine.OliUtil.MakeImageSrc(t.TopLabRow.Datei);
                    DateiImage.Visible = true;
                }

                // OLI-it Link
                OLIitHyperLink.NavigateUrl = "https://www.oli-it.com/T/" + tguidstr + ".aspx";
                RdfHyperLink.Text = "https://nulllogicone.net/TopLab/" + tguidstr + ".rdf";
                RdfHyperLink.NavigateUrl = "https://nulllogicone.net/TopLab/" + tguidstr + ".rdf";

                OLIHyperLink.Text = "https://www.oli-it.com/T/" + tguidstr + ".aspx";
                OLIHyperLink.NavigateUrl = "https://www.oli-it.com/T/" + tguidstr + ".aspx";
            }
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

        protected void RdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            OliEngine.OliMiddleTier.OLIs.TopLab t = OliUser.Stamm.TopLab;
            if (t != null)
            {
                Response.Expires = 0;
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/rdf+xml";
                Response.AddHeader("content-disposition", "attachment; filename=\"TopLab.rdf\"");
                Response.Write(t.MakeTopLabRDF());
                Response.End();
            }
        }
    }
}
