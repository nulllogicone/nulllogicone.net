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
	///		InboxGrid.
	/// </summary>
	public abstract class InboxGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.Label TitleLabel;
		protected System.Web.UI.WebControls.DataGrid InboxDataGrid;
		
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
			this.InboxDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.InboxDataGrid_ItemCommand);
			this.InboxDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.InboxDataGrid_SortCommand);
			this.InboxDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.InboxDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		// Eigenschaften
		// -------------

		// mySource
		private StammInboxDataSet.StammInboxDataTable mySource
		{
			get
			{
				if (this.OliUser.Stamm != null)
				{
					return (this.OliUser.Stamm.MyInbox);
				}
				else return (null);
			}
		}

		// Ereignisse
		// ----------

		// OnPreRender
		protected override void OnPreRender (EventArgs e)
		{
			// Title und Spalten�berschriften auf Q
			TitleLabel.Text = "neue " + this.OliUser.Stamm.Q.T + " (" + Stamm.MyInbox.Rows.Count + ")";
			InboxDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.P;
			InboxDataGrid.Columns[2].HeaderText = this.OliUser.Stamm.Q.T;
			InboxDataGrid.PageSize = this.ZeilenZahl;
			
			DataView dv = new DataView (mySource);
			if(desc)
			{
				dv.Sort = sortString + " DESC";
			}
			else
			{
				dv.Sort = sortString;
			}
			InboxDataGrid.DataSource = dv ;
			InboxDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
				Label l = new Label();
				l.Text = "<div style='font-size:8pt; text-align:center'>keine neuen Antworten vorhanden</div><hr>";
				this.Controls.Add(l);
			}

			if(this.OliUser.Stamm != null)
			{
				InboxDataGrid.Columns[4].Visible = this.OliUser.Stamm.BinIchEingeloggt;
			}

		}


		// InboxDataGrid_SortCommand
		private void InboxDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortString == e.SortExpression) 
			{
				desc = !desc;
			}
			sortString = e.SortExpression;
		}

		// InboxDataGrid_ItemCommand()
		private void InboxDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemIndex >= 0)
			{
				if(e.CommandName == "gelesen") 
				{
					this.OliUser.Stamm.InboxGelesen(new Guid(e.CommandArgument.ToString()));
					this.OliUser.Stamm.MyInbox = null;
				}
				else
				{
					InboxDataGrid.DataKeyField = "TopLabGuid";
					Guid tguid = new Guid(InboxDataGrid.DataKeys[e.Item.ItemIndex].ToString());
					this.OliUser.Stamm.ShowTopLab(tguid); // mit PostIt
				
					// InboxGesehen
					this.OliUser.Stamm.InboxGesehen(TopLab.TopLabRow);
					this.OliUser.Stamm.MyInbox = null;
					this.OliUser.Stamm.ShowPostIt(TopLab.TopLabRow.PostItGuid);

//					SichtbaresGrid = Stamm.SichtbaresGrid.None;
//					Helper.RedirectToSite();
					Response.Redirect(Helper.MakeBaseLink() + "Sites/TopLabSite.aspx");
				}
			}
		}

		private void InboxDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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


//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();
//		}
	}
}

