// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.OliDataAccess;

namespace OliEngine.OliMiddleTier
{
    /// <summary>
    ///     Statistik.
    /// </summary>
    public abstract class Statistik
    {
        public static int AnzahlEmpfaengerProCode(Guid cguid)
        {
            using (var con = OliCommon.OLIsConnection)
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM oli.Spiegel WHERE CodeGuid='" + cguid + "'", con);
                con.Open();
                int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                con.Close();

                return (i);
            }
        }

        public static int AnzahlCodeProAngler(Guid aguid)
        {
            using (var con = OliCommon.OLIsConnection)
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM oli.Spiegel WHERE AnglerGuid='" + aguid + "'",
                    con);
                con.Open();
                int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                con.Close();
                return (i);
            }
        }

        public int AnzahlPostItProAngler(int aid)
        {
            return (0);
        }

        public static decimal GesamtBoundKooK
        {
            get
            {
                try
                {
                    using (var con = OliCommon.OLIsConnection)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Sum (KooK) FROM oli.Stamm", con);
                        con.Open();
                        decimal i = (decimal.Parse(cmd.ExecuteScalar().ToString()));
                        con.Close();
                        return (i);
                    }
                }
                catch
                {
                    return 0;
                }

            }
        }

        public static decimal SummeStammKonto
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Sum (Betrag) FROM oli.StammKonto", con);
                    con.Open();
                    decimal i = (decimal.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static decimal GesamtFlowKooK
        {
            get
            {
                try
                {
                    using (var con = OliCommon.OLIsConnection)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Sum (KooK) FROM oli.PostIt", con);
                        con.Open();
                        decimal i = (decimal.Parse(cmd.ExecuteScalar().ToString()));
                        con.Close();
                        return (i);
                    }
                }
                catch 
                {
                    return 0;
                }
            }
        }

        public static decimal SummePostItKonto
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Sum (Betrag) FROM oli.PostItKonto", con);
                    con.Open();
                    decimal i = (decimal.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static int Anzahl_S
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Count (*) FROM oli.Stamm", con);
                    con.Open();
                    int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static int Anzahl_P
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Count (*) FROM oli.PostIt", con);
                    con.Open();
                    int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static int Anzahl_T
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Count (*) FROM oli.TopLab", con);
                    con.Open();
                    int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static int Anzahl_A
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Count (*) FROM oli.Angler", con);
                    con.Open();
                    int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static int Anzahl_X
        {
            get
            {
                using (var con = OliCommon.OLIsConnection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Count (*) FROM oli.Spiegel", con);
                    con.Open();
                    int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                    con.Close();
                    return (i);
                }
            }
        }

        public static decimal ProvisionsSumme
        {
            get
            {
                Provision p = new Provision();
                return (p.Summe);
            }
        }

        public static int StammDurchToll(Guid sguid)
        {
            using (var con = OliCommon.OLIsConnection)
            {
                SqlCommand cmd = new SqlCommand("SELECT DurchToll FROM oli.StammDurchToll WHERE StammGuid=@guid", con);
                cmd.Parameters.AddWithValue("@guid", sguid);
                con.Open();
                int i = (int.Parse(cmd.ExecuteScalar().ToString()));
                con.Close();
                return (i);
            }
        }
    }
}
