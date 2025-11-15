// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web;
using OliEngine;
using OliEngine.DataSetTypes;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     TopLabEdit.
    /// </summary>
    public partial class TopLabEdit : MasterStammPage
    {
        protected KlickBild KlickBild1;
        protected BildPicker BildPicker1;

        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisBasePage Initialisiert wird, wird 
        ///     auf das vorhandensein eine Stamm gepr�ft.
        ///     Auf dieser Seite muss er auch noch eingeloggt sein
        ///     und ein TopLab bestehen - oder das cmd=new �bergeben werden.
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // eingeloggter Stamm kann neuen Angler erstellen
            if (OliUser.Stamm.BinIchEingeloggt &&
                Request["cmd"] != null &&
                Request["cmd"] == "newT")
            {
                TopLab = OliUser.EingeloggterStamm.NewTopLab(PostIt);
            }

            // oder bestehenden Angler editieren
            if (!OliUser.Stamm.BinIchEingeloggt)
            {
                OliUser.Nachricht = "login required";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        /// <summary>
        ///     laden ser Seite
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // BildPicker1
            BildPicker1.BildSelect += OnBildSelect;

            // darf man den Nachrichten Typ ver�ndern
            TypDropDownList.Visible = !Stamm.Extras.ExtrasRow.TxtOnly;

            if (!IsPostBack)
            {
                Malen();
                BildPicker1.Visible = false;
            }
        }

        // Malen
        protected void Malen()
        {
            if (OliUser.Stamm != null)
            {
                // mit TopLab
                if (TopLab != null)
                {
                    TopLabDataSet.TopLabRow fbr = TopLab.TopLabRow;

                    // Wenn TopLab eine neue Reihe ist => editierMode
                    if (fbr.RowState == DataRowState.Added)
                    {
                        UpdateButton.Text = "hinzuf�gen";

                        //						// Alle Buttons disablen
                        //						Helper.SetAllButtons(this.Page.Controls, false);
                        //						BildPickerLinkButton.Enabled = true;
                        //						Helper.SetAllButtons(BildPicker1.Controls,true);
                        //						UpdateButton.Enabled = true;
                        //						CancelButton.Enabled = true;

                        // bei Enter UpdateButton
                        Page.ClientScript.RegisterHiddenField("__EVENTTARGET", "UpdateButton");
                    }
                    else
                    {
                        UpdateButton.Text = "update";
                    }

                    if (!IsPostBack)
                    {
                        TitelTextBox.Text = HttpUtility.HtmlDecode(fbr.IsTitelNull() ? "" : fbr.Titel);

                        TypDropDownList.SelectedValue = fbr.Typ;
                        if (fbr.Typ == "txt")
                        {
                            TopLabTextBox.Text = HttpUtility.HtmlDecode(fbr.TopLab);
                        }
                        else
                        {
                            TopLabTextBox.Text = fbr.TopLab;
                        }
                        URLTextBox.Text = HttpUtility.HtmlDecode(fbr.IsURLNull() ? "" : fbr.URL);

                        if (!fbr.IsDateiNull() && fbr.Datei.Length > 1)
                        {
                            DateiImage.ImageUrl = OliUtil.MakeImageSrc(fbr.Datei);
                            DateiImage.Visible = true;
                            DateiTextBox.Text = fbr.Datei;
                        }
                    }
                }
            }
        }

        // Ereignisse
        // ----------

        // OnBildSelect
        private void OnBildSelect(BildPicker source, BildSelectEventArgs e)
        {
            DateiTextBox.Text = "/" + OliUser.Stamm.StammRow.StammGuid + "/" + e.dateiName;
            DateiImage.ImageUrl = OliCommon.bilderOrdner + "/" + OliUser.Stamm.StammRow.StammGuid + "/" + e.dateiName;
            DateiImage.Visible = true;
            BildPicker1.Visible = false;
        }

        // Methoden
        // --------

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        protected void BildPickerLinkButton_Click(object sender, EventArgs e)
        {
            BildPicker1.Visible = true;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // ich will Dich k�ssen
            // ~~~~~~~~~~~~~~~~~~~~
            if (TopLabTextBox.Text.Length > 0)
            {
                TopLabDataSet.TopLabRow tlr = TopLab.TopLabRow;
                tlr.Typ = TypDropDownList.SelectedValue;
                tlr.Titel = HttpUtility.HtmlEncode(TitelTextBox.Text);
                if (tlr.Typ == "txt")
                {
                    tlr.TopLab = HttpUtility.HtmlEncode(TopLabTextBox.Text);
                }
                else
                {
                    tlr.TopLab = TopLabTextBox.Text;
                }
                tlr.Datei = DateiTextBox.Text;
                tlr.URL = HttpUtility.HtmlEncode(URLTextBox.Text);
                TopLab.UpdateTopLab();

                // Response.Redirect als GET-Request
                // damit die Post-Daten wirklich weg sind
                //			Helper.RedirectToSite();
                Response.Redirect("~/Sites/TopLabSite.aspx");
            }
            else
            {
                OliUser.Nachricht = "bitte schreiben Sie einen Antworttext";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sites/TopLabSite.aspx");
        }
    }
}
