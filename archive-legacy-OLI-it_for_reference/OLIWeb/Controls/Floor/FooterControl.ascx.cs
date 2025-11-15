// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Configuration;
using System.Net.Mime;
using System.Reflection;
using System.Web.UI;

namespace OliWeb.Controls.Floor
{
    public partial class FooterControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelNow.Text = DateTime.Now.Year.ToString();

            Assembly web = Assembly.GetExecutingAssembly();
            AssemblyName webName = web.GetName();

            var slot = ConfigurationManager.AppSettings["Slot"];
            LabelVersion.Text = $"{webName.Version.ToString()}-{slot}";
        }
    }
}
