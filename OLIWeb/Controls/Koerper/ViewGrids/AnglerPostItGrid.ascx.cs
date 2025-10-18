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
    ///    AnglerPostItGrid.
    ///</summary>
    public partial class AnglerPostItGrid : ViewGridControl
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
            this.AnglerDataGrid.ItemCommand += this.AnglerDataGrid_ItemCommand;
            this.AnglerDataGrid.PageIndexChanged += this.AnglerDataGrid_PageIndexChanged;
            this.AnglerDataGrid.SortCommand += this.AnglerDataGrid_SortCommand;
            this.AnglerDataGrid.ItemDataBound += this.AnglerDataGrid_ItemDataBound;
        }

        #endregion

        // Eigenschaften
        // -------------

        // mySource
        private AnglerPostItDataSet.AnglerPostItDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    if (Angler != null)
                    {
                        return (Angler.MyPostIt);
                    }
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

            // Title und Spaltenüberschriften auf Q
            TitleLabel.Text = OliUser.Stamm.Q.A_X + " (" + Angler.MyPostIt.Rows.Count + ")";

            AnglerDataGrid.PageSize = ZeilenZahl;
            AnglerDataGrid.Columns[1].HeaderText = OliUser.Stamm.Q.P;

            if (sortString.Length == 0)
            {
                sortString = "KooK";
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
            AnglerDataGrid.DataSource = dv;
            AnglerDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>keine Nachrichten für dieses Filterprofil vorhanden</div><hr>";
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

        // AnglerDataGrid_ItemCommand()
        private void AnglerDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                AnglerDataGrid.DataKeyField = "PostItGuid";
                Guid pguid = new Guid(AnglerDataGrid.DataKeys[e.Item.ItemIndex].ToString());

                Label gl = (Label) e.Item.FindControl("CodeGuidLabel");
                Guid cguid = new Guid(gl.Text);

                // PostIt als gelesen markieren
                // (eigentlich hat der angler hier eine CodeGuid und
                // diese wird in der Tabelle Spiegel mit gelesen gestempelt
                Angler.CodeGelesen(cguid);
                Angler.MyPostIt = null;

                OliUser.Stamm.ShowPostIt(pguid);
//				this.SichtbaresGrid = Stamm.SichtbaresGrid.None ;
//				if (e.CommandName == "Urh")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItStamm;
//				}
//				if (e.CommandName == "Angler")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItAngler;
//				}
//				if (e.CommandName == "TopLab")
//				{
//					SichtbaresGrid = Stamm.SichtbaresGrid.PostItTopLab;
//				}
//
                Response.Redirect("~/Sites/PostItSite.aspx");
            }
        }

        private void AnglerDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            AnglerDataGrid.CurrentPageIndex = e.NewPageIndex;
        }

        private void AnglerDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Im Kopf die sortier-Pfeile zeigen
            if (e.Item.ItemType == ListItemType.Header)
            {
                SortierPfeil((DataGrid) sender, e.Item);
            }

            // Zeilen formatieren
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView) e.Item.DataItem;

                // Gelesen/Ungelesen darstellen
                if (dr["gelesen"].ToString().Length == 0)
                {
                    e.Item.CssClass = "ungelesen";
                }
                else
                {
                    e.Item.CssClass = "gelesen";
                }

                // closed => Zeile grau
                if (dr["closed"].ToString() != "False")
                {
                    e.Item.BackColor = Color.WhiteSmoke;
                }
            }
        }

//
//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
    }
}