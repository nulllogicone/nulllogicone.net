// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Zusammenfassung für IMarkierer.
    /// </summary>
    public interface IMarkierer
    {
        string IsInString(KnotenDataSet.KnotenRow kr);
        string IsInString(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr);
        void Markiere(KnotenDataSet.KnotenRow kr);
        void Markiere(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr);
        void Update(KnotenDataSet.KnotenRow kr, string ogif);
        void Update(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr, string ogif);
        void Clear(KnotenDataSet.KnotenRow kr);
        void Clear(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr);
    }
}