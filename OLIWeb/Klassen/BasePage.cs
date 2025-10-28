// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIs;
using Angler = OliEngine.OliMiddleTier.OLIs.Angler;
using PostIt = OliEngine.OliMiddleTier.OLIs.PostIt;
using Stamm = OliEngine.OliMiddleTier.OLIs.Stamm;
using TopLab = OliEngine.OliMiddleTier.OLIs.TopLab;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist von System.Web.UI.Page abgeleitet und Basis f�r
    ///     die meisten aspx Seiten. Sie kann f�r besondere Seiten noch weiter
    ///     abgeleitet werden.
    /// 
    ///     Auf diesen Seiten steht immer ein Session gerechtes
    ///     Mittelschicht-<see cref="OliUser" />OliUser-Objekte zur Verf�gung.
    /// </summary>
    /// <remarks>
    ///     Eigentlich k�nnen alle Seiten von dieser Klasse abgeleitet werden.
    ///     Es ist nur zu beachten, da� sie evtl. mitgeloggt werden.
    /// </remarks>
    public class BasePage : Page
    //	public class BasePage : XHTMLPage

    {
        // Konstanten
        // ----------

        /// <summary>
        ///     Uneingeloggt kann der Stamm nur angezeigt werden. Wenn ein Stamm vorhanden ist,
        ///     wird der Wert im Konstrukor auf die StammSite gestellt
        /// </summary>
        protected string NOT_EINGELOGGT_REDIRECT = "~/default.aspx";

        /// <summary>
        ///     Ohne Stamm in der Mittelschicht -> wird auf die default.aspx weitergeleitet.
        /// </summary>
        protected const string NO_STAMM_REDIRECT = "~/default.aspx";

        /// <summary>
        ///     Ohne Angler ... auf Stamm
        /// </summary>
        protected const string NO_ANGLER_REDIRECT = "~/Sites/StammSite.aspx";

        /// <summary>
        ///     Wenn kein PostIt vorhanden, kann h�chstens die StammSite gezeigt werden
        /// </summary>
        protected const string NO_POSTIT_REDIRECT = "~/Sites/StammSite.aspx";

        /// <summary>
        ///     Wenn kein TopLab vorhanden, versucht man das PostIt zu zeigen
        /// </summary>
        protected const string NO_TOPLAB_REDIRECT = "~/Sites/PostItSite.aspx";

        /// <summary>
        ///     Wenn ich nicht Urheber der Nachricht bin, kann ich sie nur ansehen.
        /// </summary>
        protected const string NOT_MYPOSTIT_REDIRECT = "~/Sites/PostItSite.aspx";

        /// <summary>
        ///     BasePage Konstruktor.
        /// </summary>
        public BasePage()
        {
            try
            {
                if (Stamm != null)
                {
                    NOT_EINGELOGGT_REDIRECT = "~/Sites/StammSite.aspx";
                }
            }
            catch
            {
            }
        }

        // Eigenschaften
        // -------------

        #region Mittelschicht Eigenschaften (SAPCT - Objekte)

        /// <summary>
        ///     Diese Eigenschaft stellt das aktuelle <b>OliUser</b>-Objekt aus der Mittelschicht zur Verf�gung
        /// </summary>
        protected OliUser OliUser
        {
            get { return SessionManager.Instance().OliUser; }
        }

        /// <summary>
        ///     Seiten, die von der BasePage erben, k�nnen einen individuellen Titel
        ///     in die aspx Seite einbauen, der �ber den Code gef�llt wird.
        /// </summary>
        protected virtual string MyTitle
        {
            get { return "BasePage.MyTitle"; }
        }

        protected Stamm Stamm
        {
            get { return OliUser.Stamm; }
            set { OliUser.Stamm = null; }
        }

        /// <summary>
        ///     Die Eigenschaft stellt das aktuelle <b>PostIt</b> aus der Mittelschicht zur Verf�gung.
        /// </summary>
        protected PostIt PostIt
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.PostIt;
                }
                else
                {
                    return null;
                }
            }
            set { OliUser.Stamm.PostIt = value; }
        }

        /// <summary>
        ///     Diese Eigenschaft stellt das aktuelle <b>TopLab</b>-Objekt aus der Mittelschicht zur Verf�gung
        /// </summary>
        protected TopLab TopLab
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.TopLab;
                }
                else
                {
                    return null;
                }
            }
            set { OliUser.Stamm.TopLab = value; }
        }

        /// <summary>
        ///     Diese Eigenschaft stellt das aktuelle <b>Angler</b>-Objekt aus der Mittelschicht zur Verf�gung
        /// </summary>
        protected Angler Angler
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return OliUser.Stamm.Angler;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        ///<summary>
        ///    Wenn guid's �bergeben werden, werden die
        ///    entsprechenden Objekte in der Mittelschicht erstellt.
        ///    <p>Da eigentlich alle Seiten von dieser Basis abgeleitet werden,
        ///        wird hier zuerst der Aufruf in der Counter-Tabelle protokolliert -</p>
        ///    <p>Dann k�nnen weitere Aufruf Vorraussetzung gepr�ft werden 
        ///        - sollten sie nicht erf�llt sein : abbrechen oder weiterleiten</p>
        ///</summary>
        ///<remarks>
        ///    <p>Eigentlich sollte man erzwingen, da� diese Methode �berschrieben wird!
        ///        Aber bitte immer zuerst mit Aufruf der base:</p>
        ///</remarks>
        ///<example>
        ///    <code>protected override void CheckPreCondition()
        ///        {
        ///        base.CheckPreCondition();
        ///        ...</code>
        ///</example>
        protected virtual void CheckPreCondition()
        {
            // Besuch dieser Seite in den Counter eintragen
            //HttpContext ctx = HttpContext.Current;
            //Counter.AddVisit(ctx.Request.UserHostAddress, GetType().Name);

            // Angler
            // ------
            // wenn eine AnglerGuid �bergeben wird -> soll dieser Angler
            // mit seinem Stamm gezeigt werden
            if (Request["aguid"] != null)
            {
                try
                {
                    OliUser.ShowAngler(new Guid(Request["aguid"]));
                }
                catch (KeinAnglerException)
                {
                    throw new HttpException(404, "kein Angler gefunden");
                }
            }

            // Stamm
            // -----
            // wenn eine StammGuid an diese Seiten �bergeben wird,
            // soll das hei�en, da0 auch immer dieser Stamm angezeigt wird
            if (Request["sguid"] != null)
            {
                try
                {
                    OliUser.ShowStamm(new Guid(Request["sguid"]));
                }
                catch (StammGibtsNichtException)
                {
                    throw new HttpException(404, "kein Stamm gefunden");
                }
            }

            // PostIt
            // ------
            // wenn eine PostItGuid an diese Seiten �bergeben wird,
            // soll das hei�en, da� auch immer diese Nachricht angezeigt wird
            try
            {
                if (Request["pguid"] != null)
                {
                    if (OliUser.Stamm == null ||
                        OliUser.Stamm.PostIt == null ||
                        OliUser.Stamm.PostIt.PostItRow.PostItGuid.ToString() != Request["pguid"])
                    {
                        OliUser.ShowPostIt(new Guid(Request["pguid"]));
                    }
                }
            }
            catch (Exception ex)
            {
                string details = ex.Message;
                throw new HttpException(404, "kein PostIt gefunden");
            }

            // Code
            // -----
            // wenn eine CodeGuid �bergeben wird, soll sie geladen werden
            try
            {
                if (Request["cguid"] != null)
                {
                    if (PostIt != null)
                    {
                        PostIt.ShowCode(new Guid(Request["cguid"]));
                    }
                }
            }
            catch (Exception)
            {
                throw new HttpException(404, "kein Code gefunden");
            }

            // TopLab
            // ------
            // wenn eine TopLabGuid �bergeben wird,
            // soll diese Antwort angezeigt werden.
            try
            {
                if (Request["tguid"] != null)
                {
                    OliUser.ShowTopLab(new Guid(Request["tguid"]));
                }
            }
            catch (Exception)
            {
                throw new HttpException(404, "kein TopLab gefunden");
            }

            // cmd
            // -------------------------------------------------------------------
            if (Request["cmd"] != null)
            {
                // ohne Stamm kann kein cmd funktionieren
                if (Stamm != null)
                {
                    if (Request["cmd"] == "exitS")
                    {
                        Stamm = null;
                    }
                    if (Request["cmd"] == "exitA")
                    {
                        Stamm.Angler = null;
                    }
                    if (Request["cmd"] == "exitP")
                    {
                        Stamm.PostIt = null;
                        Stamm.TopLab = null;
                    }
                    if (Request["cmd"] == "exitT")
                    {
                        Stamm.TopLab = null;
                    }
                    // mit PostIt gibt es folgende cmd
                    if (PostIt != null)
                    {
                        if (Request["cmd"] == "exitC")
                        {
                            Stamm.PostIt.Code = null;
                        }
                        if (Request["cmd"] == "newC")
                        {
                            Stamm.PostIt.Code = PostIt.NewCode(true);
                            Response.Redirect("~/Sites/CodeSite.aspx");
                        }
                    }
                }
            }
        }

        // Ereignisse
        // ----------

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // if (Request.Browser.IsMobileDevice)
            //     MasterPageFile = "~/Mobile.Master";
        }

        /// <summary>
        ///     override OnInit()
        ///     Alle Seiten, die von der BasePage ableiten werden vor
        ///     ihrer Initialisierung auf das Vorhandensein eines Stammes
        ///     gepr�ft. Sonst wird auf die default weitergeleitet.
        /// 
        ///     Auf bestimmten Seiten (editieren) kann diese Methode
        ///     �berschrieben werden, um noch weitere Kriterien zu pr�fen.
        ///     Ein OliUser-Objekt steht immer zur Verf�gung
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckPreCondition();
        }

        /// <summary>
        ///     if there is a culture info in the querystring, it will be set to the <see cref="Common.CurrentCulture" />.
        ///     Then the CurrentUICulture is set.
        ///     If it is not a neutral Culture, the CurrentCulture will bet set too.
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            // set the current culture by querystring
            if (Request["hl"] != null)
            {
                string language = Request["hl"];
                try
                {
                    CultureInfo ci = new CultureInfo(language);
                    Common.CurrentCulture = ci;
                }
                catch
                {
                }
            }

            // Das UI wird auf die aktuelle Kultur einstellen (der Thread nur wenn es eine spezifische ist)
            Thread.CurrentThread.CurrentUICulture = Common.CurrentCulture;
            if (!Common.CurrentCulture.IsNeutralCulture)
            {
                Thread.CurrentThread.CurrentCulture = Common.CurrentCulture;
            }
        }
    }
}
