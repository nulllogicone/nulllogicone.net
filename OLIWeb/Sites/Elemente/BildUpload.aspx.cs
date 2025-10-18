// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Azure.Storage.Blobs;
using OliWeb.Klassen;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     Eine Seite, die einem Stamm ermöglicht, eigene Bilder in seinen Ordner
    ///     von /images/OliUpload/StammGuid/ zu legen um sich, seine Nachrichten
    ///     oder Antworten zu illustrieren.
    /// </summary>
    public partial class BildUpload : MasterStammPage
    {
        private readonly string[] _imageExtensions = {"jpg", "gif", "png", "bmp", "ico"};
        private string sguid;

        protected BlobContainerClient BlobContainer;
        // Eigenschaften
        // -------------


        // Member
        // ------
        //		OliUser user;

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

        // Methoden
        // --------

        // Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            BilderDataGrid.DeleteCommand += BilderDataGrid_DeleteCommand;

            sguid = OliUser.Stamm.StammRow.StammGuid.ToString().ToLower();
            if (!IsPostBack)
            {
                FillListBox1();
            }
        }

        // Button1_ServerClick()
        protected void Button1_ServerClick(object sender, EventArgs e)
        {   
            var file = File1.PostedFile;

            if (file != null && file.ContentLength <= 500000)
            {
                Helper.UploadFileToCloudStorage(OliUser.Stamm, file);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                OliUser.Nachricht = "< 500 KB";
            }
        }

        /// <summary>
        ///     // --------------------------------
        ///     // Hilfsfunktion zum ListBox Füllen
        ///     // --------------------------------
        /// </summary>
        private void FillListBox1()
        {
            try
            {
                BlobContainer = Helper.GetContainerClient();

                var blobs =
                    BlobContainer.GetBlobs(prefix: sguid)
                    .Where(b => _imageExtensions.Contains(b.Name.Split('.').Last().ToLower())).ToList();

                BilderDataGrid.DataSource = blobs;
                DataBind();

                if (!blobs.Any())
                {
                    var blobName = sguid + "/user.info";
                    var blobRef = BlobContainer.GetBlobClient(blobName);
                    blobRef.Upload(new BinaryData("Stamm: " + OliUser.Stamm.StammRow.Stamm));

                    OrdnerLabel.Text = "<br /><font size=1>Keine Bilder vorhanden</font>";
                }
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }

    

        // BilderDataGrid_DeleteCommand()
        private void BilderDataGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            var l = (Label) e.Item.FindControl("DateiName");
            var blobName = sguid + "/" + l.Text;
            var container = Helper.GetContainerClient();
            var blobRef = container.GetBlobClient(blobName);
            blobRef.DeleteIfExists();
            FillListBox1();
        }
    }
}