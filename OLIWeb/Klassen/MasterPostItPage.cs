// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using OliEngine;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist eine Oberklasse für alle Seiten, auf denen
    ///     ein PostIt dargestellt wird. Sie setzt die MyTitle Eigenschaft und
    ///     stellt sicher, daß ein solches Objekt vorhanden ist (sonst leitet sie entsprechend weiter).
    /// </summary>
    public class MasterPostItPage : MasterStammPage
    {
        /// <summary>
        ///     CheckPreCondition()
        /// 
        ///     Diese Methode wird in der basis beim Init Ereignis aufgerufen
        ///     Sie wird hier überschrieben um zusätzlich auf das Vorhandensein
        ///     eines <b>PostIt-objektes</b> zu prüfen.
        /// 
        ///     Wenn die hiervon abgeleiteten aspx Seiten diese Methode
        ///     wiederrum überschreiben bitte den base-Aufruf nicht vergessen 
        /// 
        ///     <code>base.CheckPreCondition()</code>
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // soll ein neues PostIt erzeugt werden?
            bool cmdnew = Request.QueryString["cmd"] != null &&
                          Request.QueryString["cmd"] == "newP";
            if (cmdnew)
            {
                // Cookie - Prüfung
                string sess = Session.SessionID;
                string prevsess = Request["prevSessionId"];

                if (!string.IsNullOrEmpty(prevsess)
                    && prevsess != sess)
                {
                    Response.Redirect("~/NoFeature.aspx?nocookie=1");
                }

                Stamm.PostIt = OliUser.EingeloggterStamm.NewPostIt();
                Response.Redirect("~/Sites/Edit/PostItMaker.aspx");
            }

                // wenn kein PostIt und auch kein neues hinzugefügt werden soll
                // => Redirect
            else if (OliUser.Stamm.PostIt == null)
            {
                OliUser.Nachricht = "n0 PostIt";
                Response.Redirect(NO_POSTIT_REDIRECT);
            }
        }

        /// <summary>
        ///     MyTitle wird in dieser Klasse mit dem gekürzten Text des PostIt überschrieben.
        /// </summary>
        protected override string MyTitle
        {
            get
            {
                string s = "";
                // Titel
                s += PostIt.PostItRow.IsTitelNull() ? "" : PostIt.PostItRow.Titel + " - ";
                // Gekürzte PostIt
                s += OliUtil.FirstXWords(PostIt.PostItRow.PostIt, 10);

                return s;
            }
        }

        /// <summary>
        ///     PostItRdfLink wird in dieser Klasse mit dem Link auf das RDF Dokument überschrieben.
        /// </summary>
        protected string PostItRdfLink
        {
            get { return "http://nulllogicone.net/PostIt/" + OliUser.Stamm.PostIt.PostItRow.PostItGuid + ".rdf"; }
        }
    }
}