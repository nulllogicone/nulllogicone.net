using System;
using System.IO;
using System.Xml;
using System.Data;

using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.OLIs
{
	/// <summary>
	/// Code.
	/// </summary>
	public class Code
	{
		// Member
		// ------

		OliDataAccess.Code  code;
		ZellBuilder zellBuilder = new ZellBuilder();

		// Konstrukor
		// ----------

		public Code(Guid cguid)
		{
			code = new OliDataAccess.Code(cguid);

			CodeMarkierer cm = new CodeMarkierer(cguid);
			zellBuilder.Markierer = cm;
		}

		public Code(Stamm stamm, PostIt postIt)
		{
			code = new OliDataAccess.Code();
			CodeDataSet.CodeRow cr = code.Code.NewCodeRow();
			
			cr.StammGuid = stamm.StammRow.StammGuid ;
			cr.PostItGuid = postIt.PostItRow.PostItGuid ;
			cr.Kommentar = "Markierung von " + stamm.StammRow.Stamm;
			cr.CodeZust = 2;
			cr.gescannt = false;
			cr.Versionsnummer = OliCommon.WortraumVersion;
			cr.CodeGuid = Guid.NewGuid();

		
			// Hinzuf�gen und update
			code.Code.AddCodeRow(cr);
			code.UpdateCode();


			// meine CodeRow neu laden
			code = new OliDataAccess.Code(cr.CodeGuid);

		}

		// Eigenschaften
		// -------------

		// ZellBuilder
		public ZellBuilder ZellBuilder
		{
			get
			{
				return(zellBuilder);
			}
		}

		// MyEmpf�nger
		public StammList MyEmpfaenger
		{
			get
			{
				StammList sl = new StammList(code.CodeRow);
				return(sl);
			}
		}

		// MyRinge
		public OliEngine.DataSetTypes.CodeDataSet.RingeDataTable MyRinge
		{
			get
			{
				OliEngine.DataSetTypes.CodeDataSet.RingeDataTable rdt;
				rdt = this.code.Ringe;
				return rdt;
			}
		}

		// CodeRow
		public CodeDataSet.CodeRow CodeRow
		{
			get
			{ 
				return (code.CodeRow);
			}
		}

		// UpdateCode()
		public int UpdateCode()
		{
			return(this.code.UpdateCode());
		}




		/// <summary>
		/// erstellt einen RDF Abschnitt, der diesen Code beschreibt
		/// </summary>
		/// <returns></returns>
		public string MakeCodeRdf()
		{
			Guid cguid = this.CodeRow.CodeGuid;
			OliEngine.OliMiddleTier.OLIs.Code c = new OliEngine.OliMiddleTier.OLIs.Code(cguid);
			CodeDataSet.CodeRow cr = c.CodeRow;
//			CodeDataSet.RingeDataTable rdt = c.MyRinge;
			
			// XML Text Writer erstellen
			MemoryStream stream = new MemoryStream();
			XmlTextWriter xw = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
			xw.Formatting = Formatting.Indented;
			xw.Namespaces = false;

			xw.WriteStartDocument(); // <?xml>
			// <rdf:RDF
			xw.WriteStartElement("rdf:RDF");
			xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
			xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/terms#");
			xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

			// -- Stamm, der den Code erstellt hat --
			xw.WriteStartElement("rdf:Description"); // Description
			xw.WriteAttributeString("rdf:about", "Stamm/?" + cr["StammGuid"].ToString()); // Subject
				xw.WriteElementString("nlo:StammName", DbDirect.GiveStamm(cr["StammGuid"].ToString())); // Predicate + Object
			xw.WriteEndElement();	
		
			// -- PostIt, das durch diesen Code beschrieben wird --
			xw.WriteStartElement("rdf:Description"); // <rdf:Description
			xw.WriteAttributeString("rdf:about", "PostIt/?" + cr["PostItGuid"].ToString()); // Subject
				xw.WriteStartElement("nlo:Code"); // Predicate
				xw.WriteAttributeString("rdf:resource", "Code/?" + cguid.ToString()); // Object
				xw.WriteEndElement();
			xw.WriteEndElement(); // ~Description

			// -- Code --
			xw.WriteStartElement("rdf:Description"); // Description
			xw.WriteAttributeString("rdf:about", "Code/?" + cguid.ToString()); // Subject
			// Stamm
			xw.WriteStartElement("nlo:Stamm"); // Predicate
			xw.WriteAttributeString("rdf:resource", "Stamm/?" + cr["StammGuid"].ToString()); // Object
			xw.WriteEndElement();
			// PostIt
			xw.WriteStartElement("nlo:PostIt"); // Predicate
			xw.WriteAttributeString("rdf:resource", "PostIt/?" + cr["PostItGuid"].ToString()); // Object
			xw.WriteEndElement();

			xw.WriteElementString("nlo:Kommentar", cr["Kommentar"].ToString()); // Predicate Object
			xw.WriteElementString("nlo:Version", cr["Versionsnummer"].ToString()); // Predicate Object

			// -- alle Ringe -- (k�nnte ein 'Bag' werden)
			foreach(DataRow dr in c.MyRinge)
			{
				xw.WriteStartElement("nlo:Ring"); // Predicate
				xw.WriteAttributeString("rdf:resource", "Ring/?" + dr["RingGuid"]); // Object
				xw.WriteEndElement();
			}
			xw.WriteEndElement(); // ~Description

			// -- jeder Ring einzeln --
			foreach(DataRow dr in c.MyRinge)
			{
				xw.WriteStartElement("rdf:Description"); // Description
				xw.WriteAttributeString("rdf:about", "Ring/?" + dr["RingGuid"]); // Subject
				xw.WriteElementString("nlo:NKBZ", makeNKBZ(dr));
				// Netz
				xw.WriteStartElement("nlo:Netz"); // Predicate
				xw.WriteAttributeString("rdf:resource", "Netz/?" + dr["NetzGuid"]); // Predicate, Object
				xw.WriteAttributeString("nlo:NetzText", DbDirect.GiveNetz(dr["NetzGuid"].ToString())); // Predicate, Object
				xw.WriteEndElement();
				// Knoten
				xw.WriteStartElement("nlo:Knoten"); // Predicate
				xw.WriteAttributeString("rdf:resource", "Knoten/?" + dr["KnotenGuid"]); // Predicate, Object
				xw.WriteAttributeString("nlo:KnotenText", DbDirect.GiveKnoten(dr["KnotenGuid"].ToString())); // Predicate, Object
				xw.WriteEndElement();
				if(dr["BaumGuid"].ToString().Length > 0)
				{
					// Baum
					xw.WriteStartElement("nlo:Baum"); // Predicate
					xw.WriteAttributeString("rdf:resource", "Baum/?" + dr["BaumGuid"]); // Predicate, Object
					xw.WriteAttributeString("nlo:BaumText", DbDirect.GiveBaum(dr["BaumGuid"].ToString())); // Predicate, Object
					xw.WriteEndElement();
					// Zweig
					xw.WriteStartElement("nlo:Zweig"); // Predicate
					xw.WriteAttributeString("rdf:resource", "Zweig/?" + dr["ZweigGuid"]); // Predicate, Object
					xw.WriteAttributeString("nlo:ZweigText", DbDirect.GiveZweig(dr["ZweigGuid"].ToString())); // Predicate, Object
					xw.WriteEndElement();
				}
				// OLIs - get
				xw.WriteElementString("nlo:OLIs", dr["OLIs"].ToString()); // Predicate, Object
				xw.WriteElementString("nlo:get", dr["get"].ToString()); // Predicate, Object
				xw.WriteEndElement(); // ~Description
			}


			xw.WriteEndElement();	// ~</rdf:RDF>
			
			xw.Flush();
			stream.Position = 0; 

			StreamReader sr = new StreamReader(stream); 
			string strOutput = sr.ReadToEnd(); 

			return strOutput;
		}

		/// <summary>
		/// Hilfsfunktion um die Koordinate im Wortraum menschenlesbar darzustellen.
		/// </summary>
		/// <param name="dr">Eine Ring DataRow des Codes</param>
		/// <returns>verkettet Netz - Knoten - Baum - Zweig</returns>
		private string makeNKBZ(DataRow dr)
		{
			string ret;
			ret = OliEngine.OliMiddleTier.DbDirect.GiveNetz(dr["NetzGuid"].ToString()) + " - ";
			ret += OliEngine.OliMiddleTier.DbDirect.GiveKnoten(dr["KnotenGuid"].ToString()) + " - ";
			ret += OliEngine.OliMiddleTier.DbDirect.GiveBaum(dr["BaumGuid"].ToString()) + " - ";
			ret += OliEngine.OliMiddleTier.DbDirect.GiveZweig(dr["ZweigGuid"].ToString());
			return ret;
		}
	}
}

