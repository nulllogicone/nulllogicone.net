// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System.Data;
using OliEngine;

namespace OliWeb.Klassen
{
    /// <summary>
    ///     ist eine Oberklasse f�r alle Seiten, auf denen
    ///     ein TopLab dargestellt wird. Sie setzt die MyTitle Eigenschaft und
    ///     stellt sicher, da� ein solches Objekt vorhanden ist (sonst leitet sie entsprechend weiter).
    /// </summary>
    public class MasterTopLabPage : MasterPostItPage
    {
        /// <summary>
        ///     CheckPreCondition()
        /// 
        ///     Diese Methode wird in der basis beim Init Ereignis aufgerufen
        ///     Sie wird hier �berschrieben um zus�tzlich auf das Vorhandensein
        ///     eines <b>TopLab-objektes</b> zu pr�fen.
        /// 
        ///     Wenn die hiervon abgeleiteten aspx Seiten diese Methode
        ///     wiederrum �berschreiben bitte den base-Aufruf nicht vergessen 
        /// 
        ///     <code>base.CheckPreCondition()</code>
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // soll ein neues TopLab erzeugt werden?
            bool cmdnew = Request.QueryString["cmd"] != null &&
                          Request.QueryString["cmd"] == "newT";

            // wenn kein TopLab und auch kein neues hinzugef�gt werden soll
            // => Redirect
            if (!cmdnew &&
                (OliUser.Stamm.TopLab == null ||
                 OliUser.Stamm.TopLab.TopLabRow.RowState == DataRowState.Added))
            {
                OliUser.Nachricht = "n0 TopLab";
                Response.Redirect(NO_TOPLAB_REDIRECT);
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
                s += TopLab.TopLabRow.IsTitelNull() ? "" : TopLab.TopLabRow.Titel + " - ";
                // Gek�rzte TopLab
                s += OliUtil.FirstXWords(TopLab.TopLabRow.TopLab, 10);

                return s;
            }
        }
    }
}
