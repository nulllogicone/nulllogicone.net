// (c) Frederic Luchting
// ---------------------

namespace OliWeb.Controls.Koerper.ViewGrids
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliDataAccess.Functions;
	using OliEngine.DataSetTypes.Views;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;

	/// <summary>
	///	PostItCodeGrid.
	/// </summary>
	public class PostItCodeGrid : ViewGridControl
	{
		const int CodeDataGridDelColumnIndex = 3;
		protected System.Web.UI.WebControls.Label TitleLabel;
		protected System.Web.UI.WebControls.DataGrid CodeDataGrid;


		// Eigenschaften
		// -------------

		// mySource
		private PostItCodeDataSet.PostItCodeDataTable mySource
		{
			get
			{
				try
				{
					return(PostIt.MyCode);
				}
				catch
				{
					return(null);
				}
			}
		}

		// Ereignisse
		// ----------

		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			TitleLabel.Text = this.OliUser.Stamm.Q.C + " (" + PostIt.MyCode.Rows.Count.ToString() + ")";
//				CodeDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.C;
//				CodeDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.C_X;


			CodeDataGrid.DataSource = mySource;
			CodeDataGrid.DataBind();

			// keine Daten vorhanden
			if (mySource.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>keine Markierung für diese Nachricht vorhanden</div><hr />";
				this.Controls.Add(l);
			}

			// die eigenen Codes darf man löschen
			if(Stamm.BinIchEingeloggt && Stamm.PostIt.BinIchMeinPostIt)
			{
				CodeDataGrid.Columns[CodeDataGridDelColumnIndex].Visible = true;
			}
		}
		#region Vom Web Form-Designer generierter Code
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Erforderliche Methode für die Designerunterstützung
		/// </summary>
		private void InitializeComponent()
		{
			this.CodeDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.CodeDataGrid_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CodeDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

			CodeDataGrid.DataKeyField = "CodeGuid";
			Guid cguid = new Guid(CodeDataGrid.DataKeys[e.Item.ItemIndex].ToString());
			
//			// Angler zeigen
//			if (e.CommandName == "angler")
//			{
//
//				PostIt.Code = null;
//				Helper.RedirectToSite();
//			}

			// Code löschen
			if (e.CommandName == "del")
			{
				if (this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
				{
					PostIt.DeleteCode(cguid);

					CodeDataGrid.EditItemIndex = -1;
					CodeDataGrid.SelectedIndex = -1;

					// WortraumController.ZellBuilder ausschalten
					PostIt.Code = null;

					// Ansicht aktualisieren
					PostIt.MyCode = null;

				}
			}
			else
			{
				// Code zeigen

				// Code zeigen und WEITERLEITEN
				PostIt.ShowCode(cguid);
				Response.Redirect(Helper.MakeBaseLink() + "Sites/CodeSite.aspx");

			}



//			if(e.CommandName == "fischen")
//			{
//				// Code Kommentar updaten
//				if(this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
//				{
//					// Kommentar TextBox holen
//					TextBox tb = (TextBox) e.Item.FindControl("KommentarTextBox");
//
//					// Kommentar updaten
//					if(PostIt.Code.CodeRow.IsKommentarNull() ||
//						tb.Text != PostIt.Code.CodeRow.Kommentar)
//					{
//						PostIt.Code.CodeRow.Kommentar = tb.Text;
//						PostIt.Code.UpdateCode();
//					}
//				}
//
//				if(this.OliUser.Stamm != null)
//				{
//					if(PostIt != null)
//					{
//						if(PostIt.Code != null)
//						{
//							Fischer f = new Fischer();
//							f.fischen(PostIt.Code.CodeRow.CodeGuid, Guid.Empty);
//
//							// neue Anzeigen
//							//						Guid cguid = PostIt.Code.CodeRow.CodeGuid ;
//							Guid pguid = PostIt.PostItRow.PostItGuid ;
//							this.OliUser.Stamm.MyPostIt = null;
//							this.OliUser.Stamm.ShowPostIt(pguid);
//
//							//							// Empfänger anzeigen
//							//							SichtbaresGrid = Stamm.SichtbaresGrid.PostItAngler;
//
//							// user.Nachricht
//							this.OliUser.Nachricht = "frisch gefischt: " + PostIt.MyEmpfaenger.Count + " Angler";
//							this.OliUser.Stamm.ShortCuts = null;
//							Response.Redirect(Helper.MakeBaseLink() + "Sites/PostItAnglerSite.aspx");
//						}
//					}
//				}
//
//				CodeDataGrid.EditItemIndex = -1;
//
//			}


		}
	}
}
