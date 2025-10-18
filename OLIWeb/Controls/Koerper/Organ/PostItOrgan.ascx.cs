// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine;
using OliEngine.DataSetTypes;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;
using OliWeb.Sites;
using OliWeb.Sites.Edit;

namespace OliWeb.Controls.Koerper.Organ
{
    ///<summary>
    ///    PostItOrgan.
    ///</summary>
    public partial class PostItOrgan : MasterControl
    {
        protected Panel PostItPanel;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        #endregion

        // Member
        // ------

        protected KlickBild KlickBild1;

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: hier kommt noch 'reaktivieren (kostenpflichtig) rein
            // falls die wurzel schon closed war || !user.Stamm.PostIt.StammClosed) ;

            PostItDataSet.PostItRow p = PostIt.PostItRow;

            // Titel
            TitelLabel.Text = p.IsTitelNull() ? "" : p.Titel;

            // gekürzten Text bei Detailtabellenansicht oder TopLabEditieransicht
            // und ohne TopLab anzeigen [...] 
            // früher ging das mit dieser Eigenschaft || SichtbaresGrid == Stamm.SichtbaresGrid.None
            // Auf der PostIt-Seite : ganzen Text
            var isPostItSite = (Page is PostItSite);
            var isTopLabEdit = (Page is TopLabEdit);
            if (isPostItSite || isTopLabEdit)
            {
                if (p.Typ == "txt")
                {
                    PostItHyperLink.Text = OliUtil.MakeHtmlLineBreak(p.PostIt);
                }
                else
                {
                    PostItHyperLink.Text = p.PostIt;
                }
            }
            else
            {
                if (p.Typ == "txt")
                {
                    PostItHyperLink.Text = OliUtil.MakeHtmlLineBreak(OliUtil.FirstXWords(p.PostIt, 40));
                }
                else
                {
                    PostItHyperLink.Text = OliUtil.FirstXWords(p.PostIt, 40);
                }
            }

            PostItHyperLink.NavigateUrl = "~/P/" + p.PostItGuid + ".aspx";
            KooKLabel.Text = OliUtil.MakeRedKook(p.KooK);
            DatumLabel.Text = p.Datum.ToShortDateString();
            DatumLabel.ToolTip = "erstellt: " + p.Datum;
            KlickBild1.BildName = p.IsDateiNull() ? "" : p.Datei;
            KlickBild1.Breite = 100;
            HitsLabel.Text = p.Hits.ToString();
            HyperLinkSinglePostIt.NavigateUrl = "~/Sites/SinglePostItPage.aspx?pguid=" + p.PostItGuid;

            if (!p.IsURLNull())
            {
                UrlLiteral.Text = OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(p.URL));
                UrlLiteral.Visible = true;

                //HyperLinkUrl.NavigateUrl = p.URL;
            }
            else
            {
                UrlLiteral.Text = "";
                UrlLiteral.Visible = false;
            }

            if (p.Typ == "uri")
            {
                //UriIframe.Attributes.Add("src", PostIt.PostItRow.URL);
                //UriIframe.Visible = true;
	            var iframe = new LiteralControl(string.Format("<iframe src=\"{0}\"></iframe>",PostIt.PostItRow.URL));
				PlaceHolderIFrame.Controls.Add(iframe);
            }
            else
            {
                //UriIframe.Visible = false;
	            PlaceHolderIFrame.Visible = false;
            }
        }

        protected Guid PostItGuid
        {
            get { return PostIt.PostItRow.PostItGuid; }
        }

        protected string PostItTitel
        {
            get { return PostIt.PostItRow.IsTitelNull()? "no title" : PostIt.PostItRow.Titel; }
        }

        protected decimal KooK
        {
            get { return PostIt.PostItRow.KooK; }
        }
    }
}