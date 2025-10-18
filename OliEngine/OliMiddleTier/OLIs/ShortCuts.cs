// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.DataSetTypes;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     ShortCuts.
    /// </summary>
    public class ShortCuts
    {
        // Member
        // ------

        private readonly Stamm stamm;
        private readonly OliDataAccess.ShortCuts shortCuts;
        private readonly ZellBuilder zellBuilder = new ZellBuilder();

        // Konstrukor
        // ----------
        public ShortCuts(Stamm stamm, Guid scguid)
        {
            this.stamm = stamm;

            shortCuts = new OliDataAccess.ShortCuts(scguid);

            ShortCutsMarkierer scm = new ShortCutsMarkierer(scguid);
            zellBuilder.Markierer = scm;
        }

        public ShortCuts(Stamm stamm)
        {
            this.stamm = stamm;

            shortCuts = new OliDataAccess.ShortCuts();
            ShortCutsDataSet.ShortCutsRow scr = shortCuts.ShortCuts.NewShortCutsRow();

            scr.ShortCutsGuid = Guid.NewGuid();
            scr.StammGuid = stamm.StammRow.StammGuid;
            scr.auto = false;
            scr.ShortCut = "neu";

            shortCuts.ShortCuts.AddShortCutsRow(scr);
            shortCuts.UpdateShortCuts();
        }

        // Eigenschaften
        // -------------

        // ZellBuilder
        public ZellBuilder ZellBuilder
        {
            get { return (zellBuilder); }
        }

        public ShortCutsDataSet.ShortCutsRow ShortCutsRow
        {
            get { return (shortCuts.ShortCutsRow); }
        }

        // Methoden
        // --------

        // UpdateShortCuts
        public int UpdateShortCuts()
        {
            if (stamm.BinIchEingeloggt)
            {
                return (shortCuts.UpdateShortCuts());
            }
            else
            {
                throw new Exception("Nicht eingeloggter Stamm versucht UpdateShortCuts");
            }
        }
    }
}