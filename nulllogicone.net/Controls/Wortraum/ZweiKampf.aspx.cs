// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  

using System;
using System.Drawing;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes.Views;
using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Functions;
using OliEngine.OliMiddleTier;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;
using OliWeb.Klassen;

namespace OliWeb.Controls.Wortraum
{
    /// <summary>
    ///     ZweiKampf.
    /// </summary>
    public partial class ZweiKampf : MasterStammPage
    {
        protected System.Web.UI.WebControls.DropDownList CodeDropDownList;

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

        protected OliWeb.Controls.Wortraum.WortraumController CodeWortraumController;
        protected OliWeb.Controls.Wortraum.WortraumController AnglerWortraumController;

        private ZellBuilder codeZellBuilder;
        private ZellBuilder anglerZellBuilder;

        /// <summary>
        ///     CheckPreCondition
        ///     Wenn die BasisMasterPage Initialisiert wird, wird 
        ///     auf das vorhandensein eine Stamm geprüft.
        ///     Auf dieser Seite muss er auch noch eingeloggt sein
        /// </summary>
        protected override void CheckPreCondition()
        {
            base.CheckPreCondition();

            // sonst muss man eingeloggt sein
            if (!OliUser.Stamm.BinIchEingeloggt)
            {
                OliUser.Nachricht = "Stamm muss eingeloggt sein";
                Response.Clear();
                Response.Redirect(NOT_EINGELOGGT_REDIRECT);
            }
        }

        // Load
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Counter

//			// OliUser
//			user = SessionManager.Instance().OliUser;

            // Code
            codeZellBuilder = (ZellBuilder) Session["codeZellBuilder"];
            if (codeZellBuilder == null)
            {
                codeZellBuilder = new ZellBuilder();
                Session["codeZellBuilder"] = new ZellBuilder();
            }
            CodeWortraumController.ZellBuilder = codeZellBuilder;

            // Angler
            anglerZellBuilder = (ZellBuilder) Session["anglerZellBuilder"];
            if (anglerZellBuilder == null)
            {
                anglerZellBuilder = new ZellBuilder();
                Session["anglerZellBuilder"] = new ZellBuilder();
            }
            AnglerWortraumController.ZellBuilder = anglerZellBuilder;


            // beim ersten Laden -> DropDown-Listen füllen
            if (!IsPostBack)
            {
//				PostItList pl = new PostItList();
                StammPostItDataSet.StammPostItDataTable pl = OliUser.Stamm.MyPostIt;
                pl.DefaultView.Sort = "Datum DESC";
                PostItDropDownList.DataSource = pl.DefaultView;

                StammList sl = new StammList();
                sl.Stamm.DefaultView.Sort = "Stamm";
                StammAnglerDropDownList.DataSource = sl.Stamm.DefaultView;

                AnglerListBox.DataSource = OliUser.Stamm.MyAngler;

                DataBind();

                PostItDropDownList.Items.Insert(0, new ListItem("---", ""));
                StammAnglerDropDownList.Items.Insert(0, new ListItem("---", ""));

//				NullMarkierer cm = new NullMarkierer();
//				codeZellBuilder.Markierer = cm;
//
//				CodeWortraumController.ZellBuilder = codeZellBuilder;	
            }
        }


        // PostItDropDownList_SelectedIndexChanged
        protected void PostItDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Guid pguid = new Guid(PostItDropDownList.SelectedItem.Value);
            OliEngine.OliDataAccess.PostIt p = new OliEngine.OliDataAccess.PostIt(pguid);

            CodeList cl = new CodeList(p.PostItRow);
            CodeListBox.DataSource = cl;
            DataBind();

            CodeWortraumController.ZellBuilder = new ZellBuilder();

            CodeXButton.Enabled = false;
        }

        // CodeListBox_SelectedIndexChanged
        protected void CodeListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Guid cguid = new Guid(CodeListBox.SelectedItem.Value);

            CodeMarkierer cm = new CodeMarkierer(cguid);
            codeZellBuilder.Markierer = cm;

            CodeWortraumController.ZellBuilder = codeZellBuilder;

            CodeXButton.Enabled = true;

            AnzAnglerLinkButton.Text = Statistik.AnzahlEmpfaengerProCode(cguid).ToString();
        }

        // StammAnglerDropDownList_SelectedIndexChanged
        protected void StammAnglerDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Guid sguid = new Guid(StammAnglerDropDownList.SelectedItem.Value);

            if (sguid != Guid.Empty)
            {
                OliEngine.OliDataAccess.Stamm s = new OliEngine.OliDataAccess.Stamm(sguid);

                AnglerList al = new AnglerList(s.StammRow);
                AnglerListBox.DataSource = al;

                DataBind();

                AnglerWortraumController.ZellBuilder = new ZellBuilder();

                AnglerXButton.Enabled = false;
            }
        }

        // AnglerListBox_SelectedIndexChanged
        protected void AnglerListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Guid aguid = new Guid(AnglerListBox.SelectedItem.Value);

            AnglerMarkierer am = new AnglerMarkierer(aguid);
            anglerZellBuilder.Markierer = am;

            AnglerWortraumController.ZellBuilder = anglerZellBuilder;

            AnglerXButton.Enabled = true;

            AnzCodeLinkButton.Text = Statistik.AnzahlCodeProAngler(aguid).ToString();
        }

        // FischenButton_Click
        protected void FischenButton_Click(object sender, System.EventArgs e)
        {
            // cid-aid festlegen
            Guid cguid = new Guid(CodeListBox.SelectedItem.Value);
            Guid aguid = new Guid(AnglerListBox.SelectedItem.Value);

            // fischen
            Fischer fischer = new Fischer();
            if (fischer.Beissen(cguid, aguid))
            {
                FischenButton.BackColor = Color.Green;
            }
            else
            {
                FischenButton.BackColor = Color.Red;
            }
        }

        // CodeXButton_Click()
        protected void CodeXButton_Click(object sender, System.EventArgs e)
        {
            Guid cguid = new Guid(CodeListBox.SelectedItem.Value);
            Fischer f = new Fischer();
            f.Fischen(cguid, Guid.Empty);

            AnzAnglerLinkButton.Text = Statistik.AnzahlEmpfaengerProCode(cguid).ToString();
        }

        // AnglerXButton_Click()
        protected void AnglerXButton_Click(object sender, System.EventArgs e)
        {
            Guid aguid = new Guid(AnglerListBox.SelectedItem.Value);
            Fischer f = new Fischer();
            f.Fischen(Guid.Empty, aguid);

            AnzCodeLinkButton.Text = Statistik.AnzahlCodeProAngler(aguid).ToString();
        }

        protected void AnzAnglerLinkButton_Click(object sender, System.EventArgs e)
        {
        }

        protected void AnzCodeLinkButton_Click(object sender, System.EventArgs e)
        {
        }
    }
}