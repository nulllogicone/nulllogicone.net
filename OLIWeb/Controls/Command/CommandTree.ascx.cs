// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command
{
    ///<summary>
    ///    vertikale Menüdarstellung mit allen GET-Commands
    ///</summary>
    public partial class CommandTree : MasterControl
    {
//		protected System.Web.UI.WebControls.HyperLink StammHyperLink;
        protected HyperLink HyperLink4;
        protected HyperLink HyperLink7;
        protected HyperLink TopLabTopLabHyperlink;
        protected HyperLink TollisHyperlink;

        /// <summary>
        ///     Die Hyperlinks werden mit den aktuellen Bezeichnungen und Anzahlen beschriftet
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (OliUser.Stamm != null)
            {
            }
        }
    }
}