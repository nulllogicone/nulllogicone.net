// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.DataSetTypes;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Tollis.
    /// </summary>
    public class Tollis
    {
        // Member
        // ------

        private readonly OliDataAccess.Tollis tollis;

        public Tollis(StammDataSet.StammRow sr, TopLabDataSet.TopLabRow tr)
        {
            tollis = new OliDataAccess.Tollis(sr, tr);
        }

        // Eigenschaften
        // -------------

        public TollisDataSet.TollisRow TollisRow
        {
            get { return tollis.TollisRow; }
        }

        // Methoden
        // --------

        // UpdateTollis()
        public int UpdateTollis()
        {
            return (tollis.UpdateTollis());
        }
    }
}
