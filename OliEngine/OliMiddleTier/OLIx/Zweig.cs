// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;

namespace OliEngine.OliMiddleTier.OLIx
{
    /// <summary>
    ///     Ein Zweig ist eine Auspr�gung (Alternative) in einem Baum.
    ///     Diese Stelle wird markiert bzw. gefiltert.
    ///     Sie kann zu weiteren Netzen und B�umen weiter verlinken.
    /// </summary>
    public class Zweig : ZweigDataSet
    {
        private SqlDataAdapter ad;

        public Zweig(Guid zguid)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Zweig_ZweigGuid";
            cmd.Parameters.Add(new SqlParameter("@ZweigGuid", zguid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Zweig);
        }

        public Zweig(BaumDataSet.BaumRow br)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Zweig_BaumGuid";
            cmd.Parameters.Add(new SqlParameter("@BaumGuid", br.BaumGuid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Zweig);
        }

        public new ZweigRow ZweigRow
        {
            get
            {
                if (Zweig.Rows.Count != 1)
                {
                    throw new Exception("Zweig Reihe nicht eindeutig");
                }
                return (ZweigRow) Zweig.Rows[0];
            }
        }

        public int UpdateZweig()
        {
            return ad.Update(Zweig);
        }
    }
}
