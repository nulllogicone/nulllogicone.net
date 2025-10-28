// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     Über den SessionManager wird eine singleton Instanz des OliUser abgerufen.
    /// </summary>
    /// <example>
    ///     Im Code wird meistens eine OliUser Objektinstanz benötigt.
    ///     <code>OliUser user = SessionManager.Instance().OliUser;</code>
    /// </example>
    public class SessionManager : IUserSession
    {
        private static readonly AsyncLocal<SessionManager> AmbientSession = new AsyncLocal<SessionManager>();
        private OliUser user;

        /// <summary>
        ///     Singleton Konstruktor ist private
        /// </summary>
        private SessionManager()
        {
        }

        /// <summary>
        ///     statische Instance() Methode um sich dieses Objekt geben zu lassen. 
        ///     Es wird in der aktuellen Session gehalten.
        /// </summary>
        /// <returns> </returns>
        public static SessionManager Instance()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx?.Session != null)
            {
                SessionManager sm = ctx.Session["sm"] as SessionManager;
                if (sm == null)
                {
                    sm = new SessionManager();
                    ctx.Session["sm"] = sm;
                }

                AmbientSession.Value = sm;
                return sm;
            }

            if (AmbientSession.Value == null)
            {
                AmbientSession.Value = new SessionManager();
            }

            return AmbientSession.Value;
        }

        /// <summary>
        ///     Allows code running outside an HTTP request (e.g. unit tests) to replace the ambient session.
        /// </summary>
        /// <param name="session">The session instance to use for the current asynchronous flow.</param>
        public static void SetAmbientSession(SessionManager session)
        {
            AmbientSession.Value = session;
        }

        /// <summary>
        ///     Returns <c>true</c> when the current execution context has access to <see cref="HttpContext" /> and <see cref="HttpSessionState" />.
        /// </summary>
        public static bool HasHttpContext => HttpContext.Current?.Session != null;

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
                    retCult = (CultureInfo)ctx.Session["culture"];
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
                    retCult = (CultureInfo)ctx.Session["culture"];
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

        /// <summary>
        ///     das aktuelle Mittelschicht Objekt. Wenn noch kein OliUser existiert,
        ///     wird er hier neu erstellt.
        /// </summary>
        public OliUser OliUser
        {
            get
            {
                if (user == null)
                {
                    user = new OliUser();
                }
                return (user);
            }
        }
    }
}

