// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     PostItSite zeigt eine Nachricht an.
    /// </summary>
    public partial class PostItSite : MasterPostItPage
    {
        protected LinkButton Empf�ngerLinkButton;

        // Member
        // ------

        protected void Page_Load(object sender, EventArgs e)
        {
	        var sguid = Stamm.StammRow.StammGuid;
	        //HyperlinkToSinglePage.Text = "full";
	        //HyperlinkToSinglePage.NavigateUrl =
		       // string.Format("http://localhost:53795/Sites/SinglePostItPage.aspx?pguid={0}", PostIt.PostItRow.PostItGuid);

            RdfHyperLink.NavigateUrl = "http://nulllogicone.net/PostIt/?" + PostIt.PostItRow.PostItGuid;
            if (PostIt.MyTopLab.Count == 1)
            {
                AntwortenHyperLink.Text = "Eine Antwort";
                AntwortenHyperLink.NavigateUrl = "TopLabSite.aspx?tguid=" + PostIt.MyTopLab[0].TopLabGuid;
                AntwortenHyperLink.Visible = true;
            }
            if (PostIt.MyTopLab.Count > 1)
            {
                AntwortenHyperLink.Text = PostIt.MyTopLab.Count + " Antworten";
                AntwortenHyperLink.NavigateUrl = "PostItTopLabSite.aspx";
                AntwortenHyperLink.Visible = true;
            }

            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;

            // KontoGrid
            KontoDataGrid.DataSource = PostIt.MyKonto;
            KontoDataGrid.DataBind();

            // Summe
            try
            {
                decimal sum = decimal.Parse(PostIt.MyKonto.Compute("SUM(Betrag)", "").ToString());
                SummeLabel.Text = sum.ToString("0.00");
            }
            catch
            {
                SummeLabel.Text = "no sum";
            }
        }
    }
}
