// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationInsights;
using OliEngine.OliDataAccess.Functions;
using OliWeb.Controls.AjaxWortraum;
using OliWeb.Controls.Wortraum;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    /// <summary>
    ///     CodeKoerper.
    /// </summary>
    public partial class CodeKoerper : MasterControl
    {
        protected AjaxWortraumControl AjaxWortraumControl1;
        protected WortraumController WortraumController1;
        protected HyperLink XMLHyperLink;

        // Ereignisse
        // ----------

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            // QLabel
            QLabel.Text = OliUser.Stamm.Q.C;

            if (PostIt != null)
            {
                // wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
                if (OliUser.Stamm.ShortCuts != null)
                {
                    WortraumController1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                    WortraumController1.Markierbar = OliUser.Stamm.BinIchEingeloggt;
                    WortraumController1.ZellBuilder = OliUser.Stamm.ShortCuts.ZellBuilder;
                    AjaxWortraumControl1.Visible = false;
                }
                // sonst Code als Wortraum anzeigen
                else if (PostIt.Code != null)
                {
                    if (!IsPostBack)
                    {
                        KommentarTextBox.Text = PostIt.Code.CodeRow.Kommentar;
                    }

                    AjaxWortraumControl1.Markierer = OliUser.Stamm.PostIt.Code;
                    AjaxWortraumControl1.Markierbar = Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt;
                    AjaxWortraumControl1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                }
            }
        }

        // OnPreRender()
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // alles ausschalten
            WortraumController1.Markierbar = false;
            WortraumControllerPanel.BackColor = Color.White;
            CancelButton.Visible = false;

            if (OliUser.Stamm != null)
            {
                if (PostIt != null)
                {
                    // wenn eingeloggt und mein PostIt
                    if (OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
                    {
                        // Wortraum: markieren und fischen
                        WortraumController1.Markierbar = true;

                        // Code: neu und delete
                        //TODO:							CodeDataGrid.Columns[1].Visible = true;

                        // ShortCutsDataGrid
                        ShortCutsDataGrid.DataSource = OliUser.Stamm.MyShortCuts;
                        ShortCutsDataGrid.DataBind();

                        //						// NUR auf der CodeSite werden die ShortCuts angezeigt
                        //						if(this.Page is Sites.CodeSite)
                        //						{
                        //							ShortCutsDataGrid.Visible = true;							
                        //						}
                    }

                    // wenn ShortCuts ausgew�hlt immer als Wortraum zeigen
                    if (OliUser.Stamm.ShortCuts != null)
                    {
                        WortraumController1.Werbefrei = OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                        WortraumController1.Markierbar = OliUser.Stamm.BinIchEingeloggt;
                        WortraumController1.ZellBuilder = OliUser.Stamm.ShortCuts.ZellBuilder;
                        WortraumController1.Visible = true;

                        AjaxWortraumControl1.Visible = false;
                        //						CodeRingeDataGrid.BackColor = Color.AntiqueWhite;
                        CancelButton.Visible = true;

                        WortraumControllerPanel.BackColor = Color.AntiqueWhite;
                        //							CodeRingeDataGrid.DataSource = PostIt.Code.MyRinge;
                        //							CodeRingeDataGrid.DataBind();
                    }
                    // sonst Code als Wortraum anzeigen
                    else if (PostIt.Code != null)
                    {
                        // Ajax ersatz
                        //						WortraumController1.Werbefrei = this.OliUser.Stamm.Extras.ExtrasRow.werbefrei;
                        //						WortraumController1.Markierbar = (this.OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt);
                        //						WortraumController1.ZellBuilder = PostIt.Code.ZellBuilder;
                        //						WortraumController1.Visible = true;

                        //						AjaxWortraumControl1.Markierer = this.OliUser.Stamm.PostIt.Code;
                        //						AjaxWortraumControl1.Markierbar = this.Stamm.BinIchEingeloggt && this.PostIt.BinIchMeinPostIt;

                        //						CodeRingeDataGrid.BackColor = Color.White;
                        //						WortraumControllerPanel.BackColor = Color.White;
                        //						CodeRingeDataGrid.DataSource = PostIt.Code.MyRinge;
                        //						CodeRingeDataGrid.DataBind();
                    }
                }
            }
        }

        // ShortCutsDataGrid_ItemCommand
        private void ShortCutsDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            ShortCutsDataGrid.DataKeyField = "ShortCutsGuid";
            var scguid = (Guid)ShortCutsDataGrid.DataKeys[e.Item.ItemIndex];

            // ausw�hlen
            if (e.CommandName == "ok" || e.CommandName == "select")
            {
                // CodeGrid deselect
                //				CodeDataGrid.SelectedIndex = -1;
                //				CodeDataGrid.EditItemIndex = -1;

                // ausw�hlen und edit mode
                //				ShortCutsDataGrid.SelectedIndex = e.Item.ItemIndex;
                ShortCutsDataGrid.EditItemIndex = e.Item.ItemIndex;

                // ShowShortCuts
                OliUser.Stamm.ShowShortCuts(scguid);

                OliUser.Nachricht = "ShortCuts selected";
            }

            // CopyShortCutsToCode
            if (e.CommandName == "set")
            {
                // Die ShortCuts TextBox holen
                var l = (TextBox)e.Item.FindControl("ShortCutsTextBox");
                OliUser.Stamm.ShortCuts.ShortCutsRow.ShortCut = l.Text;
                OliUser.Stamm.ShortCuts.UpdateShortCuts();

                ShortCutsDataGrid.EditItemIndex = -1;

                // wenn Code ausgew�hlt ist -> copieren
                if (PostIt.Code != null)
                {
                    var cguid = PostIt.Code.CodeRow.CodeGuid;
                    OliUser.Stamm.CopyShortCutsToCode(scguid, cguid);
                    PostIt.ShowCode(cguid);

                    WortraumController1.ZellBuilder = PostIt.Code.ZellBuilder;

                    OliUser.Nachricht = "ShortCuts auf Markierung kopiert";

                    //TODO: Kathrin: �bernehmen
                    OliUser.Stamm.ShortCuts = null;
                }
                OliUser.Stamm.MyShortCuts = null;
            }

            //			// Delete ShortCuts
            //			if(e.CommandName == "del")
            //			{
            //				ShortCutsDataGrid.SelectedIndex = -1;
            //				ShortCutsDataGrid.EditItemIndex = -1;
            //				ShortCuts sc = new ShortCuts(this.OliUser.Stamm, scguid);
            //				sc.ShortCutsRow.Delete();
            //				sc.UpdateShortCuts();
            //				this.OliUser.Stamm.MyShortCuts = null;
            //				this.OliUser.Stamm.ShortCuts = null;
            //				this.OliUser.Nachricht = "ShortCuts gel�scht";
            //
            //			}
        }

        //		// NeuShortCutsButton_Click()
        //		private void NeuShortCutsButton_Click(object sender, System.EventArgs e)
        //		{
        //			this.OliUser.Stamm.NewShortCuts();
        //			this.OliUser.Stamm.MyShortCuts = null;
        //		}

        private void CodeDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ShortCutsDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Abgleich dieser Markierung gegen alle Angler
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void FischenImageButton_Click(object sender, EventArgs e)
        {
            // Code Kommentar updaten
            if (OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
            {
                // Kommentar updaten
                if (KommentarTextBox.Text != PostIt.Code.CodeRow.Kommentar)
                {
                    PostIt.Code.CodeRow.Kommentar = KommentarTextBox.Text;
                    PostIt.Code.UpdateCode();
                }
            }

            if (OliUser.Stamm != null &&
                PostIt != null &&
                PostIt.Code != null)
            {
                var sw = Stopwatch.StartNew();
                var f = new Fischer();
                f.Fischen(PostIt.Code.CodeRow.CodeGuid, Guid.Empty);
                var ts = sw.Elapsed.TotalMilliseconds;

                // AppInsights
                var telemetry = new TelemetryClient();
                var props = new Dictionary<string, string>()
                {
                    {"Origin","Code" },
                    {"StammGuid", OliUser.Stamm.StammRow.StammGuid.ToString()},
                    {"Stamm", OliUser.Stamm.StammRow.Stamm},
                    {"PostItGuid", PostIt.PostItRow.PostItGuid.ToString()},
                    {"PostIt.Title", PostIt.PostItRow["Titel"].ToString()},
                    { "CodeGuid", PostIt.Code.CodeRow.CodeGuid.ToString() }
                };
                var metrics = new Dictionary<string, double>()
                {
                    {"ExecutionTime", ts}
                };
                telemetry.TrackEvent("Fischen", props, metrics);

                // neue Anzeigen
                var cguid = PostIt.Code.CodeRow.CodeGuid;
                var pguid = PostIt.PostItRow.PostItGuid;
                OliUser.Stamm.MyPostIt = null;
                OliUser.Stamm.ShowPostIt(pguid);
                OliUser.Stamm.PostIt.ShowCode(cguid);

                // user.Nachricht
                OliUser.Nachricht =  PostIt.MyEmpfaenger.Count + " Angler";
                OliUser.Stamm.ShortCuts = null;
                Response.Redirect("~/Sites/PostItAnglerSite.aspx");
            }
        }

        /// <summary>
        ///     l�schen dieser Markierung
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        private void DelLinkButton_Click(object sender, EventArgs e)
        {
            if (OliUser.Stamm.BinIchEingeloggt && PostIt.BinIchMeinPostIt)
            {
                PostIt.DeleteCode(PostIt.Code.CodeRow.CodeGuid);
                PostIt.Code = null;
                PostIt.MyCode = null;
                Response.Redirect("~/Sites/PostItCodeSite.aspx");
            }
        }

        /// <summary>
        ///     bricht den ausgew�hlten ShortCut ab
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ShortCutsDataGrid.EditItemIndex = -1;
            Stamm.ShortCuts = null;
        }

        private void RdfImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Code.rdf\"");
            Response.Write(PostIt.Code.MakeCodeRDF());
            Response.End();
        }
    }
}
