// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  

using System;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;
using OliWeb.Klassen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    DoppelMatch.
    ///</summary>
    public partial class DoppelMatch : BasePage
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

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        // Member
        // ------

        private readonly Guid sguid = new Guid("B4111E0E-48D9-42C4-A6F6-EC4991264947");
        private readonly Guid aguid = new Guid("79BBEA3F-A06A-49EE-8C19-49A66A2445A4");
        protected WortraumController CodeWortraumController;
        protected WortraumController AnglerWortraumController;


        private ZellBuilder codeZellBuilder;
        private ZellBuilder anglerZellBuilder;


        // Eigenschaften
        // -------------

        // Methoden
        // --------

        // Page_Load()
        protected void Page_Load(object sender, System.EventArgs e)
        {
            OliEngine.OliMiddleTier.OLIs.OliUser user = SessionManager.Instance().OliUser;
            user.ShowStamm(sguid);
            user.Stamm.ShowAngler(aguid);


            // CodeZellBuilder
            codeZellBuilder = (ZellBuilder) Session["codeZellBuilder"];
            if (codeZellBuilder == null)
            {
                codeZellBuilder = new ZellBuilder();
                codeZellBuilder.Markierer = new NullMarkierer();
                Session["codeZellBuilder"] = new ZellBuilder();
            }

            CodeWortraumController.ZellBuilder = codeZellBuilder;
            CodeWortraumController.Markierbar = true;

            // CodeZellBuilder
            anglerZellBuilder = (ZellBuilder) Session["anglerZellBuilder"];
            if (anglerZellBuilder == null)
            {
                anglerZellBuilder = new ZellBuilder();
                anglerZellBuilder.Markierer = new AnglerMarkierer(aguid);
                Session["anglerZellBuilder"] = new ZellBuilder();
            }

            AnglerWortraumController.ZellBuilder = anglerZellBuilder;
            AnglerWortraumController.Markierbar = true;
        }

        // OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            CodeWortraumController.ZellBuilder = codeZellBuilder;
            Session["codeZellBuilder"] = codeZellBuilder;

            AnglerWortraumController.ZellBuilder = anglerZellBuilder;
            Session["anglerZellBuilder"] = anglerZellBuilder;
        }
    }
}
