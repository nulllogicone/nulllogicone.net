namespace OliWeb.Controls.Koerper
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliMiddleTier.OLIs;
	using OliWeb.Klassen;

	/// <summary>
	///	wird als oberstes Element auf den Seiten eingebunden. Es enthält
	///	das <see cref="Floor.LogoControl"/>, daneben Platz für <b>Nachrichten</b> und das <see cref="Floor.EinAusLoggen"/>-Control.
	///	Ausserdem enthält es die <see cref="CommandBar"/> als vertikale Menüleiste.
	/// </summary>
	public abstract class Kopf : MasterControl
	{
		protected System.Web.UI.WebControls.Label NachrichtLabel;

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


		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// evtl. vorhandene Nachrichten aus der Mittelschicht werden angezeigt 
		/// und wenn niemand eingeloggt ist, wird der Fokus in die 
		/// <see cref="Floor.EinAusLoggen.StammTextBox"/> gesetzt (<b>javascript</b>).
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) 
		{
			// NachrichtLabel
			if(this.OliUser.Nachricht.Length > 0)
			{
				NachrichtLabel.Text = this.OliUser.Nachricht;
				this.OliUser.Nachricht = null;
			}

			// Niemand eingeloggt
			if(this.OliUser.Stamm == null && this.OliUser.EingeloggterStamm == null)
			{
				// Beim ersten Aufruf : Focus auf Name zum einloggen
				if(!IsPostBack)
				{
					FocusAufControl(this.Page,"Kopf1_EinAusLoggen1_StammTextBox");
				}
			}
		}
	}
}
