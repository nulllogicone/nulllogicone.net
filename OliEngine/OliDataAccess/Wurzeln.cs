// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Wurzeln.
    /// </summary>
    public class Wurzeln : WurzelnDataSet
    {
        private SqlDataAdapter wad;

        public Wurzeln()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            wad = new SqlDataAdapter("SELECT * FROM oli.Wurzeln WHERE 1=0", con);
            SqlCommandBuilder wcb = new SqlCommandBuilder(wad);

            wad.Fill(this, "Wurzeln");
        }

        public Wurzeln(StammDataSet.StammRow sr)
        {
        }

        public Wurzeln(Guid stammGuid, Guid postItGuid)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Wurzeln_StammGuid_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", stammGuid));
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", postItGuid));
            cmd.Connection = con;

            wad = new SqlDataAdapter();
            wad.SelectCommand = cmd;

            SqlCommandBuilder wcb = new SqlCommandBuilder(wad);

            wad.Fill(this, "Wurzeln");
        }

        public new WurzelnRow WurzelnRow
        {
            get
            {
                if (Wurzeln.Rows.Count == 1)
                {
                    return ((WurzelnRow) Wurzeln.Rows[0]);
                }
                else
                {
                    return (null);
                }
            }
        }

        public int UpdateWurzeln()
        {
            return (wad.Update(this, "Wurzeln"));
        }
    }
}