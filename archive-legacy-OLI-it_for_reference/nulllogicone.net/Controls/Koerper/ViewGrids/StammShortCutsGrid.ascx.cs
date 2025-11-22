// (c) Frederic Luchting
// ---------------------

namespace OliWeb.Controls.Koerper.ViewGrids
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using OliEngine.OliDataAccess.Functions;
	using OliEngine.OliMiddleTier.OLIs;
	using OliEngine.OliMiddleTier.Markierer;
	using OliEngine.OliMiddleTier.ZellHaufen;
	using OliWeb.Klassen;

	/// <summary>
	///	StammShortCutsGrid.
	/// </summary>
	public class StammShortCutsGrid : ViewGridControl
	{
		protected System.Web.UI.WebControls.DataGrid ShortCutsDataGrid;
		protected System.Web.UI.WebControls.LinkButton NeuShortCutsButton;
		protected System.Web.UI.WebControls.Label TitleLabel;
		protected Wortraum.WortraumController WortraumController1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
			if(this.OliUser.Stamm.ShortCuts != null)
			{
				WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei ;
				WortraumController1.Markierbar = this.OliUser.Stamm.BinIchEingeloggt;
				WortraumController1.ZellBuilder = this.OliUser.Stamm.ShortCuts.ZellBuilder;
			}
		}

		protected override void OnPreRender(EventArgs e) 
		{
			WortraumController1.Visible = false;

			// ShortCutsDataGrid
			ShortCutsDataGrid.DataSource = this.OliUser.Stamm.MyShortCuts;
			ShortCutsDataGrid.DataBind();

			// wenn ShortCuts ausgew�hlt => Wortraum zeigen
			if(this.OliUser.Stamm.ShortCuts != null)
			{
				WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei ;
				WortraumController1.Markierbar = this.OliUser.Stamm.BinIchEingeloggt;
				WortraumController1.ZellBuilder = this.OliUser.Stamm.ShortCuts.ZellBuilder;
				WortraumController1.Visible = true;
			}
		}

		#region Vom Web Form-Designer generierter Code
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Erforderliche Methode f�r die Designerunterst�tzung
		/// </summary>
		private void InitializeComponent()
		{
			this.ShortCutsDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ShortCutsDataGrid_ItemCommand);
			this.ShortCutsDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ShortCutsDataGrid_CancelCommand);
			this.ShortCutsDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ShortCutsDataGrid_UpdateCommand);
			this.NeuShortCutsButton.Click += new System.EventHandler(this.NeuShortCutsButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		/// <summary>
		/// erstellt einen neuen ShortCut
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NeuShortCutsButton_Click(object sender, System.EventArgs e)
		{
			this.OliUser.Stamm.NewShortCuts();
			this.OliUser.Stamm.MyShortCuts = null;
		}

		private void ShortCutsDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ShortCutsDataGrid.DataKeyField = "ShortCutsGuid";
			Guid scguid = (Guid)ShortCutsDataGrid.DataKeys[e.Item.ItemIndex];
			
			// ausw�hlen
			if (e.CommandName == "ok" || e.CommandName == "select" || e.CommandName == "Edit")
			{
				ShortCutsDataGrid.EditItemIndex = e.Item.ItemIndex;

				// ShowShortCuts
				this.OliUser.Stamm.ShowShortCuts(scguid);				
				this.OliUser.Nachricht = "ShortCuts ausgew�hlt";
			}

//			// CopyShortCutsToCode
//			if (e.CommandName == "set")
//			{
//				// Die ShortCuts TextBox holen
//				TextBox l = (TextBox) e.Item.FindControl("ShortCutsTextBox");
//				this.OliUser.Stamm.ShortCuts.ShortCutsRow.ShortCut = l.Text;
//				this.OliUser.Stamm.ShortCuts.UpdateShortCuts();
//				
//				ShortCutsDataGrid.EditItemIndex = -1;
//				
//
//				this.OliUser.Stamm.MyShortCuts = null;
//			}

			// Delete ShortCuts
			if(e.CommandName == "del")
			{
				ShortCutsDataGrid.SelectedIndex = -1;
				ShortCutsDataGrid.EditItemIndex = -1;
				ShortCuts sc = new ShortCuts(this.OliUser.Stamm, scguid);
				sc.ShortCutsRow.Delete();
				sc.UpdateShortCuts();
				this.OliUser.Stamm.MyShortCuts = null;
				this.OliUser.Stamm.ShortCuts = null;
				this.OliUser.Nachricht = "ShortCuts gel�scht";

			}
		}

		private void ShortCutsDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ShortCutsDataGrid.EditItemIndex = -1;
			Stamm.ShortCuts = null;
		}

		private void ShortCutsDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// Die ShortCuts TextBox holen
			TextBox l = (TextBox) e.Item.FindControl("ShortCutsTextBox");
			CheckBox cb = (CheckBox) e.Item.FindControl("AutoCheckBox");

			Stamm.ShortCuts.ShortCutsRow.ShortCut = l.Text;
			Stamm.ShortCuts.ShortCutsRow.auto = cb.Checked;
			Stamm.ShortCuts.UpdateShortCuts();
				
			ShortCutsDataGrid.EditItemIndex = -1;
			ShortCutsDataGrid.SelectedIndex = -1;
				

			this.OliUser.Stamm.MyShortCuts = null;
			Stamm.ShortCuts = null;
		}
	}
}

