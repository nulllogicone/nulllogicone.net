namespace OliWeb.Controls.Koerper.Organ
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;


	using OliEngine;
	using OliEngine.DataSetTypes;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;
	using Controls;
	
	/// <summary>
	///		TopLabOrgan.
	/// </summary>
	public abstract class TopLabOrgan : MasterControl
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Button UpdateButton;
		protected System.Web.UI.WebControls.Label TopLabLabel;
		protected System.Web.UI.WebControls.Literal UrlLiteral;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.ImageButton TopImageButton;
		protected System.Web.UI.WebControls.Label LohnLabel;
		protected System.Web.UI.WebControls.Label TitelLabel;
		protected System.Web.UI.WebControls.Label DatumLabel;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image1;


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		

		private void InitializeComponent()
		{
			this.TopImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.TopImageButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Member
		// ------


		protected Gimicks.KlickBild KlickBild1;
		protected Koerper.ViewGrids.TopLabTopLabGrid TopLabTopLabGrid1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Malen();
		}

		protected override void OnPreRender(EventArgs e) 
		{
			
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

				TitelLabel.Text = fbr.IsTitelNull() ? "" : fbr.Titel;
				TopLabLabel.Text = OliUtil.MakeHtmlLineBreak(fbr.TopLab);
				LohnLabel.Text = OliUtil.MakeRedKook(fbr.Lohn);
				DatumLabel.Text = fbr.IsdatumNull() ? "" : fbr.datum.ToShortDateString() ;

				KlickBild1.BildName = fbr.IsdateiNull() ? "" : fbr.datei;
				KlickBild1.Breite = 50;

				if(!fbr.IsURLNull())
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
		private void UrlImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

		private void TopImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.OliUser.Stamm.ShowTopLab(TopLab.TopLabRow.TopTopLabGuid);
//			Helper.RedirectToSite();
			Response.Redirect(Helper.MakeBaseLink() + "Sites/TopLabSite.aspx");
		}


	}
}
