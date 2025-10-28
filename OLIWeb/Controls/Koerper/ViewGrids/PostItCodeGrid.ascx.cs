// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;
using OliWeb.Sites;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    ///<summary>
    ///    PostItCodeGrid.
    ///</summary>
    public partial class PostItCodeGrid : ViewGridControl
    {
        private const int CodeDataGridDelColumnIndex = 3;

        // Eigenschaften
        // -------------

        // mySource
        private PostItCodeDataSet.PostItCodeDataTable mySource
        {
            get
            {
                try
                {
                    return (PostIt.MyCode);
                }
                catch
                {
                    return (null);
                }
            }
        }

        // Ereignisse
        // ----------

        protected void Page_Load(object sender, EventArgs e)
        {
            // wenn es nur einen Code gibt => diesen anzeigen
            // da das Control auch im PostItMaker verwendet wird und es dann auch immer weiterleiten w�rde
            // noch ein Test ob es auf der richtigen Seite ist
            if (Page is PostItCodeSite)
            {
                if (PostIt.MyCode.Count == 1)
                {
                    PostItCodeDataSet.PostItCodeRow pcr = PostIt.MyCode[0];
                    PostIt.ShowCode(pcr.CodeGuid);
                    Response.Redirect("~/Sites/CodeSite.aspx");
                }
            }
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            TitleLabel.Text = OliUser.Stamm.Q.C + " (" + PostIt.MyCode.Rows.Count + ")";
//				CodeDataGrid.Columns[0].HeaderText = this.OliUser.Stamm.Q.C;
//				CodeDataGrid.Columns[1].HeaderText = this.OliUser.Stamm.Q.C_X;

            CodeDataGrid.DataSource = mySource;
            CodeDataGrid.DataBind();

            // keine Daten vorhanden
            if (mySource.Count == 0)
            {
                Label l = new Label();
                l.Text =
                    "<div style='font-size:8pt; text-align:center'>keine Markierung f�r diese Nachricht vorhanden</div><hr />";
                Controls.Add(l);
            }

            // die eigenen Codes darf man l�schen
            if (Stamm.BinIchEingeloggt && Stamm.PostIt.BinIchMeinPostIt)
            {
                CodeDataGrid.Columns[CodeDataGridDelColumnIndex].Visible = true;
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///</summary>
        private void InitializeComponent()
        {
            CodeDataGrid.ItemCommand += CodeDataGrid_ItemCommand;
        }

        #endregion

        private void CodeDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            CodeDataGrid.DataKeyField = "CodeGuid";
            Guid cguid = new Guid(CodeDataGrid.DataKeys[e.Item.ItemIndex].ToString());

//			// Angler zeigen
//			if (e.CommandName == "angler")
//			{
//
//				PostIt.Code = null;
//				Helper.RedirectToSite();
//			}

            // Code l�schen
            if (e.CommandName == "del")
            {
                if (OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
                {
                    PostIt.DeleteCode(cguid);

                    CodeDataGrid.EditItemIndex = -1;
                    CodeDataGrid.SelectedIndex = -1;

                    // WortraumController.ZellBuilder ausschalten
                    PostIt.Code = null;

                    // Ansicht aktualisieren
                    PostIt.MyCode = null;
                }
            }
            else
            {
                // Code zeigen

                // Code zeigen und WEITERLEITEN
                PostIt.ShowCode(cguid);
                Response.Redirect("~/Sites/CodeSite.aspx");
            }
        }
    }
}
