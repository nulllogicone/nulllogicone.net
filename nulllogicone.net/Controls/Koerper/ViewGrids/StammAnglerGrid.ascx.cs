namespace OliWeb.Controls.Koerper.ViewGrids
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.DataSetTypes.Views;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;

	/// <summary>
	///		StammAnglerGrid.
	/// </summary>
	public abstract class StammAnglerGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.Label TitleLabel;
		protected System.Web.UI.WebControls.LinkButton CloseLinkButton;
		protected System.Web.UI.WebControls.DataGrid AnglerDataGrid;

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
			this.AnglerDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.AnglerDataGrid_ItemCommand);
			this.AnglerDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.AnglerDataGrid_SortCommand);
			this.AnglerDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.AnglerDataGrid_ItemDataBound);
			this.AnglerDataGrid.SelectedIndexChanged += new System.EventHandler(this.AnglerDataGrid_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		// MySource
		private StammAnglerDataSet.StammAnglerDataTable mySource
		{
			get
			{
				if(this.OliUser.Stamm != null)
				{
					return(this.OliUser.Stamm.MyAngler);
				}

				return(null);
			}
		}

		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			// Title und Spaltenüberschriften auf Q
			if(this.OliUser.Stamm != null)
			{
				TitleLabel.Text = this.OliUser.Stamm.Q.S_A + " (" + this.OliUser.Stamm.MyAngler.Rows.Count.ToString() + ")";
				AnglerDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.A;

				// für eingeloggten Stamm die Löschen - Spalte - Buttons zeigen
				AnglerDataGrid.Columns[3].Visible = this.OliUser.Stamm.BinIchEingeloggt;

			}

			DataView dv = new DataView(this.mySource );
			if( sortString.Length > 0)
			{
				if(desc)
				{
					dv.Sort = sortString + " DESC";
				}
				else
				{
					dv.Sort = sortString;
				}
			}
			AnglerDataGrid.DataSource = dv ;
			AnglerDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>Dieser Stamm hat noch keine Angler. <br>Sie können einen neuen erstellen</div><hr>";
				this.Controls.Add(l);
			}
		}

		// AnglerDataGrid_SortCommand()
		private void AnglerDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression)
			{	
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// AnglerDataGrid_ItemCommand
		private void AnglerDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemIndex >= 0)
			{
				Guid aguid = (Guid)AnglerDataGrid.DataKeys[e.Item.ItemIndex];
				if(e.CommandName == "Delete")
				{
					// Den Angler löschen!
					Angler a = new Angler(this.OliUser.Stamm, aguid);
					a.AnglerRow.Delete();
					a.UpdateAngler();

					// MyAngler aktualisieren
					Angler = null;
					this.OliUser.Stamm.MyAngler = null;
//					SichtbaresGrid = Stamm.SichtbaresGrid.StammAngler;

					return;
					// Redirect
//					Helper.RedirectToSite();
				}

				// wenn nicht gelöscht (und weitergeleitet) wurde
				// zuerst den Angler mit seinen Fischen zeigen
				this.OliUser.Stamm.ShowAngler(aguid);

			
//				if (e.CommandName == "Fische")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.AnglerPostIt;
//					Helper.RedirectToSite();
//				}

				Helper.RedirectToSite();

			}
		}

		private void AnglerDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void AnglerDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Im Kopf die sortier-Pfeile zeigen
			if(e.Item.ItemType == ListItemType.Header)
			{
				SortierPfeil((DataGrid)sender,e.Item );
			}
		}





//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
	}
}
