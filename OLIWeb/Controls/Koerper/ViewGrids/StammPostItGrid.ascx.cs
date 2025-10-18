// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    WurzelnMitPostItGridControl.
    ///</summary>
    public partial class StammPostItGrid : ViewGridControl
    {
        protected LinkButton CloseLinkButton;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.PostItDataGrid.ItemCommand += this.PostItDataGrid_ItemCommand;
            this.PostItDataGrid.PageIndexChanged += this.PostItDataGrid_PageIndexChanged;
            this.PostItDataGrid.SortCommand += this.PostItDataGrid_SortCommand;
            this.PostItDataGrid.ItemDataBound += this.PostItDataGrid_ItemDataBound;
        }

        #endregion

        //		// Eigenschaften
        //		// -------------
        //
        // MySource
        private StammPostItDataSet.StammPostItDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return (OliUser.Stamm.MyPostIt);
                }
                return (null);
            }
        }

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            PostItDataGrid.PageSize = ZeilenZahl;

            // Title und Spaltenüberschriften auf Q
            if (OliUser.Stamm != null)
            {
                TitleLabel.Text = OliUser.Stamm.Q.S_P;
                PostItDataGrid.Columns[2].HeaderText = OliUser.Stamm.Q.P;
            }

            if (sortString.Length == 0)
            {
                sortString = "Frist";
                desc = true;
            }

            if (mySource != null)
            {
                //TODO: so wie hier: Anzahl in Titel
                TitleLabel.Text = OliUser.Stamm.Q.S_P + " (" + OliUser.Stamm.MyPostIt.Rows.Count + ")";
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

                PostItDataGrid.DataSource = dv;
                PostItDataGrid.DataBind();

                // keine Daten vorhanden
                if (dv.Count == 0)
                {
                    Label l = new Label();
                    l.Text =
                        "<div style='font-size:8pt; text-align:center'>Dieser Stamm hat keine Nachrichten oder zeigt geschlossene Nachrichten nicht an</div><hr>";
                    Controls.Add(l);
                }
            }
        }

        // SortCommand
        private void PostItDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // ItemCommand
        private void PostItDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
        }

        // PostItDataGrid_ItemDataBound()
        private void PostItDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Im Kopf die sortier-Pfeile zeigen
            if (e.Item.ItemType == ListItemType.Header)
            {
                SortierPfeil((DataGrid) sender, e.Item);
            }

            // Zeilenhintergrundfarbe anpassen
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView) e.Item.DataItem;

                // wenn ich Urheber (StammZust=1) bin => Hintergrund okker
                if (dr["StammZust"].ToString() == "1")
                {
                    e.Item.Cells[0].BackColor = Color.AntiqueWhite;
                }

                // wenn closed => dann Zeile grau
                if (dr["closed"].ToString() != "False")
                {
                    e.Item.BackColor = Color.WhiteSmoke;
                }
            }
        }

        // PageIndexChanged
        private void PostItDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
        }
    }
}