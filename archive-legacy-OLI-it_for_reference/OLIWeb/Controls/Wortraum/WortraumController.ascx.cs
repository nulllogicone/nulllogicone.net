// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    WortraumController.
    ///</summary>
    public partial class WortraumController : UserControl
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

        private ZellBuilder zb;
        private int zeilenHoehe = 25;
        private bool showEdit; // ob man pflegen darf

        // Eigenschaft
        // -----------

        // ZellBuilder
        public ZellBuilder ZellBuilder
        {
            get { return (zb); }
            set
            {
                zb = value;
                AddControls(zb.Root);
            }
        }

        // ShowEdit
        public bool ShowEdit
        {
            get { return (showEdit); }
            set { showEdit = value; }
        }

        // Markierbar
        public bool Markierbar { get; set; }

        // ZeilenHoehe
        public int ZeilenHoehe
        {
            get { return (zeilenHoehe); }
            set { zeilenHoehe = value; }
        }

        // Spiegelverkehrt
        public bool Spiegelverkehrt
        {
            get
            {
                if (ZellBuilder.Markierer is AnglerMarkierer)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        // Werbefrei
        public bool Werbefrei { get; set; }

        // Methoden
        // --------

        // Load
        protected void Page_Load(object sender, EventArgs e)
        {
//			if(zb != null && zb.Root != null)
//			{
//				AddControls(zb.Root);
//			}
        }

        // Malen()
        public void Malen()
        {
//			if(zb != null && zb.Root != null)
//			{
            AddControls(zb.Root);
//			}
        }

        // AddControls(Zellen)
        private void AddControls(Zelle zellen)
        {
            Controls.Clear();

            // Zum Testen kann mit den folgenden Zeilen
            // ein Label �ber dem Wortraum
            // mit der aktuellen Markierer Guid angezeigt werden
//			Label l = new Label();
//			try
//			{
//				l.Text = this.ZellBuilder.Markierer.GetType().Name + " : " + this.ZellBuilder.Markierer.MyGuid.ToString();
//				l.Font.Size = FontUnit.XXSmall;
//				this.Controls.Add(l);
//			}
//			catch
//			{}

            Controls.Add(new HtmlGenericControl("hr"));
            while (zellen != null)
            {
                try
                {
                    AddControl(zellen);
                }
                catch (InvalidCastException ice)
                {
                    //TODO: Hier kommt oft ein Fehler -
                    // wenn der Wortraum gemalt wird und
                    // es den Zellhaufen nicht mehr so
                    // richtig gibt
                    string s = ice.Message;
                }

                zellen = zellen.Next();
            }
        }

        // AddControl(Einzel-Zelle) switch-case Aufbau

        #region AddControl(Zelle)

        // f�gt f�r die �bergebene Zelle ein
        // entsprechendes Control hinzu
        private void AddControl(Zelle ze)
        {
            string zellTyp = ze.GetType().Name;
            switch (zellTyp)
            {
                case "NetzZelle":
                    NetzControl nc;
                    if (Spiegelverkehrt)
                    {
                        nc = (NetzControl) LoadControl("~/Controls/Wortraum/NetzSpiegelControl.ascx");
                    }
                    else
                    {
                        nc = (NetzControl) LoadControl("~/Controls/Wortraum/NetzControl.ascx");
                    }
                    nc.MyZelle = (NetzZelle) ze;
                    nc.ShowEditCheckBox = showEdit;
                    nc.ZeilenHoehe = zeilenHoehe;
                    nc.Werbefrei = Werbefrei;
                    Controls.Add(nc);
                    break;

                case "KnotenZelle":
                    KnotenControl kc;
                    if (Spiegelverkehrt)
                    {
                        kc = (KnotenControl) LoadControl("~/Controls/Wortraum/KnotenSpiegelControl.ascx");
                    }
                    else
                    {
                        kc = (KnotenControl) LoadControl("~/Controls/Wortraum/KnotenControl.ascx");
                    }
                    kc.MyZelle = (KnotenZelle) ze;
                    kc.ShowEditCheckBox = showEdit;
                    kc.ZeilenHoehe = zeilenHoehe;
                    kc.Markierbar = Markierbar;
                    Controls.Add(kc);
                    break;

                case "BaumZelle":
                    BaumControl bc;
                    if (Spiegelverkehrt)
                    {
                        bc = (BaumControl) LoadControl("~/Controls/Wortraum/BaumSpiegelControl.ascx");
                    }
                    else
                    {
                        bc = (BaumControl) LoadControl("~/Controls/Wortraum/BaumControl.ascx");
                    }
                    bc.MyZelle = (BaumZelle) ze;
                    bc.ShowEditCheckBox = showEdit;
                    bc.ZeilenHoehe = zeilenHoehe;
                    bc.Werbefrei = Werbefrei;
                    Controls.Add(bc);
                    break;

                case "ZweigZelle":
                    ZweigControl z;
                    if (Spiegelverkehrt)
                    {
                        z = (ZweigControl) LoadControl("~/Controls/Wortraum/ZweigSpiegelControl.ascx");
                    }
                    else
                    {
                        z = (ZweigControl) LoadControl("~/Controls/Wortraum/ZweigControl.ascx");
                    }
                    z.MyZelle = (ZweigZelle) ze;
                    z.ShowEditCheckBox = showEdit;
                    z.ZeilenHoehe = zeilenHoehe;
                    z.Markierbar = Markierbar;
                    Controls.Add(z);
                    break;
            }
        }

        #endregion
    }
}
