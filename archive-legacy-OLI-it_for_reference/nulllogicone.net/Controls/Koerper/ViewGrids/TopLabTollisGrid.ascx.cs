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
	///		TopLabTollisGrid.
	/// </summary>
	public abstract class TopLabTollisGrid : System.Web.UI.UserControl
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
			this.TollisRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.TollisRepeater_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.Repeater TollisRepeater;

		// Member
		// ------

		OliUser user;

		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			user = SessionManager.Instance().OliUser;

			// alles aus
			TollisRepeater.Visible = false;
		}

		// OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			if(user.Stamm != null)
			{
				if(user.Stamm.TopLab != null)
				{
//					if(user.Stamm.sichtbaresGrid == Stamm.SichtbaresGrid.TopLabTollis ||
//						user.Stamm.sichtbaresGrid == Stamm.SichtbaresGrid.None)
//					{
						TollisRepeater.DataSource = user.Stamm.TopLab.MyTollis;
						TollisRepeater.DataBind();
						TollisRepeater.Visible = true;

						if(user.Stamm.TopLab.MyTollis.Rows.Count == 0)
						{
							Label l = new Label();
							l.Text = "<div style='font-size:8pt; text-align:center'>Diese Antwort hat noch keine Bewertungen</div><hr>";
							this.Controls.Add(l);
						}
//					}
				}
			}
		}

		// Item_Command
		private void TollisRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			// ids merken zum wieder herstellen
			Guid pguid = Guid.Empty ;
			Guid tguid = Guid.Empty ;
			if (user.Stamm.PostIt != null)
			{
				pguid = user.Stamm.PostIt.PostItRow.PostItGuid ;
			}
			if (user.Stamm.TopLab != null)
			{
				tguid = user.Stamm.TopLab.TopLabRow.TopLabGuid ;
			}

			// Stamm zeigen
			Label l = (Label) e.Item.FindControl("StammGuidLabel");
			Guid sguid = new Guid(l.Text);
			user.ShowStamm(sguid);

			// wenn vorher was offen war : wieder herstellen
			if (pguid != Guid.Empty )
			{
				user.Stamm.ShowPostIt(pguid);
			}
			if (tguid != Guid.Empty)
			{
				user.Stamm.ShowTopLab(tguid);
			}
			Helper.RedirectToSite();
		}

	}
}

