// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor.Chart
{
    ///<summary>
    ///    Zeigt Nachrichtentext mit Bild und KooK und Hits
    ///</summary>
    public partial class PostItChartList : MasterControl
    {
        /// <summary>
        ///     für die Darstellung der PostIt
        /// </summary>
        protected Repeater PostItRepeater;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung.
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion

        /// <summary>
        ///     Die Daten werden diesem Control von außen übergeben, da es für
        ///     unterschiedliche Werte verwendet werden kann. Danach bindet es
        ///     sein darstellendes Steuerelement (DataGrid oder Repeater).
        /// </summary>
        public object DataSource
        {
            set
            {
                PostItRepeater.DataSource = value;
                PostItRepeater.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}