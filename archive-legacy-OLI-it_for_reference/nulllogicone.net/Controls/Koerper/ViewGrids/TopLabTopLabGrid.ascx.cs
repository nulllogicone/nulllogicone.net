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
	using Controls.Koerper.ViewGrids;

	/// <summary>
	///		TopLabTopLabGrid.
	/// </summary>
	public abstract class TopLabTopLabGrid : ViewGridControl
	{

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
			this.TopLabDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.TopLabDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.DataGrid TopLabDataGrid;

		// Member
		TopLab parentTopLab;

		// Eigenschaften
		// -------------

		// Eine der folgenden drei Eigenschaften muss gesetzt sein !!!!!

		// ParentTopLabGuidString
		public string ParentTopLabGuidString
		{
			set
			{
				parentTopLab = new TopLab(new Guid(value));
			}
		}
		
		// ParentTopLabGuid
		public Guid ParentTopLabGuid
		{
			set
			{
				parentTopLab = new TopLab(value);
			}
			
		}

		// ParentTopLab
		public TopLab ParentTopLab
		{
			set
			{
				parentTopLab = value;
			}
		}

		// mySource
		private TopLabTopLabDataSet.TopLabTopLabDataTable mySource
		{
			get
			{
				return parentTopLab.MyTopLab ;
			}
		}


		// Ereignisse
		// ----------

		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			// Title und Spalten�berschriften auf Q
			TopLabDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.S;
			TopLabDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.T;

			DataView dv = new DataView(mySource);
			if(desc)
			{
				dv.Sort = sortString + " DESC";
			}
			else
			{
				dv.Sort = sortString;
			}
			TopLabDataGrid.DataSource = dv;
			TopLabDataGrid.DataBind();

			// keine Daten vorhanden
			if (dv.Count == 0)
			{
// TopLab in TopLab bleibt ohne Daten einfach leer
//				Label l = new Label();
//				l.Text = "keine Antworten auf diese Nachricht vorhanden";
//				this.Controls.Add(l);
				TopLabDataGrid.Visible = false;
			}
		}

//		// TopLabDataGrid_ItemCommand()
//		private void TopLabDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
//		{
//			if(e.Item.ItemIndex >= 0)
//			{
//				Label sl = (Label)e.Item.FindControl("StammGuidLabel");
//				Guid sguid = new Guid(sl.Text);
//
//				Label tl = (Label)e.Item.FindControl("TopLabGuidLabel");
//				Guid tguid = new Guid(tl.Text);
//
//				if (e.CommandName == "TopLab")
//				{
//					this.OliUser.Stamm.ShowTopLab(tguid);
////					SichtbaresGrid = Stamm.SichtbaresGrid.None ;
//					Helper.RedirectToSite();
//				}
//				if (e.CommandName == "Stamm")
//				{
//					// PostIt merken
//					Guid pguid = Guid.Empty ;
//					if(PostIt != null)
//					{
//						pguid = PostIt.PostItRow.PostItGuid ;
//					}
//					this.OliUser.ShowStamm(sguid);
//
//					// wieder zeigen
//					this.OliUser.Stamm.ShowTopLab(tguid);
//					if(pguid != Guid.Empty )
//					{
//						this.OliUser.Stamm.ShowPostIt(pguid);
//					}
//					Helper.RedirectToSite();
//				}
//
//			}
//		}

		// ItemDataBound
		private void TopLabDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// TGuid dieser Zeile Holen
				Label tl = (Label)e.Item.FindControl("TopLabGuidLabel");
				Guid tguid = new Guid(tl.Text);

				// TopLab erstellen und sehen ob es Kinder hat
				TopLab t = new TopLab(tguid);
				if(t.MyTopLab.Count > 0)
				{
					// neues TopLabTopLabGrid erstellen und drunter h�ngen
					TopLabTopLabGrid ttg = (TopLabTopLabGrid)this.LoadControl( "~/Controls/Koerper/ViewGrids/TopLabTopLabGrid.ascx");
					ttg.ParentTopLab = t;
					e.Item.Cells[1].Controls.Add(ttg);
				}
			}
		}
	}
}

