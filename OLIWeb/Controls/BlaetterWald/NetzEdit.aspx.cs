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

using OliEngine.OliMiddleTier.OLIx;

namespace OliWeb.Controls.BlaetterWald
{
	/// <summary>
	/// NetzEdit.
	/// </summary>
	public class NetzEdit : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox NetzTextBox;
		protected System.Web.UI.WebControls.TextBox BeschreibungTextBox;
		protected System.Web.UI.WebControls.DataGrid KnotenDataGrid;
		protected System.Web.UI.WebControls.TextBox BildTextBox;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.Button UpdateButton;

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
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.KnotenDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.KnotenDataGrid_CancelCommand);
			this.KnotenDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.KnotenDataGrid_EditCommand);
			this.KnotenDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.KnotenDataGrid_UpdateCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// Member
		// ------
		Netz n;
		bool neu; // soll ein neues Netz erstellt werden

		private void Page_Load(object sender, System.EventArgs e)
		{
			Guid nguid;
			

			// nguid im Querystring
			if(Request["nguid"] != null)
			{
				neu = false;

				// Netz Objekt für die Seite erstellen
				nguid = new Guid(Request["nguid"]);
				n = new Netz(nguid);
				Knoten k = new Knoten(n.NetzRow);
				KnotenDataGrid.DataSource = k.Knoten;				
				
				// Beschriften
				if(!IsPostBack)
				{
					NetzTextBox.Text = n.NetzRow.Netz;
					BeschreibungTextBox.Text = n.NetzRow.IsBeschreibungNull() ? "" : n.NetzRow.Beschreibung;
					DataBind();
				}
			}
			else
			{
				// neues Netz
				UpdateButton.Text = "neu";
				neu = true;
			}
		}


		private void UpdateButton_Click(object sender, System.EventArgs e)
		{
			
			if(neu)
			{
				n.NetzRow.NetzGuid = new Guid();
			}
			n.NetzRow.Netz = NetzTextBox.Text;
			n.NetzRow.Beschreibung = BeschreibungTextBox.Text;

			n.UpdateNetz();
		}

		private void KnotenDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			KnotenDataGrid.EditItemIndex = e.Item.ItemIndex;
			DataBind();
		}

		private void KnotenDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			KnotenDataGrid.EditItemIndex = -1;
			DataBind();

		}

		private void KnotenDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Label kgl = (Label)e.Item.FindControl("KnotenGuidLabel");
			Knoten k = new Knoten(new Guid(kgl.Text));
			k.KnotenRow.Knoten = ((TextBox)e.Item.FindControl("KnotenTextBox")).Text;
			k.KnotenRow.KnotenBeschreibung = ((TextBox)e.Item.FindControl("KnotenBeschreibungTextBox")).Text;
			k.UpdateKnoten();
			KnotenDataGrid.EditItemIndex = -1;
			DataBind();
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			KnotenDataGrid.EditItemIndex = -1;

			NetzTextBox.Text = n.NetzRow.Netz;
			BeschreibungTextBox.Text = n.NetzRow.IsBeschreibungNull() ? "" : n.NetzRow.Beschreibung;
			DataBind();
		}

	}
}
