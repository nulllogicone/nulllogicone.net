// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    public class Spiegel : SpiegelDataSet
    {
        // Member
        // ------
        protected SqlDataAdapter sad;

        // Konstruktor
        // -----------
        public Spiegel(Guid codeGuid, AnglerDataSet.AnglerRow anglerRow)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            sad =
                new SqlDataAdapter(
                    "SELECT * FROM oli.Spiegel WHERE CodeGuid='" + codeGuid + "' AND AnglerGuid='" +
                    anglerRow.AnglerGuid +
                    "'", con);
            SqlCommandBuilder scb = new SqlCommandBuilder(sad);
            sad.Fill(Spiegel);
        }

        // Eigenschaften
        // -------------

        public new SpiegelRow SpiegelRow
        {
            get
            {
                if (Spiegel.Rows.Count == 1)
                {
                    return ((SpiegelRow) Spiegel.Rows[0]);
                }
                else
                {
                    throw new Exception("mehrere Spiegel-Rows für codeGuid und AnglerRow");
                }
            }
        }

        // Methoden
        // --------

        public int UpdateSpiegel()
        {
            return sad.Update(Spiegel);
        }
    }
}