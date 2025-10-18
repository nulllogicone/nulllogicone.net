// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     Feedback
    ///     --------
    /// 
    ///     Gibt die Antworten auf ein PostIt
    ///     mit ihren Urhebern an
    /// </summary>
    public class PostItTopLab : PostItTopLabDataSet
    {
        public PostItTopLab()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.PostItTopLab WHERE 1=0 ORDER BY datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(PostItTopLab);
        }

        public PostItTopLab(Guid tguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostItTopLab_TopLabGuid";
            cmd.Parameters.Add(new SqlParameter("@TopLabGuid", tguid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(PostItTopLab);
        }

        public PostItTopLab(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostItTopLab_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", postItRow.PostItGuid));
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(PostItTopLab);
        }
    }
}