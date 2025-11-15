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

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    TopLabTopLabGrid.
    ///</summary>
    public partial class TopLabTopLabGrid : ViewGridControl
    {
        //#region Web Form Designer generated code

        //protected override void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{
        //    this.TopLabDataGrid.ItemDataBound += this.TopLabDataGrid_ItemDataBound;
        //}

        //#endregion

        // Member
        private TopLab parentTopLab;

        // Eigenschaften
        // -------------

        // Eine der folgenden drei Eigenschaften muss gesetzt sein !!!!!

        // ParentTopLabGuidString
        public string ParentTopLabGuidString
        {
            set { parentTopLab = new TopLab(new Guid(value)); }
        }

        // ParentTopLabGuid
        public Guid ParentTopLabGuid
        {
            set { parentTopLab = new TopLab(value); }
        }

        // ParentTopLab
        public TopLab ParentTopLab
        {
            set { parentTopLab = value; }
        }

        // mySource
        private TopLabTopLabDataSet.TopLabTopLabDataTable mySource
        {
            get { return parentTopLab.MyTopLab; }
        }

        // Ereignisse
        // ----------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Title und Spalten�berschriften auf Q
            //TopLabDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.S;
            //TopLabDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.T;

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
                TopLabDataGrid.Visible = false;
            }
        }

        // ItemDataBound
        private void TopLabDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // TGuid dieser Zeile Holen
                Label tl = (Label) e.Item.FindControl("TopLabGuidLabel");
                Guid tguid = new Guid(tl.Text);

                // TopLab erstellen und sehen ob es Kinder hat
                TopLab t = new TopLab(tguid);
                if (t.MyTopLab.Count > 0)
                {
                    // neues TopLabTopLabGrid erstellen und drunter h�ngen
                    TopLabTopLabGrid ttg =
                        (TopLabTopLabGrid) LoadControl("~/Controls/Koerper/ViewGrids/TopLabTopLabGrid.ascx");
                    ttg.ParentTopLab = t;
                    e.Item.Cells[1].Controls.Add(ttg);
                }
            }
        }
    }
}
