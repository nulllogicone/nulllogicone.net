// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OliEngine.OliDataAccess.Functions
{
    /// <summary>
    ///     Fischer.
    ///     Vielleicht gehört er in einen eigenen Thread!
    /// </summary>
    public class Fischer
    {
        public Guid CodeGuid { get; set; }

        public Guid AnglerGuid { get; set; }

        /// <summary>
        /// Beissen testet einen Code gegen einen Angler
        /// </summary>
        /// <param name="codeGuid"></param>
        /// <param name="anglerGuid"></param>
        /// <returns></returns>
        public bool Beissen(Guid codeGuid, Guid anglerGuid)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            SqlCommand cmd = new SqlCommand("beissen", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // CodeId Parameter
            SqlParameter cPar = new SqlParameter();
            cPar.ParameterName = "@CodeGuid";
            cPar.DbType = DbType.Guid;
            cPar.Value = codeGuid;
            cmd.Parameters.Add(cPar);

            // AnglerGuid Parameter
            SqlParameter aPar = new SqlParameter();
            aPar.ParameterName = "@AnglerGuid";
            aPar.DbType = DbType.Guid;
            aPar.Value = anglerGuid;
            cmd.Parameters.Add(aPar);

            // Rückgabe Parameter
            SqlParameter rcPar = new SqlParameter();
            rcPar.ParameterName = "@rc";
            rcPar.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(rcPar);

            // Ausführen
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            // Rückgabe
            bool ret;
            if ((int)rcPar.Value == 0)
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
            return (ret);
        }

        /// <summary>
        /// Fischen - hier darf ein Parameter = Guid.Empty sein
        /// </summary>
        /// <param name="codeGuid"></param>
        /// <param name="anglerGuid"></param>
        /// <returns></returns>
        public bool Fischen(Guid codeGuid, Guid anglerGuid)
        {
            var doSendMessage = bool.Parse( ConfigurationManager.AppSettings["doQueueMessageForFischen"]);
            var doFischenInProc =  bool.Parse(ConfigurationManager.AppSettings["doFischenInRequestThread"]);

            // nop

            if (doSendMessage)
            {
                // queue message to do Fischen with Azure Functions
                var oliitConnectionString = ConfigurationManager.AppSettings["OliItStorageConnectionString"];
                var azureStorage = new AzureStorage(oliitConnectionString);
                azureStorage.QueueDoFischen($"Do Fischen from ", codeGuid, anglerGuid);
            }

            if (doFischenInProc)
            {
                SqlConnection con = OliCommon.OLIsConnection;
                SqlCommand cmd = new SqlCommand("oli.fischen", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // CodeId Parameter
                SqlParameter cPar = new SqlParameter();
                cPar.ParameterName = "@CodeGuid";
                cPar.DbType = DbType.Guid;
                cPar.Value = codeGuid;
                cmd.Parameters.Add(cPar);

                // AnglerGuid Parameter
                SqlParameter aPar = new SqlParameter();
                aPar.ParameterName = "@AnglerGuid";
                aPar.DbType = DbType.Guid;
                aPar.Value = anglerGuid;
                cmd.Parameters.Add(aPar);

                // Rückgabe Parameter
                SqlParameter rcPar = new SqlParameter();
                rcPar.ParameterName = "@rc";
                rcPar.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(rcPar);

                // Ausführen
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                // Rückgabe
                bool ret;
                if ((int)rcPar.Value == 0)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
                return (ret);
            }

            return true;
        }
    }
}