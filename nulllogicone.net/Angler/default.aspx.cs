// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.DataSetTypes;
using OliWeb.Klassen;
using nulllogicone.net.Controls.AjaxWortraum;

namespace nulllogicone.net.Angler
{
    /// <summary>
    ///     Zusammenfassung für _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected AjaxWortraumControlFlip AjaxWortraumControlFlip1;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            string aguidstr = Request.QueryString.ToString();

            // wahrscheinlich eine GUID
            if (aguidstr.Length == 36)
            {
                Guid aguid = new Guid(aguidstr);
                OliUser.ShowAngler(aguid);

                AnglerDataSet.AnglerRow ar = Angler.AnglerRow;

                string uri = "https://nulllogicone.net/Angler/?" + aguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;
                AnglerLabel.Text = ar.Angler;
                BeschreibungLabel.Text = ar.IsBeschreibungNull() ? "" : ar.Beschreibung;
                DatumLabel.Text = ar.Datum.ToString("s");
                VersionLabel.Text = ar.IsVersionsnummerNull() ? "" : ar.Versionsnummer;

                string suri = "https://nulllogicone.net/Stamm/?" + ar.StammGuid;
                StammUriRefHyperLink.NavigateUrl = suri;
                StammUriRefHyperLink.Text = suri + "<img border=0 src='../images/eck_rauf_16_sw.gif'>";

                AnzPLinkButton.Text = OliUser.Stamm.Angler.MyPostIt.Count +
                                      "<img border=0 src='../images/eck_rechts_16_sw.gif'>";


                OLIitHyperLink.NavigateUrl = "https://www.oli-it.com/A/" + aguidstr + ".aspx";
                RdfHyperLink.Text = "https://nulllogicone.net/Angler/" + aguidstr + ".rdf";
                RdfHyperLink.NavigateUrl = "https://nulllogicone.net/Angler/" + aguidstr + ".rdf";

                OLIHyperLink.Text = "https://www.oli-it.com/A/" + aguidstr + ".aspx";
                OLIHyperLink.NavigateUrl = "https://www.oli-it.com/A/" + aguidstr + ".aspx";

                AjaxWortraumControlFlip1.Markierer = Angler;
                AjaxWortraumControlFlip1.Werbefrei = true;
                AjaxWortraumControlFlip1.Markierbar = false;
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
            if (Angler != null)
            {
                Response.Expires = 0;
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/rdf+xml";
                Response.AddHeader("content-disposition", "attachment; filename=\"Angler.rdf\"");
                Response.Write(Angler.MakeAnglerRDF());
                Response.End();
            }
        }

        protected void AnzPLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Visible = true;
            PostItRepeater.DataSource = Angler.MyPostIt;
            DataBind();
        }
    }
}