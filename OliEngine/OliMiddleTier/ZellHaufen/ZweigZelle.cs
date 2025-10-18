// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     ZweigZelle.
    /// </summary>
    public class ZweigZelle : VerteilZelle
    {
        // Member
        // ------

//1		NKBZ nkbz;
//1		NKBZDataSet.ZweigRow zr;
        private readonly Zweig z;
        private readonly ZweigDataSet.ZweigRow zr;

        // Konstruktor
        // -----------

        public ZweigZelle(Guid zguid, int ebene, SammelZelle parent) : base(zguid, ebene, parent)
        {
//1			nkbz = NKBZ.Instance();
            z = new Zweig(zguid);

            if (zguid != Guid.Empty)
            {
//1				zr = nkbz.Zweig.FindByZweigGuid(zguid);
                zr = z.ZweigRow;
            }
            else
            {
//1				zr = nkbz.Zweig.NewZweigRow();
                zr = z.Zweig.NewZweigRow();
                zr.Zweig = "neuer Zweig";
                zr.BaumGuid = Parent.Guid;
                zr.ZweigGuid = Guid.NewGuid();
//1				nkbz.Zweig.AddZweigRow(zr);
                z.Zweig.AddZweigRow(zr);
            }

            // Zurückverknüpfen
            Parent.Childs.Add(this);
        }

        // Eigenschaften
        // -------------

        // MyRow
//1		public new NKBZDataSet.ZweigRow MyRow
        public new ZweigDataSet.ZweigRow MyRow
        {
            get { return (zr); }
        }

        // LastKnoten
        public KnotenZelle LastKnoten
        {
            get { return (((BaumZelle) Parent).LastKnoten); }
        }

        // Methoden
        // --------

        // MakeWeiter()
        public override SammelZelle MakeWeiter()
        {
            if (! MyRow.IsweiterBaumGuidNull())
            {
                BaumZelle bz = new BaumZelle(MyRow.weiterBaumGuid, Ebene + 1, this);
                Weiter = bz;
            }

            if (! MyRow.IsweiterNetzGuidNull())
            {
                NetzZelle nz = new NetzZelle(MyRow.weiterNetzGuid, Ebene + 1, this);
                Weiter = nz;
            }

            return (Weiter);
        }

        // Next()
        public override Zelle Next()
        {
            if (Weiter != null)
            {
                return (Weiter);
            }
            else
            {
                return (Parent.Next());
            }
        }

        // Top()
        public override NetzZelle Top()
        {
            return (Parent.Top());
        }

        // ToString()
        public override string ToString()
        {
            return ("-ZweigZelle: " + Guid);
        }
    }
}