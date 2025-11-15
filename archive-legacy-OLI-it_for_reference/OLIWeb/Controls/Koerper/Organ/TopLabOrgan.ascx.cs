// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine;
using OliEngine.DataSetTypes;
using OliWeb.Controls.Gimicks;
using OliWeb.Controls.Koerper.ViewGrids;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.Organ
{
    ///<summary>
    ///    TopLabOrgan.
    ///</summary>
    public partial class TopLabOrgan : MasterControl
    {
        protected Panel Panel1;
        protected Button UpdateButton;
        protected Button CancelButton;
        protected Image Image2;
        protected Image Image1;

        //#region Web Form Designer generated code

        //protected override void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{
        //}

        //#endregion

        // Member
        // ------

        protected KlickBild KlickBild1;
        protected TopLabTopLabGrid TopLabTopLabGrid1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Malen();
        }

        // Eigenschaften
        // -------------

        // Methoden
        // --------

        // Malen
        protected void Malen()
        {
            // mit TopLab
            if (TopLab != null)
            {
                TopLabDataSet.TopLabRow fbr = TopLab.TopLabRow;

                // Wenn es ein Unter-TopLab ist => Pfeil anzeigen
                TopImageButton.Visible = !fbr.IsTopTopLabGuidNull();
                LabelTopTopLab.Visible = !fbr.IsTopTopLabGuidNull();

                TitelLabel.Text = fbr.IsTitelNull() ? "" : fbr.Titel;
                TopLabLabel.Text = OliUtil.MakeHtmlLineBreak(fbr.TopLab);
                LohnLabel.Text = OliUtil.MakeRedKook(fbr.Lohn);
                DatumLabel.Text = fbr.Datum.ToShortDateString();

                KlickBild1.BildName = fbr.IsDateiNull() ? "" : fbr.Datei;
                KlickBild1.Breite = 50;

                if (!fbr.IsURLNull())
                {
                    UrlLiteral.Text = OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(fbr.URL));
                    UrlLiteral.Visible = true;
                }
                else
                {
                    UrlLiteral.Visible = false;
                }

                // Child-TopLab zeigen
                TopLabTopLabGrid1.ParentTopLab = TopLab;
            }
        }

        // Ereignisse
        // ----------

        // UrlImageButton_Click()
        private void UrlImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(TopLab.TopLabRow.URL);
        }

//		private void CancelButton_Click(object sender, System.EventArgs e)
//		{
//			if(TopLab.TopLabRow.RowState == DataRowState.Added)
//			{
//				TopLab = null;
//			}
////			Helper.SetAllButtons(this.Page.Controls, true);
//			Helper.RedirectToSite();
//		}

        protected void TopImageButton_Click(object sender, EventArgs eventArgs)
        {
            OliUser.Stamm.ShowTopLab(TopLab.TopLabRow.TopTopLabGuid);
//			Helper.RedirectToSite();
            Response.Redirect("~/Sites/TopLabSite.aspx");
        }
    }
}
