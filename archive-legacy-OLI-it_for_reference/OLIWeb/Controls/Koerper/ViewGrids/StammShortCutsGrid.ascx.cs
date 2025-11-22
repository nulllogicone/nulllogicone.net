// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Controls.Wortraum;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    StammShortCutsGrid.
    ///</summary>
    public partial class StammShortCutsGrid : ViewGridControl
    {
        protected WortraumController WortraumController1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
            if (OliUser.Stamm.ShortCuts != null)
            {
                WortraumController1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                WortraumController1.Markierbar = OliUser.Stamm.BinIchEingeloggt;
                WortraumController1.ZellBuilder = OliUser.Stamm.ShortCuts.ZellBuilder;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            WortraumController1.Visible = false;

            // ShortCutsDataGrid
            ShortCutsDataGrid.DataSource = OliUser.Stamm.MyShortCuts;
            ShortCutsDataGrid.DataBind();

            // wenn ShortCuts ausgew�hlt => Wortraum zeigen
            if (OliUser.Stamm.ShortCuts != null)
            {
                WortraumController1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                WortraumController1.Markierbar = OliUser.Stamm.BinIchEingeloggt;
                WortraumController1.ZellBuilder = OliUser.Stamm.ShortCuts.ZellBuilder;
                WortraumController1.Visible = true;
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///</summary>
        private void InitializeComponent()
        {
            ShortCutsDataGrid.ItemCommand += ShortCutsDataGrid_ItemCommand;
            ShortCutsDataGrid.CancelCommand += ShortCutsDataGrid_CancelCommand;
            ShortCutsDataGrid.UpdateCommand += ShortCutsDataGrid_UpdateCommand;
        }

        #endregion

        /// <summary>
        ///     erstellt einen neuen ShortCut
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void NeuShortCutsButton_Click(object sender, EventArgs e)
        {
            OliUser.Stamm.NewShortCuts();
            OliUser.Stamm.MyShortCuts = null;
        }

        private void ShortCutsDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            ShortCutsDataGrid.DataKeyField = "ShortCutsGuid";
            Guid scguid = (Guid) ShortCutsDataGrid.DataKeys[e.Item.ItemIndex];

            // ausw�hlen
            if (e.CommandName == "ok" || e.CommandName == "select" || e.CommandName == "Edit")
            {
                ShortCutsDataGrid.EditItemIndex = e.Item.ItemIndex;

                // ShowShortCuts
                OliUser.Stamm.ShowShortCuts(scguid);
                OliUser.Nachricht = "ShortCuts selected";
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
            if (e.CommandName == "del")
            {
                ShortCutsDataGrid.SelectedIndex = -1;
                ShortCutsDataGrid.EditItemIndex = -1;
                ShortCuts sc = new ShortCuts(OliUser.Stamm, scguid);
                sc.ShortCutsRow.Delete();
                sc.UpdateShortCuts();
                OliUser.Stamm.MyShortCuts = null;
                OliUser.Stamm.ShortCuts = null;
                OliUser.Nachricht = "ShortCuts deleted";
            }
        }

        private void ShortCutsDataGrid_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            ShortCutsDataGrid.EditItemIndex = -1;
            Stamm.ShortCuts = null;
        }

        private void ShortCutsDataGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            // Die ShortCuts TextBox holen
            TextBox l = (TextBox) e.Item.FindControl("ShortCutsTextBox");
            CheckBox cb = (CheckBox) e.Item.FindControl("AutoCheckBox");

            Stamm.ShortCuts.ShortCutsRow.ShortCut = l.Text;
            Stamm.ShortCuts.ShortCutsRow.auto = cb.Checked;
            Stamm.ShortCuts.UpdateShortCuts();

            ShortCutsDataGrid.EditItemIndex = -1;
            ShortCutsDataGrid.SelectedIndex = -1;

            OliUser.Stamm.MyShortCuts = null;
            Stamm.ShortCuts = null;
        }
    }
}
