// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     KnotenZelle.
    /// </summary>
    public class KnotenZelle : VerteilZelle
    {
        // Member
        // ------

//1		NKBZ nkbz;
//1		NKBZDataSet.KnotenRow kr;
        private readonly Knoten k;
        private readonly KnotenDataSet.KnotenRow kr;

        // Konstruktor
        // -----------

        public KnotenZelle(Guid kguid, int ebene, SammelZelle parent) : base(kguid, ebene, parent)
        {
//1			nkbz = NKBZ.Instance();
            k = new Knoten(kguid);

            // TODO: hier soll eigentlich auf DataRowType == Added gepr�ft werden
            if (kguid != Guid.Empty)
            {
//1				kr = nkbz.Knoten.FindByKnotenGuid(kguid);
                kr = k.KnotenRow;
            }
            else
            {
//1				kr = nkbz.Knoten.NewKnotenRow();
                kr = k.Knoten.NewKnotenRow();
                kr.Knoten = "neuer Knoten";
                kr.Datum = DateTime.UtcNow;
                kr.VgbOLIs = 2;
                kr.VgbGet = 0;
                kr.VgbILOs = 2;
                kr.VgbFit = 0;
                kr.NetzGuid = Parent.Guid;
                kr.KnotenGuid = Guid.NewGuid();
//1				nkbz.Knoten.AddKnotenRow(kr);
                k.Knoten.AddKnotenRow(kr);
            }

            // Zur�ckverkn�pfen
            Parent.Childs.Add(this);
        }

        // Eigenschaften
        // -------------

        // MyRow
//1		public new NKBZDataSet.KnotenRow MyRow
        public new KnotenDataSet.KnotenRow MyRow
        {
            get { return (kr); }
        }

        // Methoden
        // --------

        // MakeWeiter
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
            return ("-KnotenZelle: " + Guid);
        }
    }
}
