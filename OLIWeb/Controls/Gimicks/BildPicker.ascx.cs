// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using OliEngine;
using OliWeb.Klassen;

namespace OliWeb.Controls.Gimicks
{
    /// <summary>
    ///     gekachelte Ansicht der Bilder aus dem dem Stamm-Unterordner mit Möglichkeit
    ///     für Upload und Auswahl.
    /// </summary>
    public partial class BildPicker : MasterControl
    {
        /// <summary>
        ///     Wenn ein Bild ausgewählt wurde, kann das Ereignis behandelt werden.
        /// </summary>
        public delegate void BildSelectEventHandler(BildPicker sender, BildSelectEventArgs e);

        /// <summary>
        ///     Größenbeschränkung für Bilddateien, die auf den Server hochgeladen werden sollen
        /// </summary>
        protected const int MAXFILESIZE = 200000;

        private readonly string[] _imageExtensions = { "jpg", "gif", "png", "bmp", "ico" };

        /// <summary>
        ///     stellt Bilder gekachelt dar und feuert bei Klick ein Event
        /// </summary>
        protected DataList BildDataList;

        protected BlobContainerClient BlobContainer;

        /// <summary>
        ///     Dateiauswahl Textfeld mit Button (Html-INPUT)
        /// </summary>
        protected HtmlInputFile FileSelect;

        /// <summary>
        ///     kleiner, grüner Pfeil
        /// </summary>
        protected Image OkImage;

        private string sguid;

        /// <summary>
        ///     stößt den Bildupload an
        /// </summary>
        protected HtmlInputButton UploadButton;

        /// <summary>
        ///     Das Ereignis wird gefeuert wenn ein Bild ausgewählt wurde.
        /// </summary>
        public event BildSelectEventHandler BildSelect;

        /// <summary>
        ///     beim ersten Laden des Controls wird die ListBox gefüllt
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BildDataList.ItemCommand += BildDataList_ItemCommand;

            sguid = OliUser.Stamm.StammRow.StammGuid.ToString().ToLower();

            if (!IsPostBack)
            {
                fillListBox();
            }
        }

        /// <summary>
        ///     durchsucht das StammDirectory nach Bildern (jpg, gif, bmp, ico) und
        ///     bindet ein Array mit den Dateipfaden an die BildDataList.
        /// </summary>
        private void fillListBox()
        {
            try
            {
                BlobContainer = Helper.GetContainerClient();

                var blobs =
                    BlobContainer.GetBlobs(prefix: sguid)
                      .Where(b => _imageExtensions.Contains(b.Name.Split('.').Last().ToLower())).ToList();

                BildDataList.DataSource = blobs;

                DataBind();
            }
            catch (Exception e)
            {
                Response.Write("BildPicker: " + e.Message);
            }
        }

        /// <summary>
        ///     wenn auf ein Bild geklickt wird und das Ereignis abboniert wurde,
        ///     wird als Event-Argument der Dateiname eingestellt und gefeuert.
        /// </summary>
        /// <param name="source"> </param>
        /// <param name="e"> </param>
        private void BildDataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditLinkButton")
            {
                UploadPanel.Visible = true;
            }
            else
            {
                if (BildSelect != null)
                {
                    var dnl = (Label)e.Item.FindControl("DateiName");
                    BildSelect(this, new BildSelectEventArgs(dnl.Text));
                }
            }
        }

        /// <summary>
        ///     nachdem ein Bild auf der lokalen Platte ausgewählt wurde, muss man auf den
        ///     UploadButton klicken. Wenn es kleiner als MAXFILESIZE ist, wird es in den
        ///     Unterordner des Stammes auf dem Webserver gespeichert.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void UploadButton_ServerClick(object sender, EventArgs e)
        {
            var file = FileSelect.PostedFile;
            if (file != null && file.ContentLength <= MAXFILESIZE)
            {
                Helper.UploadFileToCloudStorage(OliUser.Stamm, file);

                Response.Redirect(Request.RawUrl);

                fillListBox();

                if (BildSelect != null)
                {
                    BildSelect(this, new BildSelectEventArgs(Path.GetFileName(FileSelect.PostedFile.FileName)));
                }
            }
            else
            {
                OliUser.Nachricht = "< 500 kB";
            }
        }

    

    }

    /// <summary>
    ///     Wenn ein Bild ausgewählt wurde, wird ein Ereignis ausgelöst und der Dateiname
    ///     als Argument übergeben.
    /// </summary>
    public class BildSelectEventArgs : EventArgs
    {
        /// <summary>
        ///     der Name des ausgewählten Bildes
        /// </summary>
        public string dateiName;

        public BildSelectEventArgs(string dateiName)
        {
            var idxAfterCloudStorage = OliCommon.bilderOrdner.Length + 38;
            this.dateiName = dateiName.Substring(idxAfterCloudStorage);
        }
    }
}