// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     StammPostItTopLabTollis.
    /// </summary>
    public class StammPostItTopLabTollis : StammPostItTopLabTollisDataSet
    {
        public StammPostItTopLabTollis(Guid sguid, Guid pguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_StammPostItTopLabTollis_StammGuid_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", sguid));
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", pguid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(StammPostItTopLabTollis);
        }
    }
}
