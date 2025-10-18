// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web;
using OliEngine.DataSetTypes;
using OliWeb.Klassen;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     AnglerEdit.
    /// </summary>
    public partial class AnglerEdit : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Angler != null)
            {
                AnglerDataSet.AnglerRow a = Angler.AnglerRow;

                // neue Angler Reihe
                if (a.RowState == DataRowState.Added)
                {
                    UpdateButton.Text = "hinzufügen";

                    // Alle Buttons disablen
                    //						Helper.SetAllButtons(this.Page.Controls, false);
                    //						CancelButton.Enabled = true;
                    //						UpdateButton.Enabled = true;
                }
                else
                {
                    UpdateButton.Text = "update";
                }

                // beschriften
                if (!IsPostBack)
                {
                    AnglerTextBox.Text = HttpUtility.HtmlDecode(a.Angler);
                    BeschreibungTextBox.Text = HttpUtility.HtmlDecode(a.IsBeschreibungNull() ? "" : a.Beschreibung);
                }
            }
        }

        // Methoden
        // --------

        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisBasePage Initialisiert wird, wird 
        ///     auf das vorhandensein eine Stamm geprüft.
        ///     Auf dieser Seite muss er auch noch eingeloggt sein
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // eingeloggter Stamm kann neuen Angler erstellen
            if (OliUser.Stamm.BinIchEingeloggt &&
                Request["cmd"] != null &&
                Request["cmd"] == "newA")
            {
                // Cookie - Prüfung
                string sess = Session.SessionID;
                string prevsess = Request["prevSessionId"];

                if (prevsess != null
                    && prevsess.Length > 0
                    && prevsess != sess)
                {
                    Response.Redirect("~/NoFeature.aspx?nocookie=1");
                }

                OliUser.Stamm.Angler = OliUser.EingeloggterStamm.NewAngler();
                AnglerDataSet.AnglerRow a = OliUser.Stamm.Angler.AnglerRow;
            }

            // oder bestehenden Angler editieren
            if (!OliUser.Stamm.BinIchEingeloggt)
            {
                OliUser.Nachricht = "login required";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //			Helper.SetAllButtons(this.Page.Controls, true);

            // wenn neue AnglerRow zeig danach automatisch Filter
            bool zeigFilter = (Angler.AnglerRow.RowState == DataRowState.Added);

            // Felder füllen
            AnglerDataSet.AnglerRow a = Angler.AnglerRow;
            a.Angler = HttpUtility.HtmlEncode(AnglerTextBox.Text);
            a.Beschreibung = HttpUtility.HtmlEncode(BeschreibungTextBox.Text);
            Angler.UpdateAngler();

            // Ansicht auffrischen
            Guid aguid = Angler.AnglerRow.AnglerGuid;
            OliUser.Stamm.MyAngler = null;
            OliUser.Stamm.ShowAngler(aguid);

            // Wenn Angler neu war => Filter zeigen
            if (zeigFilter)
            {
                //				Angler.ShowWortraum = true;
                OliUser.Nachricht = "Bitte gewünschte Merkmale markieren";
                OliUser.Nachricht = "und dann die bunten Punkte klicken";
            }

            //			Helper.RedirectToSite();
            Response.Redirect("~/Sites/AnglerSite.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sites/AnglerSite.aspx");
        }
    }
}