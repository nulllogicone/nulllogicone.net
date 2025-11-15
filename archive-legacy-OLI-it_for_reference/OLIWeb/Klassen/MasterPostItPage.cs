// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using OliEngine;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist eine Oberklasse f�r alle Seiten, auf denen
    ///     ein PostIt dargestellt wird. Sie setzt die MyTitle Eigenschaft und
    ///     stellt sicher, da� ein solches Objekt vorhanden ist (sonst leitet sie entsprechend weiter).
    /// </summary>
    public class MasterPostItPage : MasterStammPage
    {
        /// <summary>
        ///     CheckPreCondition()
        /// 
        ///     Diese Methode wird in der basis beim Init Ereignis aufgerufen
        ///     Sie wird hier �berschrieben um zus�tzlich auf das Vorhandensein
        ///     eines <b>PostIt-objektes</b> zu pr�fen.
        /// 
        ///     Wenn die hiervon abgeleiteten aspx Seiten diese Methode
        ///     wiederrum �berschreiben bitte den base-Aufruf nicht vergessen 
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
                // Cookie - Pr�fung
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

                // wenn kein PostIt und auch kein neues hinzugef�gt werden soll
                // => Redirect
            else if (OliUser.Stamm.PostIt == null)
            {
                OliUser.Nachricht = "n0 PostIt";
                Response.Redirect(NO_POSTIT_REDIRECT);
            }
        }

        /// <summary>
        ///     MyTitle wird in dieser Klasse mit dem gek�rzten Text des PostIt �berschrieben.
        /// </summary>
        protected override string MyTitle
        {
            get
            {
                string s = "";
                // Titel
                s += PostIt.PostItRow.IsTitelNull() ? "" : PostIt.PostItRow.Titel + " - ";
                // Gek�rzte PostIt
                s += OliUtil.FirstXWords(PostIt.PostItRow.PostIt, 10);

                return s;
            }
        }

        /// <summary>
        ///     PostItRdfLink wird in dieser Klasse mit dem Link auf das RDF Dokument �berschrieben.
        /// </summary>
        protected string PostItRdfLink
        {
            get { return "http://nulllogicone.net/PostIt/" + OliUser.Stamm.PostIt.PostItRow.PostItGuid + ".rdf"; }
        }
    }
}
