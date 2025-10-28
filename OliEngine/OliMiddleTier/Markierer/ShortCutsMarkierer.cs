// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.Markierer
{
    /// <summary>
    ///     ShortCutsMarkierer.
    /// </summary>
    public class ShortCutsMarkierer : Markierer
    {
        // Member
        // ------

        private ShortCuts shortcuts;

        // Konstruktor
        // -----------

        public ShortCutsMarkierer(Guid shortCutsGuid)
        {
            MyGuid = shortCutsGuid;
            ShortCutsGuid = shortCutsGuid;
            shortcuts = new ShortCuts(shortCutsGuid);
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
            get { return (shortcuts.Strings); }
        }

        // ShortCutsGuid
        private Guid ShortCutsGuid { get; set; }

        // Methoden
        // --------

        // IsInStrings(NetzRow)
//1		public override bool IsInStrings(NKBZDataSet.NetzRow nr)
        public override bool IsInStrings(NetzDataSet.NetzRow nr)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "NetzGuid='" + nr.NetzGuid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(KnotenRow)
//1		public override bool IsInStrings(NKBZDataSet.KnotenRow kr)
        public override bool IsInStrings(KnotenDataSet.KnotenRow kr)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "KnotenGuid='" + kr.KnotenGuid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(BaumRow)
//1		public override bool IsInStrings(NKBZDataSet.BaumRow br, KnotenZelle lastKnoten)
        public override bool IsInStrings(BaumDataSet.BaumRow br, KnotenZelle lastKnoten)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "BaumGuid = '" + br.BaumGuid + "' AND KnotenGuid='" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ZweigRow)
//1		public override bool IsInStrings(NKBZDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        public override bool IsInStrings(ZweigDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "ZweigGuid = '" + zr.ZweigGuid + "' AND KnotenGuid = '" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ref KnotenZelle)
        public override bool IsInStrings(ref KnotenZelle kz)
        {
            kz.VgbILOs = -1;
            kz.VgbFit = -1;

            DataView dv = new DataView(Strings);
            dv.RowFilter = "KnotenGuid = '" + kz.Guid + "'";

            if (dv.Count > 0)
            {
                kz.VgbOLIs = int.Parse(dv[0]["Verb"].ToString());
                kz.VgbGet = int.Parse(dv[0]["Attrib"].ToString());

                return (true);
            }
            else
            {
                return (false);
            }
        }

        // IsInStrings(ZweigZelle)
        public override bool IsInStrings(ref ZweigZelle zz)
        {
            zz.VgbOLIs = -1;
            zz.VgbGet = -1;

            DataView dv = new DataView(Strings);
            dv.RowFilter = "ZweigGuid = '" + zz.Guid + "' AND KnotenGuid = '" + zz.LastKnoten.Guid + "'";

            if (dv.Count > 0)
            {
                zz.VgbOLIs = int.Parse(dv[0]["Verb"].ToString());
                zz.VgbGet = int.Parse(dv[0]["Attrib"].ToString());
            }

            return (dv.Count > 0);
        }

        // Markiere(KnotenZelle)
        public override void Markiere(KnotenZelle kz)
        {
            if (!IsInStrings(kz.MyRow))
            {
                ShortCutsDataSet.StringsRow sr = ((ShortCutsDataSet.StringsDataTable) Strings).NewStringsRow();

                sr.StringsGuid = Guid.NewGuid();
                sr.ShortCutsGuid = ShortCutsGuid;
                sr.NetzGuid = kz.Parent.Guid;
                sr.KnotenGuid = kz.Guid;

                sr.Verb = kz.VgbOLIs;
                sr.Attrib = kz.VgbGet;

                // Reihe hinzuf�gen
                Strings.Rows.Add(sr);

                // Update
                ShortCuts.UpdateStrings();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                shortcuts = new ShortCuts(ShortCutsGuid);

                // Farbpunkte setzen
                IsInStrings(ref kz);
            }
        }

        // Markiere(ZweigZelle)
        public override void Markiere(ZweigZelle zz)
        {
            if (!IsInStrings(zz.MyRow, ((BaumZelle) zz.Parent).LastKnoten))
            {
                ShortCutsDataSet.StringsRow sr = ((ShortCutsDataSet.StringsDataTable) Strings).NewStringsRow();

                KnotenZelle kz = ((BaumZelle) zz.Parent).LastKnoten;

                sr.StringsGuid = Guid.NewGuid();
                sr.ShortCutsGuid = ShortCutsGuid;
                sr.NetzGuid = kz.Parent.Guid;
                sr.KnotenGuid = kz.Guid;
                sr.BaumGuid = zz.Parent.Guid;
                sr.ZweigGuid = zz.Guid;

                sr.Verb = kz.VgbOLIs;
                sr.Attrib = kz.VgbGet;

                // Reihe hinzuf�gen
                Strings.Rows.Add(sr);

                // Update
                ShortCuts.UpdateStrings();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                shortcuts = new ShortCuts(ShortCutsGuid);

                // Farbpunkte setzen
                IsInStrings(ref zz);
            }
        }

        // Clear(KnotenZelle)
        public override void Clear(KnotenZelle kz)
        {
            DataRow[] sr = ShortCuts.Strings.Select("KnotenGuid = '" + kz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            ShortCuts.UpdateStrings();

            kz.Weiter = null;

            kz.VgbOLIs = -1;
            kz.VgbGet = -1;
            kz.VgbILOs = -1;
            kz.VgbFit = -1;
        }

        // Clear(ZweigZelle)
        public override void Clear(ZweigZelle zz)
        {
            DataRow[] sr =
                ShortCuts.Strings.Select("KnotenGuid = '" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            ShortCuts.UpdateStrings();

            zz.Weiter = null;

            zz.VgbOLIs = -1;
            zz.VgbGet = -1;
            zz.VgbILOs = -1;
            zz.VgbFit = -1;
        }

        // UpdatePunkte(KnotenZelle)
        public override void UpdatePunkte(KnotenZelle kz)
        {
            DataRow[] dr = ShortCuts.Strings.Select("KnotenGuid = '" + kz.Guid + "'");
            if (dr.Length > 0)
            {
                ShortCutsDataSet.StringsRow sr = (ShortCutsDataSet.StringsRow) dr[0];

                sr.Verb = kz.VgbOLIs;
                sr.Attrib = kz.VgbGet;

                ShortCuts.UpdateStrings();
            }
            kz.VgbILOs = -1;
            kz.VgbFit = -1;
        }

        // UpdatePunkte(ZweigZelle)
        public override void UpdatePunkte(ZweigZelle zz)
        {
            DataRow[] dr =
                ShortCuts.Strings.Select("KnotenGuid = '" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            ShortCutsDataSet.StringsRow sr = (ShortCutsDataSet.StringsRow) dr[0];

            sr.Verb = zz.VgbOLIs;
            sr.Attrib = zz.VgbGet;

            zz.VgbILOs = -1;
            zz.VgbFit = -1;

            ShortCuts.UpdateStrings();
        }
    }
}
