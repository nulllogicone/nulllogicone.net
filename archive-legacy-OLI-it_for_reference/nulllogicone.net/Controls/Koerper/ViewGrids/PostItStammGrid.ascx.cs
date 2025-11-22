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
	///		PostItStammGrid.
	/// </summary>
	public abstract class PostItStammGrid : ViewGridControl
	{
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
		private PostItStammDataSet.PostItStammDataTable mySource
		{
			get
			{
				try
				{
					return(PostIt.MyStamm);
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
			if(this.OliUser.Stamm != null)
			{
				// Title und Spalten�berschriften auf Q
				TitleLabel.Text = this.OliUser.Stamm.Q.P_S;
				PostItDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.S;

				PostItDataGrid.PageSize = this.ZeilenZahl;

				if(sortString.Length == 0)
				{
					sortString = "Frist";
					desc = true;
				}

				if(PostIt != null)
				{
//TODO: so wie hier : in allen ViewGrids Titeln noch dieses Anzahl in Klammern anh�ngen
					TitleLabel.Text = this.OliUser.Stamm.Q.P_S + " (" + PostIt.MyStamm.Rows.Count.ToString() + ")";
					DataView dv = new DataView(mySource);
					if(desc)
					{
						dv.Sort = sortString + " DESC";
					}
					else
					{
						dv.Sort = sortString;
					}
					PostItDataGrid.DataSource = dv ;
					PostItDataGrid.DataBind();

					// keine Daten vorhanden
					if (dv.Count == 0)
					{
						Label l = new Label();
						l.Text = "<div style='font-size:8pt; text-align:center'>kein stamm f�r Nachricht vorhanden</div><hr>";
						this.Controls.Add(l);
						throw new Exception("PostItGuid " + PostIt.PostItRow.PostItGuid + " ohne Stamm");
					}
				}
			}
		}

		// Sort_Command
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
//				Guid pguid = PostIt.PostItRow.PostItGuid ;
//
//				PostItDataGrid.DataKeyField = "StammGuid";
//				Guid sguid = new Guid(PostItDataGrid.DataKeys[e.Item.ItemIndex].ToString());
//				this.OliUser.ShowStamm(sguid);
//				this.OliUser.Stamm.ShowPostIt(pguid);
//
//				Helper.RedirectToSite();
//			}
		}

		// ItemDataBound
		private void PostItDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Im Kopf die sortier-Pfeile zeigen
			if(e.Item.ItemType == ListItemType.Header)
			{
				SortierPfeil((DataGrid)sender, e.Item);
			}
			
			// Hintergrundfarben anpassen
			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// wenn closed abgelaufen => zahlt und Frist grau
				Label cl = (Label)e.Item.FindControl("ClosedLabel");
				bool c = bool.Parse(cl.Text);
				if(c)
				{
					e.Item.BackColor = Color.WhiteSmoke;
				}

				// wenn ich Urheber (StammZust=1) bin => Hintergrund okker
				Label szl = (Label)e.Item.FindControl("StammZustLabel");
				int sz = int.Parse(szl.Text);
				if(sz==1)
				{
					e.Item.Cells[0].BackColor = Color.AntiqueWhite ;
					e.Item.Cells[1].BackColor = Color.AntiqueWhite ;
				}
			}
		}

		// PageIndexChanged
		private void PostItDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
		}



//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
	}
}

