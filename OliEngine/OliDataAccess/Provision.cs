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
    ///     Provision.
    /// </summary>
    public class Provision : ProvisionDataSet
    {
        protected SqlDataAdapter pad;

        public Provision()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM oli.Provision WHERE 1=0";
            cmd.Connection = con;

            pad = new SqlDataAdapter();
            pad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(pad);

            pad.Fill(Provision);
        }

        public int UpdateProvision()
        {
            return (pad.Update(Provision));
        }

        public decimal Summe
        {
            get
            {
                try
                {
                    SqlConnection con = OliCommon.OLIsConnection;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT Sum(Betrag) FROM oli.Provision ";
                    cmd.Connection = con;

                    con.Open();
                    decimal sum = (decimal) cmd.ExecuteScalar();
                    con.Close();

                    return (sum);
                }
                catch 
                {
                    return 0;
                }
            }
        }
    }
}
