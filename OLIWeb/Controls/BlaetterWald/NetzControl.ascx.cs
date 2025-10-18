namespace OliWeb.Controls.BlaetterWald
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliMiddleTier.OLIx;

	/// <summary>
	///		NetzControl.
	/// </summary>
	public abstract class NetzControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label BeschreibungLabel;
		protected System.Web.UI.WebControls.Label NetzKetteLabel;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.Label NetzLabel;
		protected System.Web.UI.WebControls.DataGrid KnotenDataGrid;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Erforderliche Methode für die Designerunterstützung.
		///		Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			NetzKette nk = NetzKette.Instance();
			NetzKetteLabel.Text = nk.MakeHtml();
		}

		// Beim setzen der Eigenschaft NetzGuid
		// werden die Anzeigeelemente gefüllt
		public Guid NetzGuid
		{
			set
			{
				Netz n = new Netz(value);

				EditHyperLink.NavigateUrl = "NetzEdit.aspx?nguid=" + value.ToString();
				
				NetzLabel.Text = n.NetzRow.Netz;
				BeschreibungLabel.Text = n.NetzRow.IsBeschreibungNull() ? "" : n.NetzRow.Beschreibung;

				Knoten k = new Knoten(n.NetzRow);
				KnotenDataGrid.DataSource = k.Knoten;
				DataBind();
			}
		}


	}
}
