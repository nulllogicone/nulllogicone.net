using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


using OliEngine.OliMiddleTier.OLIs;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper.ViewGrids
{
	/// <summary>
	/// die Detailtabellen Controls sind von dieser Klasse abgeleitet. Sie verwaltet die
	/// Sortierreihenfolge (und ob ab oder auf steigend). Ausserdem holt sie die gew�nschte
	/// Zeilenzahl aus der Extras-Tabelle und stellt sie zur Verf�gung
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
				if(this.ViewState["ss"] == null)
				{
					this.ViewState["ss"] = "";
				}
				return(this.ViewState["ss"].ToString());
			}
			set
			{
				this.ViewState["ss"] = value;
			}
		}

		// desc
		public bool desc
		{
			get
			{
				if(this.ViewState["desc"] == null)
				{
					this.ViewState["desc"] = false;
				}
				return((bool)this.ViewState["desc"]);
			}
			set
			{
				this.ViewState["desc"] = value;
			}
		}
		
		// ZeilenZahl
		protected int ZeilenZahl
		{
			get
			{
				return SessionManager.Instance().OliUser.Stamm.Extras.ExtrasRow.ZeilenZahl;
			}
//			set
//			{
//				zeilenZahl = value;
//			}
		}

		// Methoden
		// --------

		// Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// f�gt in ein DataGrid in der Spalte mit entsprechender SortExpression
		/// einen hoch/runter Pfeil ein
		/// </summary>
		/// <remarks>Diese Methode funktioniert nur in <see cref="Controls.Koerper.ViewGrids.ViewGridControl"/> Elementen
		/// (Deshalb sollte es auch dorthin oder verallgemeinert werden)
		/// Aber die Richtung des Pfeiles steckt halt in diesen abgeleiteten Controls ...</remarks>
		/// <param name="dataGrid">das DataGrid f�r das der Sortierpfeil in der Spalten�berschrift eingestellt werden soll</param>
		/// <param name="item">die Kopfzeilenelemente (??? glaub ich .???)</param>
		public static void SortierPfeil(DataGrid dataGrid, DataGridItem item)
		{
			string sort = ((ViewGridControl)dataGrid.Parent).sortString ;
			bool desc = ((ViewGridControl)dataGrid.Parent).desc;
			int i = 0;
			foreach(DataGridColumn dgc in dataGrid.Columns)
			{
				if (dgc.SortExpression == sort && sort.Length > 0)
				{
					HtmlImage img = new HtmlImage();
					if(desc)
					{
						img.Src = OliEngine.OliCommon.IconOrdner + "Ecken/eck_runter_16_sw.gif"; 
						img.Alt = "absteigend";
					}
					else
					{
						img.Src = OliEngine.OliCommon.IconOrdner + "Ecken/eck_rauf_16_sw.gif"; 
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

