// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     NetzZelle.
    /// </summary>
    public class NetzZelle : SammelZelle
    {
        // Member
        // ------

//1		NKBZ nkbz;
        private readonly Netz n;
//1		NKBZDataSet.NetzRow nr;
        private readonly NetzDataSet.NetzRow nr;
        private int nextChild;

        // Konstruktor
        // -----------

        public NetzZelle(Guid nguid, int ebene, VerteilZelle von) : base(nguid, ebene, von)
        {
//1			nkbz = NKBZ.Instance();
            n = new Netz(nguid);
            if (nguid != Guid.Empty)
            {
//1				nr = nkbz.Netz.FindByNetzGuid(nguid);
                nr = n.NetzRow;
            }
            else
            {
//1				nr = nkbz.Netz.NewNetzRow();
                nr = n.Netz.NewNetzRow();
                nr.Netz = "neues Netz";
                nr.NetzGuid = Guid.NewGuid();
//1				nkbz.Netz.AddNetzRow(nr);
                n.Netz.AddNetzRow(nr);
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

//1		public new NKBZDataSet.NetzRow MyRow
        public new NetzDataSet.NetzRow MyRow
        {
            get { return (nr); }
        }

        // Methoden
        // --------

        // RemoveMe()
        public override void RemoveMe()
        {
            Von.Weiter = null;
        }

        // MakeChild()
        public override VerteilZelle MakeChild(Guid kguid)
        {
            KnotenZelle kz = new KnotenZelle(kguid, Ebene, this);
            return (kz);
        }

        // MakeAllMyChilds()
        public override void MakeAllMyChilds()
        {
            foreach (Object o in MyRow.GetChildRows("NetzKnoten"))
            {
                NKBZDataSet.KnotenRow kr = (NKBZDataSet.KnotenRow) o;
                MakeChild(kr.KnotenGuid);
            }
        }

        // Next()
        public override Zelle Next()
        {
            KnotenZelle kz = null;
            try
            {
                kz = (KnotenZelle) Childs[nextChild];
                nextChild ++;
                return (kz);
            }
            catch
            {
                nextChild = 0;
                if (Von != null)
                {
                    return (Von.Parent.Next());
                }
            }
            return (null);
        }

        // Top()
        public override NetzZelle Top()
        {
            if (Von == null)
            {
                return (this);
            }
            else
            {
                return (Von.Top());
            }
        }

        // NewChild()
        public override VerteilZelle NewChild()
        {
            KnotenZelle kz = new KnotenZelle(Guid.Empty, Ebene, this);
            return (kz);
        }

        // ToString()
        public override string ToString()
        {
            return ("NetzZelle: " + Guid);
        }
    }
}