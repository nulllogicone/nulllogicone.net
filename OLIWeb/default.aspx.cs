// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb
{
    public partial class _default : BasePage
    {
        /// <summary>
        ///     ist die erste Seite, die vom Webserver ausgeliefert wird,
        ///     wenn nur die Domain eingegeben wurde. Wenn ein <b>Querystring</b>
        ///     mitgegeben wurde, wird er hier ausgewertet.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mit Übergabeparametern entsprechende Seite Laden
            // wird auch aufgerufen wenn eine Adresse mit S/guid.aspx auf
            // einen Querystring ge- PathForwarded wird
            if (Request.QueryString.Count > 0)
            {
                LoadFromQueryString(Request);
            }
        }

        /// <summary>
        ///     falls beim Aufruf von oli-it ein Querystring übergeben wird,
        ///     versucht diese Funktion die entsprechenden Daten in die
        ///     Mittelschicht zu laden und danach weiterzuleiten <see cref="Klassen.Helper.RedirectToSite" />
        /// </summary>
        /// <param name="request"> </param>
        private static void LoadFromQueryString(HttpRequest request)
        {
            OliUser user = SessionManager.Instance().OliUser;
            bool weiter = false;

            if (request["sguid"] != null)
            {
                user.ShowStamm(new Guid(request["sguid"]));
                weiter = true;

                if (request["aguid"] != null)
                {
                    user.Stamm.ShowAngler(new Guid(request["aguid"]));
                    weiter = true;
                }
                if (request["pguid"] != null)
                {
                    user.Stamm.ShowPostIt(new Guid(request["pguid"]));
                    weiter = true;
                }
                if (request["tguid"] != null)
                {
                    user.Stamm.ShowTopLab(new Guid(request["tguid"]));
                    weiter = true;
                }
            }
            else // ohne Stamm - nur A, P oder T
            {
                if (request["aguid"] != null)
                {
                    user.ShowAngler(new Guid(request["aguid"]));
                    weiter = true;
                }
                if (request["pguid"] != null)
                {
                    user.ShowPostIt(new Guid(request["pguid"]));
                    weiter = true;
                }
                if (request["tguid"] != null)
                {
                    user.ShowTopLab(new Guid(request["tguid"]));
                    weiter = true;
                }
            }

            // wenn in der Mittelschicht Objekte instantiiert wurden
            // => auf default - Seite (sich selbst) ohne QueryString
            // weiterleiten (also abschneiden)
            if (weiter)
            {
                // ohne Querystring auf sich weiterleiten
                Helper.RedirectToSite();
            }
        }
    }
}