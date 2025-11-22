namespace OliWeb.Controls.Koerper
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;

	/// <summary>
	///	das Element f�r die aktuelle Nachricht. Sie wird durch das <see cref="Organ.PostItOrgan"/> 
	///	mit Bild und Frist und Wert dargestellt. Oben rechts kann man sie mit x schlie�en.
	/// </summary>
	public abstract class PostItKoerper : MasterControl
	{
		protected System.Web.UI.WebControls.Panel PostItPanel;
		protected System.Web.UI.WebControls.Panel MeinsPanel;
		protected System.Web.UI.WebControls.Label QLabel;
		protected System.Web.UI.WebControls.Label BezahltLabel;
		protected System.Web.UI.WebControls.Label FristLabel;
		protected System.Web.UI.WebControls.HyperLink ExitHyperLink;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.Label VonLabel;
		protected System.Web.UI.WebControls.Panel buttons;
		



		/// <summary>
		/// es wird entweder der An- oder Abwurzeln Button aktiviert
		/// und f�r geschlossene PostIt das schleier.css eingestellt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{

			// wenn die Nachricht nicht mehr ge�ffnet bzw. geschlossen ist,
			// wird ein Schleier angezeigt
			if(!PostIt.IsOpen || PostIt.StammClosed)
			{
				PostItPanel.CssClass = "schleier";
				QLabel.ForeColor = Color.White;
			}


			// QLabel
			QLabel.Text = this.OliUser.Stamm.Q.P;

			// VonStamm wird der Urheber angezeigt ausser ... es ist neu
			if(PostIt.PostItRow.RowState != System.Data.DataRowState.Added)
			{
				VonLabel.Text = PostIt.MyStammHtmlList;

				// UrheberStamm
				if(PostIt.BinIchMeinPostIt && Stamm.BinIchEingeloggt)
				{
					// MeinsPanel
					MeinsPanel.Visible = true;
					// BezahltLabel
					BezahltLabel.Text = OliUtil.MakeRedKook(PostIt.StammZahlt);
					// FristLabel
					FristLabel.Text = OliUtil.MakeDateTimeDiff(PostIt.StammFrist);
					// EditHyperLink
					EditHyperLink.Visible = true;
				}
				else
				{
					EditHyperLink.Visible = false;
				}
			}
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
		

		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}

