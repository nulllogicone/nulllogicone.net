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
	///		StammTopLabGrid.
	/// </summary>
	public abstract class StammTopLabGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.DataGrid TopLabDataGrid;
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
			this.TopLabDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.TopLabDataGrid_ItemCommand);
			this.TopLabDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.TopLabDataGrid_PageIndexChanged);
			this.TopLabDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.TopLabDataGrid_SortCommand);
			this.TopLabDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.TopLabDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// Eigenschaften
		// -------------

		// mySource
		private StammTopLabDataSet.StammTopLabDataTable mySource
		{
			get
			{
				if(this.OliUser.Stamm != null)
				{
					return(this.OliUser.Stamm.MyTopLab);
				}
				return(null);
			}
		}

		// CurrentPageIndex
		public int CurrentPageIndex
		{
			get
			{
				return(TopLabDataGrid.CurrentPageIndex);
			}
			set
			{
				TopLabDataGrid.CurrentPageIndex = value;
			}
		}

		// OnPreRender()
		protected override void OnPreRender(EventArgs e)
		{
			// Title und Spalten�berschriften auf Q
			TitleLabel.Text = this.OliUser.Stamm.Q.S_T + " (" + this.OliUser.Stamm.MyTopLab.Rows.Count.ToString() + ")";
			TopLabDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.P;
			TopLabDataGrid.Columns[2].HeaderText = this.OliUser.Stamm.Q.T;

			TopLabDataGrid.PageSize = this.ZeilenZahl;

			if(sortString.Length == 0)
			{
				sortString = "TDatum";
				desc = true;
			}

			DataView dv = new DataView(mySource);
			if(desc)
			{
				dv.Sort = sortString + " DESC";
			}
			else
			{
				dv.Sort = sortString;
			}
			TopLabDataGrid.DataSource = dv ;
			TopLabDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>Diesesr Stamm hat noch keine Antworten verfasst</div><hr>";
				this.Controls.Add(l);
			}
		}

		// TopLabDataGrid_SortCommand
		private void TopLabDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression)
			{	
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// TopLabDataGrid_ItemCommand()
		private void TopLabDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			
			if(e.Item.ItemIndex >= 0)
			{
				if(e.CommandName == "TopLab")
				{
					this.OliUser.Stamm.ShowTopLab(new Guid(e.CommandArgument.ToString()));
					this.OliUser.Stamm.ShowPostIt(TopLab.TopLabRow.PostItGuid);
//					SichtbaresGrid = Stamm.SichtbaresGrid.None ;

					Helper.RedirectToSite();
				}
				if(e.CommandName == "PostIt")
				{
					this.OliUser.Stamm.ShowPostIt(new Guid(e.CommandArgument.ToString()));
//					this.OliUser.Stamm.sichtbaresGrid = Stamm.SichtbaresGrid.None;

					Helper.RedirectToSite();
				}
			}
		}

		// TopLabDataGrid_PageIndexChanged()
		private void TopLabDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			TopLabDataGrid.CurrentPageIndex = e.NewPageIndex;
		}

		// ItemDataGrid_ItemDataBound()
		private void TopLabDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Im Kopf die sortier-Pfeile zeigen
			if(e.Item.ItemType == ListItemType.Header)
			{
				SortierPfeil((DataGrid)sender, e.Item);
			}
		}



//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();
//		}
	}
}

