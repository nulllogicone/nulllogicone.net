namespace OliWeb.Feed.PostIt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Banner.
	/// </summary>
	public abstract class Banner : PostItFeed
	{
		protected System.Web.UI.WebControls.Repeater Repeater1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Repeater1.DataSource = this._table;
			DataBind();
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
		
		///		Erforderliche Methode f�r die Designerunterst�tzung.
		///		Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}

