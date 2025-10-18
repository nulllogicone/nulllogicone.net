// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Caching;
using System.Web.UI;

namespace OliWeb.Controls.Gimicks
{
    ///<summary>
    ///    Ein Control für aktuelle Schlagzeilen von Telepolis.
    ///</summary>
    ///<remarks>
    ///    Hier ist es schon öfters zu Fehlern gekommen, nachdem das
    ///    rss-Format geändert wurde oder als der Service nicht erreichbar war.
    ///</remarks>
    public partial class Telepolis : UserControl
    {
        /// <summary>
        ///     gewünschte Anzahl der Schlagzeilen
        /// </summary>
        protected const int HEADCOUNT = 4;

        /// <summary>
        ///     wenn keine Nachrichten im Cache vorliegen, werden die neuesten
        ///     Nachrichten von http://www.telepolis.de/news.rdf geholt, gecached (3 h).
        ///     Dann werden die Nachrichten aus dem Cache zufällig auf die gewünschte Anzahl gekürzt und im Repeater dargestellt.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Cache["telepolis"] == null)
                {
                    WebRequest req = WebRequest.Create("http://www.telepolis.de/news.rdf");
                    WebResponse res = req.GetResponse();
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));

                    DataSet ds = new DataSet();
                    ds.ReadXml(sr);

                    Cache.Insert("telepolis", ds, null, DateTime.Now.AddHours(3), Cache.NoSlidingExpiration);
                }

                DataSet dds = (DataSet) Cache.Get("telepolis");
                DataSet ds2 = dds.Copy();
                Label1.Text = " News";

                // Zufällig Artikel wegwerfen bis nur noch
                // HEADCOUNT übrig sind (wird sonst zu lang)
                while (ds2.Tables[2].Rows.Count > HEADCOUNT)
                {
                    Random r = new Random();
                    int idx = r.Next(ds2.Tables[2].Rows.Count);
                    ds2.Tables[2].Rows.RemoveAt(idx);
                }
                Repeater1.DataSource = ds2.Tables[2];
                Repeater1.DataBind();
            }
            catch
            {
                Label1.Text =
                    "<oli-it><fehler><span title=\"068fe65c-2564-426e-bf40-5045c1e323de\">kein Newsfeed von Telepolis verfügbar</span></fehler></oli-it>";
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung.
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}