// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    Zweig.
    ///</summary>
    public partial class ZweigControl : UserControl // VerteilControl
    {
        protected Image EbeneImage2;
        protected Panel ShowPanel;

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
        private ZweigZelle myZelle;
        private bool secb;
        private int zeilenHoehe;
        protected BuntePunkte BuntePunkteMark;

        // Eigenschaften
        // -------------

        // ZellBuilder
        public ZellBuilder ZellBuilder
        {
            get { return (((WortraumController) Parent).ZellBuilder); }
        }

        // MyZelle
        public ZweigZelle MyZelle
        {
            get { return (myZelle); }
            set
            {
                myZelle = value;
                Ebene = myZelle.Ebene;

                // BuntePunkte.zelle
                BuntePunkte.zelle = myZelle;

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
            set
            {
                EbeneImage.Width = value*50;
                EbeneImage.Height = 1;
            }
        }

        // myRow
//		private NKBZDataSet.ZweigRow myRow
        private ZweigDataSet.ZweigRow myRow
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

        // Markierbar
        public bool Markierbar { get; set; }

        // ZeilenHoehe
        public int ZeilenHoehe
        {
            get { return (zeilenHoehe); }
            set
            {
                zeilenHoehe = value;
                ZweigStrichImageButton.Height = zeilenHoehe;
            }
        }

        public BuntePunkte BuntePunkte
        {
            get { return (BuntePunkteMark); }
        }

        // Methoden
        // --------

        // Malen()
        private void Malen()
        {
            ZweigStrichImageButton.ToolTip = "Zweig";

            ZweigLinkButton.Text = myRow.Zweig;

            BuntePunkteMark.PunktGroesse = 10;

            ClearImageButton.Visible = myZelle.Markiert && Markierbar; //&& (!myZelle.TieferMarkiert) ;;

            // weiterBaumGuid
            if (!myRow.IsweiterBaumGuidNull() && Markierbar)
            {
                WeiterBaumImageButton.Visible = true;
                WeiterBaumImageButton.CommandArgument = myRow.weiterBaumGuid.ToString();

                // weiterBaumRow
//1				NKBZDataSet.BaumRow br = nkbz.Baum.FindByBaumGuid(myRow.weiterBaumGuid);
//				BaumDataSet.BaumRow br = new Baum(myRow.weiterBaumGuid).BaumRow ;
//				WeiterBaumImageButton.ToolTip = br.Baum.ToString();
//				WeiterBaumImageButton.ToolTip += br.IsBeschreibungNull() ? "" : br.Beschreibung;
            }
            else
            {
                WeiterBaumImageButton.Visible = false;
            }

            // weiterNetzGuid
            if (!myRow.IsweiterNetzGuidNull() && Markierbar)
            {
                WeiterNetzImageButton.Visible = true;
                WeiterNetzImageButton.CommandArgument = myRow.weiterNetzGuid.ToString();

                // weiterNetzRow
//1				NKBZDataSet.NetzRow nr = nkbz.Netz.FindByNetzGuid(myRow.weiterNetzGuid);
//				NetzDataSet.NetzRow nr = new Netz(myRow.weiterNetzGuid).NetzRow;
//				WeiterNetzImageButton.ToolTip = nr.Netz.ToString();
            }

            else
            {
                WeiterNetzImageButton.Visible = false;
            }

            // nixWeiter
            if (myRow.IsweiterNetzGuidNull() && myRow.IsweiterBaumGuidNull() && Markierbar)
            {
                weiterNixImageButton.Visible = true;
                weiterNixImageButton.ToolTip = "neu";
            }

            else
            {
                weiterNixImageButton.Visible = false;
            }

            // EditPanel ------------------------
            if (EditCheckBox.Checked)
            {
                ZweigTextBox.Text = myRow.Zweig;

                // WeiterNetzDropDown
                DropDownList wnddl = WeiterNetzDropDownList;
//1				DataView dvn = new DataView(nkbz.Netz);
                DataView dvn = new DataView(new Netz().Netz);
                dvn.Sort = "Netz";
                wnddl.DataSource = dvn;
                wnddl.DataValueField = "NetzGuid";
                wnddl.DataBind();
                wnddl.Items.Insert(0, new ListItem("---", ""));
                if (!myRow.IsweiterNetzGuidNull())
                {
                    wnddl.Items.FindByValue(myRow.weiterNetzGuid.ToString()).Selected = true;
                }

                // WeiterBaumDropDown
                DropDownList wbddl = WeiterBaumDropDownList;
//1				DataView dvb = new DataView(nkbz.Baum);
                DataView dvb = new DataView(new Baum().Baum);
                dvb.Sort = "Baum";
                wbddl.DataSource = dvb;
                wbddl.DataValueField = "BaumGuid";
                wbddl.DataBind();
                wbddl.Items.Insert(0, new ListItem("---", ""));
                if (!myRow.IsweiterBaumGuidNull())
                {
                    wbddl.Items.FindByValue(myRow.weiterBaumGuid.ToString()).Selected = true;
                }
            }
        }

        // Ereignisse
        // ----------

        // Load
        protected void Page_Load(object sender, EventArgs e)
        {
//1			nkbz = NKBZ.Instance();

            // BuntePunkte.Update - Ereignis abfangen
            BuntePunkteMark.Update += OnBuntePunkteUpdate;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Malen();
        }

        // WeiterNetzImageButton_Click
        protected void WeiterNetzImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ZweigLinkButton_Click(sender, e);
        }

        // WeiterBaumImageButton_Click
        protected void WeiterBaumImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ZweigLinkButton_Click(sender, e);
        }

        // WeiterNixImageButton_Click
        protected void weiterNixImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ZweigLinkButton_Click(sender, e);
        }

        // UpdateButton_Click
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            myRow.Zweig = ZweigTextBox.Text;

            // Weiter (wenn beides ausgew�hlt ist, wird nur das Netz genommen)
            string wn = WeiterNetzDropDownList.SelectedItem.Value;
            string wb = WeiterBaumDropDownList.SelectedItem.Value;

            if (wn.Length > 0)
            {
                myRow.weiterBaumGuid = new Guid(wn);
            }
            else
            {
                myRow["weiterNetzGuid"] = DBNull.Value;

                if (wb.Length > 0)
                {
                    myRow.weiterBaumGuid = new Guid(wb);
                }
                else
                {
                    myRow["weiterBaumGuid"] = DBNull.Value;
                }
            }

            // Update abschiessen
//1			nkbz.UpdateZweig();

            ShowEdit = false;
        }

        // EditCheckBox_CheckedChanged
        protected void EditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowEdit = EditCheckBox.Checked;
        }

        // NeuButton_Click()
        protected void NeuButton_Click(object sender, EventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            (MyZelle.Parent).NewChild();
            wrc.Malen();
        }

        // ClearImageButton_Click
        protected void ClearImageButton_Click(object sender, ImageClickEventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            ZellBuilder.Clear(MyZelle);
            wrc.Malen();
        }

        protected void ZweigStrichImageButton_Click(object sender, ImageClickEventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            wrc.ZellBuilder.ToggleZelle(MyZelle);
            wrc.Malen();
        }

        // OnBuntePunkteUpdate()
        private void OnBuntePunkteUpdate(object sender, BuntePunkteEventArgs e)
        {
            MyZelle.VgbOLIs = e.verb;
            MyZelle.VgbGet = e.attrib;

            MyZelle.VgbILOs = e.verb;
            MyZelle.VgbFit = e.attrib;

            ZellBuilder.UpdatePunkte(MyZelle);
        }

        protected void ZweigLinkButton_Click(object sender, EventArgs e)
        {
            if (!MyZelle.Markiert && Markierbar)
            {
                MyZelle.VgbOLIs = ((BaumZelle) MyZelle.Parent).LastKnoten.VgbOLIs;
                MyZelle.VgbGet = ((BaumZelle) MyZelle.Parent).LastKnoten.VgbGet;
                MyZelle.VgbILOs = ((BaumZelle) MyZelle.Parent).LastKnoten.VgbILOs;
                MyZelle.VgbFit = ((BaumZelle) MyZelle.Parent).LastKnoten.VgbFit;

                ZellBuilder.Markiere(MyZelle);
            }
            WortraumController wrc = (WortraumController) Parent;
            wrc.ZellBuilder.ToggleZelle(MyZelle);
            wrc.Malen();
        }
    }
}
