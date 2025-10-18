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
	///		AnglerPostItGrid.
	/// </summary>
	public partial  class AnglerPostItGrid : ViewGridControl
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
			this.AnglerDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.AnglerDataGrid_ItemCommand);
			this.AnglerDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.AnglerDataGrid_PageIndexChanged);
			this.AnglerDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.AnglerDataGrid_SortCommand);
			this.AnglerDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.AnglerDataGrid_ItemDataBound);

		}
		#endregion


		// Eigenschaften
		// -------------

		// mySource
		private AnglerPostItDataSet.AnglerPostItDataTable mySource
		{
			get
			{
				if(this.OliUser.Stamm != null)
				{
					if(this.Angler != null)
					{
						return (this.Angler.MyPostIt);
					}
				}
				return(null);
			}
		}





		// Ereignisse
		// ----------

		// OnPreRender
		protected override void OnPreRender (EventArgs e)
		{
			// Title und Spaltenüberschriften auf Q
			TitleLabel.Text = this.OliUser.Stamm.Q.A_X + " (" + this.Angler.MyPostIt.Rows.Count.ToString() + ")";

			AnglerDataGrid.PageSize = this.ZeilenZahl;
			AnglerDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.P ;

			if(sortString.Length == 0)
			{
				sortString = "KooK";
				desc = true;
			}

			DataView dv = new DataView (mySource);
			if(desc)
			{
				dv.Sort = sortString + " DESC";
			}
			else
			{
				dv.Sort = sortString;
			}
			AnglerDataGrid.DataSource = dv ;
			AnglerDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>keine Nachrichten für diesen Angler vorhanden</div><hr>";
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

		// AnglerDataGrid_ItemCommand()
		private void AnglerDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
//			if(e.Item.ItemIndex >= 0)
//			{
//				AnglerDataGrid.DataKeyField = "PostItGuid";
//				Guid pguid = new Guid(AnglerDataGrid.DataKeys[e.Item.ItemIndex].ToString());
//
//				Label gl = (Label) e.Item.FindControl("CodeGuidLabel");
//				Guid cguid = new Guid(gl.Text);
//
//				// PostIt als gelesen markieren
//				// (eigentlich hat der angler hier eine CodeGuid und
//				// diese wird in der Tabelle Spiegel mit gelesen gestempelt
//				Angler.CodeGelesen(cguid);
//				Angler.MyPostIt = null;
//
//				this.OliUser.Stamm.ShowPostIt(pguid);
//				this.SichtbaresGrid = Stamm.SichtbaresGrid.None ;
//				if (e.CommandName == "Urh")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItStamm;
//				}
//				if (e.CommandName == "Angler")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItAngler;
//				}
//				if (e.CommandName == "TopLab")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItTopLab;
//				}
//
//				Helper.RedirectToSite();
//			}
		}

		private void AnglerDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			AnglerDataGrid.CurrentPageIndex = e.NewPageIndex;
		}

		private void AnglerDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Im Kopf die sortier-Pfeile zeigen
			if(e.Item.ItemType == ListItemType.Header)
			{
				SortierPfeil((DataGrid)sender, e.Item);
			}

			// Zeilen formatieren
			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;

				// Gelesen/Ungelesen darstellen
				if(dr["gelesen"].ToString().Length == 0)
				{
					e.Item.CssClass = "ungelesen";
				}
				else
				{
					e.Item.CssClass = "gelesen";
				}

				// closed => Zeile grau
				if(dr["closed"].ToString() != "False")
				{
					e.Item.BackColor = Color.WhiteSmoke;
				}
			}
		}


//
//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
	}
}
