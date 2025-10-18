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
    ///     StammList.
    ///     (abgeleitet von Stamm)
    /// 
    ///     Konstruktor ohne Parameter liefert die Stamm-Tabelle
    /// 
    ///     Man kann auch int für StammID übergeben oder
    ///     eine Zeichenfolge. (es lässt sich nach genauer
    ///     Übereinstimmung oder 'enthaltensein' suchen)
    /// </summary>
    public class StammList : Stamm
    {
        public StammList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Stamm";
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            // CommandBuilder für Update
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }

        public StammList(int top, String sort)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT top " + top + " * FROM Stamm ORDER BY " + sort;
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }

        public StammList(string stamm)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Stamm WHERE Stamm ='" + stamm + "'";
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }

        public StammList(PostItDataSet.PostItRow postItRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_PostItGuid";
            cmd.Parameters.Add(new SqlParameter("@PostItGuid", postItRow.PostItGuid));
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }

        public StammList(string stamm, bool like = false)
        {
            var con = OliCommon.OLIsConnection;
            var cmd = new SqlCommand();

            if (like)
            {
                cmd.CommandText = "SELECT * FROM oli.Stamm WHERE Stamm Like '%" + stamm + "%'";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM oli.Stamm WHERE Stamm ='" + stamm + "'";
            }
            cmd.Connection = con;

            sad = new SqlDataAdapter(cmd);

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }

        public StammList(CodeDataSet.CodeRow codeRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_CodeGuid";
            cmd.Parameters.Add(new SqlParameter("@CodeGuid", codeRow.CodeGuid));
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);
        }
    }
}