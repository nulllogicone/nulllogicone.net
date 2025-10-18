// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace OliEngine.OliMiddleTier.OLIx
{
    /// <summary>
    ///     Ein Baum ist im semantischen Wortraum eine Hierarchieebene.
    ///     Ein Baum besteht aus Zweigen, die Alternativen darstellen und zu Netzen oder Bäumen weiterverzweigen.
    ///     Ein Baum kann mehrfach verlinkt werden.
    ///     Die Defaultmarkierung wird aus dem verlinkenden Knoten übernommen.
    /// </summary>
    public class Baum : BaumDataSet
    {
        private SqlDataAdapter ad;

        public Baum()
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand("SELECT * FROM oli.Baum ORDER BY Baum");
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Baum);
        }

        public Baum(Guid bguid)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Baum_BaumGuid";
            cmd.Parameters.Add(new SqlParameter("@BaumGuid", bguid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Baum);
        }

        public new BaumRow BaumRow
        {
            get
            {
                if (Baum.Rows.Count != 1)
                {
                    throw new Exception("Baum Reihe nicht eindeutig");
                }
                return (BaumRow) Baum.Rows[0];
            }
        }

        public int UpdateBaum()
        {
            return ad.Update(Baum);
        }

        public List<List<HyperLink>> PathToRoot()
        {
            List<List<HyperLink>> col = new List<List<HyperLink>>();
            List<HyperLink> hll = new List<HyperLink>();

            // such die Knoten die auf dich verweisen
            NKBZ nkbz = new NKBZ();
            var vonKnoten = from k in nkbz.Knoten
                            where
                                !k.IsweiterBaumGuidNull() && k.weiterBaumGuid == BaumRow.BaumGuid
                            select k;
            foreach (NKBZDataSet.KnotenRow k in vonKnoten)
            {
                // gib den rekursiven Pfad zurück
                Netz vonNetz = new Netz(k.NetzGuid);

                foreach (List<HyperLink> linklist in vonNetz.PathToRoot())
                {
                    HyperLink hl = new HyperLink();
                    hl.Text = BaumRow.Baum;
                    hl.CssClass = "Baum";
                    hl.NavigateUrl = "BlaetterWald.aspx?bguid=" + BaumRow.BaumGuid;

                    linklist.Add(hl);
                    col.Add(linklist);
                }
            }

            // such die Zweige die auf dich verweisen
            var vonZweigen = from z in nkbz.Zweig
                             where !z.IsweiterBaumGuidNull() && z.weiterBaumGuid == BaumRow.BaumGuid
                             select z;
            foreach (NKBZDataSet.ZweigRow z in vonZweigen)
            {
                // gib den rekursiven Pfad zurück
                Baum vonBaum = new Baum(z.BaumGuid);

                List<List<HyperLink>> pathVonBaum = vonBaum.PathToRoot();
                if (pathVonBaum != null)
                {
                    HyperLink hl = new HyperLink();
                    hl.Text = BaumRow.Baum;
                    hl.CssClass = "Baum";
                    hl.NavigateUrl = string.Format("BlaetterWald.aspx?bguid={0}", BaumRow.BaumGuid);
                    foreach (List<HyperLink> linklist in vonBaum.PathToRoot())
                    {
                        linklist.Add(hl);
                        col.Add(linklist);
                    }
                }
            }

            return col;
        }
    }
}