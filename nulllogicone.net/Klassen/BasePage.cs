// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Web;
using OliEngine.OliMiddleTier.OLIs;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist von BasePage abgeleitet und Basis für
    ///     die meisten aspx Seiten. Sie kann für besondere Seiten noch weiter
    ///     abgeleitet werden.
    /// 
    ///     Auf diesen Seiten steht immer ein Session gerechtes
    ///     Mittelschicht-<see cref = "OliUser" />OliUser-Objekte zur Verfügung.
    /// </summary>
    /// <remarks>
    ///     Eigentlich können alle Seiten von dieser Klasse abgeleitet werden.
    ///     Es ist nur zu beachten, daß sie evtl. mitgeloggt werden.
    /// </remarks>
    public class BasePage : System.Web.UI.Page
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
        ///     Wenn kein PostIt vorhanden, kann höchstens die StammSite gezeigt werden
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
        ///     Diese Eigenschaft stellt das aktuelle <b>OliUser</b>-Objekt aus der Mittelschicht zur Verfügung
        /// </summary>
        protected OliUser OliUser
        {
            get { return SessionManager.Instance().OliUser; }
        }

//
//		/// <summary>Löschen!
//		/// Der Zustand wird durch die unterschiedlichen seiten gesteuert.
//		/// </summary>
//		protected Stamm.SichtbaresGrid SichtbaresGrid
//		{
//			get
//			{
//				return this.OliUser.Stamm.sichtbaresGrid;
//			}
//			set
//			{
//				this.OliUser.Stamm.sichtbaresGrid = value;
//			}
//		}

        /// <summary>
        ///     Seiten, die von der BasePage erben, können einen individuellen Titel
        ///     in die aspx Seite einbauen, der über den Code gefüllt wird.
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
        ///     Die Eigenschaft stellt das aktuelle <b>PostIt</b> aus der Mittelschicht zur Verfügung.
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
        ///     Diese Eigenschaft stellt das aktuelle <b>TopLab</b>-Objekt aus der Mittelschicht zur Verfügung
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
        ///     Diese Eigenschaft stellt das aktuelle <b>Angler</b>-Objekt aus der Mittelschicht zur Verfügung
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
        ///    Wenn guid's übergeben werden, werden die
        ///    entsprechenden Objekte in der Mittelschicht erstellt.
        ///    <p>
        ///        Da eigentlich alle Seiten von dieser Basis abgeleitet werden,
        ///        wird hier zuerst der Aufruf in der Counter-Tabelle protokolliert -</p>
        ///    <p>Dann können weitere Aufruf Vorraussetzung geprüft werden 
        ///        - sollten sie nicht erfüllt sein : abbrechen oder weiterleiten</p>
        ///</summary>
        ///<remarks>
        ///    <p>Eigentlich sollte man erzwingen, daß diese Methode überschrieben wird!
        ///        Aber bitte immer zuerst mit Aufruf der base:</p>
        ///</remarks>
        ///<example>
        ///    <code>
        ///        protected override void CheckPreCondition()
        ///        {
        ///        base.CheckPreCondition();
        ///        ...
        ///    </code>
        ///</example>
        protected virtual void CheckPreCondition()
        {
            // Besuch dieser Seite in den Counter eintragen
            System.Web.HttpContext ctx = System.Web.HttpContext.Current;
            Counter.AddVisit(ctx.Request.UserHostAddress, GetType().Name);

            // wenn eine AnglerGuid übergeben wird -> soll dieser Angler
            // mit seinem Stamm gezeigt werden
            if (Request["aguid"] != null)
            {
                try
                {
                    OliUser.ShowAngler(new Guid(Request["aguid"]));
                }
                catch (OliEngine.OliDataAccess.KeinAnglerException)
                {
                    throw new HttpException(404, "kein Angler gefunden");
                }
            }

            // wenn eine StammGuid an diese Seiten übergeben wird,
            // soll das heißen, da0 auch immer dieser Stamm angezeigt wird
            if (Request["sguid"] != null)
            {
                OliUser.ShowStamm(new Guid(Request["sguid"]));
            }

            // wenn eine PostItGuid an diese Seiten übergeben wird,
            // soll das heißen, daß auch immer diese Nachricht angezeigt wird
            if (Request["pguid"] != null)
            {
                if (OliUser.Stamm == null ||
                    OliUser.Stamm.PostIt == null ||
                    OliUser.Stamm.PostIt.PostItRow.PostItGuid.ToString() != Request["pguid"])
                    OliUser.ShowPostIt(new Guid(Request["pguid"]));
            }

            // wenn eine CodeGuid übergeben wird, soll sie geladen werden
            if (Request["cguid"] != null)
            {
                if (PostIt != null)
                {
                    PostIt.ShowCode(new Guid(Request["cguid"]));
                }
            }

            // wenn eine TopLabGuid übergeben wird,
            // soll diese Antwort angezeigt werden.
            if (Request["tguid"] != null)
            {
                OliUser.ShowTopLab(new Guid(Request["tguid"]));
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
                            Response.Redirect(Helper.MakeBaseLink() + "Sites/CodeSite.aspx");
                        }
                    }
                }
            }
        }


        // Ereignisse
        // ----------

        /// <summary>
        ///     override OnInit()
        ///     Alle Seiten, die von der BasePage ableiten werden vor
        ///     ihrer Initialisierung auf das Vorhandensein eines Stammes
        ///     geprüft. Sonst wird auf die default weitergeleitet.
        /// 
        ///     Auf bestimmten Seiten (editieren) kann diese Methode
        ///     überschrieben werden, um noch weitere Kriterien zu prüfen.
        ///     Ein OliUser-Objekt steht immer zur Verfügung
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckPreCondition();
        }
    }
}