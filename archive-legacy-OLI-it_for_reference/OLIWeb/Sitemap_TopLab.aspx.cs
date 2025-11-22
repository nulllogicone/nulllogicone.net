// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using OliEngine.OliDataAccess;
using OliWeb.Klassen;

namespace OliWeb
{
    /// <summary>
    ///     erstellt eine Google Sitemap mit allen TopLab-Seiten Links
    /// </summary>
    public partial class Sitemap_TopLab : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TopLabList tll = CacheManager.CachedTopLab;
            Repeater.DataSource = tll.TopLab;
            DataBind();
        }

        protected string MakeDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return dt.ToString("s");
            }
            catch
            {
                return "2005-06-06";
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
    }
}
