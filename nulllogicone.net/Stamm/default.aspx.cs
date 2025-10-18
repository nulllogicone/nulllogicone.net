// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Stamm
{
    /// <summary>
    ///     Zusammenfassung für _default.
    /// </summary>
    public partial class _default : BasePage
    {
        private OliEngine.OliMiddleTier.OLIs.Stamm s;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string sguidstr = Request.QueryString.ToString();
            if (sguidstr.Length > 0)
            {
                try
                {
                    Guid sguid = new Guid(sguidstr);

                    s = SessionManager.Instance().OliUser.ShowStamm(sguid);

                    string uri = "https://nulllogicone.net/Stamm/?" + sguidstr;
                    UriHyperLink.NavigateUrl = uri;
                    UriHyperLink.Text = uri;
                    StammLabel.Text = s.StammRow.Stamm;
                    DatumLabel.Text = s.StammRow.Datum.ToString("s");

                    AnzAnglerLinkButton.Text = s.MyAngler.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";
                    AnzPostItLinkButton.Text = s.MyPostIt.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";
                    AnzTopLabLinkButton.Text = s.MyTopLab.Count + "<img border=0 src='../images/eck_rechts_16_sw.gif'>";

                    if (s.StammRow.IsDateiNull() || s.StammRow.Datei.Length == 0)
                    {
                        DateiImage.Visible = false;
                    }
                    else
                    {
                        DateiImage.ImageUrl = OliEngine.OliUtil.MakeImageSrc(s.StammRow.Datei);
                        DateiImage.Visible = true;
                    }

                    OLIitHyperLink.NavigateUrl = "https://www.oli-it.com/S/" + sguidstr + ".aspx";
                    RdfHyperLink.Text = "https://nulllogicone.net/Stamm/" + sguidstr + ".rdf";
                    RdfHyperLink.NavigateUrl = "https://nulllogicone.net/Stamm/" + sguidstr + ".rdf";

                    OLIHyperLink.Text = "https://www.oli-it.com/S/" + sguidstr + ".aspx";
                    OLIHyperLink.NavigateUrl = "https://www.oli-it.com/S/" + sguidstr + ".aspx";
                }
                catch
                {
                }
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
            s = OliWeb.Klassen.SessionManager.Instance().OliUser.Stamm;
            if (s != null)
            {
                Response.Expires = 0;
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/rdf+xml";
                Response.AddHeader("content-disposition", "attachment; filename=\"Stamm.rdf\"");
                Response.Write(s.MakeStammRDF());
                Response.End();
            }
        }

        protected void AnzAnglerLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle Angler";
            AnglerRepeater.DataSource = s.MyAngler;
            DataBind();
        }

        protected void AnzPostItLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle PostIt";
            PostItRepeater.DataSource = s.MyPostIt;
            DataBind();
        }

        protected void AnzTopLabLinkButton_Click(object sender, System.EventArgs e)
        {
            DetailLabel.Text = "alle TopLab";
            TopLabRepeater.DataSource = s.MyTopLab;
            DataBind();
        }
    }
}