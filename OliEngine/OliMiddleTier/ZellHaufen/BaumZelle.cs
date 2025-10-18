// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     BaumZelle.
    /// </summary>
    public class BaumZelle : SammelZelle
    {
        // Member
        // ------

//1		NKBZ nkbz;
//1		NKBZDataSet.BaumRow br;
        private readonly Baum b;
        private readonly BaumDataSet.BaumRow br;

        private int nextChild;

        // Konstruktor
        // -----------

        public BaumZelle(Guid bguid, int ebene, VerteilZelle von) : base(bguid, ebene, von)
        {
//1			nkbz = NKBZ.Instance();
            b = new Baum(bguid);

            if (bguid != Guid.Empty)
            {
//1				br = nkbz.Baum.FindByBaumGuid(bguid);
                br = b.BaumRow;
            }
            else
            {
//1				br = nkbz.Baum.NewBaumRow();
                br = b.Baum.NewBaumRow();
                br.Baum = "neuer Baum";
                br.BaumGuid = Guid.NewGuid();
//1				nkbz.Baum.AddBaumRow(br);
                b.Baum.AddBaumRow(br);
            }

            // sicherstellen, dass dieses neue Objekt auch an
            // seinen Ursprung gebunden ist
            if (von != null)
            {
                von.Weiter = this;
            }
        }

        // Eigenschaften
        // -------------

//1		public new NKBZDataSet.BaumRow MyRow
        public new BaumDataSet.BaumRow MyRow
        {
            get { return (br); }
        }

        // LastKnoten
        public KnotenZelle LastKnoten
        {
            get
            {
                VerteilZelle vz = Von;
                while (!(vz is KnotenZelle))
                {
                    vz = vz.Parent.Von;
                }
                return ((KnotenZelle) vz);
            }
        }

        // Methoden
        // --------

        // RemoveMe()
        public override void RemoveMe()
        {
            Von.Weiter = null;
        }

        // MakeChild()
        public override VerteilZelle MakeChild(Guid zguid)
        {
            ZweigZelle zz = new ZweigZelle(zguid, Ebene, this);
            return (zz);
        }

        // MakeAllMyChilds
        public override void MakeAllMyChilds()
        {
            foreach (Object o in MyRow.GetChildRows("BaumZweig"))
            {
                NKBZDataSet.ZweigRow zr = (NKBZDataSet.ZweigRow) o;
                MakeChild(zr.ZweigGuid);
            }
        }

        // Next()
        public override Zelle Next()
        {
            ZweigZelle zz = null;
            try
            {
                zz = (ZweigZelle) Childs[nextChild];
                nextChild ++;
            }
            catch
            {
                nextChild = 0;
                return (Von.Parent.Next());
            }

            return (zz);
        }

        // Top()
        public override NetzZelle Top()
        {
            return (Von.Top());
        }

        public override VerteilZelle NewChild()
        {
            ZweigZelle zz = new ZweigZelle(Guid.Empty, Ebene, this);
            return (zz);
        }

        // ToString()
        public override string ToString()
        {
            return ("BaumZelle: " + Guid);
        }
    }
}