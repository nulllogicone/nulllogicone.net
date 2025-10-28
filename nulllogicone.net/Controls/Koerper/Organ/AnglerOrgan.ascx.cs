namespace OliWeb.Controls.Koerper.Organ
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.DataSetTypes;
	using OliEngine.OliMiddleTier.OLIs;
	using OliWeb.Klassen;

	/// <summary>
	///		AnglerOrgan.
	/// </summary>
	public abstract class AnglerOrgan : MasterControl
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label AnglerLabel;
		protected System.Web.UI.WebControls.Label BeschreibungLabel;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Member
		// ------



		private void Page_Load(object sender, System.EventArgs e)
		{
//			user = SessionManager.Instance().OliUser;
		}

		protected override void OnPreRender(EventArgs e) 
		{
			Malen();
		}

		// Eigenschaften
		// -------------




		// Methoden
		// --------
		
		// Malen
		protected void Malen()
		{
			if(this.OliUser.Stamm != null)
			{
				if (Angler != null)
				{

					AnglerDataSet.AnglerRow a = Angler.AnglerRow;


					// beschriften
					AnglerLabel.Text = a.Angler;
					AnglerLabel.ToolTip = "AnglerGuid: " + a.AnglerGuid.ToString();
					BeschreibungLabel.Text = a.IsBeschreibungNull() ? "" : a.Beschreibung;

				}
			}
		}


		// Ereignisse
		// ----------



	}
}

