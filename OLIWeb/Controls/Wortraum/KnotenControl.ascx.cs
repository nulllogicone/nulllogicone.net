// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    Knoten.
    ///</summary>
    public partial class KnotenControl : UserControl
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

        private void InitializeComponent()
        {
        }

        #endregion

        // Member
        // ------

//1		NKBZ nkbz;
        private KnotenZelle myZelle;
        private bool secb; // ShowEditCheckBox
//		bool sc; // ShowClear
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
        public KnotenZelle MyZelle
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
//1		private NKBZDataSet.KnotenRow myRow
        private KnotenDataSet.KnotenRow myRow
        {
            // gibt die Reihe aus dem Zellobjekt zurück
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
                KnotenPunktImageButton.Height = zeilenHoehe;
            }
        }

        // Markierbar
        public bool Markierbar { get; set; }

        // BuntePunkte
        public BuntePunkte BuntePunkte
        {
            get { return (BuntePunkteMark); }
        }

        // Methoden
        // --------

        // Malen()

        #region Malen()

        private void Malen()
        {
            if (myRow == null)
            {
                return;
            }

            KnotenPunktImageButton.ToolTip = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;
//1			KnotenLinkButton.Text = myRow.IsKnotenNull() ? "" : myRow.Knoten;
            KnotenLinkButton.Text = myRow.Knoten;
            KnotenLinkButton.ToolTip = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;

            // BuntePunkte
            BuntePunkteMark.PunktGroesse = 10;

            // wenn markiert ClearButton zeigen
            ClearImageButton.Visible = myZelle.Markiert && Markierbar && myZelle.MyRow.IsweiterBaumGuidNull();
            //&& (!myZelle.TieferMarkiert) ;

            // weiterBaum
            if (!myRow.IsweiterBaumGuidNull() && Markierbar)
            {
                WeiterBaumImageButton.Visible = true;
                WeiterBaumImageButton.CommandArgument = myRow.weiterBaumGuid.ToString();

                // weiterBaumRow
//1				NKBZDataSet.BaumRow br = nkbz.Baum.FindByBaumGuid(myRow.weiterBaumGuid);
//				BaumDataSet.BaumRow br = new Baum(myRow.weiterBaumGuid).BaumRow;
//				WeiterBaumImageButton.ToolTip = "B: " + br.Baum + "\n";
//				WeiterBaumImageButton.ToolTip += br.IsBeschreibungNull() ? "" : br.Beschreibung;
            }
            else
            {
                WeiterBaumImageButton.Visible = false;
            }

            // weiterNetz
            if (!myRow.IsweiterNetzGuidNull() && Markierbar)
            {
                WeiterNetzImageButton.Visible = true;
                WeiterNetzImageButton.CommandArgument = myRow.weiterNetzGuid.ToString();

                // weiterNetzRow
//1				NKBZDataSet.NetzRow nr = nkbz.Netz.FindByNetzGuid(myRow.weiterNetzGuid);
//				NetzDataSet.NetzRow nr = new Netz(myRow.weiterNetzGuid).NetzRow ;
//				WeiterNetzImageButton.ToolTip = nr.Netz.ToString();
//
//				try
//				{
//					WeiterNetzImageButton.ToolTip = "weiter: " + nr.Netz + "\n";
//					WeiterNetzImageButton.ToolTip += nr.IsBeschreibungNull() ? "" : nr.Beschreibung;
//				}
//				catch
//				{}
            }

            else
            {
                WeiterNetzImageButton.Visible = false;
            }

            // weiterNix
            if (myRow.IsweiterBaumGuidNull() && myRow.IsweiterNetzGuidNull() && Markierbar)
            {
                weiterNixImageButton.Visible = true;
            }

            else
            {
                weiterNixImageButton.Visible = false;
            }

            // EditPanel ------------
            if (EditCheckBox.Checked)
            {
//1				KnotenTextBox.Text = myRow.IsKnotenNull() ? "" : myRow.Knoten;
                KnotenTextBox.Text = myRow.Knoten;
                BeschreibungTextBox.Text = myRow.IsBeschreibungNull() ? "" : myRow.Beschreibung;

                // VorgabeWerte
//1				OLIsTextBox.Text = myRow.IsVgbOLIsNull() ? "" : myRow.VgbOLIs.ToString();
//1				GetTextBox.Text = myRow.IsVgbGetNull() ? "" : myRow.VgbGet.ToString();
//1				ILOsTextBox.Text = myRow.IsVgbILOsNull() ? "" : myRow.VgbILOs.ToString();
//1				FitTextBox.Text =myRow.IsVgbFitNull() ? "" : myRow.VgbFit.ToString();
                OLIsTextBox.Text = myRow.VgbOLIs.ToString();
                GetTextBox.Text = myRow.VgbGet.ToString();
                ILOsTextBox.Text = myRow.VgbILOs.ToString();
                FitTextBox.Text = myRow.VgbFit.ToString();

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

        #endregion

        // Ereignisse
        // ----------

        // Load
        protected void Page_Load(object sender, EventArgs e)
        {
//1			nkbz = NKBZ.Instance();

            // BuntePunkte.Update - Ereignis abfangen
            BuntePunkteMark.Update += OnBuntePunkteUpdate;
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Malen();
        }

        // WeiterNetzImageButton_Click
        protected void WeiterNetzImageButton_Click(object sender, ImageClickEventArgs e)
        {
            KnotenLinkButton_Click(sender, e);
        }

        // WeiterBaumImageButton_Click
        protected void WeiterBaumImageButton_Click(object sender, ImageClickEventArgs e)
        {
            KnotenLinkButton_Click(sender, e);
        }

        // WeiterNixImageButton_Click()
        protected void weiterNixImageButton_Click(object sender, ImageClickEventArgs e)
        {
            KnotenLinkButton_Click(sender, e);
        }

        // UpdateButton_Click
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            myRow.Knoten = KnotenTextBox.Text;
            myRow.Beschreibung = BeschreibungTextBox.Text;

            // VorgabeWerte
            myRow.VgbOLIs = int.Parse(OLIsTextBox.Text.Length > 0 ? OLIsTextBox.Text : "2");
            myRow.VgbGet = int.Parse(GetTextBox.Text.Length > 0 ? GetTextBox.Text : "0");
            myRow.VgbILOs = int.Parse(ILOsTextBox.Text.Length > 0 ? ILOsTextBox.Text : "2");
            myRow.VgbFit = int.Parse(FitTextBox.Text.Length > 0 ? FitTextBox.Text : "0");

            // Weiter (wenn beides ausgewählt wird nur das Netz genommen)
            string wn = WeiterNetzDropDownList.SelectedItem.Value;
            string wb = WeiterBaumDropDownList.SelectedItem.Value;

            if (wn.Length > 0)
            {
                myRow.weiterNetzGuid = new Guid(wn);
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
//1			nkbz.UpdateKnoten();

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

        // ClearImageButton_Click()
        protected void ClearImageButton_Click(object sender, ImageClickEventArgs e)
        {
            WortraumController wrc = (WortraumController) Parent;
            ZellBuilder.Clear(MyZelle);
            wrc.Malen();
        }

        // KnotenPunktImageButton_Click()
        protected void KnotenPunktImageButton_Click(object sender, ImageClickEventArgs e)
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

        protected void KnotenLinkButton_Click(object sender, EventArgs e)
        {
            if (!MyZelle.Markiert && Markierbar)
            {
                if (ZellBuilder.Markierer is ShortCutsMarkierer ||
                    ZellBuilder.Markierer is CodeMarkierer)
                {
                    MyZelle.VgbOLIs = myRow.VgbOLIs;
                    MyZelle.VgbGet = myRow.VgbGet;
                }
                else if (ZellBuilder.Markierer is AnglerMarkierer)
                {
                    MyZelle.VgbILOs = myRow.VgbILOs;
                    MyZelle.VgbFit = myRow.VgbFit;
                }
//TODO:				// Ist hier die Leiche ???? TODO
                // in einen Baum soll man reinschauen können - ohne Markierung
                if (MyZelle.MyRow.IsweiterBaumGuidNull())
                {
                    ZellBuilder.Markiere(MyZelle);
                }
            }

            WortraumController wrc = (WortraumController) Parent;
            wrc.ZellBuilder.ToggleZelle(MyZelle);
            wrc.Malen();
        }
    }
}