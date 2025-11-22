// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliEngine.DataSetTypes;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.Organ
{
    ///<summary>
    ///    AnglerOrgan.
    ///</summary>
    public partial class AnglerOrgan : MasterControl
    {
        // Member
        // ------

        protected void Page_Load(object sender, EventArgs e)
        {
//			user = SessionManager.Instance().OliUser;
            AnglerLabel.Text = GetGlobalResourceObject("SAPCT", "A").ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Malen();
        }

        // Eigenschaften
        // -------------

        // Methoden
        // --------

        // Malen
        protected void Malen()
        {
            if (OliUser.Stamm != null)
            {
                if (Angler != null)
                {
                    AnglerDataSet.AnglerRow a = Angler.AnglerRow;

                    // beschriften
                    AnglerLabel.Text = a.Angler;
                    AnglerLabel.ToolTip = "AnglerGuid: " + a.AnglerGuid;
                    BeschreibungLabel.Text = a.IsBeschreibungNull() ? "" : a.Beschreibung;
                }
            }
        }

        // Ereignisse
        // ----------
    }
}
