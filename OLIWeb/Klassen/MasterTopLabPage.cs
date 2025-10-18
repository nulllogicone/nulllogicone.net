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
    ///     ist eine Oberklasse für alle Seiten, auf denen
    ///     ein TopLab dargestellt wird. Sie setzt die MyTitle Eigenschaft und
    ///     stellt sicher, daß ein solches Objekt vorhanden ist (sonst leitet sie entsprechend weiter).
    /// </summary>
    public class MasterTopLabPage : MasterPostItPage
    {
        /// <summary>
        ///     CheckPreCondition()
        /// 
        ///     Diese Methode wird in der basis beim Init Ereignis aufgerufen
        ///     Sie wird hier überschrieben um zusätzlich auf das Vorhandensein
        ///     eines <b>TopLab-objektes</b> zu prüfen.
        /// 
        ///     Wenn die hiervon abgeleiteten aspx Seiten diese Methode
        ///     wiederrum überschreiben bitte den base-Aufruf nicht vergessen 
        /// 
        ///     <code>base.CheckPreCondition()</code>
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // soll ein neues TopLab erzeugt werden?
            bool cmdnew = Request.QueryString["cmd"] != null &&
                          Request.QueryString["cmd"] == "newT";

            // wenn kein TopLab und auch kein neues hinzugefügt werden soll
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
        ///     MyTitle wird in dieser Klasse mit dem gekürzten Text des PostIt überschrieben.
        /// </summary>
        protected override string MyTitle
        {
            get
            {
                string s = "";
                // Titel
                s += TopLab.TopLabRow.IsTitelNull() ? "" : TopLab.TopLabRow.Titel + " - ";
                // Gekürzte TopLab
                s += OliUtil.FirstXWords(TopLab.TopLabRow.TopLab, 10);

                return s;
            }
        }
    }
}