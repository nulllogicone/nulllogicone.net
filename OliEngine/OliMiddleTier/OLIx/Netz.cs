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
    ///     Ein Netz ist eine 'semantische Ebene' im Wortraum.
    ///     Ein Netz ist im Wortraum eindeutig (kann nicht mehrfach verlinkt sein).
    ///     Es enthält Knoten, die markiert werden können (mit default Werten), 
    ///     und von denen weitere Netze bzw. Bäume verlinkt sind.
    /// </summary>
    public class Netz : NetzDataSet
    {
        private SqlDataAdapter ad;

        /// <summary>
        ///     erstellt ein neues Netz und lädt alle Datensätze
        /// </summary>
        public Netz()
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand("SELECT * FROM oli.Netz ORDER BY Netz");
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Netz);
        }

        public Netz(Guid nguid)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Netz_NetzGuid";
            cmd.Parameters.Add(new SqlParameter("@NetzGuid", nguid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Netz);
        }

        public static Netz GetRoot()
        {
            Netz n = new Netz(new Guid("76035F19-F4AE-4D58-A388-4BBC72C51CEF"));
            return n;
        }

        public new NetzRow NetzRow
        {
            get
            {
                if (Netz.Rows.Count != 1)
                {
                    throw new Exception("Netz Reihe nicht eindeutig");
                }
                return (NetzRow) Netz.Rows[0];
            }
        }

        public int UpdateNetz()
        {
            return ad.Update(Netz);
        }

        public List<List<HyperLink>> PathToRoot()
        {
            List<List<HyperLink>> col = new List<List<HyperLink>>();
            List<HyperLink> hll = new List<HyperLink>();
            if (NetzRow.NetzGuid == GetRoot().NetzRow.NetzGuid)
            {
                hll = new List<HyperLink>();
                HyperLink hl = new HyperLink();
                hl.Text = "root";
                hl.CssClass = "Netz";
                hl.NavigateUrl = "BlaetterWald.aspx?nguid=" + GetRoot().NetzRow.NetzGuid;
                hll.Add(hl);

                col.Add(hll);
            }
            else
            {
                // such die Knoten die auf dich verweisen
                NKBZ nkbz = new NKBZ();
                var vonKnoten = from k in nkbz.Knoten
                                where
                                    !k.IsweiterNetzGuidNull() && k.weiterNetzGuid == NetzRow.NetzGuid
                                select k;

                foreach (NKBZDataSet.KnotenRow k in vonKnoten)
                {
                    // gib den rekursiven Pfad zurück
                    Netz vonNetz = new Netz(k.NetzGuid);

                    HyperLink hl = new HyperLink();
                    hl.Text = NetzRow.Netz;
                    hl.CssClass = "Netz";
                    hl.NavigateUrl = "BlaetterWald.aspx?nguid=" + NetzRow.NetzGuid;

                    foreach (List<HyperLink> linklist in vonNetz.PathToRoot())
                    {
                        linklist.Add(hl);
                        col.Add(linklist);
                    }
                }

                // such die Zweige die auf dich verweisen
                var vonZweigen = from z in nkbz.Zweig
                                 where !z.IsweiterNetzGuidNull() && z.weiterNetzGuid == NetzRow.NetzGuid
                                 select z;
                foreach (NKBZDataSet.ZweigRow z in vonZweigen)
                {
                    // gib den rekursiven Pfad zurück
                    Baum vonBaum = new Baum(z.BaumGuid);

                    List<List<HyperLink>> pathVonBaum = vonBaum.PathToRoot();
                    if (pathVonBaum != null)
                    {
                        HyperLink hl = new HyperLink();
                        hl.Text = NetzRow.Netz;
                        hl.CssClass = "Netz";
                        hl.NavigateUrl = string.Format("BlaetterWald.aspx?nguid={0}", NetzRow.NetzGuid);

                        foreach (List<HyperLink> linklist in vonBaum.PathToRoot())
                        {
                            linklist.Add(hl);
                            col.Add(linklist);
                        }
                    }
                }
            }
            return col;
        }
    }
}