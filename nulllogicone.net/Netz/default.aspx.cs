// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Netz
{
    /// <summary>
    ///     Zusammenfassung für _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string nguidstr = Request.QueryString.ToString();
            if (nguidstr.Length > 0)
            {
                try
                {
                    string uri = "https://nulllogicone.net/Netz/?" + nguidstr;
                    UriHyperLink.NavigateUrl = uri;
                    UriHyperLink.Text = uri;
                    Guid nguid = new Guid(nguidstr);

                    OliEngine.OliMiddleTier.OLIx.Netz n = new OliEngine.OliMiddleTier.OLIx.Netz(nguid);
                    NetzLabel.Text = n.NetzRow.Netz;
                    BeschreibungLabel.Text = n.NetzRow.IsBeschreibungNull() ? "" : n.NetzRow.Beschreibung;

                    // Bild
                    if (n.NetzRow.IsDateiNull() || n.NetzRow.Datei.Length == 0)
                    {
                        DateiImage.Visible = false;
                    }
                    else
                    {
                        DateiImage.ImageUrl = OliEngine.OliUtil.MakeImageSrc(n.NetzRow.Datei);
                        DateiImage.Visible = true;
                    }

                    // Knoten
                    OliEngine.OliMiddleTier.OLIx.Knoten k = new OliEngine.OliMiddleTier.OLIx.Knoten(n.NetzRow);
                    KnotenRepeater.DataSource = k;
                    KnotenRepeater.DataBind();
                }
                catch
                {
                }
            }
        }

        protected string MakeKnotenURIRef(string kguid)
        {
            string kuri = "https://nulllogicone.net/Knoten/?" + kguid;
            return kuri;
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}