// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Net;
using System.Web.UI;
using OliWeb.Klassen;

namespace OliWeb
{
    /// <summary>
    ///     Wenn es zu einem Fehler kommt, wird diese Seite angezeigt.
    /// </summary>
    public partial class OliError : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.StatusCode = (int) HttpStatusCode.NotFound;
            var ex = Server.GetLastError();
            var msg = ex?.Message ?? "no LastError";

            var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetry.TrackPageView("OliError page view: " + msg);
        }


        protected void BestLinkButton_Click(object sender, EventArgs e)
        {
            Helper.RedirectToSite();
        }
    }
}