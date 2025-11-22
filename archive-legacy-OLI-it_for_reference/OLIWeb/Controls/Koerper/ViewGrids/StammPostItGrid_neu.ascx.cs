namespace OliWeb.Controls.Koerper.ViewGrids
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.DataSetTypes;
	using OliEngine.DataSetTypes.Views;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;

	/// <summary>
	///		WurzelnMitPostItGridControl.
	/// </summary>
	public abstract class StammPostItGrid : ViewGridControl
	{
		const int PColumn = 0;
		protected System.Web.UI.WebControls.DataGrid PostItDataGrid;
		protected System.Web.UI.WebControls.LinkButton CloseLinkButton;
		protected System.Web.UI.WebControls.Label TitleLabel;

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
			this.PostItDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PostItDataGrid_PageIndexChanged);
			this.PostItDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.PostItDataGrid_ItemDataBound);

		}
		#endregion


//		// Eigenschaften
//		// -------------
//
		// MySource
		private StammPostItDataSet.StammPostItDataTable mySource
		{
			get
			{
				if(this.OliUser.Stamm != null)
				{
					return (this.OliUser.Stamm.MyPostIt);
				}
				return(null);
			}
		}



		// Ereignisse
		// ----------


		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			PostItDataGrid.PageSize = this.ZeilenZahl;

			// Title und Spalten�berschriften auf Q
			if(this.OliUser.Stamm != null)
			{
				TitleLabel.Text = this.OliUser.Stamm.Q.S_P ;
//				PostItDataGrid.Columns[PColumn].HeaderText = this.OliUser.Stamm.Q.P;
				PostItDataGrid.Columns[PColumn].HeaderText = " ";
			}

			if(this.sortString.Length == 0)
			{
				this.sortString = "Frist";
				this.desc = true;
			}

			if(this.mySource != null)
			{
//TODO: so wie hier: Anzahl in Titel
				TitleLabel.Text = this.OliUser.Stamm.Q.S_P + " (" + this.OliUser.Stamm.MyPostIt.Rows.Count.ToString() + ")";
				DataView dv = new DataView(this.mySource);
				if (sortString.Length > 0)
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


				PostItDataGrid.DataSource = dv ;
				PostItDataGrid.DataBind();


				// keine Daten vorhanden
				if (dv.Count == 0)
				{
					Label l = new Label();
					l.Text = "<div style='font-size:8pt; text-align:center'>Dieser Stamm hat keine Nachrichten oder zeigt geschlossene Nachrichten nicht an</div><hr>";
					this.Controls.Add(l);
				}
			}
		}

		// SortCommand
		private void PostItDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression)
			{	
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// ItemCommand
		private void PostItDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			if(e.Item.ItemIndex >= 0)
//			{
//				PostItDataGrid.DataKeyField = "PostItGuid";
//				Guid pguid = new Guid(PostItDataGrid.DataKeys[e.Item.ItemIndex].ToString());
//				this.OliUser.Stamm.ShowPostIt(pguid);
//				SichtbaresGrid = Stamm.SichtbaresGrid.None ;

//				if (e.CommandName == "Urh")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItStamm;
//					Helper.RedirectToSite();
//				}
//				if (e.CommandName == "Empf")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItAngler;
//					Helper.RedirectToSite();
//				}
//				if (e.CommandName == "TopLab")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItTopLab;
//				}

//				Helper.RedirectToSite();
//			}
		}

		// PostItDataGrid_ItemDataBound()
		private void PostItDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Im Kopf die sortier-Pfeile zeigen
			if(e.Item.ItemType == ListItemType.Header)
			{
				try
				{
					SortierPfeil((DataGrid)sender, e.Item);
				}
				catch
				{}
			}

			// Zeilenhintergrundfarbe anpassen
			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;

				// wenn ich Urheber (StammZust=1) bin => Hintergrund okker
				if(dr["StammZust"].ToString() == "1")
				{
					e.Item.Cells[0].BackColor = Color.AntiqueWhite;
				}

				// wenn closed => dann Zeile grau
				if(dr["closed"].ToString() != "False")
				{
					e.Item.BackColor = Color.WhiteSmoke;
				}
			}
		}

		// PageIndexChanged
		private void PostItDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
		}



	}
}

