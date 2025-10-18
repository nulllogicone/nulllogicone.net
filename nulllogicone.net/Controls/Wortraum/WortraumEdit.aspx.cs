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
    /// <summary>
    ///     WortraumEdit.
    /// </summary>
    public partial class WortraumEdit : BasePage
    {
        protected OliWeb.Controls.Wortraum.WortraumController WortraumController1;

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

        private ZellBuilder zb;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            zb = (ZellBuilder) Session["zb"];
            if (zb == null)
            {
                NullMarkierer nm = new NullMarkierer();
                zb = new ZellBuilder();
                zb.Markierer = nm;
                Session["zb"] = zb;
            }
            WortraumController1.Markierbar = true;
            WortraumController1.ZellBuilder = zb;
            WortraumController1.ShowEdit = true;
            WortraumController1.Werbefrei = false;
        }
    }
}