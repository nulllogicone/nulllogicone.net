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
    ///     AnglerMarkierer.
    /// </summary>
    public class AnglerMarkierer : Markierer
    {
        // Member
        // ------

        private Angler angler;

        // Konstruktor
        // -----------

        public AnglerMarkierer(Guid aguid)
        {
            MyGuid = aguid;
            AnglerGuid = aguid;
            angler = new Angler(aguid);
        }

        // Eigenschaften
        // -------------

        // Code
        private Angler Angler
        {
            get { return (angler); }
        }

        // Strings
        public override DataTable Strings
        {
            get { return (angler.L�cher); }
        }

        // AnglerGuid
        private Guid AnglerGuid { get; set; }

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
            dv.RowFilter = "BaumGuid='" + br.BaumGuid + "' AND KnotenGuid='" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ZweigRow)
//1		public override bool IsInStrings(NKBZDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        public override bool IsInStrings(ZweigDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "ZweigGuid='" + zr.ZweigGuid + "' AND KnotenGuid='" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ref KnotenZelle)
        public override bool IsInStrings(ref KnotenZelle kz) // Obacht: Parameter wird ge�ndert!
        {
            kz.VgbILOs = -1;
            kz.VgbFit = -1;

            DataView dv = new DataView(Strings);
            dv.RowFilter = "KnotenGuid = '" + kz.Guid + "'";

            if (dv.Count > 0)
            {
                kz.VgbILOs = int.Parse(dv[0]["ILOs"].ToString());
                kz.VgbFit = int.Parse(dv[0]["fit"].ToString());

                return (true);
            }
            else
            {
                return (false);
            }
        }

        // IsInStrings(ref ZweigZelle)
        public override bool IsInStrings(ref ZweigZelle zz) // Obacht: Parameter wird ge�ndert!
        {
            zz.VgbOLIs = -1;
            zz.VgbGet = -1;

            DataView dv = new DataView(Strings);
            dv.RowFilter = "ZweigGuid = '" + zz.Guid + "' AND KnotenGuid = '" + zz.LastKnoten.Guid + "'";

            if (dv.Count > 0)
            {
                zz.VgbILOs = int.Parse(dv[0]["ILOs"].ToString());
                zz.VgbFit = int.Parse(dv[0]["fit"].ToString());
                return (true);
            }

            return (dv.Count > 0);
        }

        // Markiere(KnotenZelle)
        public override void Markiere(KnotenZelle kz)
        {
            if (!IsInStrings(kz.MyRow))
            {
                AnglerDataSet.L�cherRow lr = ((AnglerDataSet.L�cherDataTable) Strings).NewL�cherRow();

                lr.LochGuid = Guid.NewGuid();
                lr.AnglerGuid = AnglerGuid;
                lr.NetzGuid = kz.Parent.Guid;
                lr.KnotenGuid = kz.Guid;

                lr.ILOs = kz.VgbILOs;
                lr.Fit = kz.VgbFit;

                // Reihe hinzuf�gen
                Strings.Rows.Add(lr);

                // Update
                Angler.UpdateL�cher();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                angler = new Angler(MyGuid);

                // Farbpunkte setzen
                IsInStrings(ref kz);
            }
        }

        // Markiere(ZweigZelle)
        public override void Markiere(ZweigZelle zz)
        {
            if (!IsInStrings(zz.MyRow, ((BaumZelle) zz.Parent).LastKnoten))
            {
                AnglerDataSet.L�cherRow lr = ((AnglerDataSet.L�cherDataTable) Strings).NewL�cherRow();
                KnotenZelle kz = ((BaumZelle) zz.Parent).LastKnoten;

                lr.LochGuid = Guid.NewGuid();
                lr.AnglerGuid = AnglerGuid;
                lr.NetzGuid = kz.Parent.Guid;
                lr.KnotenGuid = kz.Guid;
                lr.BaumGuid = zz.Parent.Guid;
                lr.ZweigGuid = zz.Guid;

                lr.ILOs = kz.VgbILOs;
                lr.Fit = kz.VgbFit;

                // Reihe hinzuf�gen
                Strings.Rows.Add(lr);

                // Update
                Angler.UpdateL�cher();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                angler = new Angler(MyGuid);

                // Farbpunkte setzen
                IsInStrings(ref zz);
            }
        }

        // Clear(KnotenZelle)
        public override void Clear(KnotenZelle kz)
        {
            DataRow[] sr = Angler.L�cher.Select("KnotenGuid = '" + kz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            Angler.UpdateL�cher();

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
                Angler.L�cher.Select("KnotenGuid = '" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            Angler.UpdateL�cher();

            zz.Weiter = null;

            zz.VgbOLIs = -1;
            zz.VgbGet = -1;
            zz.VgbILOs = -1;
            zz.VgbFit = -1;
        }

        // UpdatePunkte(KnotenZelle)
        public override void UpdatePunkte(KnotenZelle kz)
        {
            DataRow[] dr = Angler.L�cher.Select("KnotenGuid = '" + kz.Guid + "'");
            if (dr.Length > 0)
            {
                AnglerDataSet.L�cherRow lr = (AnglerDataSet.L�cherRow) dr[0];

                lr.ILOs = kz.VgbILOs;
                lr.Fit = kz.VgbFit;

                Angler.UpdateL�cher();
            }
            kz.VgbOLIs = -1;
            kz.VgbGet = -1;
        }

        // UpdatePunkte(ZweigZelle)
        public override void UpdatePunkte(ZweigZelle zz)
        {
            DataRow[] dr =
                Angler.L�cher.Select("KnotenGuid = '" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            AnglerDataSet.L�cherRow lr = (AnglerDataSet.L�cherRow) dr[0];

            lr.ILOs = zz.VgbILOs;
            lr.Fit = zz.VgbFit;

            zz.VgbOLIs = -1;
            zz.VgbGet = -1;

            Angler.UpdateL�cher();
        }
    }
}
