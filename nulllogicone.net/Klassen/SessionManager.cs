// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
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
    public class SessionManager
    {
        private OliEngine.OliMiddleTier.OLIs.OliUser user;

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
        /// <returns></returns>
        public static SessionManager Instance()
        {
            HttpContext ctx = HttpContext.Current;

            SessionManager sm = (SessionManager) ctx.Session["sm"];
            if (sm == null)
            {
                sm = new SessionManager();
                ctx.Session["sm"] = sm;
            }
            return (sm);
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