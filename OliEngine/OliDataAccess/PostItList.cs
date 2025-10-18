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
    ///     PostItList.
    /// </summary>
    public class PostItList : PostItDataSet
    {
        private SqlDataAdapter pad;

        public PostItList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.PostIt";
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(PostIt);
        }

        public PostItList(int top, String sort)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP " + top + " * FROM oli.PostIt ORDER BY " + sort;
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(PostIt);
        }

        public PostItList(StammDataSet.StammRow stammRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_PostIt_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", stammRow.StammGuid));
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            pad.Fill(PostIt);
        }

        public PostItList(AnglerDataSet.AnglerRow anglerRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql = "SELECT Distinct PostIt.* ";
            sql += " FROM PostIt INNER JOIN Code ";
            sql += " ON PostIt.PostItGuid = Code.PostItGuid ";
            sql += " INNER JOIN Spiegel ";
            sql += " ON Code.CodeGuid = Spiegel.CodeGuid ";
            sql += " WHERE Spiegel.AnglerGuid = '" + anglerRow.AnglerGuid + "'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            pad.Fill(PostIt);
        }

        public PostItList(string PostIt, bool like)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            string sql = "SELECT * FROM oli.PostIt ";
            if (like)
            {
                sql += " WHERE PostIt LIKE '%" + PostIt + "%'";
                sql += " OR Titel LIKE '%" + PostIt + "%'";
            }
            else
            {
                sql += " WHERE PostIt = '" + PostIt + "'";
                sql += " OR Titel = '" + PostIt + "'";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(this, "PostIt");
        }
    }
}