// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace OliWeb.Klassen
{
    public static class Common
    {
        // Bug: TODO: move this property to SessionManager. NO static Function!!
        /// <summary>
        ///     get or set the current culture of the session
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            set
            {
                HttpContext ctx = HttpContext.Current;
                ctx.Session.Add("culture", value);
            }
            get
            {
                //try
                //{
                List<string> allowedLanguages = new List<string>();
                allowedLanguages.Add("de");
                allowedLanguages.Add("en");

                allowedLanguages.Add("es");
                allowedLanguages.Add("fr");
                allowedLanguages.Add("ja");
                allowedLanguages.Add("ru");
                allowedLanguages.Add("nl");

                CultureInfo retCult;
                HttpContext ctx = HttpContext.Current;
                if (ctx != null && ctx.Session != null && ctx.Session["culture"] != null)
                {
                    retCult = (CultureInfo) ctx.Session["culture"];
                }
                else
                {
                    try
                    {
                        string[] langs = ctx.Request.UserLanguages;
                        if (langs.Length > 0)
                        {
                            CurrentCulture = new CultureInfo(langs[0]);
                        }
                    }
                    catch
                    {
                        CurrentCulture = new CultureInfo("en-US");
                    }
                    retCult = (CultureInfo) ctx.Session["culture"];
                }

                // Nur erlaubte Kulturen zurückgeben
                if (allowedLanguages.Contains(retCult.TwoLetterISOLanguageName))
                {
                    return retCult;
                }
                else
                {
                    return new CultureInfo("en-US");
                }
            }
        }
    }
}
