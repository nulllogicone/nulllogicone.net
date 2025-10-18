// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using OliEngine;
using OliEngine.OliDataAccess;
using OliWeb.Controls.Gimicks;
using OliWeb.Klassen;
using System;
using System.Data;
using System.Web;

namespace OliWeb.Sites.Edit
{
    /// <summary>
    ///     StammEdit.
    /// </summary>
    public partial class StammEdit : MasterStammPage
    {
        protected KlickBild KlickBild1;

        // Member
        // ------

        protected BildPicker BildPicker1;

        // Load
        protected void Page_Load(object sender, EventArgs e)
        {
            // BildPicker1
            BildPicker1.BildSelect += OnBildSelect;

            if (!IsPostBack)
            {
                BildPicker1.Visible = false;
            }
        }

        // OnPreRender()
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Malen();
        }

        // Methoden
        // --------

        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisBasePage Initialisiert wird, wird 
        ///     auf das vorhandensein eine Stamm geprüft.
        ///     Auf dieser Seite muss er auch noch eingeloggt sein
        ///     oder über <c>cmd=new</c> hinzugefügt werden sollen.
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // wenn ein neuer Stamm erstellt werden soll
            if (Request.QueryString["cmd"] != null &&
                Request.QueryString["cmd"] == "newS")
            {
                UpdateButton.Text = "hinzufügen";
                ExtrasHyperLink.Enabled = false;
                ExtrasHyperLink.NavigateUrl = "";
            }
            // sonst muss man als echter Account eingeloggt sein
            else if (OliUser.Stamm.BinIchEingeloggt &&
                     Stamm.StammRow.Stamm.ToLower() != "gast" &&
                     Stamm.StammRow.Stamm.ToLower() != "test")
            {
                // alles OK (weder test noch gast Account und eingeloggt)
            }
            else
            {
                OliUser.Nachricht = "login required";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        // Malen
        protected void Malen()
        {
            var s = OliUser.Stamm.StammRow;

            // Neue Stamm Row
            if (s.RowState == DataRowState.Added)
            {
                UpdateButton.Text = "hinzufügen";
            }
            else
            {
                UpdateButton.Text = "update";

                // Editieransicht beschriften
                if (!IsPostBack)
                {
                    StammTextBox.Text = HttpUtility.HtmlDecode(s.Stamm);
                    KennwortTextBox.Text = HttpUtility.HtmlDecode(s.IsUnterschriftNull() ? "" : s.Unterschrift);
                    BeschreibungTextBox.Text = HttpUtility.HtmlDecode(s.IsBeschreibungNull() ? "" : s.Beschreibung);
                    BildTextBox.Text = HttpUtility.HtmlDecode(s.IsDateiNull() ? "" : s.Datei);
                    KlickBild1.BildName = HttpUtility.HtmlDecode(s.IsDateiNull() ? "" : s.Datei);
                    EmailTextBox.Text = HttpUtility.HtmlDecode(s.IseMailNull() ? "" : s.eMail);
                    LinkTextBox.Text = HttpUtility.HtmlDecode(s.IsLinkNull() ? "" : s.Link);
                }
            }
        }

        // Ereignisse
        // ----------

        // OnBildSelect
        private void OnBildSelect(BildPicker source, BildSelectEventArgs e)
        {
            var bildname = "/" + OliUser.Stamm.StammRow.StammGuid + "/" + e.dateiName;
            BildTextBox.Text = bildname;
            KlickBild1.BildName = bildname;
            BildPicker1.Visible = false;
        }


        protected void BildPickerLinkButton_Click(object sender, EventArgs e)
        {
            BildPicker1.Visible = !BildPicker1.Visible;
        }


        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var s = OliUser.Stamm.StammRow;

                // Namen merken falls update fehlschlägt
                var oldname = s.Stamm;
                // Werte setzen
                s.Stamm = HttpUtility.HtmlEncode(StammTextBox.Text);
                s.Unterschrift = HttpUtility.HtmlEncode(KennwortTextBox.Text);
                s.Beschreibung = HttpUtility.HtmlEncode(BeschreibungTextBox.Text);
                s.Datei = HttpUtility.HtmlEncode(BildTextBox.Text);
                s.eMail = HttpUtility.HtmlEncode(EmailTextBox.Text);
                if (LinkTextBox.Text.Length > 0)
                {
                    if (LinkTextBox.Text.StartsWith("http"))
                    {
                        s.Link = HttpUtility.HtmlEncode(LinkTextBox.Text);
                    }
                    else
                    {
                        s.Link = HttpUtility.HtmlEncode("http://" + LinkTextBox.Text);
                    }
                }
                else
                {
                    s.Link = "";
                }

                var isNewStammRegistration = (s.RowState == DataRowState.Added);
                try
                {
                    if (OliUser.Freund != null)
                    {
                        OliUser.Stamm.UpdateStamm(OliUser.Freund);
                        OliUser.Freund = null;
                    }
                    else
                    {
                        OliUser.Stamm.UpdateStamm();
                    }

                    // Add Start Kook to StammKonto ! BUG ! TODO: only do it for new stamm
                    if (isNewStammRegistration)
                    {
                        var sk = new StammKonto(s.StammGuid);
                        sk.AddPosten(OliCommon.DefaultBoundKooK, "Startwert");
                    }
                    // user.Nachricht
                    OliUser.Nachricht = s.Stamm + " aktualisiert";

                    // Alle Buttons enablen
                    //					Helper.SetAllButtons(this.Page.Controls, true);

                    Response.Write("<h1>das editieren sollte beendet sein</h1>");
                }
                catch
                {
                    OliUser.Nachricht = "<font color=red>Benutzer mit diesem Namen ist bereits vorhanden</font>";
                    s.Stamm = oldname;
                }

                //				Helper.RedirectToSite();
                Response.Redirect("~/Sites/StammSite.aspx");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sites/StammSite.aspx");
        }
    }
}