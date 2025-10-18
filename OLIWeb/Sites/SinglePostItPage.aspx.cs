using System;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    public partial class SinglePostItPage : MasterPostItPage
    {
        // http://localhost:53795/Sites/SinglePostItPage.aspx?pguid=401457c4-aca8-4ffc-b070-93de3e2c98ac


        protected void Page_Load(object sender, EventArgs e)
        {
            lblServerName.Text = Environment.MachineName;
            lblDateTime.Text = DateTime.Now.ToString("s");
            lnkPostIt.NavigateUrl = "~/Sites/PostItSite.aspx?pguid=" + PostIt.PostItRow.PostItGuid;
            lnkPostIt.Text = "//P/" + PostIt.PostItRow.PostItGuid;

            // Full page PostIt
            lblTitle.Text = PostIt.PostItRow.IsTitelNull() ? "" : PostIt.PostItRow.Titel;
            lblDescription.Text = PostIt.PostItRow.PostIt;
            imgPostIt.ImageUrl =  PostIt.PostItRow.IsDateiNull() ? "" : OliEngine.OliUtil.MakeImageSrc(PostIt.PostItRow.Datei);

        }
    }
}