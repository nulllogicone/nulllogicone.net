// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Controls.Koerper.Organ;
using OliWeb.Controls.Koerper.ViewGrids;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    ///<summary>
    ///    TopLabKoerper.
    ///</summary>
    public partial class TopLabKoerper : MasterControl
    {
        protected LinkButton TopButton;


        protected TopLabOrgan TopLabOrgan1;
        protected TopLabTollisGrid TopLabTollisGrid1;

        // Ereignisse
        // ----------

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                StammHyperLink.Text = TopLab.MyStamm.StammRow.Stamm;
                StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" +
                                             TopLab.MyStamm.StammRow.StammGuid;
            }
            catch
            {
            }
        }

        // OnPreRender()
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Alles unsichtbar
            TopLabTollisGrid1.Visible = true;

            if (OliUser.Stamm != null)
            {
                if (TopLab != null)
                {
                    TopLabOrgan1.Visible = true;
                    QLabel.Text = OliUser.Stamm.Q.T;

                    if (TopLab.BinIchMeinTopLab &&
                        Stamm.BinIchEingeloggt)
                    {
                        EditHyperLink.Visible = true;
                    }
                    else
                    {
                        EditHyperLink.Visible = false;
                    }
                }
            }
        }
    }
}
