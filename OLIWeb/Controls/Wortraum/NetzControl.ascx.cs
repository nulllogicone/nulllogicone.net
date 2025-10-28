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
    ///    Netz.
    ///</summary>
    public partial class NetzControl : UserControl
    {
        protected DataGrid DataGrid;
        protected DataGrid KnotenDataGrid;
        protected Button Button1;

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

        // eigene Member
        // -------------

//1		NKBZ nkbz;
        private NetzZelle myZelle;
        private bool secb;
        private int zeilenHoehe;

        // Eigenschaften
        // -------------

        // MyZelle
        public NetzZelle MyZelle
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
//1		private NKBZDataSet.NetzRow myRow
        private NetzDataSet.NetzRow myRow
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
                NetzTIMG.Height = zeilenHoehe;
            }
        }

        // Werbefrei
        public bool Werbefrei { get; set; }

        // Methoden
        // --------

        // Malen
        private void Malen()
        {
//1			NetzLabel.Text = myRow.IsNetzNull() ? "Netz-Name" : myRow.Netz ;
            NetzLabel.Text = myRow.Netz;
            NetzLabel.ToolTip = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;

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
//1			NetzTextBox.Text = myRow.IsNetzNull() ? "" : myRow.Netz;
            NetzTextBox.Text = myRow.Netz;
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
            myRow.Netz = NetzTextBox.Text;
            myRow.Beschreibung = BeschreibungTextBox.Text;
            myRow.Datei = DateiTextBox.Text;

//1			nkbz.UpdateNetz();

            ShowEdit = false;
        }

        // EditCheckBox_CheckedChanged
        protected void EditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowEdit = EditCheckBox.Checked;
        }

        // NeuButton_Click
        protected void NeuButton_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            ZellBuilder zb = wrc.ZellBuilder;
            zb.NewNetz(MyZelle.Von);
            wrc.Malen();
        }

        // NeuKnotenButton_Click
        protected void NeuKnotenButton_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            MyZelle.NewChild();
            wrc.Malen();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
//			this.MyZelle.Next().RemoveMe();
            wrc.ZellBuilder.OpenMarkedNetz(MyZelle.Guid, MyZelle.Ebene, MyZelle.Von);
//			wrc.Malen();		
        }
    }
}
