// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist eine Oberklasse f�r alle Seiten, auf denen
    ///     ein Stamm dargestellt wird. Sie setzt die MyTitle Eigenschaft und
    ///     stellt sicher, da� ein solches Objekt vorhanden ist (sonst leitet sie entsprechend weiter).
    /// </summary>
    public class MasterStammPage : BasePage
    {
        /// <summary>
        ///     CheckPreCondition()
        /// 
        ///     Diese Methode wird in der basis beim Init Ereignis aufgerufen
        ///     Sie wird hier �berschrieben um zus�tzlich auf das Vorhandensein
        ///     eines Stammobjektes zu pr�fen.
        /// 
        ///     Wenn die hiervon abgeleiteten aspx Seiten diese Methode
        ///     wiederrum �berschreiben bitte den base-Aufruf nicht vergessen 
        /// 
        ///     <code>base.CheckPreCondition()</code>
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // soll ein neuer Stamm erzeugt werden?
            bool cmdnew = Request.QueryString["cmd"] != null &&
                          Request.QueryString["cmd"] == "newS";
            if (cmdnew)
            {
                OliUser.Stamm = OliUser.NewStamm();
            }
            else if (OliUser.Stamm == null)
            {
                OliUser.Nachricht = "n0 Stamm";
                Response.Clear();
                Response.Write("Kein Stamm :=> Redirect");
                Response.Redirect(NO_STAMM_REDIRECT);
            }
        }

        /// <summary>
        ///     MyTitle wird in dieser Klasse mit dem Namen des Stammes �berschrieben.
        /// </summary>
        protected override string MyTitle
        {
            get { return OliUser.Stamm.StammRow.Stamm; }
        }

        /// <summary>
        ///     StammRdfLink wird in dieser Klasse mit dem Link auf das RDF Dokument �berschrieben.
        /// </summary>
        protected string StammRdfLink
        {
            get { return "http://nulllogicone.net/Stamm/" + OliUser.Stamm.StammRow.StammGuid + ".rdf"; }
        }
    }
}
