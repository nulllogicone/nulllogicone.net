// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;
using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    StammAnglerGrid.
    ///</summary>
    public partial class StammAnglerGrid : ViewGridControl
    {
        protected LinkButton CloseLinkButton;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.AnglerDataGrid.ItemCommand += this.AnglerDataGrid_ItemCommand;
            this.AnglerDataGrid.SortCommand += this.AnglerDataGrid_SortCommand;
            this.AnglerDataGrid.ItemDataBound += this.AnglerDataGrid_ItemDataBound;
        }

        #endregion

        // MySource
        private StammAnglerDataSet.StammAnglerDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return (OliUser.Stamm.MyAngler);
                }

                return (null);
            }
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Title und Spalten�berschriften auf Q
            if (OliUser.Stamm != null)
            {
                TitleLabel.Text = OliUser.Stamm.Q.S_A + " (" + OliUser.Stamm.MyAngler.Rows.Count + ")";
                AnglerDataGrid.Columns[0].HeaderText = OliUser.Stamm.Q.A;

                // f�r eingeloggten Stamm die L�schen - Spalte - Buttons zeigen
                AnglerDataGrid.Columns[3].Visible = OliUser.Stamm.BinIchEingeloggt;
            }

            DataView dv = new DataView(mySource);
            if (sortString.Length > 0)
            {
                if (desc)
                {
                    dv.Sort = sortString + " DESC";
                }
                else
                {
                    dv.Sort = sortString;
                }
            }
            AnglerDataGrid.DataSource = dv;
            AnglerDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>Dieser Stamm hat noch kein Filterprofil. <br />Sie k�nnen einen neuen erstellen</div><hr>";
                Controls.Add(l);
            }
        }

        // AnglerDataGrid_SortCommand()
        private void AnglerDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // AnglerDataGrid_ItemCommand
        private void AnglerDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                Guid aguid = (Guid) AnglerDataGrid.DataKeys[e.Item.ItemIndex];
                if (e.CommandName == "Delete")
                {
                    // Den Angler l�schen!
                    Angler a = new Angler(OliUser.Stamm, aguid);
                    a.AnglerRow.Delete();
                    a.UpdateAngler();

                    // MyAngler aktualisieren
                    Angler = null;
                    OliUser.Stamm.MyAngler = null;
//					SichtbaresGrid = Stamm.SichtbaresGrid.StammAngler;

                    return;
                    // Redirect
//					Helper.RedirectToSite();
                }

                // wenn nicht gel�scht (und weitergeleitet) wurde
                // zuerst den Angler mit seinen Fischen zeigen
                OliUser.Stamm.ShowAngler(aguid);

                Helper.RedirectToSite();
            }
        }

        protected void AnglerDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AnglerDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Im Kopf die sortier-Pfeile zeigen
            if (e.Item.ItemType == ListItemType.Header)
            {
                SortierPfeil((DataGrid) sender, e.Item);
            }
        }

//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
    }
}
