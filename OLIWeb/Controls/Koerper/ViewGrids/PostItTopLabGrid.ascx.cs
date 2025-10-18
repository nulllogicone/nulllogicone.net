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
    ///    PostItTopLabGrid.
    ///</summary>
    public partial class PostItTopLabGrid : ViewGridControl
    {
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

        // Eigenschaften
        // -------------

        // mySource
        private PostItTopLabDataSet.PostItTopLabDataTable mySource
        {
            get
            {
                try
                {
                    return (PostIt.MyTopLab);
                }
                catch
                {
                    return (null);
                }
            }
        }

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Title und Spaltenüberschriften auf Q
            TitleLabel.Text = OliUser.Stamm.Q.P_T + " (" + OliUser.Stamm.PostIt.MyTopLab.Rows.Count + ")";
            PostItDataGrid.Columns[0].HeaderText = OliUser.Stamm.Q.S;
            PostItDataGrid.Columns[1].HeaderText = OliUser.Stamm.Q.T;

            PostItDataGrid.PageSize = ZeilenZahl;

            if (sortString.Length == 0)
            {
                sortString = "DurchToll";
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
            PostItDataGrid.DataSource = dv;
            PostItDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>keine Antworten auf diese Nachricht vorhanden</div><hr>";
                Controls.Add(l);
            }
        }

        // PostItDataGrid_SortCommand()
        private void PostItDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // PostItDataGrid_ItemCommand()
        private void PostItDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                Label sl = (Label) e.Item.FindControl("StammGuidLabel");
                Guid sguid = new Guid(sl.Text);

                Label tl = (Label) e.Item.FindControl("TopLabGuidLabel");
                Guid tguid = new Guid(tl.Text);

//				if (e.CommandName == "TopLab")
//				{
//					this.OliUser.Stamm.ShowTopLab(tguid);
//					SichtbaresGrid = Stamm.SichtbaresGrid.None ;
//					Helper.RedirectToSite();
//				}
                if (e.CommandName == "Stamm")
                {
                    // PostIt merken
                    Guid pguid = Guid.Empty;
                    if (PostIt != null)
                    {
                        pguid = PostIt.PostItRow.PostItGuid;
                    }
                    OliUser.ShowStamm(sguid);

                    // wieder zeigen
                    OliUser.Stamm.ShowTopLab(tguid);
                    if (pguid != Guid.Empty)
                    {
                        OliUser.Stamm.ShowPostIt(pguid);
                    }
                    Helper.RedirectToSite();
                }
            }
        }

        private void PostItDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
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

//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
    }
}