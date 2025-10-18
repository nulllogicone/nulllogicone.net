// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess.Chart;

namespace OliEngine.OliMiddleTier.Chart
{
    /// <summary>
    ///     TopLabChart.
    /// </summary>
    public abstract class TopLabChart
    {
        public static TopLabDataSet.TopLabDataTable Top(int anz)
        {
            return TopLabChartAccess.Top(anz);
        }

        public static TopLabDataSet.TopLabDataTable Flop(int anz)
        {
            return TopLabChartAccess.Flop(anz);
        }
    }
}