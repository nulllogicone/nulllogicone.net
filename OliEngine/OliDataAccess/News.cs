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
    ///     Code.
    /// </summary>
    public class News : NewsDataSet
    {
        protected SqlDataAdapter nad;

        protected News()
        {
        }

        public News(Guid nguid)
        {
            SqlConnection con = OliCommon.OLIsConnection;

            nad = new SqlDataAdapter("Select * From oli.News WHERE NewsGuid='" + nguid + "'", con);
            SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
            nad.Fill(News);
        }

        public new NewsRow NewsRow
        {
            get { return ((NewsRow) News.Rows[0]); }
        }

        public int UpdateNews()
        {
            return (nad.Update(News));
        }
    }
}