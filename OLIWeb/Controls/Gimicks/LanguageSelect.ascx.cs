// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Gimicks
{
    public partial class LanguageSelect : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string cur = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            switch (cur)
            {
                case "en":
                    LinkButtonEn_US.Font.Bold = true;
                    break;
                case "de":
                    LinkButtonDe_DE.Font.Bold = true;
                    break;
            }
        }

        protected void LinkButtonCulture_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton) sender;
            CultureInfo ci = new CultureInfo(lb.CommandArgument);
            Common.CurrentCulture = ci;
            Response.Redirect(Request.RawUrl);
        }
    }
}