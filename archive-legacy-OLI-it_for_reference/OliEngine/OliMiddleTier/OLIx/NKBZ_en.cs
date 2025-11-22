using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

namespace OliEngine.OliMiddleTier.OLIx
{
	/// <summary>
	/// ** Hirngeflecht ** Wortraum **
	/// 
	/// NKBZ.
	/// Jetzt sind sie angekommen wo sie von Anfang an hingeh�rten.
	/// Die Tabellen: Netz, Knoten, Baum, Zweige 
	/// 
	/// Diese Klasse stellt ein NKBZ Objekt dar.
	/// Es kann eigentlich im Arbeitsspeicher des Server bleiben
	/// oder auf jedem Client local vorgehalten werden.
	/// (seltene �nderung, h�ufige Abfragen)
	/// </summary>
	

	public class NKBZ : NKBZDataSet
	{
		SqlDataAdapter Nad;
		SqlDataAdapter Kad;
		SqlDataAdapter Bad;
		SqlDataAdapter Zad;

		/// <summary>
		/// Instanzvariable, die den Wortraum h�lt - damit er nicht 
		/// immer wieder neu gelesen werden muss (Singleton)
		/// </summary>
		static NKBZ nkbz;

		public NKBZ()
		{
			SqlConnection con = OliCommon.OLIxConnection ;
			SqlCommand Ncmd = new SqlCommand("SELECT * FROM Netz ORDER BY Netz ",con);
			SqlCommand Kcmd = new SqlCommand("SELECT * FROM Knoten ORDER BY Knoten",con);
			SqlCommand Bcmd = new SqlCommand("SELECT * FROM Baum ORDER BY Baum",con);
			SqlCommand Zcmd = new SqlCommand("SELECT * FROM Zweig ORDER BY Zweig",con);

			Nad = new SqlDataAdapter(Ncmd);
			Kad = new SqlDataAdapter(Kcmd);
			Bad = new SqlDataAdapter(Bcmd);
			Zad = new SqlDataAdapter(Zcmd);

			SqlCommandBuilder Ncb = new SqlCommandBuilder(Nad);
			SqlCommandBuilder Kcb = new SqlCommandBuilder(Kad);
			SqlCommandBuilder Bcb = new SqlCommandBuilder(Bad);
			SqlCommandBuilder Zcb = new SqlCommandBuilder(Zad);

			con.Open();
			Nad.Fill(this.Netz);
			Kad.Fill(this.Knoten);
			Bad.Fill(this.Baum);
			Zad.Fill(this.Zweig);
			con.Close();
		}


		public static NKBZ Instance()
		{
			if (nkbz == null)
			{
				nkbz = new NKBZ();
			}
			return(nkbz);
		}

		#region Update NKBZ
		public void UpdateNetz()
		{
			
			Nad.Update(this.Netz);

		}

		public void UpdateKnoten()
		{
			int i = Kad.Update(this.Knoten);
		}
		
		public void UpdateBaum()
		{
			Bad.Update(this.Baum);
			
		}

		public void UpdateZweig()
		{
			Zad.Update(this.Zweig);
			
		}
		#endregion



		/// <summary>
		/// Erstellt f�r alle Netze mit Knoten die RDF Serialisation
		/// </summary>
		/// <returns></returns>
		public string MakeWortraumRDF()
		{
			int MAX = 5;
									
			// XML Text Writer erstellen
			MemoryStream stream = new MemoryStream();
			XmlTextWriter xw = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
			xw.Formatting = Formatting.Indented;
			xw.Namespaces = false;

			xw.WriteStartDocument();			// <?xml>
			xw.WriteStartElement("rdf:RDF");	// <rdf:RDF
			xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
			xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/schema.rdfs#");
			xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

			// Datengrundlage
			int i = 0;
			DataView netzDv = new DataView(this.Netz);
//			netzDv.RowFilter = "RDF = 1";

			// Netze
			foreach(DataRowView drv in netzDv)
			{
				NKBZDataSet.NetzRow nr = (NKBZDataSet.NetzRow)drv.Row;
				// -- Netz --
				xw.WriteComment("Net");			
				xw.WriteComment("====");
				xw.WriteStartElement("nlo:Net"); // Description
				xw.WriteAttributeString("rdf:about", "Net/?" + nr.NetzGuid.ToString()); // Subject
				
				// -- Netz Felder
				xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(nr.Isen_nameNull()?nr.Netz:nr.en_name));
                xw.WriteElementString("nlo:description", HttpUtility.HtmlEncode(nr.Isen_descriptionNull() ? (nr.IsBeschreibungNull() ? "" : nr.Beschreibung) : nr.en_description));
				xw.WriteElementString("nlo:date", nr.Datum.ToString("s"));
				xw.WriteStartElement("nlo:file");
				xw.WriteAttributeString("rdf:resource", nr.IsDateiNull() ? "" : OliEngine.OliUtil.MakeImageSrc(nr.Datei));
				xw.WriteEndElement();

				// -- Netz Knoten
				foreach(NKBZDataSet.KnotenRow kr in nr.GetChildRows("NetNode"))
				{
					xw.WriteComment("Node");			
					xw.WriteComment("======");
					xw.WriteStartElement("nlo:netNode");// Predicate
					xw.WriteStartElement("nlo:Node"); // Description
					xw.WriteAttributeString("rdf:about", "Node/?" + kr.KnotenGuid.ToString()); // Subject

					// Knoten Felder
					xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(kr.Isen_nameNull()? kr.Knoten : kr.en_name));
					xw.WriteElementString("nlo:description", HttpUtility.HtmlEncode(kr.Isen_descriptionNull()?(kr.IsBeschreibungNull()?"":kr.Beschreibung)  : kr.en_description));
					xw.WriteElementString("nlo:date", kr.Datum.ToString("s"));

					#region f�r diesen Knoten und ggf. weitere B�ume die Vorgabewerte
					xw.WriteComment("Default Values");
					xw.WriteComment("**************");
					// VgbOLIs
					xw.WriteStartElement("nlo:vgbolis");
					switch (kr["VgbOLIs"].ToString())
					{
						case "3" :
							xw.WriteElementString("nlo:Must", "");
							break;
						case "2" :
							xw.WriteElementString("nlo:Should","");
							break;
						case "1" :
							xw.WriteElementString("nlo:Not","");
							break;
					}
					xw.WriteEndElement();

					// get
					xw.WriteStartElement("nlo:vgbget");
					switch (kr["VgbGet"].ToString())
					{
						case "3" :
							xw.WriteElementString("nlo:Must", "");
							break;
						case "2" :
							xw.WriteElementString("nlo:Should","");
							break;
						case "1" :
							xw.WriteElementString("nlo:Not","");
							break;
						case "0" :
							xw.WriteElementString("nlo:Whatever", "");
							break;
					}
					xw.WriteEndElement();

					// ILOs
					xw.WriteStartElement("nlo:vgbilos");
					switch (kr["VgbILOs"].ToString())
					{
						case "3" :
							xw.WriteElementString("nlo:Must", "");
							break;
						case "2" :
							xw.WriteElementString("nlo:Should","");
							break;
						case "1" :
							xw.WriteElementString("nlo:Not","");
							break;
						case "0" :
							xw.WriteElementString("nlo:Whatever", "");
							break;
					}
					xw.WriteEndElement();

					// fit
					// get
					xw.WriteStartElement("nlo:vgbfit");
					switch (kr["VgbFit"].ToString())
					{
						case "3" :
							xw.WriteElementString("nlo:Must", "");
							break;
						case "2" :
							xw.WriteElementString("nlo:Should","");
							break;
						case "1" :
							xw.WriteElementString("nlo:Not","");
							break;
						case "0" :
							xw.WriteElementString("nlo:Whatever", "");
							break;
					}
					xw.WriteEndElement();
					#endregion

					#region weiter Netz/Baum Verzweigung Sprungadressen
					// weiter Netz
					if(!kr.IsweiterNetzGuidNull() && kr.weiterNetzGuid.ToString().Length > 0)
					{
						xw.WriteStartElement("nlo:nodeNextNext");
						if(!kr.IsweiterNetzGuidNull())
						{
							xw.WriteStartElement("nlo:Net");
							xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Net/?" + kr.weiterNetzGuid.ToString());
							xw.WriteEndElement();
						}
						xw.WriteEndElement();
					}

					// weiter Baum
					if(!kr.IsweiterBaumGuidNull() && kr.weiterBaumGuid.ToString().Length > 0)
					{
						xw.WriteStartElement("nlo:nodeNextTree");
						if(!kr.IsweiterBaumGuidNull())
						{
							xw.WriteStartElement("nlo:Tree");
							xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Tree/?" + kr.weiterBaumGuid.ToString());
							xw.WriteEndElement();
						}
						xw.WriteEndElement();
					}
					#endregion

					// Ende Knoten
					xw.WriteEndElement();	// knoten
					xw.WriteEndElement();	// Knoten
				}

				// Ende Netz
				xw.WriteEndElement();	// Netz

				// Schleifenabbruch
//				i++; if (i>MAX) break;
			}


			// Baum einf�gen
			xw.WriteRaw(this.MakeInnerBaumZweigRDF());


			xw.WriteEndElement();	// ~</rdf:RDF>
			
			xw.Flush();
			stream.Position = 0; 

			StreamReader sr = new StreamReader(stream); 
			string strOutput = sr.ReadToEnd(); 
			return strOutput;
		}

		private string MakeInnerBaumZweigRDF()
		{
			int MAX = 5;
									
			// XML Text Writer erstellen
			MemoryStream stream = new MemoryStream();
			XmlTextWriter xw = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
			xw.Formatting = Formatting.Indented;
			xw.Namespaces = false;

//			xw.WriteStartDocument();			// <?xml>
//			xw.WriteStartElement("rdf:RDF");	// <rdf:RDF
//			xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
//			xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/schema.rdfs#");
//			xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

			// Datengrundlage
			int i = 0;
			DataView baumDv = new DataView(this.Baum);
			//			netzDv.RowFilter = "RDF = 1";

			// B�ume
			foreach(DataRowView drv in baumDv)
			{
				NKBZDataSet.BaumRow br = (NKBZDataSet.BaumRow)drv.Row;
				// -- Baum --
				xw.WriteComment("Tree");			
				xw.WriteComment("====");
				xw.WriteStartElement("nlo:Tree"); // Description
				xw.WriteAttributeString("rdf:about", "Tree/?" + br.BaumGuid.ToString()); // Subject
				
				// -- Baum Felder
                xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(br.Isen_nameNull() ? br.Baum : br.en_name));
				xw.WriteElementString("nlo:description", HttpUtility.HtmlEncode(br.Isen_descriptionNull()? (br.IsBeschreibungNull()?"":br.Beschreibung):br.en_description));
				xw.WriteElementString("nlo:date", br.Datum.ToString("s"));
				xw.WriteStartElement("nlo:file");
				xw.WriteAttributeString("rdf:resource", br.IsDateiNull() ? "" : OliEngine.OliUtil.MakeImageSrc(br.Datei));
				xw.WriteEndElement();

				// -- Baum Zweige
				foreach(NKBZDataSet.ZweigRow zr in br.GetChildRows("BaumZweig"))
				{
					xw.WriteComment("Branch");			
					xw.WriteComment("======");
					xw.WriteStartElement("nlo:treeBranch");// Predicate
					xw.WriteStartElement("nlo:Branch"); // Description
					xw.WriteAttributeString("rdf:about", "Branch/?" + zr.ZweigGuid.ToString()); // Subject

					// Knoten Felder
					xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(zr.Isen_nameNull()?zr.Zweig:zr.en_name));
					xw.WriteElementString("nlo:date", zr.Datum.ToString("s"));



					#region weiter Netz/Baum Verzweigung Sprungadressen
					// weiter Netz
					if(!zr.IsweiterNetzGuidNull() && zr.weiterNetzGuid.ToString().Length > 0)
					{
						xw.WriteStartElement("nlo:branchNextNet");
						if(!zr.IsweiterNetzGuidNull())
						{
							xw.WriteStartElement("nlo:Net");
							xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Net/?" + zr.weiterNetzGuid.ToString());
							xw.WriteEndElement();
						}
						xw.WriteEndElement();
					}

					// weiter Baum
					if(!zr.IsweiterBaumGuidNull() && zr.weiterBaumGuid.ToString().Length > 0)
					{
						xw.WriteStartElement("nlo:branchNextTree");
						if(!zr.IsweiterBaumGuidNull())
						{
							xw.WriteStartElement("nlo:Tree");
							xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Tree/?" + zr.weiterBaumGuid.ToString());
							xw.WriteEndElement();
						}
						xw.WriteEndElement();
					}
					#endregion

					// Ende Knoten
					xw.WriteEndElement();	// knoten
					xw.WriteEndElement();	// Knoten
				}

				// Ende Netz
				xw.WriteEndElement();	// Netz

				// Schleifenabbruch
//				i++; if (i>MAX) break;
			}




//			xw.WriteEndElement();	// ~</rdf:RDF>
			
			xw.Flush();
			stream.Position = 0; 

			StreamReader sr = new StreamReader(stream); 
			string strOutput = sr.ReadToEnd(); 
			return strOutput;
		}
	}
}

