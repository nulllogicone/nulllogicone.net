// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using OliWeb.Controls.Koerper.Organ;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    ///<summary>
    ///    das Filterprofil eines Stammes.
    ///</summary>
    public partial class AnglerKoerper : MasterControl
    {
        protected AnglerOrgan AnglerOrgan1;

        // Ereignisse
        // ----------

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            QLabel.Text = OliUser.Stamm.Q.A;

            if (Stamm.BinIchEingeloggt)
            {
                EditHyperLink.Visible = true;
            }
        }

        private void RdfImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Angler.rdf\"");
            Response.Write(Angler.MakeAnglerRDF());
            Response.End();
        }
    }
}