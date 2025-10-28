// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using OliEngine;
using OliEngine.DataSetTypes;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    public partial class PostItMaker : MasterPostItPage
    {


        protected BildPicker BildPicker1;
        protected KlickBild KlickBild1;

        private enum PanelEnum
        {
            Formulieren,
            Illustrieren,
            Frankieren,
            Frist,
            Markier
        }

        // Eigenschaften
        // -------------

        // Methoden
        // --------

        /// <summary>
        ///     Wenn die BasisBasePage Initialisiert wird, wird 
        ///     auf das vorhandensein von Stamm und PostIt gepr�ft.
        ///     Auf dieser Seite muss er auch noch eingeloggt und es muss 
        ///     sein PostIt sein oder ein neues.
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            if (OliUser.Eingeloggt)
            {
                // wenn ein neues PostIt erstellt werden soll
                if (PostIt.PostItRow.RowState == DataRowState.Added)
                {
                    OkButton.Text = "hinzuf�gen";
                }
                else
                {
                    // wenn es nicht neu und nicht mein PostIt ist 
                    // -> Editseite canceln und zur�ck zur PostIt-Anzeige
                    if (PostIt.PostItRow.RowState != DataRowState.Added &&
                        !OliUser.Stamm.PostIt.BinIchMeinPostIt)
                    {
                        OliUser.Nachricht =
                            "Das ist nicht eine Nachricht des aktuell angezeigten Stammes oder sie ist nicht neu";
                        Response.Redirect(NOT_MYPOSTIT_REDIRECT);
                    }
                }
            }
                // sonst muss man eingeloggt sein
            else
            {
                OliUser.Nachricht = "login required";
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            // BildPicker1
            BildPicker1.BildSelect += OnBildSelect;

            if (OliUser.Stamm != null)
            {
                // neues PostIt erstellen
                if (PostIt.PostItRow.RowState == DataRowState.Added)
                {
                    OliUser.Nachricht = "neue Nachricht default";
//					PostIt = this.OliUser.EingeloggterStamm.NewPostIt();

                    FristLabel.Text = DateTime.Now.AddDays(OliCommon.FristOffset).ToString();
                    Calendar1.SelectedDate = DateTime.Now.AddDays(OliCommon.FristOffset);
                    Calendar1.VisibleDate = DateTime.Now.AddDays(OliCommon.FristOffset);
                }

                // bestehendes PostIt bearbeiten
                if (PostIt != null)
                {
                    // darf man den Nachrichten Typ ver�ndern
                    TypDropDownList.Visible = !Stamm.Extras.ExtrasRow.TxtOnly;

                    // PostItRow
                    PostItDataSet.PostItRow pr = PostIt.PostItRow;

                    KlickBild1.Breite = 70;
                    KlickBild1.BildName = pr.IsDateiNull() ? "" : pr.Datei;

                    // Beim ersten Laden
                    if (!IsPostBack)
                    {
                        // Formulieren
//						Editor.Text = pr.PostIt;	
                        if (pr.Typ == "txt")
                        {
                            PostItTextBox.Text = HttpUtility.HtmlDecode(pr.PostIt);
                        }
                        else
                        {
                            PostItTextBox.Text = pr.PostIt;
                        }

                        TitelTextBox.Text = HttpUtility.HtmlDecode(pr.IsTitelNull() ? "" : pr.Titel);
                        GesamtKooKLabel.Text = OliUtil.MakeRedKook(pr.KooK);

                        // Illustrieren
                        TypDropDownList.SelectedValue = pr.Typ;
                        DateiTextBox.Text = pr.IsDateiNull() ? "" : pr.Datei;
                        UrlTextBox.Text = HttpUtility.HtmlDecode(pr.IsURLNull() ? "" : pr.URL);

                        // Frankieren
                        KooKLabel.Text = OliUtil.MakeRedKook(OliUser.Stamm.PostIt.StammZahlt);

                        // Frist
                        FristLabel.Text = OliUtil.MakeDateTimeDiff(PostIt.StammFrist);
                        UhrzeitTextBox.Text = PostIt.StammFrist.TimeOfDay.ToString().Substring(0, 8);
                        if (PostIt.StammFrist > DateTime.MinValue)
                        {
                            Calendar1.SelectedDate = PostIt.StammFrist.Date;
                            Calendar1.VisibleDate = PostIt.StammFrist.Date;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     speichert das PostIt, setzt Frist und Frankiert (danach Wert im Formular auf 0) und
        ///     l�dt die relevanten Tabellen neu.
        /// </summary>
        /// <returns> </returns>
        private bool PostItSpeichern()
        {
            // Feldwerte setzen 
            PostItDataSet.PostItRow pr = PostIt.PostItRow;

            if (PostItTextBox.Text.Length > 0)
            {
                // zuweisen
                pr.Titel = HttpUtility.HtmlEncode(TitelTextBox.Text);
                pr.Typ = TypDropDownList.SelectedValue;
                if (pr.Typ == "txt")
                {
                    pr.PostIt = HttpUtility.HtmlEncode(PostItTextBox.Text);
                }
                else
                {
                    pr.PostIt = PostItTextBox.Text;
                }

                pr.Datei = DateiTextBox.Text;

                if (UrlTextBox.Text.Length > 0)
                {
                    if (UrlTextBox.Text.StartsWith("http"))
                    {
                        pr.URL = HttpUtility.HtmlEncode(UrlTextBox.Text);
                    }
                    else
                    {
                        pr.URL = "http://" + HttpUtility.HtmlEncode(UrlTextBox.Text);
                    }
                }

                // und update
                PostIt.UpdatePostIt();

                // KooK und Frist
                decimal betrag = decimal.Parse(BetragTextBox.Text.Replace(".", ","));

                DateTime frist = Calendar1.SelectedDate.Date;
                //UhrzeitTextBox.Text);
                // PostIt.Frankieren() !
                if (frist > DateTime.Now)
                {
                    PostIt.Frankieren(OliUser.EingeloggterStamm, betrag, frist);
                    BetragTextBox.Text = "0";
                }
                else
                {
                    OliUser.Nachricht = "Die Frist ist abgelaufen";
                }

                OliUser.Nachricht = "message updated";

                // Werte merken
                Guid sguid = OliUser.Stamm.StammRow.StammGuid;
                Guid pguid = PostIt.PostItRow.PostItGuid;
                Guid cguid = Guid.Empty;
                if (PostIt.Code != null)
                {
                    cguid = PostIt.Code.CodeRow.CodeGuid;
                }

                // Werte neu laden
                OliUser.ShowStamm(sguid);
                OliUser.Stamm.MyPostIt = null;
                OliUser.Stamm.ShowPostIt(pguid);
                if (cguid != Guid.Empty)
                {
                    PostIt.ShowCode(cguid);
                }

                return true;
            }
            else
            {
                OliUser.Nachricht = "Nachricht darf nicht leer sein";
                return false;
            }
        }

        protected void FormulierenLinkButton_Click(object sender, EventArgs e)
        {
            PostItSpeichern();
            ShowPanel(PanelEnum.Formulieren);
        }

        protected void IllustrierenLinkButton_Click(object sender, EventArgs e)
        {
            PostItSpeichern();
            ShowPanel(PanelEnum.Illustrieren);
        }

        protected void FrankierenLinkButton_Click(object sender, EventArgs e)
        {
            PostItSpeichern();
            ShowPanel(PanelEnum.Frankieren);
        }

        protected void FristLinkButton_Click(object sender, EventArgs e)
        {
            PostItSpeichern();
            ShowPanel(PanelEnum.Frist);
        }

        protected void MarkierLinkButton_Click(object sender, EventArgs e)
        {
            PostItSpeichern();
            ShowPanel(PanelEnum.Markier);
        }

        // OkButton_Click()
        protected void OkButton_Click(object sender, EventArgs e)
        {
            bool result = PostItSpeichern();
            // wenn speichern erfolgreich -> weiterleiten
            if (result)
            {
                // wenn noch keine Empf�nger -> CodeSite
                if (PostIt.MyEmpfaenger.Count == 0)
                {
                    OliUser.Nachricht = "Die Nachricht hat noch keine Empf�nger.";
                    Response.Redirect("~/Sites/PostItCodeSite.aspx");
                }
                else
                {
                    Response.Redirect("~/Sites/PostItSite.aspx");
                }
            }
        }

        // OnBildSelect
        private void OnBildSelect(BildPicker source, BildSelectEventArgs e)
        {
            DateiTextBox.Text = "/" + OliUser.Stamm.StammRow.StammGuid + "/" + e.dateiName;
            KlickBild1.BildName = "/" + OliUser.Stamm.StammRow.StammGuid + "/" + e.dateiName;
        }

        // Hilfsfunktionen
        // ---------------

        private void ShowPanel(PanelEnum panel)
        {
            AllePanels(false);
            AlleLinks(true);
            ContentPlaceHolder c = (ContentPlaceHolder) Master.FindControl("ContentPlaceHolder1");
            Panel p = (Panel) c.FindControl(panel + "Panel");
            p.Visible = true;
            LinkButton lb = (LinkButton) c.FindControl(panel + "LinkButton");
            lb.Enabled = false;
        }

        // AlleLinks()
        private void AlleLinks(bool enabled)
        {
            FormulierenLinkButton.Enabled = enabled;
            IllustrierenLinkButton.Enabled = enabled;
            FrankierenLinkButton.Enabled = enabled;
            FristLinkButton.Enabled = enabled;
            MarkierLinkButton.Enabled = enabled;
        }

        // AllePanels()
        private void AllePanels(bool visible)
        {
            FormulierenPanel.Visible = visible;
            IllustrierenPanel.Visible = visible;
            FrankierenPanel.Visible = visible;
            FristPanel.Visible = visible;
            MarkierPanel.Visible = visible;
        }

        // CancelButton_Click()
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            if (PostIt.PostItRow.RowState == DataRowState.Added)
            {
                PostIt = null;
            }
//			Helper.RedirectToSite();
            Response.Redirect("~/Sites/PostItSite.aspx");
        }
    }
}
