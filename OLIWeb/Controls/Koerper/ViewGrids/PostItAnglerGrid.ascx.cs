// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    PostItTopLabGrid.
    ///</summary>
    public partial class PostItAnglerGrid : ViewGridControl
    {
        // Eigenschaften
        // -------------

        // mySource
        private PostItAnglerDataSet.PostItAnglerDataTable mySource
        {
            get
            {
                try
                {
                    return (PostIt.MyEmpfaenger);
                }
                catch
                {
                    return (null);
                }
            }
        }

        // CurrentPageIndex
        //public int CurrentPageIndex
        //{
        //    get { return (PostItDataGrid.CurrentPageIndex); }
        //    set { PostItDataGrid.CurrentPageIndex = value; }
        //}

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Title und Spaltenüberschriften auf Q
            if (OliUser.Stamm != null)
            {
                TitleLabel.Text = OliUser.Stamm.Q.P_X + " (" + PostIt.MyEmpfaenger.Rows.Count + ")";
                PostItDataGrid.Columns[0].HeaderText = OliUser.Stamm.Q.S;
                PostItDataGrid.Columns[1].HeaderText = OliUser.Stamm.Q.A;

                PostItDataGrid.PageSize = ZeilenZahl;
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
            PostItDataGrid.DataSource = dv;
            PostItDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>keine Empfänger für diese Nachricht vorhanden</div><hr>";
                Controls.Add(l);
            }
        }

        private void PostItDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        private void PostItDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
//			if(e.Item.ItemIndex >= 0)
//			{
//				Guid pguid = PostIt.PostItRow.PostItGuid ;
//				PostItDataGrid.DataKeyField = "StammGuid";
//				Guid sguid = new Guid(PostItDataGrid.DataKeys[e.Item.ItemIndex].ToString());
//				this.OliUser.ShowStamm(sguid);
//				this.OliUser.Stamm.ShowPostIt(pguid);
//
//				if(e.CommandName == "Angler") 
//				{
//					Guid aguid = new Guid(e.CommandArgument.ToString());
//					this.OliUser.Stamm.ShowAngler(aguid);
//				}	
//				Helper.RedirectToSite();
//			}
        }

        protected void PostItDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
        }

        private void PostItDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Im Kopf die sortier-Pfeile zeigen
            if (e.Item.ItemType == ListItemType.Header)
            {
                SortierPfeil((DataGrid) sender, e.Item);
            }
        }
    }
}