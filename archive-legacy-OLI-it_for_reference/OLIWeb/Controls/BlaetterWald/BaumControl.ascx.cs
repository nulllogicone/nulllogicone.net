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
	///		BaumControl.
	/// </summary>
	public abstract class BaumControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Repeater ZweigeRepeater;
		protected System.Web.UI.WebControls.Label BeschreibungLabel;
		protected System.Web.UI.WebControls.Label BaumKetteLabel;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.Label BaumLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			BaumKette bk = BaumKette.Instance();
			BaumKetteLabel.Text = bk.MakeHtml();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Erforderliche Methode f�r die Designerunterst�tzung.
		///		Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public Guid NetzGuid
		{
			get
			{
				if(Request["nguid"] != null)
				{
					Guid nguid = new Guid( Request["nguid"]);
					return nguid;
				}
				else
				{
					return Guid.Empty;
				}
			}
		}

		public Guid BaumGuid
		{
			set
			{
				Baum b = new Baum(value);
				BaumLabel.Text = b.BaumRow.Baum;
				BeschreibungLabel.Text = b.BaumRow.IsBeschreibungNull() ? "" : b.BaumRow.Beschreibung;

				Zweig z = new Zweig(b.BaumRow);
				ZweigeRepeater.DataSource = z.Zweig;
				DataBind();

				EditHyperLink.Visible = true;
			}
		}
	}
}

