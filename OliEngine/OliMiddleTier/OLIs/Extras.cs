// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.DataSetTypes;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Extras.
    /// </summary>
    public class Extras
    {
        private OliUser user;
        private readonly OliDataAccess.Extras extras;

        // Default Extras
        public Extras(OliUser user)
        {
            this.user = user;
            extras = new OliDataAccess.Extras();
        }

        // Stamm-Extras
        public Extras(StammDataSet.StammRow stammRow)
        {
            extras = new OliDataAccess.Extras(stammRow);
        }

        public ExtrasDataSet.ExtrasRow ExtrasRow
        {
            get { return (extras.ExtrasRow); }
        }

        public int UpdateExtras()
        {
            return (extras.UpdateExtras());
        }
    }
}