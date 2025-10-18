// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

namespace OliEngine.OliDataAccess.Views
{
    /// <summary>
    ///     AnglerPostIt
    ///     -----------------
    /// 
    ///     Das sind die Fische, die dem Angler ins Netz gehen.
    /// 
    ///     Also alle Nachriten, die ein Angler-Filter erhält,
    ///     mit ihren Anzahlen (Urheber, Antworten, Empfänger)
    /// </summary>
    public class AnglerPostIt : AnglerPostItDataSet
    {
        public AnglerPostIt()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.AnglerPostIt WHERE 1=0";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(AnglerPostIt);
        }

        public AnglerPostIt(Guid aguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.AnglerPostIt WHERE AnglerGuid='" + aguid + "' ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(AnglerPostIt);
        }

        public AnglerPostIt(AnglerDataSet.AnglerRow anglerRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.AnglerPostIt WHERE AnglerGuid='" + anglerRow.AnglerGuid +
                              "' ORDER BY Datum DESC";
            cmd.Connection = con;

            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;

            ad.Fill(AnglerPostIt);
        }
    }
}