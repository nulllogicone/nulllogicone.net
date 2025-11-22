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
    ///    InboxGrid.
    ///</summary>
    public partial class InboxGrid : ViewGridControl
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
            this.InboxDataGrid.ItemCommand += this.InboxDataGrid_ItemCommand;
            this.InboxDataGrid.SortCommand += this.InboxDataGrid_SortCommand;
            this.InboxDataGrid.ItemDataBound += this.InboxDataGrid_ItemDataBound;
        }

        #endregion

        // Eigenschaften
        // -------------

        // mySource
        private StammInboxDataSet.StammInboxDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return (OliUser.Stamm.MyInbox);
                }
                else return (null);
            }
        }

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Title und Spalten�berschriften auf Q
            TitleLabel.Text = "new: " + OliUser.Stamm.Q.T + " (" + Stamm.MyInbox.Rows.Count + ")";
            InboxDataGrid.Columns[1].HeaderText = OliUser.Stamm.Q.P;
            InboxDataGrid.Columns[2].HeaderText = OliUser.Stamm.Q.T;
            InboxDataGrid.PageSize = ZeilenZahl;

            DataView dv = new DataView(mySource);
            if (desc)
            {
                dv.Sort = sortString + " DESC";
            }
            else
            {
                dv.Sort = sortString;
            }
            InboxDataGrid.DataSource = dv;
            InboxDataGrid.DataBind();

            // keine Daten vorhanden
            if (dv.Count == 0)
            {
                Label l = new Label();
                l.Text = "<div style='font-size:8pt; text-align:center'>keine neuen Antworten vorhanden</div><hr>";
                Controls.Add(l);
            }

            if (OliUser.Stamm != null)
            {
                InboxDataGrid.Columns[4].Visible = OliUser.Stamm.BinIchEingeloggt;
            }
        }

        // InboxDataGrid_SortCommand
        private void InboxDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // InboxDataGrid_ItemCommand()
        private void InboxDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                if (e.CommandName == "gelesen")
                {
                    OliUser.Stamm.InboxGelesen(new Guid(e.CommandArgument.ToString()));
                    OliUser.Stamm.MyInbox = null;
                }
                else
                {
                    InboxDataGrid.DataKeyField = "TopLabGuid";
                    Guid tguid = new Guid(InboxDataGrid.DataKeys[e.Item.ItemIndex].ToString());
                    OliUser.Stamm.ShowTopLab(tguid); // mit PostIt

                    // InboxGesehen
                    OliUser.Stamm.InboxGesehen(TopLab.TopLabRow);
                    OliUser.Stamm.MyInbox = null;
                    OliUser.Stamm.ShowPostIt(TopLab.TopLabRow.PostItGuid);

//					SichtbaresGrid = Stamm.SichtbaresGrid.None;
//					Helper.RedirectToSite();
                    Response.Redirect("~/Sites/TopLabSite.aspx");
                }
            }
        }

        private void InboxDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
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
                if (dr["gesehen"].ToString().Length == 0)
                {
                    e.Item.CssClass = "ungelesen";
                }
                else
                {
                    e.Item.CssClass = "gelesen";
                }

                // Closed Nachrichten => hellgrau
                if (dr["closed"].ToString() != "False")
                {
                    e.Item.BackColor = Color.WhiteSmoke;
                }
            }
        }

//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();
//		}
    }
}
