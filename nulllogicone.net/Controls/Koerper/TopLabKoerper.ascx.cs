namespace OliWeb.Controls.Koerper
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliWeb.Klassen;
	using OliEngine.OliMiddleTier.OLIs;

	/// <summary>
	///		TopLabKoerper.
	/// </summary>
	public abstract class TopLabKoerper : MasterControl
	{
		protected System.Web.UI.WebControls.LinkButton TopButton;
		protected System.Web.UI.WebControls.HyperLink StammHyperLink;
		protected System.Web.UI.WebControls.HyperLink ExitHyperLink;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.Label QLabel;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		protected Controls.Koerper.Organ.TopLabOrgan TopLabOrgan1;
		protected OliWeb.Controls.Koerper.ViewGrids.TopLabTollisGrid TopLabTollisGrid1;

		// Ereignisse
		// ----------

		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				StammHyperLink.Text = TopLab.MyStamm.StammRow.Stamm;
				StammHyperLink.NavigateUrl = Request.ServerVariables["SCRIPT_NAME"] + "?sguid=" + TopLab.MyStamm.StammRow.StammGuid.ToString();
			}
			catch{}
		}

		// OnPreRender()
		protected override void OnPreRender(EventArgs e) 
		{
			// Alles unsichtbar
			TopLabTollisGrid1.Visible = true;

			if (this.OliUser.Stamm != null)
			{
				if(TopLab != null)
				{
					TopLabOrgan1.Visible = true;
					QLabel.Text = this.OliUser.Stamm.Q.T;

					if(TopLab.BinIchMeinTopLab &&
						Stamm.BinIchEingeloggt)
					{
						EditHyperLink.Visible = true;
					}
					else
					{
						EditHyperLink.Visible = false;
					}
					

				}
			}
		}



	}
}
