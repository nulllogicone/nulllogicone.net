// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data.SqlClient;
using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
    public class NewsList : News
    {
        // Member
        // ------

        // Konstruktor
        // -----------
        public NewsList()
        {
            SqlConnection con = OliCommon.OLIsConnection;

            nad = new SqlDataAdapter("Select * From oli.News", con);
            SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
            nad.Fill(News);
        }

        public NewsList(bool toMail)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            if (toMail)
            {
                nad =
                    new SqlDataAdapter(
                        "Select * From oli.News WHERE (gelesen IS NULL) AND (gesehen IS NULL) AND (gemailt IS NULL)",
                        con);
            }
            else
            {
                nad = new SqlDataAdapter("Select * From oli.News", con);
            }
            SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
            nad.Fill(News);
        }

        public NewsList(AnglerDataSet.AnglerRow anglerRow, bool toMail)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            if (toMail)
            {
                nad =
                    new SqlDataAdapter(
                        "Select * From oli.News WHERE AnglerGuid ='" + anglerRow.AnglerGuid +
                        "' AND (gelesen IS NULL) AND (gesehen IS NULL) AND (gemailt IS NULL)", con);
            }
            else
            {
                nad = new SqlDataAdapter("Select * From oli.News WHERE AnglerGuid ='" + anglerRow.AnglerGuid + "'", con);
            }

            SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
            nad.Fill(News);
        }

        // Eigenschaften
        // -------------

        // Methoden
        // --------

        public new int UpdateNews()
        {
            return nad.Update(News);
        }
    }
}