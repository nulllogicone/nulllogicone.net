// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OliEngine;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.ViewGrids
{
    /// <summary>
    ///     die Detailtabellen Controls sind von dieser Klasse abgeleitet. Sie verwaltet die
    ///     Sortierreihenfolge (und ob ab oder auf steigend). Ausserdem holt sie die gewünschte
    ///     Zeilenzahl aus der Extras-Tabelle und stellt sie zur Verfügung
    /// </summary>
    public class ViewGridControl : MasterControl
    {
        // Eigenschaften
        // -------------

        // sortString
        public string sortString
        {
            get
            {
                if (ViewState["ss"] == null)
                {
                    ViewState["ss"] = "";
                }
                return (ViewState["ss"].ToString());
            }
            set { ViewState["ss"] = value; }
        }

        // desc
        public bool desc
        {
            get
            {
                if (ViewState["desc"] == null)
                {
                    ViewState["desc"] = false;
                }
                return ((bool) ViewState["desc"]);
            }
            set { ViewState["desc"] = value; }
        }

        // ZeilenZahl
        protected int ZeilenZahl
        {
            get { return SessionManager.Instance().OliUser.Stamm.Extras.ExtrasRow.ZeilenZahl; }
//			set
//			{
//				zeilenZahl = value;
//			}
        }

        // Methoden
        // --------

        // Page_Load
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}

        /// <summary>
        ///     fügt in ein DataGrid in der Spalte mit entsprechender SortExpression
        ///     einen hoch/runter Pfeil ein
        /// </summary>
        /// <remarks>
        ///     Diese Methode funktioniert nur in <see cref="Controls.Koerper.ViewGrids.ViewGridControl" /> Elementen
        ///     (Deshalb sollte es auch dorthin oder verallgemeinert werden)
        ///     Aber die Richtung des Pfeiles steckt halt in diesen abgeleiteten Controls ...
        /// </remarks>
        /// <param name="dataGrid"> das DataGrid für das der Sortierpfeil in der Spaltenüberschrift eingestellt werden soll </param>
        /// <param name="item"> die Kopfzeilenelemente (??? glaub ich .???) </param>
        public static void SortierPfeil(DataGrid dataGrid, DataGridItem item)
        {
            string sort = ((ViewGridControl) dataGrid.Parent).sortString;
            bool desc = ((ViewGridControl) dataGrid.Parent).desc;
            int i = 0;
            foreach (DataGridColumn dgc in dataGrid.Columns)
            {
                if (dgc.SortExpression == sort && sort.Length > 0)
                {
                    HtmlImage img = new HtmlImage();
                    if (desc)
                    {
                        img.Src = OliCommon.IconOrdner + "Ecken/eck_runter_16_sw.gif";
                        img.Alt = "absteigend";
                    }
                    else
                    {
                        img.Src = OliCommon.IconOrdner + "Ecken/eck_rauf_16_sw.gif";
                        img.Alt = "aufsteigend";
                    }
                    TableCell tc = item.Cells[i];

                    tc.Controls.Add(img);
                    break;
                }
                i++;
            }
        }
    }
}