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
    ///    PostItStammGrid.
    ///</summary>
    public partial class PostItStammGrid : ViewGridControl
    {
        protected LinkButton CloseLinkButton;

        //#region Web Form Designer generated code

        //protected override void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{
        //    this.PostItDataGrid.ItemCommand += this.PostItDataGrid_ItemCommand;
        //    this.PostItDataGrid.PageIndexChanged += this.PostItDataGrid_PageIndexChanged;
        //    this.PostItDataGrid.SortCommand += this.PostItDataGrid_SortCommand;
        //    this.PostItDataGrid.ItemDataBound += this.PostItDataGrid_ItemDataBound;
        //}

        //#endregion

        // Eigenschaften
        // -------------

        //public string stammClasses = "Stamm";


        // mySource
        private PostItStammDataSet.PostItStammDataTable mySource
        {
            get
            {
                try
                {
                    return (PostIt.MyStamm);
                }
                catch
                {
                    return (null);
                }
            }
        }

        protected string myCssClass(string stamm, int zust)
        {
            {
                
                string ret = "";
                switch (zust)
                {

                    case 2:
                        ret += " follower "; // TODO being implemented in css
                        break;
                    case 1:
                        ret += " Stamm "; // real creator (owner) of the message (PostIt)
                        break;     
                    default :
                        ret += " error "; // never seen this 
                        break;
                }

                if(OliUser.Stamm.StammRow.Stamm == stamm)
                {
                    ret += " current ";
                }
                return ret;
            }
        }


        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (OliUser.Stamm != null)
            {
                // Title und Spaltenüberschriften auf Q
                TitleLabel.Text = OliUser.Stamm.Q.P_S;
                PostItDataGrid.Columns[0].HeaderText = OliUser.Stamm.Q.S;

                PostItDataGrid.PageSize = ZeilenZahl;

                if (sortString.Length == 0)
                {
                    sortString = "Frist";
                    desc = true;
                }

                if (PostIt != null)
                {
                    //TODO: so wie hier : in allen ViewGrids Titeln noch dieses Anzahl in Klammern anhängen
                    TitleLabel.Text = OliUser.Stamm.Q.P_S + " (" + PostIt.MyStamm.Rows.Count + ")";
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
                }
            }
        }

        // Sort_Command
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
            //			if(e.Item.ItemIndex >= 0)
            //			{
            //				Guid pguid = PostIt.PostItRow.PostItGuid ;
            //
            //				PostItDataGrid.DataKeyField = "StammGuid";
            //				Guid sguid = new Guid(PostItDataGrid.DataKeys[e.Item.ItemIndex].ToString());
            //				this.OliUser.ShowStamm(sguid);
            //				this.OliUser.Stamm.ShowPostIt(pguid);
            //
            //				Helper.RedirectToSite();
            //			}
        }

        // ItemDataBound
        private void PostItDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // Im Kopf die sortier-Pfeile zeigen
            if (e.Item.ItemType == ListItemType.Header)
            {
                SortierPfeil((DataGrid)sender, e.Item);
            }

            // Hintergrundfarben anpassen
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // wenn closed abgelaufen => zahlt und Frist grau
                Label cl = (Label)e.Item.FindControl("ClosedLabel");
                bool c = bool.Parse(cl.Text);
                if (c)
                {
                    e.Item.BackColor = Color.WhiteSmoke;
                }

                // wenn ich Urheber (StammZust=1) bin => Hintergrund okker
                Label szl = (Label)e.Item.FindControl("StammZustLabel");
                int sz = int.Parse(szl.Text);
                if (sz == 1)
                {
                    e.Item.Cells[0].BackColor = Color.AntiqueWhite;
                    e.Item.Cells[1].BackColor = Color.AntiqueWhite;
                }
            }
        }

        // PageIndexChanged
        private void PostItDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            PostItDataGrid.CurrentPageIndex = e.NewPageIndex;
        }

        //		private void CloseLinkButton_Click(object sender, System.EventArgs e)
        //		{
        //			SichtbaresGrid = Stamm.SichtbaresGrid.None;
        //			Helper.RedirectToSite();	
        //		}
    }
}