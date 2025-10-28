// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace nulllogicone.net.Knoten
{
    /// <summary>
    ///     Zusammenfassung f�r _default.
    /// </summary>
    public partial class _default : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string kguidstr = Request.QueryString.ToString();
            string uri;
            if (kguidstr.Length > 0)
            {
                uri = "https://nulllogicone.net/Knoten/?" + kguidstr;
                UriHyperLink.NavigateUrl = uri;
                UriHyperLink.Text = uri;

                Guid kguid = new Guid(kguidstr);
                OliEngine.OliMiddleTier.OLIx.Knoten k = new OliEngine.OliMiddleTier.OLIx.Knoten(kguid);
                KnotenLabel.Text = k.KnotenRow.Knoten;
                BeschreibungLabel.Text = k.KnotenRow.IsBeschreibungNull() ? "" : k.KnotenRow.Beschreibung;
                uri = "https://nulllogicone.net/Netz/?" + k.KnotenRow.NetzGuid;
                NetzHyperLink.Text = uri;
                NetzHyperLink.NavigateUrl = uri;
                OlisLabel.Text = k.KnotenRow.VgbOLIs.ToString();
                GetLabel.Text = k.KnotenRow.VgbGet.ToString();
                IlosLabel.Text = k.KnotenRow.VgbILOs.ToString();
                FitLabel.Text = k.KnotenRow.VgbFit.ToString();
                if (!k.KnotenRow.IsweiterNetzGuidNull())
                {
                    uri = "https://nulllogicone.net/Netz/?" + k.KnotenRow.weiterNetzGuid;
                    WeiterNetzHyperLink.Text = uri;
                    WeiterNetzHyperLink.NavigateUrl = uri;
                }
                if (!k.KnotenRow.IsweiterBaumGuidNull())
                {
                    uri = "https://nulllogicone.net/Baum/?" + k.KnotenRow.weiterBaumGuid;
                    WeiterBaumHyperLink.Text = uri;
                    WeiterBaumHyperLink.NavigateUrl = uri;
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
