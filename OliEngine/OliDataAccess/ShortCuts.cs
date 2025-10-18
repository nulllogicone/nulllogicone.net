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
    ///     ShortCuts.
    /// </summary>
    public class ShortCuts : ShortCutsDataSet
    {
        private SqlDataAdapter SCad;
        private SqlDataAdapter Stad;

        public ShortCuts()
        {
            SqlConnection con = OliCommon.OLIsConnection;
            con.Open();

            // ShortCuts -Tabelle
            SCad = new SqlDataAdapter("Select * From oli.ShortCuts", con);
            SCad.Fill(ShortCuts);
            SqlCommandBuilder SCcb = new SqlCommandBuilder(SCad);

            // Strings -Tabelle
            Stad = new SqlDataAdapter("Select * From oli.Strings", con);
            Stad.Fill(Strings);
            SqlCommandBuilder Stcb = new SqlCommandBuilder(Stad);
        }

        // nur mit ShortCutID zu erstellen
        public ShortCuts(Guid scguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;
            con.Open();

            // ShortCuts -Tabelle
            SCad = new SqlDataAdapter("Select * From oli.ShortCuts WHERE ShortCutsGuid='" + scguid + "'", con);
            SCad.Fill(ShortCuts);
            SqlCommandBuilder SCcb = new SqlCommandBuilder(SCad);

            // Strings -Tabelle
            Stad = new SqlDataAdapter("Select * From oli.Strings WHERE ShortCutsGuid='" + scguid + "'", con);
            Stad.Fill(Strings);
            SqlCommandBuilder Stcb = new SqlCommandBuilder(Stad);
        }

        public new ShortCutsRow ShortCutsRow
        {
            get
            {
                ShortCutsRow scr = (ShortCutsRow) ShortCuts.Rows[0];
                return (scr);
            }
        }

        // erstellt einen neuen ShortCut für den Stamm
        // und gibt die ShortCutReihe zurück
        public ShortCutsRow NewShortCut(Guid StammGuid, string ShortCutText)
        {
            ShortCutsRow scr = ShortCuts.NewShortCutsRow();
            scr.StammGuid = StammGuid;
            scr.ShortCut = ShortCutText;

            ShortCuts.AddShortCutsRow(scr);

            SCad.Update(ShortCuts);
            SCad.Fill(ShortCuts);

            return (scr);
        }

        public StringsRow[] myStrings
        {
            get { return ((StringsRow[]) ShortCuts.Rows[0].GetChildRows("ShortCutsStrings")); }
        }

        public int UpdateShortCuts()
        {
            return (SCad.Update(ShortCuts));
        }

        public int UpdateStrings()
        {
            int ret = Stad.Update(Strings);
//			Stad.Fill(this.Strings);
            return (ret);
        }
    }
}