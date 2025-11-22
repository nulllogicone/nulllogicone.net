// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace OliEngine.OliMiddleTier.OLIx
{
    /// <summary>
    ///     ** Hirngeflecht ** Wortraum **
    /// 
    ///     NKBZ.
    ///     Jetzt sind sie angekommen wo sie von Anfang an hingeh�rten.
    ///     Die Tabellen: Netz, Knoten, Baum, Zweige 
    /// 
    ///     Diese Klasse stellt ein NKBZ Objekt dar.
    ///     Es kann eigentlich im Arbeitsspeicher des Server bleiben
    ///     oder auf jedem Client local vorgehalten werden.
    ///     (seltene �nderung, h�ufige Abfragen)
    /// </summary>
    public class NKBZ : NKBZDataSet
    {
        private SqlDataAdapter Nad;
        private SqlDataAdapter Kad;
        private SqlDataAdapter Bad;
        private SqlDataAdapter Zad;

        /// <summary>
        ///     Der ganze Wortraum geht von einer Wurzel aus.
        ///     Das ist das Netz mit 'Urheber - Inhalt - Empf�nger'
        ///     In der Netz - Klasse ist die NetzGuid codiert {76035F19-F4AE-4D58-A388-4BBC72C51CEF}
        /// </summary>
        public static Netz RootNetz
        {
            get { return OLIx.Netz.GetRoot(); }
        }

        /// <summary>
        ///     Instanzvariable, die den Wortraum h�lt - damit er nicht 
        ///     immer wieder neu gelesen werden muss (Singleton)
        /// </summary>
        private static NKBZ nkbz;

        public NKBZ()
        {
            SqlConnection con = OliCommon.OLIxConnection;
            SqlCommand Ncmd = new SqlCommand("SELECT * FROM oli.Netz ORDER BY Netz ", con);
            SqlCommand Kcmd = new SqlCommand("SELECT * FROM oli.Knoten ORDER BY Knoten", con);
            SqlCommand Bcmd = new SqlCommand("SELECT * FROM oli.Baum ORDER BY Baum", con);
            SqlCommand Zcmd = new SqlCommand("SELECT * FROM oli.Zweig ORDER BY Zweig", con);

            Nad = new SqlDataAdapter(Ncmd);
            Kad = new SqlDataAdapter(Kcmd);
            Bad = new SqlDataAdapter(Bcmd);
            Zad = new SqlDataAdapter(Zcmd);

            SqlCommandBuilder Ncb = new SqlCommandBuilder(Nad);
            SqlCommandBuilder Kcb = new SqlCommandBuilder(Kad);
            SqlCommandBuilder Bcb = new SqlCommandBuilder(Bad);
            SqlCommandBuilder Zcb = new SqlCommandBuilder(Zad);

            con.Open();
            Nad.Fill(Netz);
            Kad.Fill(Knoten);
            Bad.Fill(Baum);
            Zad.Fill(Zweig);
            con.Close();
        }

        public static NKBZ Instance()
        {
            if (nkbz == null)
            {
                nkbz = new NKBZ();
            }
            return (nkbz);
        }

        #region Update NKBZ

        public void UpdateNetz()
        {
            Nad.Update(Netz);
        }

        public void UpdateKnoten()
        {
            int i = Kad.Update(Knoten);
        }

        public void UpdateBaum()
        {
            Bad.Update(Baum);
        }

        public void UpdateZweig()
        {
            Zad.Update(Zweig);
        }

        #endregion

        /// <summary>
        ///     Erstellt f�r alle Netze mit Knoten die RDF Serialisation
        /// </summary>
        /// <returns></returns>
        public string MakeWortraumRDF()
        {
            // XML Text Writer erstellen
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xw = new XmlTextWriter(stream, Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            xw.Namespaces = false;

            xw.WriteStartDocument(); // <?xml>
            xw.WriteStartElement("rdf:RDF"); // <rdf:RDF
            xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

            // Datengrundlage
            var netzDv = new DataView(Netz);
            //			netzDv.RowFilter = "RDF = 1";

            // Netze
            foreach (DataRowView drv in netzDv)
            {
                NetzRow nr = (NetzRow) drv.Row;
                // -- Netz --
                xw.WriteComment("Netz");
                xw.WriteComment("====");
                xw.WriteStartElement("nlo:Netz"); // Description
                xw.WriteAttributeString("rdf:about", "Netz/?" + nr.NetzGuid); // Subject

                // -- Netz Felder
                xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(nr.Netz));
                xw.WriteElementString("nlo:beschreibung",
                                      nr.IsBeschreibungNull() ? "" : HttpUtility.HtmlEncode(nr.Beschreibung));
                xw.WriteElementString("nlo:datum", nr.Datum.ToString("s"));
                xw.WriteStartElement("nlo:datei");
                xw.WriteAttributeString("rdf:resource", nr.IsDateiNull() ? "" : OliUtil.MakeImageSrc(nr.Datei));
                xw.WriteEndElement();

                // -- Netz Knoten
                foreach (KnotenRow kr in nr.GetChildRows("NetzKnoten"))
                {
                    xw.WriteComment("Knoten");
                    xw.WriteComment("======");
                    xw.WriteStartElement("nlo:netzKnoten"); // Predicate
                    xw.WriteStartElement("nlo:Knoten"); // Description
                    xw.WriteAttributeString("rdf:about", "Knoten/?" + kr.KnotenGuid); // Subject

                    // Knoten Felder
                    xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(kr.Knoten));
                    xw.WriteElementString("nlo:beschreibung",
                                          kr.IsBeschreibungNull() ? "" : HttpUtility.HtmlEncode(kr.Beschreibung));
                    xw.WriteElementString("nlo:datum", kr.Datum.ToString("s"));

                    #region f�r diesen Knoten und ggf. weitere B�ume die Vorgabewerte

                    xw.WriteComment("Vorgabewerte");
                    xw.WriteComment("************");
                    // VgbOLIs
                    xw.WriteStartElement("nlo:vgbolis");
                    switch (kr["VgbOLIs"].ToString())
                    {
                        case "3":
                            xw.WriteElementString("nlo:Muss", "");
                            break;
                        case "2":
                            xw.WriteElementString("nlo:Sollte", "");
                            break;
                        case "1":
                            xw.WriteElementString("nlo:Nicht", "");
                            break;
                    }
                    xw.WriteEndElement();

                    // get
                    xw.WriteStartElement("nlo:vgbget");
                    switch (kr["VgbGet"].ToString())
                    {
                        case "3":
                            xw.WriteElementString("nlo:Muss", "");
                            break;
                        case "2":
                            xw.WriteElementString("nlo:Sollte", "");
                            break;
                        case "1":
                            xw.WriteElementString("nlo:Nicht", "");
                            break;
                        case "0":
                            xw.WriteElementString("nlo:Egal", "");
                            break;
                    }
                    xw.WriteEndElement();

                    // ILOs
                    xw.WriteStartElement("nlo:vgbilos");
                    switch (kr["VgbILOs"].ToString())
                    {
                        case "3":
                            xw.WriteElementString("nlo:Muss", "");
                            break;
                        case "2":
                            xw.WriteElementString("nlo:Sollte", "");
                            break;
                        case "1":
                            xw.WriteElementString("nlo:Nicht", "");
                            break;
                        case "0":
                            xw.WriteElementString("nlo:Egal", "");
                            break;
                    }
                    xw.WriteEndElement();

                    // fit
                    // get
                    xw.WriteStartElement("nlo:vgbfit");
                    switch (kr["VgbFit"].ToString())
                    {
                        case "3":
                            xw.WriteElementString("nlo:Muss", "");
                            break;
                        case "2":
                            xw.WriteElementString("nlo:Sollte", "");
                            break;
                        case "1":
                            xw.WriteElementString("nlo:Nicht", "");
                            break;
                        case "0":
                            xw.WriteElementString("nlo:Egal", "");
                            break;
                    }
                    xw.WriteEndElement();

                    #endregion

                    #region weiter Netz/Baum Verzweigung Sprungadressen

                    // weiter Netz
                    if (!kr.IsweiterNetzGuidNull() && kr.weiterNetzGuid.ToString().Length > 0)
                    {
                        xw.WriteStartElement("nlo:knotenWeiterNetz");
                        if (!kr.IsweiterNetzGuidNull())
                        {
                            xw.WriteStartElement("nlo:Netz");
                            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Netz/?" + kr.weiterNetzGuid);
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }

                    // weiter Baum
                    if (!kr.IsweiterBaumGuidNull() && kr.weiterBaumGuid.ToString().Length > 0)
                    {
                        xw.WriteStartElement("nlo:knotenWeiterBaum");
                        if (!kr.IsweiterBaumGuidNull())
                        {
                            xw.WriteStartElement("nlo:Baum");
                            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Baum/?" + kr.weiterBaumGuid);
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }

                    #endregion

                    // Ende Knoten
                    xw.WriteEndElement(); // knoten
                    xw.WriteEndElement(); // Knoten
                }

                // Ende Netz
                xw.WriteEndElement(); // Netz

                // Schleifenabbruch
                //				i++; if (i>MAX) break;
            }

            // Baum einf�gen
            xw.WriteRaw(MakeInnerBaumZweigRDF());

            xw.WriteEndElement(); // ~</rdf:RDF>

            xw.Flush();
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string strOutput = sr.ReadToEnd();
            return strOutput;
        }

        private string MakeInnerBaumZweigRDF()
        {
            // XML Text Writer erstellen
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xw = new XmlTextWriter(stream, Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            xw.Namespaces = false;

            //			xw.WriteStartDocument();			// <?xml>
            //			xw.WriteStartElement("rdf:RDF");	// <rdf:RDF
            //			xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            //			xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/schema.rdfs#");
            //			xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

            // Datengrundlage
            var baumDv = new DataView(Baum);
            //			netzDv.RowFilter = "RDF = 1";

            // B�ume
            foreach (DataRowView drv in baumDv)
            {
                BaumRow br = (BaumRow) drv.Row;
                // -- Baum --
                xw.WriteComment("Baum");
                xw.WriteComment("====");
                xw.WriteStartElement("nlo:Baum"); // Description
                xw.WriteAttributeString("rdf:about", "Baum/?" + br.BaumGuid); // Subject

                // -- Baum Felder
                xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(br.Baum));
                xw.WriteElementString("nlo:beschreibung",
                                      br.IsBeschreibungNull() ? "" : HttpUtility.HtmlEncode(br.Beschreibung));
                xw.WriteElementString("nlo:datum", br.Datum.ToString("s"));
                xw.WriteStartElement("nlo:datei");
                xw.WriteAttributeString("rdf:resource", br.IsDateiNull() ? "" : OliUtil.MakeImageSrc(br.Datei));
                xw.WriteEndElement();

                // -- Baum Zweige
                foreach (ZweigRow zr in br.GetChildRows("BaumZweig"))
                {
                    xw.WriteComment("Zweig");
                    xw.WriteComment("======");
                    xw.WriteStartElement("nlo:baumZweig"); // Predicate
                    xw.WriteStartElement("nlo:Zweig"); // Description
                    xw.WriteAttributeString("rdf:about", "Zweig/?" + zr.ZweigGuid); // Subject

                    // Knoten Felder
                    xw.WriteElementString("nlo:name", HttpUtility.HtmlEncode(zr.Zweig));
                    xw.WriteElementString("nlo:datum", zr.Datum.ToString("s"));

                    #region weiter Netz/Baum Verzweigung Sprungadressen

                    // weiter Netz
                    if (!zr.IsweiterNetzGuidNull() && zr.weiterNetzGuid.ToString().Length > 0)
                    {
                        xw.WriteStartElement("nlo:zweigWeiterNetz");
                        if (!zr.IsweiterNetzGuidNull())
                        {
                            xw.WriteStartElement("nlo:Netz");
                            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Netz/?" + zr.weiterNetzGuid);
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }

                    // weiter Baum
                    if (!zr.IsweiterBaumGuidNull() && zr.weiterBaumGuid.ToString().Length > 0)
                    {
                        xw.WriteStartElement("nlo:zweigWeiterBaum");
                        if (!zr.IsweiterBaumGuidNull())
                        {
                            xw.WriteStartElement("nlo:Baum");
                            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Baum/?" + zr.weiterBaumGuid);
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }

                    #endregion

                    // Ende Knoten
                    xw.WriteEndElement(); // knoten
                    xw.WriteEndElement(); // Knoten
                }

                // Ende Netz
                xw.WriteEndElement(); // Netz

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
