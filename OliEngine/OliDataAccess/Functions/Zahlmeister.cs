// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess.Functions
{
    /// <summary>
    ///     Zahlmeister.
    /// </summary>
    public class Zahlmeister
    {
        // Member
        // ------

        private readonly Guid sguid;
        private readonly Guid pguid;

        // Konstruktor
        public Zahlmeister(StammDataSet.StammRow stammRow, PostItDataSet.PostItRow postItRow)
        {
            sguid = stammRow.StammGuid;
            pguid = postItRow.PostItGuid;
        }

        // Bezahlen
        public void Bezahlen(decimal betrag, DateTime frist, string kommentar)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand("oli.zahlen", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // StammGuid Parameter
            SqlParameter sPar = new SqlParameter();
            sPar.ParameterName = "@sguid";
            sPar.DbType = DbType.Guid;
            sPar.Value = sguid;
            cmd.Parameters.Add(sPar);

            // Betrag Parameter
            SqlParameter bPar = new SqlParameter();
            bPar.ParameterName = "@betrag";
            bPar.DbType = DbType.Currency;
            bPar.Value = betrag;
            cmd.Parameters.Add(bPar);

            // PostItGuid Parameter
            SqlParameter pPar = new SqlParameter();
            pPar.ParameterName = "@pguid";
            pPar.DbType = DbType.Guid;
            pPar.Value = pguid;
            cmd.Parameters.Add(pPar);

            // Frist Parameter
            SqlParameter fPar = new SqlParameter();
            fPar.ParameterName = "@frist";
            fPar.DbType = DbType.DateTime;
            fPar.Value = frist;
            cmd.Parameters.Add(fPar);

            // Kommentar Parameter
            SqlParameter kPar = new SqlParameter();
            kPar.ParameterName = "@kommentar";
            kPar.DbType = DbType.String;
            kPar.Value = kommentar;
            cmd.Parameters.Add(kPar);

            // Ausf�hren
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
