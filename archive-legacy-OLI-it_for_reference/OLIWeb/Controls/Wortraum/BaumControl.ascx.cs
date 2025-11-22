// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    Baum.
    ///</summary>
    public partial class BaumControl : UserControl
    {
        protected CheckBox ShowZweigeCheckBox;
        protected DataGrid ZweigeDataGrid;

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

//1		NKBZ nkbz;
        private BaumZelle myZelle;
        private bool secb;
        private int zeilenHoehe;

        // Eigenschaften
        // -------------

        // MyZelle
        public BaumZelle MyZelle
        {
            get { return (myZelle); }
            set
            {
                myZelle = value;
                Ebene = myZelle.Ebene;

                // wenn Zelle == neu dann EditMode
                if (myZelle.MyRow.RowState == DataRowState.Added)
                {
                    ShowEdit = true;
                    NeuButton.Enabled = false;
                }
            }
        }

        // Ebene
        private int Ebene
        {
            // setzt die Einr�ckbreite
            set
            {
                EbeneImage.Width = value*50;
                EbeneImage.Height = 1;
            }
        }

        // myRow
        private BaumDataSet.BaumRow myRow
        {
            // gibt die Reihe aus dem Zellobjekt zur�ck
            get { return (MyZelle.MyRow); }
        }

        // ShowEdit
        public bool ShowEdit
        {
            get { return (EditCheckBox.Checked); }
            set
            {
                EditCheckBox.Checked = value;
                EditPanel.Visible = value;
            }
        }

        // ShowEditCheckBox
        public bool ShowEditCheckBox
        {
            get { return (secb); }
            set
            {
                secb = value;
                EditCheckBox.Visible = secb;
            }
        }

        // ZeilenHoehe
        public int ZeilenHoehe
        {
            get { return (zeilenHoehe); }
            set
            {
                zeilenHoehe = value;
                BaumTIMG.Height = zeilenHoehe;
            }
        }

        // Werbefrei
        public bool Werbefrei { get; set; }

        // Methoden
        // --------

        // Malen()
        private void Malen()
        {
            BaumTIMG.Alt = "BaumGuid = " + myRow.BaumGuid;
            BaumLabel.Text = myRow.Baum;
            BaumLabel.ToolTip = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;

            if (!myRow.IsDateiNull() && myRow.Datei.Length > 0 && !Werbefrei)
            {
                DateiImageButton.ImageUrl = OliUtil.MakeImageSrc(myRow.Datei);
                DateiImageButton.Visible = true;
            }
            else
            {
                DateiImageButton.Visible = false;
            }

            // EditPanel ------------------
            BaumTextBox.Text = myRow.Baum;
            BeschreibungTextBox.Text = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;
            DateiTextBox.Text = myRow.IsDateiNull() ? "" : myRow.Datei;
        }

        // Ereignisse
        // ----------

        // Load
        protected void Page_Load(object sender, EventArgs e)
        {
//1			nkbz = NKBZ.Instance();
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Malen();
        }

        // UpdateButton_Click
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            myRow.Baum = BaumTextBox.Text;
            myRow.Beschreibung = BeschreibungTextBox.Text;
            myRow.Datei = DateiTextBox.Text;

//1			nkbz.UpdateBaum();

            ShowEdit = false;
        }

        // EditCheckBox_CheckedChanged
        protected void EditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowEdit = EditCheckBox.Checked;
        }

        protected void NeuButton_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            ZellBuilder zb = wrc.ZellBuilder;

            zb.NewBaum(MyZelle.Von);

            wrc.Malen();
        }

        protected void NeuZweigButton_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            MyZelle.NewChild();
            wrc.Malen();
        }
    }
}
