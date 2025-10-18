// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web;
using OliEngine;
using OliWeb.Klassen;
using System.Net.Mail;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     MailSchreiben.
    /// </summary>
    public partial class MailSchreiben : MasterStammPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StammLabel.Text = OliUser.Stamm.StammRow.Stamm;
            StammTxtLabel.Text = OliUser.Stamm.StammRow.Stamm;
            IPLabel.Text = " (" + Request.UserHostAddress + ") ";
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

        protected void SendenButton_Click(object sender, EventArgs e)
        {
            string absender = VonTextBox.Text;

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("info@oli-it.com","OLI-it");
	        mm.To.Add(new MailAddress(HttpUtility.HtmlDecode(OliUser.Stamm.StammRow.eMail)));
            mm.Bcc.Add(new MailAddress("info@oli-it.com"));
			mm.Subject = "[OLI-it] " + HttpUtility.HtmlEncode(BetreffTextBox.Text);
            mm.Body = "From: " + absender + Environment.NewLine;
            mm.Body += BodyTextBox.Text;
            mm.Body += OliCommon.EmailSignature;
            mm.Body += Request.UserHostAddress;

	        var smtpClient = new SmtpClient();
			smtpClient.Send(mm);

            OliUser.Nachricht = "Email wurde versendet";
            Helper.RedirectToSite();
        }
    }
}