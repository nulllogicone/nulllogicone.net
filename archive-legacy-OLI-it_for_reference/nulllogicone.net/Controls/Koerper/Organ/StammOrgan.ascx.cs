namespace OliWeb.Controls.Koerper.Organ
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.ComponentModel;

	using OliEngine;
	using OliEngine.OliMiddleTier.OLIs ;
	using OliEngine.DataSetTypes;

	using OliWeb.Klassen;
	using OliWeb.Controls.Gimicks;

	/// <summary>
	///		StammOrgan.
	/// </summary>
	public abstract class StammOrgan : MasterControl
	{
		protected System.Web.UI.WebControls.Panel StammPanel;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		

		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

//		protected System.Web.UI.WebControls.LinkButton StammLinkButton;
		protected System.Web.UI.WebControls.HyperLink StammHyperLink;
		protected System.Web.UI.WebControls.Label BeschreibungLabel;
		protected System.Web.UI.WebControls.Label KooKLabel;

		// Member
		// ------


		protected System.Web.UI.WebControls.Label DatumLabel;
		protected System.Web.UI.WebControls.HyperLink MailHyperLink;
		protected Controls.Gimicks.KlickBild KlickBild1;
		
		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			Malen();
		}

//		// OnPreRender()
//		protected override void OnPreRender(EventArgs e) 
//		{
//			
//		}



		// Methoden
		// --------

		// Malen
		protected void Malen()
		{
			if(this.OliUser.Stamm != null)
			{

				if(!this.OliUser.Stamm.BinIchEingeloggt)
				{
//					ShowEdit = false;
					MailHyperLink.Visible = true;
				}


				StammDataSet.StammRow  s = this.OliUser.Stamm.StammRow ;
			
				// Neue Stamm Row
				if (s.RowState == DataRowState.Added)
				{
					StammHyperLink.Visible = false;
//					StammHyperLink.NavigateUrl = OliWeb.Klassen.Helper.MakeBaseLink() + "S/" + s.StammGuid.ToString() + ".aspx" ;
					StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" + s.StammGuid.ToString();


				}
				else
				{
					StammHyperLink.Visible = true;
				}


				// Einzelansicht beschriften
//				StammHyperLink.NavigateUrl = OliWeb.Klassen.Helper.MakeBaseLink() + "S/" + s.StammGuid.ToString() + ".aspx" ;
				StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" + s.StammGuid.ToString();


				StammHyperLink.Text = s.Stamm;
				StammHyperLink.ToolTip = "Diesen Stamm anzeigen";
				BeschreibungLabel.Text = s.IsBeschreibungNull() ? "" : s.Beschreibung;
				KooKLabel.Text = OliUtil.MakeRedKook(s.KooK);
				DatumLabel.Text = s.IsDatumNull() ? "" : s.Datum.ToShortDateString();
				DatumLabel.ToolTip = "erzeugt: " + (s.IsDatumNull() ? "?" : s.Datum.ToString());
				// KlickBild
				// dem Stamm wird immer ein Bild eingef�gt - und wenns das O ist
				KlickBild1.Breite = 75;
				if(s.IsDateiNull() || s.Datei.Length == 0)
				{
					KlickBild1.BildName = "o.gif";
				}
				else
				{
					KlickBild1.BildName = s.Datei;
				}
			



				
			}
		}

		// Ereignisse
		// ----------





//		// StammLinkButton_Click
//		private void StammLinkButton_Click(object sender, System.EventArgs e)
//		{
//			// ids merken zum wieder herstellen
//			Guid pguid = Guid.Empty ;
//			Guid tguid = Guid.Empty ;
//			Guid aguid = Guid.Empty ;
//			
//			if(this.OliUser != null && this.OliUser.Stamm != null)
//			{
//				if (this.PostIt != null)
//				{
//					pguid = this.PostIt.PostItRow.PostItGuid ;
//				}
//				if (this.TopLab != null)
//				{
//					tguid = this.TopLab.TopLabRow.TopLabGuid ;
//				}
//				if(this.Angler != null)
//				{
//					aguid = this.Angler.AnglerRow.AnglerGuid ;
//				}
//			}
//
//			// Stamm zeigen
//			this.OliUser.ShowStamm(this.OliUser.Stamm.StammRow.Stamm,"",this.Request.UserHostName );
//
//
//			try
//			{
//				// wenn vorher was offen war : wieder herstellen
//				if (pguid != Guid.Empty)
//				{
//					this.OliUser.Stamm.ShowPostIt(pguid);
//				}
//				if (tguid != Guid.Empty)
//				{
//					this.OliUser.Stamm.ShowTopLab(tguid);
//				}
//				if (aguid != Guid.Empty)
//				{
//					this.OliUser.Stamm.ShowAngler(aguid);
//				}
//			}
//			catch (Exception ex)
//			{
//				
//				this.OliUser.Nachricht = "Kathrin du bringst es auf den Punkt - genau ! Liebe Dich ! Bussi ";
//				Helper.RedirectToSite();
//				throw new Exception("was soll das? " + ex.Message);
//
//			}
//
//			Helper.RedirectToSite();
//		}
//
//		private void CancelButton_Click(object sender, System.EventArgs e)
//		{
////			Helper.SetAllButtons(this.Page.Controls, true);
//			if(this.OliUser.Stamm.StammRow.RowState == DataRowState.Added)
//			{
//				this.OliUser.Stamm = null;
//			}
//			Helper.RedirectToSite();
//		}
//
//
//
//		private void ExtrasButton_Click(object sender, System.EventArgs e)
//		{
//		
//		}








	}
}

