// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier.ZellHaufen
{
    /// <summary>
    ///     ZellBuilder.
    /// </summary>
    public class ZellBuilder
    {
        // Member
        // ------
        private NetzZelle root;
        private Markierer.Markierer markierer;

        // Konstruktor
        // -----------

        // Eigenschaften
        // -------------

        // Root
        public NetzZelle Root
        {
            get { return (root); }
        }

        /// <summary>
        ///     welcher Markierer steckt hinter diesem Zellbuilder.
        ///     Ein markierer arbeitet auf Code, Angler, ShortCut oder es ist ein Nullmarkierer.
        ///     Wenn er zugewiesen wird, wird die Wurzel mit den daranhängenden Markierungen gefüllt.
        /// </summary>
        public Markierer.Markierer Markierer
        {
            get { return (markierer); }
            set
            {
                markierer = value;
                root = OpenMarkedNetz(Netz.GetRoot().NetzRow.NetzGuid, 0, null);
            }
        }

        // Methoden
        // --------

        /// <summary>
        ///     wenn es nicht von hier nicht weitergeht werden alle Knoten bzw. Zweige geöffnet
        ///     sonst wird alles weitere entfernt
        /// </summary>
        /// <param name = "toggleZelle"></param>
        public void ToggleZelle(VerteilZelle toggleZelle)
        {
            if (toggleZelle.Weiter == null)
            {
                SammelZelle sz = toggleZelle.MakeWeiter();
                if (sz is BaumZelle)
                {
                    OpenAllZweige((BaumZelle) sz);
                }
                else
                {
                    OpenAllKnoten((NetzZelle) sz);
                }
            }
            else
            {
                toggleZelle.Weiter.RemoveMe();
            }
        }

        // NewChild(Netz)
        public VerteilZelle NewChild(NetzZelle netzZ)
        {
            KnotenZelle kz = new KnotenZelle(Guid.Empty, netzZ.Ebene, netzZ);
            return (kz);
        }

        // NewChild(Baum)
        public VerteilZelle NewChild(BaumZelle baumZ)
        {
            ZweigZelle zz = new ZweigZelle(Guid.Empty, baumZ.Ebene, baumZ);
            return (zz);
        }

        // NewNetz(VerteilZelle)
        public NetzZelle NewNetz(VerteilZelle von)
        {
            int e;
            if (von == null)
            {
                e = 0;
            }
            else
            {
                e = von.Ebene;
            }

            NetzZelle nz = new NetzZelle(Guid.Empty, e + 1, von);
            return nz;
        }

        // NewBaum(VerteilZelle)
        public BaumZelle NewBaum(VerteilZelle von)
        {
            BaumZelle bz = new BaumZelle(Guid.Empty, von.Ebene + 1, von);
            return bz;
        }

        // OpenMarkedNetz

        #region OpenMarkedNetz

        public NetzZelle OpenMarkedNetz(Guid nguid, int ebene, VerteilZelle von)
        {
            // NetzReihe
            NetzDataSet.NetzRow nr = new Netz(nguid).NetzRow;

            // entweder markiert oder oberste Ebene
            if (markierer.IsInStrings(nr) || von == null)
            {
                NetzZelle nz = new NetzZelle(nr.NetzGuid, ebene, von);

                // KnotenReihen
                KnotenDataSet.KnotenDataTable myKnoten = new Knoten(nz.MyRow).Knoten;
                foreach (KnotenDataSet.KnotenRow kr in myKnoten)
                {
                    // entweder markiert oder oberste Ebene
                    if (markierer.IsInStrings(kr) || nz.Von == null)
                    {
                        KnotenZelle kz = new KnotenZelle(kr.KnotenGuid, ebene, nz);

                        // Farbpunkte setzen
                        markierer.IsInStrings(ref kz);

                        // weiter Netz
                        if (!kr.IsweiterNetzGuidNull())
                        {
                            OpenMarkedNetz(kr.weiterNetzGuid, ebene + 1, kz);
                        }

                        // weiter Baum
                        if (!kr.IsweiterBaumGuidNull())
                        {
                            OpenMarkedBaum(kr.weiterBaumGuid, ebene + 1, kz, kz);
                        }
                    }
                }
                return (nz);
            }
            else
            {
                return (null);
            }
        }

        #endregion

        // OpenMarkedBaum

        #region OpenMarkedBaum

        public void OpenMarkedBaum(Guid bguid, int ebene, VerteilZelle von, KnotenZelle lastKnoten)
        {
            // BaumReihe
//1			NKBZDataSet.BaumRow br = nkbz.Baum.FindByBaumGuid(bguid);
            BaumDataSet.BaumRow br = new Baum(bguid).BaumRow;
            if (markierer.IsInStrings(br, lastKnoten))
            {
                BaumZelle bz = new BaumZelle(br.BaumGuid, ebene, von);

                // Zweige Reihen
//1				foreach(DataRow dr in br.GetChildRows("BaumZweig"))
                ZweigDataSet.ZweigDataTable myZweige = new Zweig(bz.MyRow).Zweig;
                foreach (ZweigDataSet.ZweigRow zr in myZweige)
                {
//1					NKBZDataSet.ZweigRow zr = (NKBZDataSet.ZweigRow)dr;
                    if (markierer.IsInStrings(zr, lastKnoten))
                    {
                        ZweigZelle zz = new ZweigZelle(zr.ZweigGuid, ebene, bz);

                        // Farbpunkte setzen
                        markierer.IsInStrings(ref zz);

                        // weiter Netz
                        if (!zr.IsweiterNetzGuidNull())
                        {
                            OpenMarkedNetz(zr.weiterNetzGuid, ebene + 1, zz);
                        }

                        // weiter Baum
                        if (!zr.IsweiterBaumGuidNull())
                        {
                            OpenMarkedBaum(zr.weiterBaumGuid, ebene + 1, zz, lastKnoten);
                        }
                    }
                }
            }
        }

        #endregion

        // OpenFullNetz()
//		private NetzZelle OpenFullNetz(int nid, int ebene, VerteilZelle von)
//		{
//			NetzZelle nz = new NetzZelle(nid, ebene, von);
//			OpenAllKnoten(nz);
//			return(nz);
//		}

        // OpenFullBaum()
//		private void OpenFullBaum(int bid, int ebene, VerteilZelle von)
//		{
//			BaumZelle bz = new BaumZelle(bid, ebene, von);
//			OpenAllZweige(bz);
//		}

        // OpenAllKnoten(NetzZelle)
        private void OpenAllKnoten(NetzZelle nz)
        {
            if (nz != null)
            {
                Knoten k = new Knoten(nz.MyRow);
                foreach (KnotenDataSet.KnotenRow kr in k.Knoten)
                {
                    KnotenZelle kz = new KnotenZelle(kr.KnotenGuid, nz.Ebene, nz);

                    // Farbpunkte setzen
                    markierer.IsInStrings(ref kz);
                }
            }
        }

        // OpenAllZweige(BaumZelle)
        private void OpenAllZweige(BaumZelle bz)
        {
            Zweig z = new Zweig(bz.MyRow);
            foreach (ZweigDataSet.ZweigRow zr in z.Zweig)
            {
                ZweigZelle zz = new ZweigZelle(zr.ZweigGuid, bz.Ebene, bz);

                // Farbpunkte setzen
                markierer.IsInStrings(ref zz);
            }
        }

        // Markiere(KnotenZelle)
        public void Markiere(KnotenZelle kz)
        {
            markierer.Markiere(kz);
        }

        // Markiere(ZweigZelle)
        public void Markiere(ZweigZelle zz)
        {
            markierer.Markiere(zz);
        }

        // Clear(KnotenZelle)
        public void Clear(KnotenZelle kz)
        {
            markierer.Clear(kz);
        }

        // Clear(ZweigZelle)
        public void Clear(ZweigZelle zz)
        {
            markierer.Clear(zz);
        }

        // UpdatePunkte(KnotenZelle)
        public void UpdatePunkte(KnotenZelle kz)
        {
            markierer.UpdatePunkte(kz);
        }

        // UpdatePunkte(ZweigZelle)
        public void UpdatePunkte(ZweigZelle zz)
        {
            markierer.UpdatePunkte(zz);
        }

        // ToString()
        public override string ToString()
        {
            string s = "";
            Zelle z = Root;

            while (z != null)
            {
                for (int i = 0; i < z.Ebene; i++)
                {
                    s += "  ";
                }
                s += z + "<br>";
                z = z.Next();
            }
            return (s);
        }
    }
}