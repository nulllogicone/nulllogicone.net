// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess.Chart;

namespace OliEngine.OliMiddleTier.Chart
{
    /// <summary>
    ///     StammChart.
    /// </summary>
    public abstract class StammChart
    {
        public static StammDataSet.StammDataTable Reichste(int anz)
        {
            return StammChartAccess.Reichste(anz);
        }

        public static StammDataSet.StammDataTable Aermste(int anz)
        {
            return StammChartAccess.Aermste(anz);
        }
    }
}
