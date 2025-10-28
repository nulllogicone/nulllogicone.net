// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.Markierer
{
    /// <summary>
    ///     ShortCutsMarkierer.
    /// </summary>
    public class NullMarkierer : Markierer
    {
        // eigene Member
        // -------------

        private readonly ShortCuts shortcuts;

        // Konstruktor
        // -----------

        public NullMarkierer()
        {
            MyGuid = Guid.Empty;
            shortcuts = new ShortCuts();
        }

        // Eigenschaften
        // -------------

        // ShortCuts
        private ShortCuts ShortCuts
        {
            get { return (shortcuts); }
        }

        // Strings
        public override DataTable Strings
        {
            get
            {
                shortcuts.Strings.Rows.Clear();
                return (shortcuts.Strings);
            }
        }

        // Methoden
        // --------

        // IsInStrings(NetzRow)
//1		public override bool IsInStrings(NKBZDataSet.NetzRow nr)
        public override bool IsInStrings(NetzDataSet.NetzRow nr)
        {
            return (false);
        }

        // IsInStrings(KnotenRow)
//		public override bool IsInStrings(NKBZDataSet.KnotenRow kr)
        public override bool IsInStrings(KnotenDataSet.KnotenRow kr)
        {
            return (false);
        }

        // IsInStrings(BaumRow)
//1		public override bool IsInStrings(NKBZDataSet.BaumRow br, KnotenZelle lastKnoten)
        public override bool IsInStrings(BaumDataSet.BaumRow br, KnotenZelle lastKnoten)
        {
            return (false);
        }

        // IsInStrings(ZweigRow)
//1		public override bool IsInStrings(NKBZDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        public override bool IsInStrings(ZweigDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        {
            return (false);
        }

        // IsInStrings(KnotenZelle)
        public override bool IsInStrings(ref KnotenZelle kz)
        {
            kz.VgbOLIs = -1;
            kz.VgbGet = -1;
            return (false);
        }

        // IsInStrings(ZweigZelle)
        public override bool IsInStrings(ref ZweigZelle zz)
        {
            zz.VgbOLIs = -1;
            zz.VgbGet = -1;
            return (false);
        }

        // Markiere(KnotenZelle)
        public override void Markiere(KnotenZelle kc)
        {
        }

        // Markiere(ZweigZelle)
        public override void Markiere(ZweigZelle zz)
        {
        }

        // Clear(KnotenZelle)
        public override void Clear(KnotenZelle kz)
        {
        }

        // Clear(ZweigZelle)
        public override void Clear(ZweigZelle zz)
        {
        }

        // UpdatePunkte(KnotenZelle)
        public override void UpdatePunkte(KnotenZelle kz)
        {
        }

        // UpdatePunkte(ZweigZelle)
        public override void UpdatePunkte(ZweigZelle zz)
        {
        }
    }
}
