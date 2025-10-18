namespace OliWeb.Controls.Koerper
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

//	using OliEngine.OliDataAccess.Functions;
//	using OliEngine.OliMiddleTier.OLIs;
//	using OliEngine.OliMiddleTier.Markierer;
//	using OliEngine.OliMiddleTier.ZellHaufen;

	using OliWeb.Klassen;

	/// <summary>
	///	das Filterprofil eines Stammes.
	/// </summary>
	public abstract class AnglerKoerper : MasterControl
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
			this.RdfImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.RdfImageButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.Label QLabel;
		protected System.Web.UI.WebControls.HyperLink ExitHyperLink;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.ImageButton RdfImageButton;
		protected System.Web.UI.WebControls.HyperLink RdfHyperLink;
		protected OliWeb.Controls.Koerper.Organ.AnglerOrgan AnglerOrgan1;
		

		// Ereignisse
		// ----------

		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			QLabel.Text = this.OliUser.Stamm.Q.A;

			// rdf Link einstellen
			string rdf = "http://nulllogicone.net/Angler/" + Angler.AnglerRow.AnglerGuid.ToString() + ".rdf";
			RdfHyperLink.Text = rdf;
			RdfHyperLink.NavigateUrl = rdf;

			if(Stamm.BinIchEingeloggt)
			{
				EditHyperLink.Visible = true;
			}
		}

		private void RdfImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Expires = 0;
			Response.Buffer = true;
			Response.Clear();
			Response.ContentType = "application/rdf+xml";
			Response.AddHeader("content-disposition", "attachment; filename=\"Angler.rdf\"");
			Response.Write(this.Angler.MakeAnglerRdf());
			Response.End();

		}

	}
}
