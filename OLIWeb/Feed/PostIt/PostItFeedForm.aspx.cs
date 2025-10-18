using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace OliWeb.Feed.PostIt
{
	/// <summary>
	/// PostItFeedForm.
	/// </summary>
	public class PostItFeedForm : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox StammGuidTextBox;
		protected System.Web.UI.WebControls.Button ShowButton;
		protected System.Web.UI.WebControls.DropDownList StyleDropDownList;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
	
		private Guid sguid;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["sguid"] != null)
			{
				sguid = new Guid(Request.QueryString["sguid"].ToString());
				StammGuidTextBox.Text = sguid.ToString();
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
		
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{    
			this.StyleDropDownList.SelectedIndexChanged += new System.EventHandler(this.StyleDropDownList_SelectedIndexChanged);
			this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ShowButton_Click(object sender, System.EventArgs e)
		{
			Show();
		}

		private void StyleDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Show();
		}	

		private void Show()
		{
			try
			{
				// Datengrundlage einstellen
				sguid = new Guid(StammGuidTextBox.Text);

				// Darstellung auswählen
				PostItFeed pif = (PostItFeed)this.LoadControl(StyleDropDownList.SelectedItem.Value + ".ascx");
				pif.StammGuid = sguid;

				PlaceHolder1.Controls.Add(pif);		
			}
			catch
			{}
		}

	}
}
