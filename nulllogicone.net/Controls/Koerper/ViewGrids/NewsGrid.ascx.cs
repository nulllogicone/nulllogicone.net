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
	///		NewsGrid.
	/// </summary>
	public abstract class NewsGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.DataGrid NewsDataGrid;
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
			this.NewsDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.NewsDataGrid_ItemCommand);
			this.NewsDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.NewsDataGrid_PageIndexChanged);
			this.NewsDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.NewsDataGrid_SortCommand);
			this.NewsDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.NewsDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Member
		// ------

		string letzterAngler;

		// Eigenschaften
		// -------------

		// mySource
		private StammNewsDataSet.StammNewsDataTable mySource
		{
			get
			{
				if(this.OliUser.Stamm != null)
				{
					return (this.OliUser.Stamm.MyNews);
				}
				else
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
				return(NewsDataGrid.CurrentPageIndex);
			}
			set
			{
				NewsDataGrid.CurrentPageIndex = value;
			}
		}

		// Ereignisse
		// ----------


		// OnPreRender
		protected override void OnPreRender (EventArgs e)
		{
			if(this.OliUser.Stamm != null)
			{
				
				// Title und Spaltenüberschriften auf Q
				TitleLabel.Text = "neue " + this.OliUser.Stamm.Q.P + " (" + Stamm.MyNews.Rows.Count + ")";
				NewsDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.A;
				NewsDataGrid.Columns[2].HeaderText = this.OliUser.Stamm.Q.P;
				NewsDataGrid.PageSize = this.ZeilenZahl;

				DataView dv = new DataView (mySource);

				if(this.sortString.Length == 0)
				{
					this.sortString = "Kook ";
					this.desc = true;
				}

				if(desc)
				{
					dv.Sort = sortString + " DESC";
				}
				else
				{
					dv.Sort = sortString;
				}
				NewsDataGrid.DataSource = dv ;
				NewsDataGrid.DataBind();

				// keine Daten vorhanden
				if (dv.Count == 0)
				{
					Label l = new Label();
					l.Text = "<div style='font-size:8pt; text-align:center'>keine neuen Nachrichten an ihre Angler vorhanden</div><hr>";
					this.Controls.Add(l);
				}

				// gelesen Button
				NewsDataGrid.Columns[6].Visible = this.OliUser.Stamm.BinIchEingeloggt;
			}
		}

		// NewsDataGrid_SortCommand()
		private void NewsDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression) 
			{
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// NewsDataGrid_ItemCommand
		private void NewsDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemIndex >= 0)
			{
				if(this.OliUser.Stamm != null)
				{
					if(e.CommandName == "gelesen") 
					{
						this.OliUser.Stamm.NewsGelesen(new Guid(e.CommandArgument.ToString()));
						this.OliUser.Stamm.MyNews = null;
					}
					else if(e.CommandName == "AlleNewsGelesen")
					{
						Label aidl = (Label)e.Item.FindControl("AnglerGuidLabel");
						Guid aguid = new Guid(aidl.Text);
						this.OliUser.Stamm.ShowAngler(aguid);
						Angler.AlleNewsGelesen();
						Angler = null;

						// Stamm neu zeigen
						Guid sguid = this.OliUser.Stamm.StammRow.StammGuid ;
						this.OliUser.ShowStamm(sguid);
						NewsDataGrid.CurrentPageIndex = 0;
					}
					else if(e.CommandName == "AnglerLinkButton")
					{
						Label aguidl = (Label)e.Item.FindControl("AnglerGuidLabel");
						Guid aguid = new Guid(aguidl.Text);
						this.OliUser.Stamm.ShowAngler(aguid);
//						SichtbaresGrid = Stamm.SichtbaresGrid.AnglerPostIt ;
//						Helper.RedirectToSite();
						Response.Redirect(Helper.MakeBaseLink() + "Sites/AnglerSite.aspx");
					}
					else
					{
						// News gesehen
						string nguid = ((Label)e.Item.FindControl("NewsGuidLabel")).Text;
						this.OliUser.Stamm.NewsGesehen(new Guid(nguid));

						// PostItGuid holen und anzeigen
						Guid pguid = new Guid(NewsDataGrid.DataKeys[e.Item.ItemIndex].ToString());
						this.OliUser.Stamm.MyNews = null;
						this.OliUser.Stamm.ShowPostIt(pguid);
//						this.OliUser.Stamm.sichtbaresGrid = Stamm.SichtbaresGrid.None ;

						if(e.CommandName == "zeigT")
						{
//							SichtbaresGrid = Stamm.SichtbaresGrid.PostItTopLab;
						}
						Helper.RedirectToSite();
						Response.Redirect(Helper.MakeBaseLink() + "Sites/PostItSite.aspx");
					}
				}
			}
		}

		// NewsDataGrid_ItemDataBound()
		private void NewsDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
				// Mehrfachvorkommen von Angler entfernen
				LinkButton alb = (LinkButton)e.Item.FindControl("AnglerLinkButton");
				if(letzterAngler == alb.Text )
				{
					alb.Text = "";
					e.Item.FindControl("AlleNewsGelesenButton").Visible = false;
				}
				else
				{
					letzterAngler = alb.Text;
					if(this.OliUser.Stamm.BinIchEingeloggt && this.sortString == "Angler")
					e.Item.FindControl("AlleNewsGelesenButton").Visible = true ;
				}

				DataRowView dr = (DataRowView)e.Item.DataItem;

				// Gesehene zart andere fett
				if(dr["gesehen"].ToString().Length == 0)
				{
					e.Item.CssClass = "ungelesen";
				}
				else
				{
					e.Item.CssClass = "gelesen";
				}

				// Closed Nachrichten => hellgrau
				if(dr["closed"].ToString() != "False")
				{
					e.Item.BackColor = Color.WhiteSmoke;
				}
			}
		}

		private void NewsDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			NewsDataGrid.CurrentPageIndex = e.NewPageIndex;
		}


//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
	}
}
