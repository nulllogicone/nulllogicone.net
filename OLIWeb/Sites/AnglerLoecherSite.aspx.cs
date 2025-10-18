// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using OliEngine.OliDataAccess.Functions;
using OliWeb.Controls.AjaxWortraum;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     AnglerLoecherSite.
    /// </summary>
    public partial class AnglerLoecherSite : MasterStammPage
    {
        protected AjaxWortraumControlFlip AjaxWortraumControlFlip1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Angler != null)
            {
                AjaxWortraumControlFlip1.Markierer = Angler;
                AjaxWortraumControlFlip1.Markierbar = Stamm.BinIchEingeloggt;
                AjaxWortraumControlFlip1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;

                RdfHyperLink.NavigateUrl = "http://nulllogicone.net/Angler/?" + Angler.AnglerRow.AnglerGuid;

                if (Stamm.BinIchEingeloggt && Stamm.Angler.BinIchMeinAngler)
                {
                    FischenPanel.Visible = true;
                }
                else
                {
                    FischenPanel.Visible = false;
                }
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        protected void FischenImageButton_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            Fischer f = new Fischer();
            f.Fischen(Guid.Empty, Angler.AnglerRow.AnglerGuid);
            var ts = sw.Elapsed.TotalMilliseconds;

            // AppInsights
            var telemetry = new TelemetryClient();
            var props = new Dictionary<string, string>()
                {
                    {"Origin","Angler" },
                    {"StammGuid", OliUser.Stamm.StammRow.StammGuid.ToString()},
                    {"Stamm", OliUser.Stamm.StammRow.Stamm},
                    {"AnglerGuid", Angler.AnglerRow.AnglerGuid.ToString()},
                    {"Angler.Title", string.IsNullOrEmpty(Angler.AnglerRow.Angler) ? "" : Angler.AnglerRow.Angler}
                };
            var metrics = new Dictionary<string, double>()
                {
                    {"ExecutionTime", ts}
                };
            telemetry.TrackEvent("Fischen", props, metrics);

            // Anzeige auffrischen
            Guid aguid = Angler.AnglerRow.AnglerGuid;
            OliUser.Stamm.MyAngler = null;
            OliUser.Stamm.ShowAngler(aguid);

            // user.Nachricht
            OliUser.Nachricht = "Angler: " + Angler.MyPostIt.Count;
            Response.Redirect("~/Sites/AnglerPostItSite.aspx");
        }
    }
}