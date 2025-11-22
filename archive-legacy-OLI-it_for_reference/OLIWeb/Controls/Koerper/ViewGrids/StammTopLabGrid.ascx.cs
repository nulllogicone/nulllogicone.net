// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    StammTopLabGrid.
    ///</summary>
    public partial class StammTopLabGrid : ViewGridControl
    {
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
            this.TopLabDataGrid.ItemCommand += this.TopLabDataGrid_ItemCommand;
            this.TopLabDataGrid.PageIndexChanged += this.TopLabDataGrid_PageIndexChanged;
            this.TopLabDataGrid.SortCommand += this.TopLabDataGrid_SortCommand;
            this.TopLabDataGrid.ItemDataBound += this.TopLabDataGrid_ItemDataBound;
        }

        #endregion

        // Eigenschaften
        // -------------

        // mySource
        private StammTopLabDataSet.StammTopLabDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return (OliUser.Stamm.MyTopLab);
                }
                return (null);
            }
        }

        // CurrentPageIndex
        public int CurrentPageIndex
        {
            get { return (TopLabDataGrid.CurrentPageIndex); }
            set { TopLabDataGrid.CurrentPageIndex = value; }
        }

        // OnPreRender()
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Title und Spalten�berschriften auf Q
            TitleLabel.Text = OliUser.Stamm.Q.S_T + " (" + OliUser.Stamm.MyTopLab.Rows.Count + ")";
            TopLabDataGrid.Columns[1].HeaderText = OliUser.Stamm.Q.P;
            TopLabDataGrid.Columns[2].HeaderText = OliUser.Stamm.Q.T;

            TopLabDataGrid.PageSize = ZeilenZahl;

            if (sortString.Length == 0)
            {
                sortString = "TDatum";
                desc = true;
            }

            DataView dv = new DataView(mySource);
            if (desc)
            {
                dv.Sort = sortString + " DESC";
            }
            else
            {
                dv.Sort = sortString;
            }
            TopLabDataGrid.DataSource = dv;
            TopLabDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>Diesesr Stamm hat noch keine Antworten verfasst</div><hr>";
                Controls.Add(l);
            }
        }

        // TopLabDataGrid_SortCommand
        private void TopLabDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // TopLabDataGrid_ItemCommand()
        private void TopLabDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                if (e.CommandName == "TopLab")
                {
                    OliUser.Stamm.ShowTopLab(new Guid(e.CommandArgument.ToString()));
                    OliUser.Stamm.ShowPostIt(TopLab.TopLabRow.PostItGuid);
//					SichtbaresGrid = Stamm.SichtbaresGrid.None ;

                    Helper.RedirectToSite();
                }
                if (e.CommandName == "PostIt")
                {
                    OliUser.Stamm.ShowPostIt(new Guid(e.CommandArgument.ToString()));
//					this.OliUser.Stamm.sichtbaresGrid = Stamm.SichtbaresGrid.None;

                    Helper.RedirectToSite();
                }
            }
        }

        // TopLabDataGrid_PageIndexChanged()
        private void TopLabDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            TopLabDataGrid.CurrentPageIndex = e.NewPageIndex;
        }

        // ItemDataGrid_ItemDataBound()
        private void TopLabDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
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
