// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliWeb.Klassen;

namespace OliWeb.Controls.Gimicks
{
    ///<summary>
    ///    als Aufmacher auf der Startseite wird eine zuf�llige Nachricht mit
    ///    ihren Antworten dargestellt. Sie sind in festdimensionierte DIVs
    ///    gepackt. Die Daten werden vom <see cref="CacheManager" /> zur Verf�gung
    ///    gestellt.
    ///</summary>
    public partial class ZufallsPostItmitTopLab : MasterControl
    {
        protected KlickBild KlickBild1;
//		protected System.Web.UI.WebControls.Label PositionLabel;
//		protected System.Web.UI.WebControls.HyperLink NextHyperLink;

        /// <summary>
        ///     aus dem CacheManager wird ein zuf�lliges PostIt ausgew�hlt und als
        ///     Hyperlink und mit <see cref="KlickBild" /> dargestellt.
        ///     Dann wird eine TopLabList zu dieser Nachricht an den TopLabRepeater
        ///     gebunden.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PostItList pl = CacheManager.CachedPostIt;
            int anzP = pl.PostIt.Rows.Count;
            Random r = new Random();
            int rndidx = r.Next(anzP);

            PostItDataSet.PostItRow pr = (PostItDataSet.PostItRow) pl.PostIt.Rows[rndidx];

            TitelLabel.Text = pr.IsTitelNull() ? "" : pr.Titel;

            PostItHyperLink.Text = pr.PostIt;
            PostItHyperLink.NavigateUrl = "~/P/" + pr.PostItGuid + ".aspx";

            KlickBild1.BildName = pr.IsDateiNull() ? "" : pr.Datei;
            KlickBild1.Breite = 100;

            TopLabList tll = new TopLabList(pr);
            TopLabRepeater.DataSource = tll;

            DataBind();
        }

        //#region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{

        //}
        //#endregion
    }
}
