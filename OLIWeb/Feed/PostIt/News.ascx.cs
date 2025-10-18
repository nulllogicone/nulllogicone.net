namespace OliWeb.Feed.PostIt
{
	using System;
	using System.IO;
	using System.Xml;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		News.
	/// </summary>
	public abstract class News : PostItFeed
	{
		protected System.Web.UI.WebControls.Xml Xml1;
		protected System.Web.UI.WebControls.DataList PostItDataList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			PostItDataList.DataSource = this._table;
			DataBind();
//			MemoryStream ms = new MemoryStream();
//			
//			this._table.DataSet.WriteXml(ms);
//			XmlDocument xd = new XmlDocument;
//			
//			Xml1
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
		
		///		Erforderliche Methode für die Designerunterstützung.
		///		Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
