// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     TopLabSite.
    /// </summary>
    public partial class TopLabSite : MasterTopLabPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hilfepanel zeigen/verstecken
            HilfePanel.Visible = OliUser.Stamm.Extras.ExtrasRow.hilfe;

            RdfHyperLink.NavigateUrl = "http://nulllogicone.net/TopLab/?" + TopLab.TopLabRow.TopLabGuid;
        }
    }
}
