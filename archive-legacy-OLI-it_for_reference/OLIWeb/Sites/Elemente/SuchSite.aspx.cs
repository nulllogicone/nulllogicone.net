// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using Microsoft.ApplicationInsights;
using OliEngine.DataSetTypes;
using OliEngine.OliMiddleTier;
using OliWeb.Controls.Floor.Suche;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     Die Seite nimmt im <b>Querystring</b> eine Zeichenfolge entgegen,
    ///     nach der gesucht werden soll (?such=abc). Dann �bergibt sie die 
    ///     Trefferergebnisse an die SuchList-Controls.
    /// </summary>
    public partial class SuchList : Page
    {
        private TelemetryClient telemetry = new TelemetryClient();
        protected StammListsControl StammListsControl1;
        protected PostItListsControl PostItListsControl1;
        protected TopLabListsControl TopLabListsControl1;

        /// <summary>
        ///     Querystring auswerten und gefundene Daten an ListsControls �bergeben
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string such = Request.QueryString["such"];

            if (!string.IsNullOrEmpty(such))
            {
                SuchLabel.Text = such.Trim();
                var sw = Stopwatch.StartNew();

                var st = DbDirect.SuchStamm(such);
                StammListsControl1.DataSource = st;
                StammListsControl1.Visible = true;

                var pt = DbDirect.SuchPostIt(such);
                PostItListsControl1.DataSource = pt;
                PostItListsControl1.Visible = true;

                var tt = DbDirect.SuchTopLab(such);
                TopLabListsControl1.DataSource = tt;
                TopLabListsControl1.Visible = true;

                // Application Insights telemetry
                var props = new Dictionary<string, string>
                {
                    { "SearchString", such },
                    { "ExecutionTime", sw.ElapsedMilliseconds.ToString() },
                    { "FoundStamm", st.Count.ToString() },
                    {"FoundPostIt", pt.Count.ToString() }
                };
                telemetry.TrackEvent("SearchInDbDirect", props, null);
            }
        }


        protected void GoogleLinkButton_Click(object sender, EventArgs e)
        {
            string target = "http://www.google.de/search?q=site%3Awww.oli-it.com+" +
                            HttpUtility.UrlEncode(Request.QueryString["such"]);
            Response.Redirect(target);
        }
    }
}

