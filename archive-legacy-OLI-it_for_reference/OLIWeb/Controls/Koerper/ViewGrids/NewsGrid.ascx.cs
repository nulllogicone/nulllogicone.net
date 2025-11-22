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
    ///    NewsGrid.
    ///</summary>
    public partial class NewsGrid : ViewGridControl
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
            this.NewsDataGrid.ItemCommand += this.NewsDataGrid_ItemCommand;
            this.NewsDataGrid.PageIndexChanged += this.NewsDataGrid_PageIndexChanged;
            this.NewsDataGrid.SortCommand += this.NewsDataGrid_SortCommand;
            this.NewsDataGrid.ItemDataBound += this.NewsDataGrid_ItemDataBound;
        }

        #endregion

        // Member
        // ------

        private string letzterAngler;

        // Eigenschaften
        // -------------

        // mySource
        private StammNewsDataSet.StammNewsDataTable mySource
        {
            get
            {
                if (OliUser.Stamm != null)
                {
                    return (OliUser.Stamm.MyNews);
                }
                else
                {
                    return (null);
                }
            }
        }

        // CurrentPageIndex
        public int CurrentPageIndex
        {
            get { return (NewsDataGrid.CurrentPageIndex); }
            set { NewsDataGrid.CurrentPageIndex = value; }
        }

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (OliUser.Stamm != null)
            {
                // Title und Spalten�berschriften auf Q
                TitleLabel.Text = "new: " + OliUser.Stamm.Q.P + " (" + Stamm.MyNews.Rows.Count + ")";
                NewsDataGrid.Columns[0].HeaderText = OliUser.Stamm.Q.A;
                NewsDataGrid.Columns[2].HeaderText = OliUser.Stamm.Q.P;
                NewsDataGrid.PageSize = ZeilenZahl;

                // Daten-Bindung ???
                DataView dv = new DataView(mySource);

                if (sortString.Length == 0)
                {
                    sortString = "Datum "; // war davor von Anfang an immer nur 'Kook'
                    desc = true;
                }

                if (desc)
                {
                    dv.Sort = sortString + " DESC";
                }
                else
                {
                    dv.Sort = sortString;
                }
                NewsDataGrid.DataSource = dv;
                NewsDataGrid.DataBind();

                // keine Daten vorhanden
                if (dv.Count == 0)
                {
                    Label l = new Label();
                    l.Text =
                        "<div style='font-size:8pt; text-align:center'>keine neuen Nachrichten an ihr Filterprofil vorhanden</div><hr>";
                    Controls.Add(l);
                }

                // gelesen Button
                NewsDataGrid.Columns[6].Visible = OliUser.Stamm.BinIchEingeloggt;
            }
        }

        // NewsDataGrid_SortCommand()
        private void NewsDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (sortString == e.SortExpression)
            {
                desc = !desc;
            }
            sortString = e.SortExpression;
        }

        // NewsDataGrid_ItemCommand
        private void NewsDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                if (OliUser.Stamm != null)
                {
                    if (e.CommandName == "gelesen")
                    {
                        OliUser.Stamm.NewsGelesen(new Guid(e.CommandArgument.ToString()));
                        OliUser.Stamm.MyNews = null;
                    }
                    else if (e.CommandName == "AlleNewsGelesen")
                    {
                        Label aidl = (Label) e.Item.FindControl("AnglerGuidLabel");
                        Guid aguid = new Guid(aidl.Text);
                        OliUser.Stamm.ShowAngler(aguid);
                        Angler.AlleNewsGelesen();
                        Angler = null;

                        // Stamm neu zeigen
                        Guid sguid = OliUser.Stamm.StammRow.StammGuid;
                        OliUser.ShowStamm(sguid);
                        NewsDataGrid.CurrentPageIndex = 0;
                    }
                    else if (e.CommandName == "AnglerLinkButton")
                    {
                        Label aguidl = (Label) e.Item.FindControl("AnglerGuidLabel");
                        Guid aguid = new Guid(aguidl.Text);
                        OliUser.Stamm.ShowAngler(aguid);
//						SichtbaresGrid = Stamm.SichtbaresGrid.AnglerPostIt ;
//						Helper.RedirectToSite();
                        Response.Redirect("~/Sites/AnglerSite.aspx");
                    }
                    else
                    {
                        // News gesehen
                        string nguid = ((Label) e.Item.FindControl("NewsGuidLabel")).Text;
                        OliUser.Stamm.NewsGesehen(new Guid(nguid));

                        // PostItGuid holen und anzeigen
                        Guid pguid = new Guid(NewsDataGrid.DataKeys[e.Item.ItemIndex].ToString());
                        OliUser.Stamm.MyNews = null;
                        OliUser.Stamm.ShowPostIt(pguid);
//						this.OliUser.Stamm.sichtbaresGrid = Stamm.SichtbaresGrid.None ;

                        if (e.CommandName == "zeigT")
                        {
//							SichtbaresGrid = Stamm.SichtbaresGrid.PostItTopLab;
                        }
                        Response.Redirect("~/Sites/PostItSite.aspx");
                    }
                }
            }
        }

        // NewsDataGrid_ItemDataBound()
        private void NewsDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
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
                // Mehrfachvorkommen von Angler entfernen
                LinkButton alb = (LinkButton) e.Item.FindControl("AnglerLinkButton");
                if (letzterAngler == alb.Text)
                {
                    alb.Text = "";
                    e.Item.FindControl("AlleNewsGelesenButton").Visible = false;
                }
                else
                {
                    letzterAngler = alb.Text;
                    if (OliUser.Stamm.BinIchEingeloggt && sortString == "Angler")
                        e.Item.FindControl("AlleNewsGelesenButton").Visible = true;
                }

                DataRowView dr = (DataRowView) e.Item.DataItem;

                // Gesehene zart andere fett
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

        private void NewsDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            NewsDataGrid.CurrentPageIndex = e.NewPageIndex;
        }

//		private void CloseLinkButton_Click(object sender, System.EventArgs e)
//		{
//			SichtbaresGrid = Stamm.SichtbaresGrid.None;
//			Helper.RedirectToSite();	
//		}
    }
}
