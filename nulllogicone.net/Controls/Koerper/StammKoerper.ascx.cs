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
	///	aktueller Stamm in der Mittelschicht. An ihm hängen PostIt, Angler und TopLab.
	///	Er wird durch das <see cref="Organ.StammOrgan"/> Control mit Bild, Name, Beschreibung, Erstelldatum und seinem Vermögen (KooK)
	///	angezeigt. Oben rechts mit x - kann man ihn schließen.
	/// </summary>
	public abstract class StammKoerper : MasterControl
	{
		/// <summary>
		/// die Beschriftung für die Buttons wird aus einer individuell einstellbaren
		/// Tabelle genommen. Zuerst wird der Name des Sprachmusters genannt
		/// </summary>
		protected System.Web.UI.WebControls.Label QLabel;
		/// <summary>
		/// Der Name für die Stamm-Entität (Urheber von Fragen, Verkäufer von Angeboten)
		/// </summary>
		protected System.Web.UI.WebControls.Label QQLabel;
		protected System.Web.UI.WebControls.HyperLink ExitHyperLink;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;

		/// <summary>
		/// stellt die Datenfelder eines Stammes dar (Name, Bild, KooK, ...)
		/// </summary>
		protected Controls.Koerper.Organ.StammOrgan StammOrgan1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// meine Q-Row zum beschriften holen
			if(Stamm != null)
			{
				OliEngine.DataSetTypes.QDataSet.QRow Q = this.OliUser.Stamm.Q;
				
				QLabel.Text = "angezeigter " + Q.S;
				QQLabel.Text = "(" + Q.Q + ")";

				if(Stamm.BinIchEingeloggt)
				{
					EditHyperLink.Visible = true;
				}
			}
		}

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

	}
}
