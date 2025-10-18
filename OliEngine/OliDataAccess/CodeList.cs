// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     CodeList.
    /// </summary>
    public class CodeList : CodeDataSet
    {
        private SqlDataAdapter cad;

        public CodeList()
        {
        }

        public CodeList(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Code WHERE PostItGuid = '" + postItRow.PostItGuid + "'";
            cmd.Connection = con;

            cad = new SqlDataAdapter();
            cad.SelectCommand = cmd;

            cad.Fill(Code);
        }

        public CodeList(Guid pguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Code WHERE PostItGuid = '" + pguid + "'";
            cmd.Connection = con;

            cad = new SqlDataAdapter();
            cad.SelectCommand = cmd;

            cad.Fill(Code);

            foreach (CodeRow cr in Code)
            {
                cmd.CommandText = "SELECT * FROM oli.Ringe WHERE CodeGuid='" + cr.CodeGuid + "'";
                cad.Fill(Ringe);
            }
        }

        public int UpdateCode()
        {
            return (cad.Update(Code));
        }
    }
}