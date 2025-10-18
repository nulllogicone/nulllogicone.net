// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Controls.Floor.Chart;
using OliWeb.Klassen;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     Diese Seite zeigt eine zusammenfassende Übersicht aller TOP und FLOP Daten.
    ///     (die teuersten, reichsten, billigsten ...)
    /// </summary>
    public partial class ChartPage : BasePage
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

        protected StammChartList ReichsteStammChartList;
        protected StammChartList AermsteStammChartList;
        protected PostItChartList TeuerstePostItChartList;
        protected PostItChartList BilligstePostItChartList;
        protected TopLabChartList TopTopLabChartList;
        protected TopLabChartList FlopTopLabChartList;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}