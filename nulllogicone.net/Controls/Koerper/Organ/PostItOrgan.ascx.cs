namespace OliWeb.Controls.Koerper.Organ
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine;
	using OliEngine.DataSetTypes;
	using OliEngine.OliMiddleTier.OLIs;

	using OliWeb.Klassen;
	using OliWeb.Controls.Gimicks;
	using OliWeb.Sites;


	/// <summary>
	///		PostItOrgan.
	/// </summary>
	public abstract class PostItOrgan : MasterControl
	{
		protected System.Web.UI.WebControls.Label KooKLabel;
		protected System.Web.UI.WebControls.Literal UrlLiteral;
		protected System.Web.UI.WebControls.Label HitsLabel;
		protected System.Web.UI.WebControls.Panel PostItPanel;
		protected System.Web.UI.WebControls.Label TitelLabel;
		protected System.Web.UI.WebControls.HyperLink PostItHyperLink;
		protected System.Web.UI.WebControls.Label DatumLabel;
		protected System.Web.UI.HtmlControls.HtmlGenericControl UriIframe;


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

		// Member
		// ------


		protected Controls.Gimicks.KlickBild KlickBild1;

		// Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{



			
			// TODO: hier kommt noch 'reaktivieren (kostenpflichtig) rein
			// falls die wurzel schon closed war || !user.Stamm.PostIt.StammClosed) ;

			PostItDataSet.PostItRow p = PostIt.PostItRow;


			// Titel
			TitelLabel.Text = p.IsTitelNull() ? "" : p.Titel;

			// gekürzten Text bei Detailtabellenansicht oder TopLabEditieransicht
			// und ohne TopLab anzeigen [...] 
			// früher ging das mit dieser Eigenschaft || SichtbaresGrid == Stamm.SichtbaresGrid.None
			// Auf der PostIt-Seite : ganzen Text
			bool isPostItSite = (this.Page is PostItSite);
			bool isTopLabEdit = (this.Page is Sites.Edit.TopLabEdit);
			if(isPostItSite || isTopLabEdit)
			{
				if(p.Typ == "txt")
				{
					PostItHyperLink.Text = OliUtil.MakeHtmlLineBreak(p.PostIt);
				}
				else
				{
					PostItHyperLink.Text = p.PostIt;
				}
			}
			else
			{
				if(p.Typ == "txt")
				{
					PostItHyperLink.Text = OliUtil.MakeHtmlLineBreak(OliUtil.FirstXWords(p.PostIt, 20));
				}
				else
				{
					PostItHyperLink.Text = OliUtil.FirstXWords(p.PostIt, 20);
				}
			}

			PostItHyperLink.NavigateUrl = Helper.MakeBaseLink() + "P/" + p.PostItGuid + ".aspx";
			KooKLabel.Text = OliUtil.MakeRedKook(p.KooK);
			DatumLabel.Text = p.IsDatumNull() ? "" : p.Datum.ToShortDateString();
			DatumLabel.ToolTip = "erstellt: " + (p.IsDatumNull() ? "" : p.Datum.ToString());
			KlickBild1.BildName = p.IsDateiNull() ? "" : p.Datei;
			KlickBild1.Breite = 100;
			HitsLabel.Text = p.Hits.ToString();

			if(!p.IsURLNull())
			{
				UrlLiteral.Text = "Link: " + OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(p.URL));
				UrlLiteral.Visible = true;
			}
			else
			{
				UrlLiteral.Text = "";
				UrlLiteral.Visible = false;
			}

			if(p.Typ == "uri")
			{
				UriIframe.Attributes.Add("src", PostIt.PostItRow.URL);
				UriIframe.Visible = true;
			}
			else
			{
				UriIframe.Visible = false;
			}
		}
	}
}

