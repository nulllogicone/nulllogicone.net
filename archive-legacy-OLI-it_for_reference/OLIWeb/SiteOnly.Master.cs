// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;

namespace OliWeb
{
    public partial class SiteOnly : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // AnonymMenu - Seiten  
            bool showAnonymMenu = false;
            string siteName = Request.FilePath.ToLower();
            if (siteName.Contains("default") ||
                siteName.Contains("neuanmelden") ||
                siteName.Contains("journal") ||
                siteName.Contains("chart") ||
                siteName.Contains("nutzungsbedingungen") ||
                siteName.Contains("impressum") ||
                siteName.Contains("suchsite") ||
                siteName.Contains("links") ||
                siteName.Contains("olierror") ||
                siteName.Contains("notfound"))
            {
                showAnonymMenu = true;
            }

            // show AnonymMenu XOR CommandTree
            AnonymMenu1.Visible = showAnonymMenu;
            CommandTree1.Visible = !showAnonymMenu;
        }
    }
}
