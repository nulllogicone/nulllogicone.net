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
    ///     Ein Knoten ist ein Teil von einem Netz.
    ///     Sie können sich semantisch überschneiden und stellen eine Ansammlung von Unterpunkten dar.
    /// </summary>
    public class Knoten : KnotenDataSet
    {
        private SqlDataAdapter ad;

        public Knoten(Guid kguid)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Knoten_KnotenGuid";
            cmd.Parameters.Add(new SqlParameter("@KnotenGuid", kguid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Knoten);
        }

        public Knoten(NetzDataSet.NetzRow nr)
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Knoten_NetzGuid";
            cmd.Parameters.Add(new SqlParameter("@NetzGuid", nr.NetzGuid));
            cmd.Connection = con;
            ad = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Fill(Knoten);
        }

        public new KnotenRow KnotenRow
        {
            get
            {
                if (Knoten.Rows.Count != 1)
                {
                    throw new Exception("Knoten Reihe nicht eindeutig");
                }
                return (KnotenRow) Knoten.Rows[0];
            }
        }

        public int UpdateKnoten()
        {
            return ad.Update(Knoten);
        }
    }
}