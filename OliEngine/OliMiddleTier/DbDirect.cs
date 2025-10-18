// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIx;

namespace OliEngine.OliMiddleTier
{
    /// <summary>
    ///     DbDirect.
    /// </summary>
    public abstract class DbDirect
    {
        public static string GiveStamm(Guid sguid)
        {
            DataRow[] drs = OliDb.GiveRows("oli.Stamm", "StammGuid", sguid.ToString());
            return (drs[0]["Stamm"].ToString());
        }

        public static string GiveStamm(string strsguid)
        {
            Guid sguid = new Guid(strsguid);
            DataRow[] drs = OliDb.GiveRows("oli.Stamm", "StammGuid", sguid.ToString());
            return (drs[0]["Stamm"].ToString());
        }

        public static PostItDataSet.PostItDataTable SuchPostIt(string postit)
        {
            PostItList pl = new PostItList(postit, true);
            return (pl.PostIt);
        }

        public static TopLabDataSet.TopLabDataTable SuchTopLab(string topLab)
        {
            TopLabList tl = new TopLabList(topLab);
            return (tl.TopLab);
        }

        public static StammDataSet.StammDataTable SuchStamm(string stamm)
        {
            StammList sl = new StammList(stamm, true);
            return (sl.Stamm);
        }

        //public static DataTable GiveTable(string name)
        //{
        //    DataTable dt = OliDb.GiveTable(name);
        //    return (dt);
        //}

        public static string GiveNetz(string nguidstr)
        {
            string netz = "";
            if (nguidstr.Length > 0)
            {
                Guid nguid = new Guid(nguidstr);
//1				NKBZ nkbz = NKBZ.Instance();
//1				OliEngine.OliMiddleTier.OLIx.NKBZDataSet.NetzRow nr;
//1				nr = nkbz.Netz.FindByNetzGuid(nguid);
//1				netz = nr.Netz;
                Netz n = new Netz(nguid);
                netz = n.NetzRow.Netz;
            }
            return (netz);
        }

        public static string GiveKnoten(string kguidstr)
        {
            string knoten = "";
            if (kguidstr.Length > 0)
            {
                Guid kguid = new Guid(kguidstr);
//1				NKBZ nkbz = NKBZ.Instance();
//1				OliEngine.OliMiddleTier.OLIx.NKBZDataSet.KnotenRow kr;
//1				kr = nkbz.Knoten.FindByKnotenGuid(kguid);
                Knoten k = new Knoten(kguid);
                knoten = k.KnotenRow.Knoten;
            }
            return (knoten);
        }

        public static string GiveBaum(string bguidstr)
        {
            string baum = "";
            if (bguidstr.Length > 0)
            {
                Guid bguid = new Guid(bguidstr);
//1				NKBZ nkbz = NKBZ.Instance();
//1				OliEngine.OliMiddleTier.OLIx.NKBZDataSet.BaumRow br;
//1				br = nkbz.Baum.FindByBaumGuid(bguid);
                Baum b = new Baum(bguid);
                baum = b.BaumRow.Baum;
            }
            return (baum);
        }

        public static string GiveZweig(string zguidstr)
        {
            string zweig = "";
            if (zguidstr.Length > 0)
            {
                Guid zguid = new Guid(zguidstr);
//				NKBZ nkbz = NKBZ.Instance();
//				OliEngine.OliMiddleTier.OLIx.NKBZDataSet.ZweigRow zr;
//				zr = nkbz.Zweig.FindByZweigGuid(zguid);
                Zweig z = new Zweig(zguid);
                zweig = z.ZweigRow.Zweig;
            }
            return (zweig);
        }
    }
}