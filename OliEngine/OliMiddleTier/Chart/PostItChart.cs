// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess.Chart;

namespace OliEngine.OliMiddleTier.Chart
{
    /// <summary>
    ///     PostItChart.
    /// </summary>
    public abstract class PostItChart
    {
        /// <summary>
        ///     Teuerstes the specified anz.
        /// </summary>
        /// <param name = "anz">The anz.</param>
        /// <returns></returns>
        public static PostItDataSet.PostItDataTable Teuerste(int anz)
        {
            return PostItChartAccess.Teuerste(anz);
        }

        /// <summary>
        ///     Billigstes the specified anz.
        /// </summary>
        /// <param name = "anz">The anz.</param>
        /// <returns></returns>
        public static PostItDataSet.PostItDataTable Billigste(int anz)
        {
            return PostItChartAccess.Billigste(anz);
        }

        /// <summary>
        ///     Viels the beachtet.
        /// </summary>
        /// <param name = "anz">The anz.</param>
        /// <returns></returns>
        public static PostItDataSet.PostItDataTable VielBeachtet(int anz)
        {
            return PostItChartAccess.VielBeachtet(anz);
        }

        /// <summary>
        ///     Uns the beachtet.
        /// </summary>
        /// <param name = "anz">The anz.</param>
        /// <returns></returns>
        public static PostItDataSet.PostItDataTable UnBeachtet(int anz)
        {
            return PostItChartAccess.UnBeachtet(anz);
        }
    }
}