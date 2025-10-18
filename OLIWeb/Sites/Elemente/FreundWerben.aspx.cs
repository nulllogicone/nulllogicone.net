// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using OliWeb.Klassen;
using System;
using System.Net.Mail;
using System.Text;

namespace OliWeb.Sites.Elemente
{
    public partial class FreundWerben : MasterStammPage
    {
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

        // Member
        // ------
        //		OliUser user;

        // Eigenschaften
        // -------------

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

            // sonst muss man eingeloggt sein
            if (!OliUser.Stamm.BinIchEingeloggt)
            {
                OliUser.Nachricht = "login required";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            //			user = SessionManager.Instance().OliUser;
            Label1.Text = "Hallo " + OliUser.Stamm.StammRow.Stamm;
            VonTextBox.Text = OliUser.Stamm.StammRow.eMail;

            if (!IsPostBack)
            {
                var sb = new StringBuilder();
                sb.Append("Hallo,\n\n");
                sb.Append("Ich habe gerade eine neue Website entdeckt. \n");
                sb.Append("OLI-it ist ein neues, etwas anderes Nachrichtensystem.\n\n");
                sb.Append("Einfach anschauen: https://www.oli-it.com \n\n");
                sb.Append(
                    "Um dich anzumelden, verwende bitte den Link, dann bekomme ich Punkte (du bekommst auch ein Startguthaben geschenkt) \n\n");
                sb.Append("~/Sites/Elemente/NeuAnmelden.aspx?von=" +
                          OliUser.Stamm.StammRow.StammGuid);
                sb.Append("\n\nLieben Gruss und viel Spass");
                sb.Append("\n\n" + OliUser.Stamm.StammRow.Stamm);

                BetreffTextBox.Text = "Schau Dir mal OLI-it an";
                BodyTextBox.Text = sb.ToString();
            }
        }

        protected void SendenButton_Click(object sender, EventArgs e)
        {
            // TODO: remove all SMTP configuration from this method!!!
            // -------------------------------------------------------
            if (IsValid)
            {
                var mm = new MailMessage
                {
                    From = new MailAddress(VonTextBox.Text)
                };
                mm.To.Add(new MailAddress(AnTextBox.Text));
                mm.Bcc.Add(new MailAddress("info@oli-it.com"));
                mm.Subject = BetreffTextBox.Text;
                mm.Body = BodyTextBox.Text;

                var smtClient = new SmtpClient();
                smtClient.Send(mm);

                OliUser.Nachricht = "Vielen Dank für die Freundschaftswerbung";
                Helper.RedirectToSite();
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Helper.RedirectToSite();
        }
    }
}