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
	///		PostItTopLabGrid.
	/// </summary>
	public abstract class PostItTopLabGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.DataGrid PostItDataGrid;
		protected System.Web.UI.WebControls.Label TitleLabel;

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
			this.PostItDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PostItDataGrid_ItemCommand);
			this.PostItDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PostItDataGrid_PageIndexChanged);
			this.PostItDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.PostItDataGrid_SortCommand);
			this.PostItDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.PostItDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// Eigenschaften
		// -------------

		// mySource
		private PostItTopLabDataSet.PostItTopLabDataTable mySource
		{
			get
			{
				try
				{
					return(PostIt.MyTopLab);
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
			// Title und Spaltenüberschriften auf Q
			TitleLabel.Text = this.OliUser.Stamm.Q.P_T + " (" + this.OliUser.Stamm.PostIt.MyTopLab.Rows.Count.ToString() + ")";
			PostItDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.S;
			PostItDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.T;

			PostItDataGrid.PageSize = this.ZeilenZahl;

			if(sortString.Length == 0)
			{
				sortString = "DurchToll";
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
			PostItDataGrid.DataSource = dv;
			PostItDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>keine Antworten auf diese Nachricht vorhanden</div><hr>";
				this.Controls.Add(l);
			}
		}

		// PostItDataGrid_SortCommand()
		private void PostItDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression)
			{	
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// PostItDataGrid_ItemCommand()
		private void PostItDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemIndex >= 0)
			{
				Label sl = (Label)e.Item.FindControl("StammGuidLabel");
				Guid sguid = new Guid(sl.Text);

				Label tl = (Label)e.Item.FindControl("TopLabGuidLabel");
				Guid tguid = new Guid(tl.Text);

//				if (e.CommandName == "TopLab")
//				{
//					this.OliUser.Stamm.ShowTopLab(tguid);
//					SichtbaresGrid = Stamm.SichtbaresGrid.None ;
//					Helper.RedirectToSite();
//				}
				if (e.CommandName == "Stamm")
				{
					// PostIt merken
					Guid pguid = Guid.Empty ;
					if(PostIt != null)
					{
						pguid = PostIt.PostItRow.PostItGuid ;
					}
					this.OliUser.ShowStamm(sguid);

					// wieder zeigen
					this.OliUser.Stamm.ShowTopLab(tguid);
					if(pguid != Guid.Empty )
					{
						this.OliUser.Stamm.ShowPostIt(pguid);
					}
					Helper.RedirectToSite();

				}

			}
		}

		private void PostItDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
		}

		private void PostItDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
