// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Code.
    /// </summary>
    public class Code : IMarkierer
    {
        // Member
        // ------

        private readonly OliDataAccess.Code code;
        private readonly ZellBuilder zellBuilder = new ZellBuilder();

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

            cr.StammGuid = stamm.StammRow.StammGuid;
            cr.PostItGuid = postIt.PostItRow.PostItGuid;
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
            get { return (zellBuilder); }
        }

        // MyEmpf�nger
        public StammList MyEmpfaenger
        {
            get
            {
                StammList sl = new StammList(code.CodeRow);
                return (sl);
            }
        }

        // MyAngler
        public AnglerList MyAngler
        {
            get
            {
                AnglerList al = new AnglerList(code.CodeRow);
                return (al);
            }
        }

        // MyRinge
        public CodeDataSet.RingeDataTable MyRinge
        {
            get
            {
                CodeDataSet.RingeDataTable rdt;
                rdt = code.Ringe;
                return rdt;
            }
        }

        // CodeRow
        public CodeDataSet.CodeRow CodeRow
        {
            get { return (code.CodeRow); }
        }

        /// <summary>
        ///     aktualisiert die Ringe in der Datenbank
        /// </summary>
        /// <returns></returns>
        public int UpdateRinge()
        {
            return (code.UpdateRinge());
        }

        /// <summary>
        ///     aktualisiert den Code in der Datenbank (Ringe muss extra aufgerufen werden)
        /// </summary>
        /// <returns></returns>
        public int UpdateCode()
        {
            return (code.UpdateCode());
        }

        /// <summary>
        ///     IMarkierer - Schnittstellenimplementierung. Ist dieser Knoten im Code enthalten?
        ///     Wenn ja wie ist er markiert.
        /// </summary>
        /// <param name = "kr">zu �berpr�fender Knoten</param>
        /// <returns>Die Zahlen f�r OLIs und Get aneinandergeh�ngt</returns>
        public string IsInString(KnotenDataSet.KnotenRow kr)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid)
                {
                    return rr.OLIs + rr.Get.ToString();
                }
            }
            return "";
        }

        /// <summary>
        ///     IMarkierer - Schnittstellenimplementierung. Ist dieser Zweig im Code enthalten?
        ///     Wenn ja wie ist er markiert.
        /// </summary>
        /// <param name = "kr">Letzter Knoten von dem dieser Zweig ausging</param>
        /// <param name = "zr">zu �berpr�fende Zweigreihe</param>
        /// <returns>OLIs und Get anenandergeh�ngt</returns>
        public string IsInString(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid && !rr.IsZweigGuidNull() && rr.ZweigGuid == zr.ZweigGuid)
                {
                    return rr.OLIs + rr.Get.ToString();
                }
            }
            return "";
        }

        /// <summary>
        ///     markiert einen Knoten
        /// </summary>
        /// <param name = "kr"></param>
        public void Markiere(KnotenDataSet.KnotenRow kr)
        {
            if (IsInString(kr).Length == 0)
            {
                CodeDataSet.RingeRow rr = code.Ringe.NewRingeRow();

                rr.RingGuid = Guid.NewGuid();
                rr.CodeGuid = CodeRow.CodeGuid;
                rr.NetzGuid = kr.NetzGuid;
                rr.KnotenGuid = kr.KnotenGuid;

                rr.OLIs = kr.VgbOLIs;
                rr.Get = kr.VgbGet;

                // Reihe hinzuf�gen
                MyRinge.Rows.Add(rr);

                // Update
                UpdateRinge();
            }
        }

        /// <summary>
        ///     markiert einen Zweig (mit �bergeordnetem Knoten)
        /// </summary>
        /// <param name = "kr"></param>
        /// <param name = "zr"></param>
        public void Markiere(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            if (IsInString(kr, zr).Length == 0)
            {
                CodeDataSet.RingeRow rr = code.Ringe.NewRingeRow();

                rr.RingGuid = Guid.NewGuid();
                rr.CodeGuid = CodeRow.CodeGuid;
                rr.NetzGuid = kr.NetzGuid;
                rr.KnotenGuid = kr.KnotenGuid;
                rr.BaumGuid = zr.BaumGuid;
                rr.ZweigGuid = zr.ZweigGuid;

                rr.OLIs = kr.VgbOLIs;
                rr.Get = kr.VgbGet;

                // Reihe hinzuf�gen
                MyRinge.Rows.Add(rr);

                // Update
                UpdateRinge();
            }
        }

        public void Update(KnotenDataSet.KnotenRow kr, string og)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid)
                {
                    rr.OLIs = int.Parse(og[0].ToString());
                    rr.Get = int.Parse(og[1].ToString());
                    // Update
                    UpdateRinge();
                    return;
                }
            }
        }

        public void Update(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr, string og)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid && rr.ZweigGuid == zr.ZweigGuid)
                {
                    rr.OLIs = int.Parse(og[0].ToString());
                    rr.Get = int.Parse(og[1].ToString());
                    // Update
                    UpdateRinge();
                    return;
                }
            }
        }

        /// <summary>
        ///     l�scht eine Knotenmarkierung
        /// </summary>
        /// <param name = "kr"></param>
        public void Clear(KnotenDataSet.KnotenRow kr)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid)
                {
                    rr.Delete();
                }
            }
            code.UpdateRinge();
        }

        /// <summary>
        ///     l�scht eine Zweigmarkierung (unterhalb des angegebenen Knotens)
        /// </summary>
        /// <param name = "kr"></param>
        /// <param name = "zr"></param>
        public void Clear(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            foreach (CodeDataSet.RingeRow rr in MyRinge)
            {
                if (rr.KnotenGuid == kr.KnotenGuid && rr.ZweigGuid == zr.ZweigGuid)
                {
                    rr.Delete();
                }
            }
            code.UpdateRinge();
        }

        #region MakeCodeRDF

        /// <summary>
        ///     erstellt einen RDF Abschnitt, der diesen Code beschreibt
        /// </summary>
        /// <returns></returns>
        public string MakeCodeRDF()
        {
            Guid cguid = CodeRow.CodeGuid;
            Code c = new Code(cguid);
            CodeDataSet.CodeRow cr = c.CodeRow;
//			CodeDataSet.RingeDataTable rdt = c.MyRinge;

            // XML Text Writer erstellen
            MemoryStream stream = new MemoryStream();
            XmlTextWriter xw = new XmlTextWriter(stream, Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            xw.Namespaces = false;

            xw.WriteStartDocument(); // <?xml>
            // <rdf:RDF
            xw.WriteStartElement("rdf:RDF");
            xw.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            xw.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/");
            xw.WriteAttributeString("xmlns:nlo", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("xml:base", "http://nulllogicone.net/");

            // -- Code -- HIER gehts eigentlich erst mit dem Code los
            xw.WriteStartElement("nlo:Code"); // Description
            xw.WriteAttributeString("rdf:about", "Code/?" + cguid); // Subject
            // Dublin Core
            xw.WriteComment("Dublin Core");
            xw.WriteComment("===========");
            xw.WriteElementString("dc:date", DateTime.Now.ToString("s"));
            xw.WriteStartElement("dc:publisher");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net");
            xw.WriteEndElement();
            // Kommentar, Version - Literale
            xw.WriteComment("Code Felder");
            xw.WriteComment("===========");
            xw.WriteElementString("nlo:codeGuid", cr.CodeGuid.ToString());
            xw.WriteElementString("nlo:beschreibung", cr["Kommentar"].ToString()); // Predicate Object
            xw.WriteElementString("nlo:version", cr["Versionsnummer"].ToString()); // Predicate Object
            // Stamm
            xw.WriteStartElement("nlo:codeStamm"); // Predicate
            xw.WriteStartElement("nlo:Stamm");
            xw.WriteAttributeString("rdf:about", "Stamm/?" + cr["StammGuid"]); // Object
            xw.WriteAttributeString("nlo:name", DbDirect.GiveStamm(cr["StammGuid"].ToString()));
            xw.WriteAttributeString("nlo:stammGuid", cr["StammGuid"].ToString());
            xw.WriteStartElement("nlo:resource");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Stamm/" + cr["StammGuid"] + ".rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
            // PostIt
            xw.WriteStartElement("nlo:codePostIt"); // Predicate
            xw.WriteStartElement("nlo:PostIt");
            xw.WriteAttributeString("rdf:about", "PostIt/?" + cr["PostItGuid"]); // Object
            xw.WriteAttributeString("nlo:postItGuid", cr["PostItGuid"].ToString());
            xw.WriteStartElement("nlo:resource");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/PostIt/" + cr["PostItGuid"] + ".rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteComment("CodeAngler - die passenden Filterprofile");
            xw.WriteComment("========================================");
            foreach (AnglerDataSet.AnglerRow ar in c.MyAngler.Angler)
            {
                xw.WriteStartElement("nlo:codeAngler"); // Predicate
                xw.WriteStartElement("nlo:Angler");
                xw.WriteAttributeString("rdf:about", "Angler/?" + ar.AnglerGuid); // Object
                xw.WriteAttributeString("nlo:anglerGuid", ar.AnglerGuid.ToString());
                xw.WriteAttributeString("nlo:name", ar.Angler); // Predicate, Object
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Angler/" + ar.AnglerGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            // -- alle Ringe -- (k�nnte ein 'Bag' werden)
            xw.WriteComment("CodeRinge - die einzelnen Markierungen (Quadruptel + bunte Punkte)");
            xw.WriteComment("==================================================================");
            foreach (DataRow dr in c.MyRinge)
            {
                xw.WriteStartElement("nlo:codeRing"); // Predicate 
                xw.WriteStartElement("nlo:Ring");
                xw.WriteAttributeString("rdf:about", "Ring/?" + dr["RingGuid"]); // = Subject

                // Beschreibung 
                xw.WriteElementString("nlo:beschreibung", makeNKBZ(dr)); // Predicate Object

                // Netz
                xw.WriteStartElement("nlo:markierungsStelleNetz"); // Predicate
                xw.WriteStartElement("nlo:Netz");
                xw.WriteAttributeString("rdf:about", "Netz/?" + dr["NetzGuid"]); // Predicate, Object
                xw.WriteAttributeString("nlo:name", DbDirect.GiveNetz(dr["NetzGuid"].ToString()));
                xw.WriteEndElement();
                xw.WriteEndElement();

                // Knoten
                xw.WriteStartElement("nlo:markierungsStelleKnoten"); // Predicate
                xw.WriteStartElement("nlo:Knoten");
                xw.WriteAttributeString("rdf:about", "Knoten/?" + dr["KnotenGuid"]); // Predicate, Object
                xw.WriteAttributeString("nlo:name", DbDirect.GiveKnoten(dr["KnotenGuid"].ToString()));
                // Predicate, Object
                xw.WriteEndElement();
                xw.WriteEndElement();

                if (dr["BaumGuid"].ToString().Length > 0)
                {
                    // Baum
                    xw.WriteStartElement("nlo:markierungsStelleBaum"); // Predicate
                    xw.WriteStartElement("nlo:Baum");
                    xw.WriteAttributeString("rdf:about", "Baum/?" + dr["BaumGuid"]); // Predicate, Object
                    xw.WriteAttributeString("nlo:name", DbDirect.GiveBaum(dr["BaumGuid"].ToString()));
                    // Predicate, Object
                    xw.WriteEndElement();
                    xw.WriteEndElement();

                    // Zweig
                    xw.WriteStartElement("nlo:markierungsStelleZweig"); // Predicate
                    xw.WriteStartElement("nlo:Zweig");
                    xw.WriteAttributeString("rdf:about", "Zweig/?" + dr["ZweigGuid"]); // Predicate, Object
                    xw.WriteAttributeString("nlo:name", DbDirect.GiveZweig(dr["ZweigGuid"].ToString()));
                    // Predicate, Object
                    xw.WriteEndElement();
                    xw.WriteEndElement();
                }

                // OLIs
                xw.WriteStartElement("nlo:olis");
                switch (dr["OLIs"].ToString())
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

                // get
                xw.WriteStartElement("nlo:get");
                switch (dr["get"].ToString())
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

                xw.WriteEndElement(); // ~Ring
                xw.WriteEndElement(); // ~ring weil ich Dich so liebe! Und nur deshalb! 
            }
            xw.WriteEndElement(); // ~ letzten Ring

            xw.WriteEndElement(); // ~</rdf:RDF>

            xw.Flush();
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string strOutput = sr.ReadToEnd();

            return strOutput;
        }

        /// <summary>
        ///     Hilfsfunktion um die Koordinate im Wortraum menschenlesbar darzustellen.
        /// </summary>
        /// <param name = "dr">Eine Ring DataRow des Codes</param>
        /// <returns>verkettet Netz - Knoten - Baum - Zweig</returns>
        private string makeNKBZ(DataRow dr)
        {
            string ret;
            ret = DbDirect.GiveNetz(dr["NetzGuid"].ToString()) + " - ";
            ret += DbDirect.GiveKnoten(dr["KnotenGuid"].ToString()) + " - ";
            ret += DbDirect.GiveBaum(dr["BaumGuid"].ToString()) + " - ";
            ret += DbDirect.GiveZweig(dr["ZweigGuid"].ToString());
            return ret;
        }

        #endregion
    }
}
