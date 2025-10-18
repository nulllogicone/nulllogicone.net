// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliEngine.OliMiddleTier.Chart;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor.Chart
{
    ///<summary>
    ///    <b>Container-</b> Control für die einzelnen (Stamm, PostIt, TopLab)-ChartControls.
    ///    Hier werden die Daten an die Controls übergeben, da sie mehrfach verwendet
    ///    werden können (für TOP und FLOP)
    ///</summary>
    ///<remarks>
    ///    Dieses Control wird auf der <see cref="Sites.Elemente.ChartPage" />
    ///    verwendet
    ///</remarks>
    public partial class ChartControl : MasterControl
    {
        protected StammChartList ReichsteStammChartList;
        protected StammChartList AermsteStammChartList;
        protected PostItChartList TeuerstePostItChartList;
        protected PostItChartList BilligstePostItChartList;
        protected PostItChartList VielBeachtetPostItChartList;
        protected PostItChartList UnBeachtetPostItChartList;
        protected TopLabChartList TopTopLabChartList;
        protected TopLabChartList FlopTopLabChartList;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Die TOP und FLOP Daten werden an die passenden Controls übergeben.
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ReichsteStammChartList.DataSource = StammChart.Reichste(5);
            AermsteStammChartList.DataSource = StammChart.Aermste(5);

            TeuerstePostItChartList.DataSource = PostItChart.Teuerste(5);
            BilligstePostItChartList.DataSource = PostItChart.Billigste(5);

            VielBeachtetPostItChartList.DataSource = PostItChart.VielBeachtet(5);
            UnBeachtetPostItChartList.DataSource = PostItChart.UnBeachtet(5);

            TopTopLabChartList.DataSource = TopLabChart.Top(5);
            FlopTopLabChartList.DataSource = TopLabChart.Flop(5);
        }
    }
}