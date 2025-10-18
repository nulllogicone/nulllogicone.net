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
	public partial  class PostItAnglerGrid : ViewGridControl
	{

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

		}
		#endregion


		// Eigenschaften
		// -------------

		// mySource
		private PostItAnglerDataSet.PostItAnglerDataTable mySource
		{
			get
			{
				try
				{
					return(PostIt.MyEmpfaenger);
				}
				catch
				{
					return(null);
				}
			}
		}

		// CurrentPageIndex
		public int CurrentPageIndex
		{
			get
			{
				return(PostItDataGrid.CurrentPageIndex);
			}
			set
			{
				PostItDataGrid.CurrentPageIndex = value;
			}
		}

		// Ereignisse
		// ----------

		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			// Title und Spaltenüberschriften auf Q
			if(this.OliUser.Stamm != null)
			{
				TitleLabel.Text = this.OliUser.Stamm.Q.P_X + " (" + PostIt.MyEmpfaenger.Rows.Count.ToString() + ")";
//				PostItDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.A;
//				PostItDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.S;

				PostItDataGrid.PageSize = this.ZeilenZahl;
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
				l.Text = "<div style='font-size:8pt; text-align:center'>keine Empfänger für diese Nachricht vorhanden</div><hr>";
				this.Controls.Add(l);
			}
		}

		private void PostItDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression)
			{	
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		private void PostItDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			if(e.Item.ItemIndex >= 0)
//			{
//				Guid pguid = PostIt.PostItRow.PostItGuid ;
//				PostItDataGrid.DataKeyField = "StammGuid";
//				Guid sguid = new Guid(PostItDataGrid.DataKeys[e.Item.ItemIndex].ToString());
//				this.OliUser.ShowStamm(sguid);
//				this.OliUser.Stamm.ShowPostIt(pguid);
//
//				if(e.CommandName == "Angler") 
//				{
//					Guid aguid = new Guid(e.CommandArgument.ToString());
//					this.OliUser.Stamm.ShowAngler(aguid);
//				}	
//				Helper.RedirectToSite();
//			}
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
	}
}
