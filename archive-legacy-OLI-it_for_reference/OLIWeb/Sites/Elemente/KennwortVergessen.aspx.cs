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
using System.Text;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     KennwortVergessen.
    /// </summary>
    public partial class KennwortVergessen : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StammLabel.Text = OliUser.Stamm.StammRow.Stamm;
        }



        protected void SendenButton_Click(object sender, EventArgs e)
        {
            // TODO: What a hack! Horrible!
            // We should never have plain text passwords in the database.
            // The following email html concatenation must be refactored.

            
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("info@oli-it.com");
			mm.To.Add(new MailAddress(HttpUtility.HtmlDecode(OliUser.Stamm.StammRow.eMail)));
			mm.Bcc.Add(new MailAddress("info@oli-it.com")); 
            mm.Subject = "[OLI-it] Kennwort vergessen";
            mm.Body = "Hallo " + HttpUtility.HtmlDecode(OliUser.Stamm.StammRow.Stamm) + Environment.NewLine;
            mm.Body +=
                "auf der Seite von OLI-it wurde der Button geklickt, da� Sie ihr Kennwort neu zugeschickt bekommen m�chten.";
            mm.Body += "Es lautet: " + HttpUtility.HtmlDecode(OliUser.Stamm.StammRow.Unterschrift) + Environment.NewLine;
            mm.Body +=
                "Falls Sie es nicht selber waren oder da das Kennwort im Klartext versendet wird, empfiehlt es sich das Kennwort regelm��ig zu �ndern." + Environment.NewLine;
            mm.Body += "Mit freundlichen Gr��en" + Environment.NewLine;
            mm.Body += "Ihr OLI-it Team";
            mm.Body += OliCommon.EmailSignature;
            if (Request.UserHostAddress != null) mm.Body += Request.UserHostAddress;
            mm.BodyEncoding = Encoding.UTF8;

        
	        var smtpClient = new SmtpClient();
			smtpClient.Send(mm);
            OliUser.Nachricht = "Kennwort als Email verschickt";

            Helper.RedirectToSite();
        }
    }
}
