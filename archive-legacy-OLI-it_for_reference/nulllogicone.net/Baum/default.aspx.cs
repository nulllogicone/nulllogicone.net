// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Baum
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string bguidstr = Request.QueryString.ToString();
            if (bguidstr.Length > 0)
            {
                Guid bguid = new Guid(bguidstr);

                OliEngine.OliMiddleTier.OLIx.Baum b = new OliEngine.OliMiddleTier.OLIx.Baum(bguid);
                BaumLabel.Text = b.BaumRow.Baum;

                string uri = "https://nulllogicone.net/Baum/?" + bguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;

                BeschreibungLabel.Text = b.BaumRow.IsBeschreibungNull() ? "" : b.BaumRow.Beschreibung;

                // Bild
                if (b.BaumRow.IsDateiNull() || b.BaumRow.Datei.Length == 0)
                {
                    DateiImage.Visible = false;
                }
                else
                {
                    DateiImage.ImageUrl = OliEngine.OliUtil.MakeImageSrc(b.BaumRow.Datei);
                    DateiImage.Visible = true;
                }

                // Knoten
                OliEngine.OliMiddleTier.OLIx.Zweig z = new OliEngine.OliMiddleTier.OLIx.Zweig(b.BaumRow);
                ZweigRepeater.DataSource = z;
                ZweigRepeater.DataBind();
            }
        }

        protected string MakeZweigURIRef(string zguid)
        {
            string zuri = "https://nulllogicone.net/Zweig/?" + zguid;
            return zuri;
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
