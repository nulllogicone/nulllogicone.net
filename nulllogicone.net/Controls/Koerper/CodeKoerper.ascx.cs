namespace OliWeb.Controls.Koerper
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliDataAccess.Functions;
	using OliEngine.OliMiddleTier.OLIs;
	using OliEngine.OliMiddleTier.Markierer;
	using OliEngine.OliMiddleTier.ZellHaufen;
	using OliWeb.Klassen;

	/// <summary>
	///		CodeKoerper.
	/// </summary>
	public abstract class CodeKoerper : MasterControl
	{
		protected System.Web.UI.WebControls.Label QLabel;
		protected System.Web.UI.WebControls.Panel WortraumControllerPanel;
		protected System.Web.UI.WebControls.Label CodeLabel;
		protected System.Web.UI.WebControls.ImageButton FischenImageButton;
		protected System.Web.UI.WebControls.TextBox KommentarTextBox;
		protected System.Web.UI.WebControls.HyperLink XMLHyperLink;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.DataGrid ShortCutsDataGrid;
		protected System.Web.UI.WebControls.HyperLink ExitHyperLink;
		protected System.Web.UI.WebControls.ImageButton RdfImageButton;
		protected System.Web.UI.WebControls.HyperLink RdfHyperLink;
//		protected System.Web.UI.WebControls.DataGrid CodeDataGrid;

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
			this.FischenImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.FischenImageButton_Click);
			this.ShortCutsDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ShortCutsDataGrid_ItemCommand);
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.RdfImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.RdfImageButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Member
		// ------

//		OliUser user;

		protected OliWeb.Controls.Wortraum.WortraumController WortraumController1;

		// Ereignisse
		// ----------

		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{


				// QLabel
				QLabel.Text = this.OliUser.Stamm.Q.C;

			if(PostIt != null)
			{
				// wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
				if(this.OliUser.Stamm.ShortCuts != null)
				{
					WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei ;
					WortraumController1.Markierbar = this.OliUser.Stamm.BinIchEingeloggt;
					WortraumController1.ZellBuilder = this.OliUser.Stamm.ShortCuts.ZellBuilder;
				}
					// sonst Code als Wortraum anzeigen
				else if(PostIt.Code != null)
				{
					// rdf Link einstellen
					string rdf = "http://nulllogicone.net/Code/" + PostIt.Code.CodeRow.CodeGuid.ToString() + ".rdf";
					RdfHyperLink.Text = rdf;
					RdfHyperLink.NavigateUrl = rdf;

					if(PostIt.Code.CodeRow.IsKommentarNull())
					{
						CodeLabel.Text = "Code von " + Stamm.StammRow.Stamm;
					}
					else
					{
						CodeLabel.Text = PostIt.Code.CodeRow.Kommentar;
					}

					if(!IsPostBack)
					{
						KommentarTextBox.Text = PostIt.Code.CodeRow.IsKommentarNull() ? "Code von " + Stamm.StammRow.Stamm : PostIt.Code.CodeRow.Kommentar;
					}

					WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei;
					WortraumController1.Markierbar = (this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt);
					WortraumController1.ZellBuilder = PostIt.Code.ZellBuilder;

					//						CodeRingeDataGrid.DataSource = PostIt.Code.MyRinge;
					//						CodeRingeDataGrid.DataBind();
				}
				
			}
		}

		// OnPreRender()
		protected override void OnPreRender(EventArgs e) 
		{
			// alles ausschalten
			WortraumController1.Markierbar = false;
			WortraumControllerPanel.BackColor = Color.White;
			CancelButton.Visible = false;

			if(this.OliUser.Stamm != null)
			{
				if(PostIt != null)
				{

					// wenn eingeloggt und mein PostIt
					if(this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
					{
						// Wortraum: markieren und fischen
						WortraumController1.Markierbar = true;							

						// Code: neu und delete
//TODO:							CodeDataGrid.Columns[1].Visible = true;
						

						// ShortCutsDataGrid
						ShortCutsDataGrid.DataSource = this.OliUser.Stamm.MyShortCuts;
						ShortCutsDataGrid.DataBind();

//						// NUR auf der CodeSite werden die ShortCuts angezeigt
//						if(this.Page is Sites.CodeSite)
//						{
//							ShortCutsDataGrid.Visible = true;							
//						}
					}

					// wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
					if(this.OliUser.Stamm.ShortCuts != null)
					{
						WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei;
						WortraumController1.Markierbar = this.OliUser.Stamm.BinIchEingeloggt;
						WortraumController1.ZellBuilder = this.OliUser.Stamm.ShortCuts.ZellBuilder;
						WortraumController1.Visible = true;
//						CodeRingeDataGrid.BackColor = Color.AntiqueWhite;
						CancelButton.Visible = true;
						
						WortraumControllerPanel.BackColor = Color.AntiqueWhite;
//							CodeRingeDataGrid.DataSource = PostIt.Code.MyRinge;
//							CodeRingeDataGrid.DataBind();
					}
					// sonst Code als Wortraum anzeigen
					else if(PostIt.Code != null)
					{
						WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei;
						WortraumController1.Markierbar = (this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt);
						WortraumController1.ZellBuilder = PostIt.Code.ZellBuilder;
						WortraumController1.Visible = true;
//						CodeRingeDataGrid.BackColor = Color.White;
//						WortraumControllerPanel.BackColor = Color.White;
//						CodeRingeDataGrid.DataSource = PostIt.Code.MyRinge;
//						CodeRingeDataGrid.DataBind();
					}
					
				}
			}
		}






		// ShortCutsDataGrid_ItemCommand
		private void ShortCutsDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{				
			ShortCutsDataGrid.DataKeyField = "ShortCutsGuid";
			Guid scguid = (Guid)ShortCutsDataGrid.DataKeys[e.Item.ItemIndex];
			
			// ausw�hlen
			if (e.CommandName == "ok" || e.CommandName == "select")
			{
				// CodeGrid deselect
//				CodeDataGrid.SelectedIndex = -1;
//				CodeDataGrid.EditItemIndex = -1;

				// ausw�hlen und edit mode
//				ShortCutsDataGrid.SelectedIndex = e.Item.ItemIndex;
				ShortCutsDataGrid.EditItemIndex = e.Item.ItemIndex;

				// ShowShortCuts
				this.OliUser.Stamm.ShowShortCuts(scguid);
				
				this.OliUser.Nachricht = "ShortCuts ausgew�hlt";
			}

			// CopyShortCutsToCode
			if (e.CommandName == "set")
			{
				// Die ShortCuts TextBox holen
				TextBox l = (TextBox) e.Item.FindControl("ShortCutsTextBox");
				this.OliUser.Stamm.ShortCuts.ShortCutsRow.ShortCut = l.Text;
				this.OliUser.Stamm.ShortCuts.UpdateShortCuts();
				
				ShortCutsDataGrid.EditItemIndex = -1;
				
				// wenn Code ausgew�hlt ist -> copieren
				if(PostIt.Code != null)
				{
					Guid cguid = PostIt.Code.CodeRow.CodeGuid ;
					this.OliUser.Stamm.CopyShortCutsToCode(scguid, cguid);
					PostIt.ShowCode(cguid);
				
					WortraumController1.ZellBuilder = PostIt.Code.ZellBuilder;
		
					this.OliUser.Nachricht = "ShortCuts auf Markierung �bertragen";

//TODO: Kathrin: �bernehmen
					this.OliUser.Stamm.ShortCuts = null;
				}
				this.OliUser.Stamm.MyShortCuts = null;
			}

//			// Delete ShortCuts
//			if(e.CommandName == "del")
//			{
//				ShortCutsDataGrid.SelectedIndex = -1;
//				ShortCutsDataGrid.EditItemIndex = -1;
//				ShortCuts sc = new ShortCuts(this.OliUser.Stamm, scguid);
//				sc.ShortCutsRow.Delete();
//				sc.UpdateShortCuts();
//				this.OliUser.Stamm.MyShortCuts = null;
//				this.OliUser.Stamm.ShortCuts = null;
//				this.OliUser.Nachricht = "ShortCuts gel�scht";
//
//			}
		}

//		// NeuShortCutsButton_Click()
//		private void NeuShortCutsButton_Click(object sender, System.EventArgs e)
//		{
//			this.OliUser.Stamm.NewShortCuts();
//			this.OliUser.Stamm.MyShortCuts = null;
//		}

		private void CodeDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void ShortCutsDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Abgleich dieser Markierung gegen alle Angler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FischenImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
				// Code Kommentar updaten
				if(this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
				{

					// Kommentar updaten
					if(PostIt.Code.CodeRow.IsKommentarNull() ||
						KommentarTextBox.Text != PostIt.Code.CodeRow.Kommentar)
					{
						PostIt.Code.CodeRow.Kommentar = KommentarTextBox.Text;
						PostIt.Code.UpdateCode();
					}
				}

				if(this.OliUser.Stamm != null && 
					PostIt != null &&
                    PostIt.Code != null)
				{
					Fischer f = new Fischer();
					f.fischen(PostIt.Code.CodeRow.CodeGuid, Guid.Empty);

					// neue Anzeigen
					Guid cguid = PostIt.Code.CodeRow.CodeGuid ;
					Guid pguid = PostIt.PostItRow.PostItGuid ;
					this.OliUser.Stamm.MyPostIt = null;
					this.OliUser.Stamm.ShowPostIt(pguid);
					this.OliUser.Stamm.PostIt.ShowCode(cguid);

					// user.Nachricht
					this.OliUser.Nachricht = "frisch gefischt: " + PostIt.MyEmpfaenger.Count + " Angler";
					this.OliUser.Stamm.ShortCuts = null;
					Response.Redirect(Helper.MakeBaseLink() + "Sites/PostItAnglerSite.aspx");
				}
//				CodeDataGrid.EditItemIndex = -1;

		}

		/// <summary>
		/// l�schen dieser Markierung
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DelLinkButton_Click(object sender, System.EventArgs e)
		{
			if (this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
			{
				PostIt.DeleteCode(PostIt.Code.CodeRow.CodeGuid);
				PostIt.Code = null;
				PostIt.MyCode = null;
				Response.Redirect(Helper.MakeBaseLink() + "Sites/PostItCodeSite.aspx");
			}
		}

		/// <summary>
		/// bricht den ausgew�hlten ShortCut ab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			ShortCutsDataGrid.EditItemIndex = -1;
			Stamm.ShortCuts = null;
		}

		private void RdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Expires = 0;
			Response.Buffer = true;
			Response.Clear();
			Response.ContentType = "application/rdf+xml";
			Response.AddHeader("content-disposition", "attachment; filename=\"Code.rdf\"");
			Response.Write(this.PostIt.Code.MakeCodeRdf());
			Response.End();
		}





	}
}

