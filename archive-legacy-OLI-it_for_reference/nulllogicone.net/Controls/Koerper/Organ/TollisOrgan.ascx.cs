namespace OliWeb.Controls.Koerper.Organ
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliMiddleTier.OLIs ;
	using OliEngine.DataSetTypes;
	using OliWeb.Klassen;

	/// <summary>
	///		TollisOrgan.
	/// </summary>
	public abstract class TollisOrgan : MasterControl
	{
		protected System.Web.UI.WebControls.Button AbgebenButton;

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
			this.AbgebenButton.Click += new System.EventHandler(this.AbgebenButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.TextBox TollTextBox;

		// Member
		// ------

//		OliUser user;
//		protected System.Web.UI.WebControls.Panel TollPanel;
		protected System.Web.UI.WebControls.TextBox TollTextTextBox;
		protected System.Web.UI.WebControls.Image Image1;
		protected Controls.Gimicks.TollChart TollChart1;

		// Ereignisse
		// ----------

		// Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				if(this.OliUser.Stamm.Tollis != null)
				{
					TollTextBox.Text = this.OliUser.Stamm.Tollis.TollisRow.Toll.ToString();
					TollTextTextBox.Text = HttpUtility.HtmlDecode(this.OliUser.Stamm.Tollis.TollisRow.TollText);
					TollChart1.TollWert = this.OliUser.Stamm.Tollis.TollisRow.Toll;
				}
			}
		}


		private void AbgebenButton_Click(object sender, System.EventArgs e)
		{
			short toll = short.Parse(TollTextBox.Text);

			if(toll>= 0 && toll <= 100)
			{
				if(Stamm.Tollis == null)
				{
					this.OliUser.Stamm.ShowTollis(this.OliUser.Stamm.StammRow, TopLab.TopLabRow);
				}
				this.OliUser.Stamm.Tollis.TollisRow.Toll = toll;
				this.OliUser.Stamm.Tollis.TollisRow.TollText = HttpUtility.HtmlEncode(TollTextTextBox.Text);
				this.OliUser.Stamm.Tollis.UpdateTollis();

				this.OliUser.Stamm.InboxGelesen(TopLab.TopLabRow);
				this.OliUser.Stamm.MyInbox = null;

				this.OliUser.Stamm.Tollis = null;
//				SichtbaresGrid = Stamm.SichtbaresGrid.TopLabTollis;

				// PostIt.MyTopLab aktualisieren
				if(PostIt != null)
				{
					PostIt.MyTopLab = null;
				}
				Response.Redirect(Helper.MakeBaseLink() + "Sites/TopLabSite.aspx");
			}

		}

	}
}

