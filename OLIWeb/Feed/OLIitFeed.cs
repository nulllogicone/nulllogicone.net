using System;
using System.Data;

using OliEngine.OliDataAccess;

namespace OliWeb.Feed
{
	/// <summary>
	/// OLIitFeed.
	/// </summary>
	public class OLIitFeed : System.Web.UI.UserControl
	{
		
		protected Guid sguid;
		protected Stamm s;

		public int anzahl;
		public string sort;
		public string style;
		
		public DataTable _table;


		public OLIitFeed()
		{}

//		// es muss immer eine sguid angegeben werden
//		// die abgeleitetetn konkreten klassen müssen
//		// diesen Konstruktor aufrufen und dann das
//		// Tabellen-füllen implementieren
//		public OLIitFeed(Guid sguid)
//		{
//			this.sguid = sguid;
//			s = new Stamm(sguid);
//		}


	}
}
