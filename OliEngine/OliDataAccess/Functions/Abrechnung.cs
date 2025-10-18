using System;
using System.Data;
using System.Data.SqlClient;

using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;

using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Views;

namespace OliEngine.OliDataAccess.Functions
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Abrechnen.
	/// </summary>
	public class Abrechnung
	{
		Stamm stamm;
		PostIt postIt;

		public Abrechnung(StammDataSet.StammRow stammRow, PostItDataSet.PostItRow postItRow)
		{
			stamm = new Stamm(stammRow.StammGuid);
			postIt = new PostIt(postItRow.PostItGuid);
		}

		public bool Abrechnen(out string message)
		{
			message = "";

			// Wurzelreihe (für bezahlt und closed)
			Wurzeln w = new Wurzeln(stamm.StammRow.StammGuid, postIt.PostItRow.PostItGuid );
			WurzelnDataSet.WurzelnRow wr = w.WurzelnRow ;

			decimal bezahlt = wr.bezahlt;
			decimal prov = Math.Abs(bezahlt * OliEngine.OliCommon.Provision);
			decimal rest = bezahlt - prov;

			// wenn nix bezahlt gleich raus
			if(bezahlt==0)
			{
				// Wurzel schliessen
				wr.closed = true;
				w.UpdateWurzeln();

				// Nachricht und weg!
				message = "kein Betrag in Aussicht gestellt";
				return(true);
			}

			// Provision und neue Reihe
			Provision provision = new Provision();
			ProvisionDataSet.ProvisionRow provRow = provision.Provision.NewProvisionRow();
			provRow.StammGuid = stamm.StammRow.StammGuid ;
			provRow.PostItGuid = postIt.PostItRow.PostItGuid ;
			provRow.Datum = DateTime.Now;
			provRow.Betrag = prov;
			provision.Provision.AddProvisionRow(provRow);

			// Provision eintragen
			provision.UpdateProvision();

			// Alle Antworten holen
			TopLabList tl = new TopLabList(postIt.PostItRow);

			// ohne Antworten geht Geld zurück
			if(tl.TopLab.Count == 0)
			{
				// Stamm Restgeld zurück
				stamm.StammRow.Trolle += rest;
				stamm.UpdateStamm();

				// PostIt-Wert verringern
				postIt.PostItRow.Trollis -= bezahlt;
				postIt.UpdatePostIt();

				// Wurzel schliessen
				wr.closed = true;
				w.UpdateWurzeln();

				// Nachricht und weg !!!!!!!!!
				message = "Keine Antworten also " + rest.ToString() + " zurück <br>";
				message += "Provision: " + prov.ToString();
				return(true);
			}

			// Die Antworten mit meinen Bewertungen
			StammPostItTopLabTollisDataSet.StammPostItTopLabTollisDataTable sptt =
				new StammPostItTopLabTollis(stamm.StammRow.StammGuid, postIt.PostItRow.PostItGuid).StammPostItTopLabTollis;

			// wenn ich keine Bewertungen vergeben habe
			if(sptt.Count == 0)
			{
				// den Rest auf gegebene Antworten verteilen
				int anzT = tl.TopLab.Count;
				decimal gutschrift = rest/anzT;

				message = "Keine Bewertung, viele Antworter:<br>";
				message += "Provision: " + prov.ToString() + "<br>";
				foreach(DataRow dr in tl.TopLab)
				{
					TopLabDataSet.TopLabRow tlr = (TopLabDataSet.TopLabRow)dr;
					BezahlAntwort(tlr.TopLabGuid, gutschrift);
					string st = OliEngine.OliMiddleTier.DbDirect.GiveStamm(tlr.StammGuid);
					message += "Stamm: " + st + " bekommt " + gutschrift + "<br>"; 
				}

				// PostIt-Wert verringern
				postIt.PostItRow.Trollis -= bezahlt;
				postIt.UpdatePostIt();

				// Wurzel schliessen und weg
				wr.closed = true;
				w.UpdateWurzeln();
				return(true);
			}
			
			// Ich habe Bewertungen vergeben also mal zusammenzählen:
			short gesamtToll = 0;
			foreach(DataRow dr in sptt)
			{
				StammPostItTopLabTollisDataSet.StammPostItTopLabTollisRow spttr =
					(StammPostItTopLabTollisDataSet.StammPostItTopLabTollisRow)dr;
				gesamtToll += spttr.Toll;
			}

			// Rest aufteilen
			message = "Geld aufteilen <br>";
			message += "Provision: " + prov.ToString() + "<br>";

			if(gesamtToll < 100)
			{
				message += "Bewertung unter 100 => ";
				// Rest Gutschreiben
				stamm.StammRow.Trolle += rest * (100-gesamtToll) / 100;
				stamm.UpdateStamm();
				message += stamm.StammRow.Stamm + " bekommt " + (rest * (100-gesamtToll) / 100).ToString() + " zurück<br>";
				// vom Rest abziehen
				rest -= rest * (100-gesamtToll) / 100;
			}
			else
			{
				message += "Bewertung über 100 <br>";
			}

			// Antworten bezahlen
			foreach(DataRow dr in sptt)
			{
				StammPostItTopLabTollisDataSet.StammPostItTopLabTollisRow spttr =
					(StammPostItTopLabTollisDataSet.StammPostItTopLabTollisRow)dr;

				decimal betrag = 0;
				if(gesamtToll > 0)
				{
					betrag = Math.Round((spttr.Toll * rest / gesamtToll),2);
				}

				string st = OliEngine.OliMiddleTier.DbDirect.GiveStamm(spttr.TStammGuid);
				message += "StammID: " + st + " bekommt " + betrag.ToString() + "<br>";
				BezahlAntwort(spttr.TopLabGuid, betrag);
			}

			// PostIt-Wert verringern
			postIt.PostItRow.Trollis -= bezahlt;
			postIt.UpdatePostIt();

			// Wurzel schliessen und weg
			wr.closed = true;
			w.UpdateWurzeln();
			return(true);



			

		}

		// BezahlAntwort(TopLabGuid, betrag)
		private bool BezahlAntwort(Guid tguid, decimal betrag)
		{
			// TopLab
			TopLab t = new TopLab(tguid);

			// Lohn addieren
			t.TopLabRow.Lohn += betrag;
			t.UpdateTopLab();

			// Stamm
			Stamm s = new Stamm(t.TopLabRow);

			// Vermögen aufstocken
			s.StammRow.Trolle += betrag;
			s.UpdateStamm();

			string text = "Sie haben für Ihre Antwort: <br>";
			text += "<p>" + t.TopLabRow.TopLab + "</p>";
			text += "<b>" + betrag + " Trolle </b> gutgeschrieben bekommen";

			Nachrichten.NeueNachricht(s.StammRow.StammGuid, text);
			return(true);
		}
	}
}
