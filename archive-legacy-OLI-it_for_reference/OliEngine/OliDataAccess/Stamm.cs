// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using OliEngine.DataSetTypes;
using VicSoft.Rdf;

namespace OliEngine.OliDataAccess
{
    /// <summary>
    ///     Ein Stamm ein abgeleitetes StammDataSet - Objekt 
    ///     (eine Stamm-Tabelle mit einer Zeile)
    /// </summary>
    /// <remarks>
    ///     Man muss eine StammId �bergeben.
    /// </remarks>
    public class Stamm : StammDataSet
    {
        protected SqlDataAdapter sad;

        // Konstruktor ohne Parameter kann �berschrieben werden
        public Stamm()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_StammGuid";
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
        }

        public Stamm(Guid sguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", sguid));

            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);

            // ****** Exception **********
            if (Stamm.Count != 1)
            {
                throw new StammGibtsNichtException("Kein Stamm mit StammGuid = '" + sguid + "'");
            }
        }

        /// <summary>
        /// This is the **main Log-in** function against OLI-it DB!
        /// It is not best practice anymore and should be refactored as soon as possible.
        /// </summary>
        /// <param name="stamm"></param>
        /// <param name="pwd"></param>
        /// <exception cref="StammGibtsNichtException"></exception>
        public Stamm(string stamm, string pwd)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_Stamm_Kennwort";
            cmd.Parameters.Add(new SqlParameter("@stamm", stamm));
            cmd.Parameters.Add(new SqlParameter("@kennwort", pwd));
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);

            // ****** Exception **********
            if (Stamm.Count != 1)
            {
                throw new StammGibtsNichtException("Kein eindeutiger Stamm mit Stamm = " + stamm + " und Kennwort");
            }
        }

        public Stamm(TopLabDataSet.TopLabRow topLabRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

//			string sql = "SELECT * FROM Stamm WHERE StammGuid = '" + topLabRow.StammGuid + "'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "oli.Get_Stamm_StammGuid";
            cmd.Parameters.Add(new SqlParameter("@StammGuid", topLabRow.StammGuid));
            cmd.Connection = con;

            sad = new SqlDataAdapter();
            sad.SelectCommand = cmd;

            SqlCommandBuilder cb = new SqlCommandBuilder(sad);

            sad.Fill(Stamm);

            // ****** Exception **********
            if (Stamm.Count != 1)
            {
                throw new StammGibtsNichtException("Kein eindeutiger Stamm f�r TopLab = " + topLabRow.TopLab);
            }
        }

        // da das this-DataSet immer nur eine Reihe hat
        // wird folgende Eigenschaft neu gesetzt:
        // (von Table auf Row geschrumpft)
        public new StammRow StammRow
        {
            get
            {
                StammRow sr = (StammRow) Stamm.Rows[0];
                return (sr);
            }
        }

        // Methoden
        // --------

        public int UpdateStamm()
        {
            return (sad.Update(Stamm));
        }

        public static Stamm ParseRdf(string stammRdf)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Stamm sds = new Stamm();
            StammRow sr = sds.Stamm.NewStammRow();
            Guid sguid;

            IRdfParser parser = new RdfParser();
            parser.Load(stammRdf);

            Objects objs = parser.GetObjects(null, "http://nulllogicone.net/schema.rdfs#stammGuid");
            if (objs.Count != 1)
                throw new Exception(
                    "Keiner oder mehrere stammGuid gefunden. In einer Stamm.rdf Datei darf nur einen geben.");
            sguid = new Guid(objs[0].Value);

            string sSubj = "http://nulllogicone.net/Stamm/?" + sguid;

            string sName = parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#name");

            sr.StammGuid = sguid;
            sr.Stamm = parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#name");
            sr.Datum = DateTime.Parse(parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#datum"));
            sr.Beschreibung = parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#beschreibung");
            sr.Datei = parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#datei");
            sr.Link = parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#link");
            sr.KooK = decimal.Parse(parser.GetObjectValue(sSubj, "http://nulllogicone.net/schema.rdfs#boundKook"));
            sr.zuQID = 5;

            sds.Stamm.AddStammRow(sr);

            Thread.CurrentThread.CurrentCulture = ci;
            return sds;
        }
    }

    public class StammGibtsNichtException : Exception
    {
        public StammGibtsNichtException()
        {
        }

        public StammGibtsNichtException(string meldung) : base(meldung)
        {
        }
    }
}
