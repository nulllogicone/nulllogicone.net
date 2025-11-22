// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Zweig
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected System.Web.UI.WebControls.Label NetzGuidLabel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string zguidstr = Request.QueryString.ToString();
            string uri;
            if (zguidstr.Length > 0)
            {
                try
                {
                    uri = "https://nulllogicone.net/Zweig/?" + zguidstr;
                    UriHyperLink.NavigateUrl = uri;
                    UriHyperLink.Text = uri;

                    Guid zguid = new Guid(zguidstr);
                    OliEngine.OliMiddleTier.OLIx.Zweig z = new OliEngine.OliMiddleTier.OLIx.Zweig(zguid);
                    ZweigLabel.Text = z.ZweigRow.Zweig;

                    uri = "https://nulllogicone.net/Baum/?" + z.ZweigRow.BaumGuid;
                    BaumHyperLink.Text = uri;
                    BaumHyperLink.NavigateUrl = uri;

                    if (!z.ZweigRow.IsweiterNetzGuidNull())
                    {
                        uri = "https://nulllogicone.net/Netz/?" + z.ZweigRow.weiterNetzGuid;
                        WeiterNetzHyperLink.Text = uri;
                        WeiterNetzHyperLink.NavigateUrl = uri;
                    }
                    if (!z.ZweigRow.IsweiterBaumGuidNull())
                    {
                        uri = "https://nulllogicone.net/Baum/?" + z.ZweigRow.weiterBaumGuid;
                        WeiterBaumHyperLink.Text = uri;
                        WeiterBaumHyperLink.NavigateUrl = uri;
                    }
                }
                catch (Exception ex)
                {
                    //				Response.Write (ex.Message);
                }
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
