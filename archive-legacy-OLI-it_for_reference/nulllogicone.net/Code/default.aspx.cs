// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Code
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected nulllogicone.net.Controls.AjaxWortraum.AjaxWortraumControl AjaxWortraumControl1;
        private OliEngine.OliDataAccess.Code c;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string cguidstr = Request.QueryString.ToString();

            // wahrscheinlich eine GUID
            if (cguidstr.Length == 36)
            {
                Guid cguid = new Guid(cguidstr);

                // den Code direkt aus der Datenbank holen
                c = new OliEngine.OliDataAccess.Code(cguid);

                OliUser.ShowPostIt(c.CodeRow.PostItGuid);
                Stamm.PostIt.ShowCode(c.CodeRow.CodeGuid);


                AjaxWortraumControl1.Markierer = PostIt.Code;
                AjaxWortraumControl1.Markierbar = false;
                AjaxWortraumControl1.Werbefrei = true;


                string uri = "https://nulllogicone.net/Code/?" + cguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;
                CodeLabel.Text = c.CodeRow.Kommentar;
                VersionLabel.Text = c.CodeRow.IsVersionsnummerNull() ? "" : c.CodeRow.Versionsnummer;

                string suri = "https://nulllogicone.net/Stamm/?" + c.CodeRow.StammGuid;
                StammHyperLink.NavigateUrl = suri;
                StammHyperLink.Text = suri + "<img border=0 src='../images/eck_rauf_16_sw.gif'>";

                string puri = "https://nulllogicone.net/PostIt/?" + c.CodeRow.PostItGuid;
                PostItHyperLink.NavigateUrl = puri;
                PostItHyperLink.Text = puri + "<img border=0 src='../images/eck_rauf_16_sw.gif'>";

                AnzAnglerLinkButton.Text = Stamm.PostIt.Code.MyAngler.Angler.Count +
                                           "<img border=0 src='../images/eck_rechts_16_sw.gif'>";

                RdfHyperLink.Text = "https://nulllogicone.net/Code/" + cguidstr + ".rdf";
                RdfHyperLink.NavigateUrl = "https://nulllogicone.net/Code/" + cguidstr + ".rdf";
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
            if (PostIt != null && PostIt.Code != null)
            {
                Response.Expires = 0;
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/rdf+xml";
                Response.AddHeader("content-disposition", "attachment; filename=\"Code.rdf\"");
                Response.Write(PostIt.Code.MakeCodeRDF());
                Response.End();
            }
        }

        protected void AnzAnglerLinkButton_Click(object sender, System.EventArgs e)
        {
            AnglerRepeater.DataSource = Stamm.PostIt.Code.MyAngler.Angler;
            DataBind();
        }
    }
}
