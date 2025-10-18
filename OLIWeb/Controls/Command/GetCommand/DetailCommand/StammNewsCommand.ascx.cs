// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Linq;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammNewsCommand.
    ///</summary>
    public partial class StammNewsCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "StammNews";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "+ neue Nachrichten an meine Filterprofile";

            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                // meine News (Neu/Gesamt)
                var myNews = Stamm.MyNews.AsEnumerable();
                int allNews = myNews.Count();
                int newNews = myNews
                    .Where(n => !n.Field<DateTime?>("gesehen").HasValue)
                    .Where(n => !n.Field<DateTime?>("gelesen").HasValue)
                    .Count();
                string newString = newNews > 0 ? string.Format("<b>{0}</b>", newNews) : "";
                Text = string.Format("News ({1}) - {0}", newString, allNews);

                HyperLink1.NavigateUrl = "~/Sites/StammNewsSite.aspx?sguid=" + Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is StammNewsSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }
    }
}