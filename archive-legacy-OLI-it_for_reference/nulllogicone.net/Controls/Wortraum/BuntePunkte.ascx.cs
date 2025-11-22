// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Drawing;
using OliEngine;
using OliEngine.OliMiddleTier.OLIs;
using OliEngine.OliMiddleTier.ZellHaufen;
using OliWeb.Klassen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    BuntePunkte.
    ///</summary>
    public partial class BuntePunkte : System.Web.UI.UserControl
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }


        private void InitializeComponent()
        {
        }

        #endregion

        // Member
        // ------

//TODO: Eigenschaft draus machen
        public VerteilZelle zelle;
        private OliUser user;

        // Delegate und Event
        public delegate void BuntePunkteEventHandler(object sender, BuntePunkteEventArgs e);

        public event BuntePunkteEventHandler Update;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string[] mark = {"3-3", "3-2", "2-2", "2-0", "1-0"};

            SelectListBox.DataSource = mark;
            SelectListBox.DataBind();

            user = SessionManager.Instance().OliUser;
        }


        // Eigenschaften
        // -------------


        private bool ShowEdit
        {
            get { return (zelle.BuntePunkteEdit); }
            set { zelle.BuntePunkteEdit = value; }
        }

        public int VgbOlis
        {
            get { return (zelle.VgbOLIs); }
        }

        public int Vgbget
        {
            get { return (zelle.VgbGet); }
        }

        public int VgbIlos
        {
            get { return (zelle.VgbILOs); }
        }

        public int Vgbfit
        {
            get { return (zelle.VgbFit); }
        }

        public int PunktGroesse
        {
            set
            {
                OlisImageButton.Width = value;
                OlisImageButton.Height = value;
                GetImageButton.Width = value;
                GetImageButton.Height = value;
                IlosImageButton.Width = value;
                IlosImageButton.Height = value;
                FitImageButton.Width = value;
                FitImageButton.Height = value;
            }
        }


        // Methoden
        // --------

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            // Diese Eigenschaft muss eingestellt werden
            // wenn dieses Control verwendet wird.
            SelectListBox.Visible = ShowEdit;

            if (zelle is KnotenZelle)
            {
                if (!((KnotenZelle) zelle).MyRow.IsweiterBaumGuidNull())
                {
                    OlisImageButton.BackColor = Color.LightGray;
                    GetImageButton.BackColor = Color.LightGray;
                    IlosImageButton.BackColor = Color.LightGray;
                    FitImageButton.BackColor = Color.LightGray;

                    OlisImageButton.BorderColor = Color.LightGray;
                    GetImageButton.BorderColor = Color.LightGray;
                    IlosImageButton.BorderColor = Color.LightGray;
                    FitImageButton.BorderColor = Color.LightGray;
                }
            }

            // *******************************************************************************************
            // F�r die Verwendung des Wortraums auf den Seiten von nulllogicone.net
            // werden immer die bunten Punkte angezeigt !!
            // Deshalb ist die folgende Zuweisung immer auf true gesetzt worden
            // *******************************************************************************************
//			bool showBuntePunkte = user.Stamm.Extras.ExtrasRow.freakmode && user.Stamm.BinIchEingeloggt;
            bool showBuntePunkte = true;

            OlisImageButton.Visible = (VgbOlis >= 0 && showBuntePunkte);
            GetImageButton.Visible = (Vgbget >= 0 && showBuntePunkte);
            IlosImageButton.Visible = (VgbIlos >= 0 && showBuntePunkte);
            FitImageButton.Visible = (Vgbfit >= 0 && showBuntePunkte);


            OlisImageButton.ImageUrl = OliCommon.imagesOrdner + "punkte/Olis/" + VgbOlis + ".gif";
            GetImageButton.ImageUrl = OliCommon.imagesOrdner + "punkte/Ilos/" + Vgbget + ".gif";
            IlosImageButton.ImageUrl = OliCommon.imagesOrdner + "punkte/Ilos/" + VgbIlos + ".gif";
            FitImageButton.ImageUrl = OliCommon.imagesOrdner + "punkte/Olis/" + Vgbfit + ".gif";
        }

        // PunktImageButton_Click()
        protected void PunktImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (user.Stamm.BinIchEingeloggt)
            {
                SelectListBox.Visible = true;
                ShowEdit = true;
            }
        }

        // SelectListBox_SelectedIndexChanged()
        protected void SelectListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int verb = 0;
            int attrib = 0;

            switch (SelectListBox.SelectedItem.Value)
            {
                case "3-3":
                    verb = 3;
                    attrib = 3;
                    break;
                case "3-2":
                    verb = 3;
                    attrib = 2;
                    break;
                case "2-2":
                    verb = 2;
                    attrib = 2;
                    break;
                case "2-0":
                    verb = 2;
                    attrib = 0;
                    break;
                case "1-0":
                    verb = 1;
                    attrib = 0;
                    break;
            }

            // wieder unsichtbar
            SelectListBox.Visible = false;
            ShowEdit = false;

            // EreignisArgument erstellen
            BuntePunkteEventArgs bpea = new BuntePunkteEventArgs(verb, attrib);

            // Ereignis feuern
            if (Update != null)
            {
                Update(this, bpea);
            }
        }
    }


    // ***************************
    // Klasse BuntePunkteEventArgs
    // ***************************

    public class BuntePunkteEventArgs : EventArgs
    {
        public int verb;
        public int attrib;

        public BuntePunkteEventArgs(int verb, int attrib)
        {
            this.verb = verb;
            this.attrib = attrib;
        }
    }
}
