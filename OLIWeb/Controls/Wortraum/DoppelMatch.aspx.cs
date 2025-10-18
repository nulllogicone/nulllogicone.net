// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.OLIs;
using OliEngine.OliMiddleTier.ZellHaufen;
using OliWeb.Klassen;

namespace OliWeb.Controls.Wortraum
{
    ///<summary>
    ///    DoppelMatch.
    ///</summary>
    public partial class DoppelMatch : Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            OliUser user = SessionManager.Instance().OliUser;
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
            base.OnPreRender(e);

            CodeWortraumController.ZellBuilder = codeZellBuilder;
            Session["codeZellBuilder"] = codeZellBuilder;

            AnglerWortraumController.ZellBuilder = anglerZellBuilder;
            Session["anglerZellBuilder"] = anglerZellBuilder;
        }
    }
}