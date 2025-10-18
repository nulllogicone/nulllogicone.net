// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace OliEngine
{
    ///<summary>
    ///    OliCommon ist eine abstrakte Klasse mit Statischen Methoden 
    ///    für allgemeine Umgebungsvariablen
    ///</summary>
    ///<remarks>
    ///    Diese Klasse ist abstract - es sollen keine Objekte 
    ///    instantiiert werden können
    ///
    ///    Die meisten Funktionen sind statisch implementiert
    ///    und liefern nur feste Werte zurück
    ///
    ///    Sie liest Werte aus der Web.Config Datei aus und stellt
    ///    sie als statische Eigenschaften zur Verfügung
    ///</remarks>
    public abstract class OliCommon
    {
        public enum DbServer
        {
            local,
            fal,
            kat,
            netdiscounter
        };

        /// <summary>
        ///     CopyRight Vermerk (Frederic & Kathrin)
        /// </summary>
        public static string CopyRight
        {
            get { return ("(c) 1994 - 2017"); }
        }

        public static string EmailSignature
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n\n-- \nDiese Nachricht wurde über www.oli-it.com versendet.");
                sb.Append("\nBei Missbrauch bitte Mitteilung an info@oli-it.com");
                return sb.ToString();
            }
        }

        /// <summary>
        ///     DbServer Eine Enumerations-Eigenschaft, über die man
        ///     den Server einstellen kann
        /// </summary>
        public static DbServer MyDbServer { get; set; }

        public static string RootPath
        {
            get
            {
                string rs = ConfigurationManager.AppSettings["root"];
                return rs;
            }
        }

        /// <summary>
        /// returns a new instance of a SqlConnection to OLIs-DB
        /// </summary>
        public static SqlConnection OLIsConnection
        {
            get
            {
                var constr = ConfigurationManager.ConnectionStrings["OLIsConnectionString"].ConnectionString;
                var con = new SqlConnection(constr);
                return (con);
            }
        }

        /// <summary>
        /// returns a new instance of a SqlConnection to OLIx-DB
        /// </summary>
        public static SqlConnection OLIxConnection
        {
            get
            {
                var constr = ConfigurationManager.ConnectionStrings["OLIxConnectionString"].ConnectionString;
                var con = new SqlConnection(constr);
                return (con);
            }
        }

        // -------------------------------------------------
        // BilderOrdner
        public static string bilderOrdner
        {
            get { return (ConfigurationManager.AppSettings["bilderOrdner"]); }
        }

        // ImagesOrdner
        public static string imagesOrdner
        {
            get { return (ConfigurationManager.AppSettings["imagesOrdner"]); }
        }

        // IconOrdner
        public static string IconOrdner
        {
            get { return (ConfigurationManager.AppSettings["IconOrdner"]); }
        }

        // SymboleOrdner
        public static string SymboleOrdner
        {
            get { return (ConfigurationManager.AppSettings["SymboleOrdner"]); }
        }

        // PunkteOrdner
        public static string PunkteOrdner
        {
            get { return (ConfigurationManager.AppSettings["PunkteOrdner"]); }
        }

        // FristOffset
        // für angewurzelte Stämme ....
        public static int FristOffset
        {
            get { return (10); }
        }

        // WortraumVersion
        public static string WortraumVersion
        {
            get { return ("07.03"); }
        }

        // DefaultBoundKooK
        public static decimal DefaultBoundKooK
        {
            get { return (10); }
        }

        // DefaultFlowKooK
        public static decimal DefaultFlowKooK
        {
            get { return (0.01m); }
        }

        // DefaultTransferKooK
        public static decimal DefaultTransferKooK
        {
            get { return (1.0m); }
        }

        // Provision
        public static decimal Provision
        {
            get { return (0.05m); }
        }
    }
}