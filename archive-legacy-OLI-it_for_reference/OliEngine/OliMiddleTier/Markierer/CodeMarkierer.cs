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
    ///     CodeMarkierer.
    /// </summary>
    public class CodeMarkierer : Markierer
    {
        // Member
        // ------

//		OliEngine.OliDataAccess.ShortCuts shortcuts;
//		int shortCutsId;
        private Code code;

        // Konstruktor
        // -----------

        public CodeMarkierer(Guid codeGuid)
        {
            MyGuid = codeGuid;
            CodeGuid = codeGuid;
            code = new Code(codeGuid);
        }

        // Eigenschaften
        // -------------

        // Code
        private Code Code
        {
            get { return (code); }
        }

        // Strings
        public override DataTable Strings
        {
            get { return (code.Ringe); }
        }

        // CodeGuid
        private Guid CodeGuid { get; set; }

        // Methoden
        // --------

        // IsInStrings(NetzRow)
        public override bool IsInStrings(NetzDataSet.NetzRow nr)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "NetzGuid='" + nr.NetzGuid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(KnotenRow)
        public override bool IsInStrings(KnotenDataSet.KnotenRow kr)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "KnotenGuid='" + kr.KnotenGuid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(BaumRow)
        public override bool IsInStrings(BaumDataSet.BaumRow br, KnotenZelle lastKnoten)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "BaumGuid = '" + br.BaumGuid + "' AND KnotenGuid = '" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ZweigRow)
        public override bool IsInStrings(ZweigDataSet.ZweigRow zr, KnotenZelle lastKnoten)
        {
            DataView dv = new DataView(Strings);
            dv.RowFilter = "ZweigGuid = '" + zr.ZweigGuid + "' AND KnotenGuid = '" + lastKnoten.Guid + "'";

            return (dv.Count > 0);
        }

        // IsInStrings(ref KnotenZelle)
        public override bool IsInStrings(ref KnotenZelle kz) // Obacht: Parameter wird ge�ndert!
        {
            kz.VgbOLIs = -1;
            kz.VgbGet = -1;

            DataView dv = new DataView(Strings);
            dv.RowFilter = "KnotenGuid = '" + kz.Guid + "'";

            if (dv.Count > 0)
            {
                kz.VgbOLIs = int.Parse(dv[0]["OLIs"].ToString());
                kz.VgbGet = int.Parse(dv[0]["get"].ToString());

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
                zz.VgbOLIs = int.Parse(dv[0]["OLIs"].ToString());
                zz.VgbGet = int.Parse(dv[0]["get"].ToString());
            }

            return (dv.Count > 0);
        }

        // Markiere(KnotenZelle)
        public override void Markiere(KnotenZelle kz)
        {
            if (!IsInStrings(kz.MyRow))
            {
                CodeDataSet.RingeRow rr = ((CodeDataSet.RingeDataTable) Strings).NewRingeRow();

                rr.RingGuid = Guid.NewGuid();
                rr.CodeGuid = CodeGuid;
                rr.NetzGuid = kz.Parent.Guid;
                rr.KnotenGuid = kz.Guid;

                rr.OLIs = kz.VgbOLIs;
                rr.Get = kz.VgbGet;

                // Reihe hinzuf�gen
                Strings.Rows.Add(rr);

                // Update
                Code.UpdateRinge();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                code = new Code(CodeGuid);

                // Farbpunkte setzen
                IsInStrings(ref kz);
            }
        }

        // Markiere(ZweigZelle)
        public override void Markiere(ZweigZelle zz)
        {
            if (!IsInStrings(zz.MyRow, ((BaumZelle) zz.Parent).LastKnoten))
            {
                CodeDataSet.RingeRow rr = ((CodeDataSet.RingeDataTable) Strings).NewRingeRow();

                KnotenZelle kz = ((BaumZelle) zz.Parent).LastKnoten;

                rr.RingGuid = Guid.NewGuid();
                rr.CodeGuid = CodeGuid;
                rr.NetzGuid = kz.Parent.Guid;
                rr.KnotenGuid = kz.Guid;
                rr.BaumGuid = zz.Parent.Guid;
                rr.ZweigGuid = zz.Guid;

                rr.OLIs = kz.VgbOLIs;
                rr.Get = kz.VgbGet;

                // Reihe hinzuf�gen
                Strings.Rows.Add(rr);

                // Update
                Code.UpdateRinge();

                // mein DataSet neu erstellen
                // (neu laden - damit die neu hinzugef�gten ihre IDs erhalten)
                code = new Code(CodeGuid);

                // Farbpunkte setzen
                IsInStrings(ref zz);
            }
        }

        // Clear(KnotenZelle)
        public override void Clear(KnotenZelle kz)
        {
            DataRow[] sr = Code.Ringe.Select("KnotenGuid = '" + kz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            Code.UpdateRinge();

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
                Code.Ringe.Select("KnotenGuid = '" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            foreach (DataRow dr in sr)
            {
                dr.Delete();
            }
            Code.UpdateRinge();

            zz.Weiter = null;

            zz.VgbOLIs = -1;
            zz.VgbGet = -1;
            zz.VgbILOs = -1;
            zz.VgbFit = -1;
        }

        // UpdatePunkte(KnotenZelle)
        public override void UpdatePunkte(KnotenZelle kz)
        {
            DataRow[] dr = Code.Ringe.Select("KnotenGuid = '" + kz.Guid + "'");
            if (dr.Length > 0)
            {
                CodeDataSet.RingeRow rr = (CodeDataSet.RingeRow) dr[0];

                rr.OLIs = kz.VgbOLIs;
                rr.Get = kz.VgbGet;

                Code.UpdateRinge();
            }

            kz.VgbILOs = -1;
            kz.VgbFit = -1;
        }

        // UpdatePunkte(ZweigZelle)
        public override void UpdatePunkte(ZweigZelle zz)
        {
            DataRow[] dr = Code.Ringe.Select("KnotenGuid='" + zz.LastKnoten.Guid + "' AND ZweigGuid = '" + zz.Guid + "'");
            CodeDataSet.RingeRow rr = (CodeDataSet.RingeRow) dr[0];

            rr.OLIs = zz.VgbOLIs;
            rr.Get = zz.VgbGet;

            zz.VgbILOs = -1;
            zz.VgbFit = -1;

            Code.UpdateRinge();
        }
    }
}
