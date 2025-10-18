// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.Markierer
{
    /// <summary>
    ///     Markierer.
    /// </summary>
    public abstract class Markierer
    {
        public virtual DataTable Strings
        {
            get { return (null); }
        }

        public Guid MyGuid { get; set; }

        // IsInStrings(NetzRow)
        public abstract bool IsInStrings(NetzDataSet.NetzRow nr);

        // IsInStrings(KnotenRow)
        public abstract bool IsInStrings(KnotenDataSet.KnotenRow kr);

        // IsInStrings(BaumRow)
        public abstract bool IsInStrings(BaumDataSet.BaumRow br, KnotenZelle lastKnoten);

        // IsInStrings(ZweigRow)
        public abstract bool IsInStrings(ZweigDataSet.ZweigRow zr, KnotenZelle lastKnoten);

        // IsInStrings(KnotenZelle)
        public abstract bool IsInStrings(ref KnotenZelle kz); // Obacht: Parameter wird geändert!

        // IsInStrings(ZweigZelle)
        public abstract bool IsInStrings(ref ZweigZelle zz); // Obacht: Parameter wird geändert!

        // Markiere(KnotenControl)
        public abstract void Markiere(KnotenZelle kz);

        // Markiere(ZweigControl)
        public abstract void Markiere(ZweigZelle zz);

        // Clear(KnotenZelle)
        public abstract void Clear(KnotenZelle kz);

        // Clear(ZweigZelle)
        public abstract void Clear(ZweigZelle zz);

        // UpdatePunkte()
        public abstract void UpdatePunkte(KnotenZelle kz);

        // UpdatePunkte()
        public abstract void UpdatePunkte(ZweigZelle zz);
    }
}