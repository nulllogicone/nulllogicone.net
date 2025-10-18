// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:58
// --------------------------
//  

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Views;
using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Views;
using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Angler.
    /// </summary>
    public class Angler : IMarkierer
    {
        // Member 
        // ------

        private OliDataAccess.Angler angler;
        private readonly Stamm stamm;
        private readonly ZellBuilder zellBuilder = new ZellBuilder();
//		bool showWortraum;

        private AnglerPostItDataSet.AnglerPostItDataTable anglerPostIt;

        // Konstruktoren
        // -------------

        /// <summary>
        ///     OBACHT: dieser Konstruktor erstellt einen Angler nur anhand seiner
        ///     AnglerGuid und implizit den dazugehörenden Stamm.
        ///     Normalerweise sollte ein Angler nur über das bestehende OliUser.Stamm
        ///     objekt mit der Methode ShowAngler erstellt werden.
        /// </summary>
        /// <param name="aguid"> </param>
        public Angler(Guid aguid)
        {
            DataRow[] dr = OliDb.GiveRows("oli.Angler", "AnglerGuid", aguid);
            if (dr.Length == 1)
            {
                Guid sguid = new Guid(dr[0]["StammGuid"].ToString());
                stamm = new Stamm(sguid);

                angler = new OliDataAccess.Angler(aguid);
                AnglerMarkierer am = new AnglerMarkierer(aguid);
                zellBuilder.Markierer = am;
            }
        }

        public Angler(Stamm stamm, Guid aguid)
        {
            this.stamm = stamm;
            angler = new OliDataAccess.Angler(aguid);

            AnglerMarkierer am = new AnglerMarkierer(aguid);
            zellBuilder.Markierer = am;
        }

        public Angler(EingeloggterStamm eingeloggterStamm)
        {
            stamm = eingeloggterStamm;
            angler = new OliDataAccess.Angler();
            AnglerDataSet.AnglerRow ar = angler.Angler.NewAnglerRow();

            ar.StammGuid = eingeloggterStamm.StammRow.StammGuid;
            ar.Angler = "";
            ar.gescannt = false;
            ar.Versionsnummer = OliCommon.WortraumVersion;
            ar.Datum = DateTime.Now;
            ar.AnglerGuid = Guid.NewGuid();

            // Reihe hinzufügen
            angler.Angler.AddAnglerRow(ar);
        }

        // Eigenschaften
        // -------------

        // AnglerRow
        public AnglerDataSet.AnglerRow AnglerRow
        {
            get { return angler.AnglerRow; }
        }

        // ZellBuilder
        public ZellBuilder ZellBuilder
        {
            get { return (zellBuilder); }
        }

        // MyPostIt
        public AnglerPostItDataSet.AnglerPostItDataTable MyPostIt
        {
            get
            {
                if (anglerPostIt == null)
                {
                    anglerPostIt = new AnglerPostIt(AnglerRow).AnglerPostIt;
                }
                return (anglerPostIt);
            }
            set { anglerPostIt = value; }
        }

        public AnglerDataSet.LöcherDataTable MyLöcher
        {
            get
            {
                AnglerDataSet.LöcherDataTable ldt;
                ldt = angler.Löcher;
                return ldt;
            }
        }

        public void ClearLoecher()
        {
            angler.ClearLoecher();
        }

//		/// <summary>
//		/// ShowWortraum ************ sollte gelöscht werden ***************
//		/// </summary>
//		public bool ShowWortraum
//		{
//			get
//			{
//				return(showWortraum);
//			}
//			set
//			{
//				showWortraum = value;
//			}
//		}

        // BinIchMeinAngler
        public bool BinIchMeinAngler
        {
            get
            {
                try
                {
                    return (stamm.StammRow.StammGuid == AnglerRow.StammGuid);
                }
                catch
                {
                    return (false);
                }
            }
        }

        // Methoden
        // --------

        // AlleNewsGelesen
        public void AlleNewsGelesen()
        {
            StammNews sn = new StammNews(stamm.StammRow);
            DataRow[] drs = sn.StammNews.Select("AnglerGuid='" + AnglerRow.AnglerGuid + "'");
            foreach (DataRow dr in drs)
            {
                StammNewsDataSet.StammNewsRow snr = (StammNewsDataSet.StammNewsRow) dr;
                News n = new News(snr.NewsGuid);
                n.NewsRow.Delete();
                n.UpdateNews();
            }
        }

        // CodeGelesen
        public void CodeGelesen(Guid codeGuid)
        {
            Spiegel sp = new Spiegel(codeGuid, AnglerRow);
            if (stamm.BinIchEingeloggt)
            {
                if (sp != null)
                {
                    sp.SpiegelRow.gelesen = DateTime.Now;
                    sp.UpdateSpiegel();
                }
            }
        }

        // Update
        public int UpdateAngler()
        {
//			return angler.UpdateAngler();
            if (angler.AnglerRow.RowState == DataRowState.Deleted)
            {
                int r = angler.UpdateAngler();
                return (r);
            }
            else
            {
                Guid aguid = angler.AnglerRow.AnglerGuid;
                int r = angler.UpdateAngler();

                angler = new OliDataAccess.Angler(aguid);

                AnglerMarkierer am = new AnglerMarkierer(aguid);
                zellBuilder.Markierer = am;

                return (r);
            }
        }

        public int UpdateLöcher()
        {
            return angler.UpdateLöcher();
        }

        /// <summary>
        ///     erstellt einen RDF Abschnitt, der diesen Angler beschreibt
        /// </summary>
        /// <returns> </returns>
        public string MakeAnglerRDF()
        {
            AnglerDataSet.AnglerRow ar = AnglerRow;

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

            // -- Angler -- HIER gehts eigentlich erst mit dem Angler los
            xw.WriteStartElement("nlo:Angler"); // Description
            xw.WriteAttributeString("rdf:about", "Angler/?" + ar.AnglerGuid); // Subject
            // Dublin Core
            xw.WriteComment("Dublin Core");
            xw.WriteComment("===========");
            xw.WriteElementString("dc:date", DateTime.Now.ToString("s"));
            xw.WriteStartElement("dc:publisher");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net");
            xw.WriteEndElement();
            // Angler Felder
            xw.WriteComment("Angler Felder");
            xw.WriteComment("=============");
            // Stamm
            xw.WriteStartElement("nlo:anglerStamm"); // Predicate
            xw.WriteStartElement("nlo:Stamm");
            xw.WriteAttributeString("rdf:about", "Stamm/?" + ar.StammGuid); // Object
            xw.WriteAttributeString("nlo:name", DbDirect.GiveStamm(ar["StammGuid"].ToString()));
            xw.WriteStartElement("nlo:resource");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Stamm/" + ar["StammGuid"] + ".rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteElementString("nlo:anglerGuid", ar.AnglerGuid.ToString());
            xw.WriteElementString("nlo:name", ar.Angler); // Predicate Object
            xw.WriteElementString("nlo:version", ar.IsVersionsnummerNull() ? "" : ar.Versionsnummer);
            // Predicate Object
            xw.WriteElementString("nlo:datum", ar.Datum.ToString("s")); // Predicate Object
            xw.WriteElementString("nlo:beschreibung", ar.IsBeschreibungNull() ? "" : ar.Beschreibung);
            // Predicate Object

            xw.WriteComment("AnglerPostIt - die geangelten Nachrichten (Fische)");
            xw.WriteComment("==================================================");
            foreach (AnglerPostItDataSet.AnglerPostItRow apr in MyPostIt)
            {
                xw.WriteStartElement("nlo:anglerPostIt");
                xw.WriteStartElement("nlo:PostIt"); // Predicate
                xw.WriteAttributeString("rdf:about", "PostIt/?" + apr.PostItGuid); // Object
                xw.WriteAttributeString("nlo:postItGuid", apr.PostItGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/PostIt/" + apr.PostItGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            // -- alle Löcher -- (könnte ein 'Bag' werden)

            xw.WriteComment("AnglerLoecher - die einzelnen Quadrupel mit bunten Punkten");
            xw.WriteComment("========================================================");
            foreach (DataRow dr in MyLöcher)
            {
                xw.WriteStartElement("nlo:anglerLoch"); // Predicate 
                xw.WriteStartElement("nlo:Loch");
                xw.WriteAttributeString("rdf:about", "Loch/?" + dr["LochGuid"]); // = Subject

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

                // ILOs
                xw.WriteStartElement("nlo:ilos");
                switch (dr["ILOs"].ToString())
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
                xw.WriteStartElement("nlo:fit");
                switch (dr["fit"].ToString())
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

                // ---- Ende
                xw.WriteEndElement(); // ~Loch
                xw.WriteEndElement(); // ~loch
            }

            xw.WriteEndElement(); // ~ letztes Loch

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
        /// <param name="dr"> Eine Ring DataRow des Codes </param>
        /// <returns> verkettet Netz - Knoten - Baum - Zweig </returns>
        private string makeNKBZ(DataRow dr)
        {
            string ret;
            ret = DbDirect.GiveNetz(dr["NetzGuid"].ToString()) + " - ";
            ret += DbDirect.GiveKnoten(dr["KnotenGuid"].ToString()) + " - ";
            ret += DbDirect.GiveBaum(dr["BaumGuid"].ToString()) + " - ";
            ret += DbDirect.GiveZweig(dr["ZweigGuid"].ToString());
            return ret;
        }

        #region IMarkierer Member

        public string IsInString(KnotenDataSet.KnotenRow kr)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid)
                {
                    return lr.ILOs + lr.Fit.ToString();
                }
            }
            return "";
        }

        public string IsInString(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid && !lr.IsZweigGuidNull() && lr.ZweigGuid == zr.ZweigGuid)
                {
                    return lr.ILOs + lr.Fit.ToString();
                }
            }
            return "";
        }

        public void Markiere(KnotenDataSet.KnotenRow kr)
        {
            if (IsInString(kr).Length == 0)
            {
                AnglerDataSet.LöcherRow lr = angler.Löcher.NewLöcherRow();

                lr.LochGuid = Guid.NewGuid();
                lr.AnglerGuid = AnglerRow.AnglerGuid;
                lr.NetzGuid = kr.NetzGuid;
                lr.KnotenGuid = kr.KnotenGuid;

                lr.ILOs = kr.VgbILOs;
                lr.Fit = kr.VgbFit;

                // Reihe hinzufügen
                MyLöcher.Rows.Add(lr);

                // Update
                UpdateLöcher();
            }
        }

        public void Markiere(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            if (IsInString(kr, zr).Length == 0)
            {
                AnglerDataSet.LöcherRow lr = angler.Löcher.NewLöcherRow();

                lr.LochGuid = Guid.NewGuid();
                lr.AnglerGuid = AnglerRow.AnglerGuid;
                lr.NetzGuid = kr.NetzGuid;
                lr.KnotenGuid = kr.KnotenGuid;
                lr.BaumGuid = zr.BaumGuid;
                lr.ZweigGuid = zr.ZweigGuid;

                lr.ILOs = kr.VgbILOs;
                lr.Fit = kr.VgbFit;

                // Reihe hinzufügen
                MyLöcher.Rows.Add(lr);

                // Update
                UpdateLöcher();
            }
        }

        public void Update(KnotenDataSet.KnotenRow kr, string og)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid)
                {
                    lr.ILOs = int.Parse(og[0].ToString());
                    lr.Fit = int.Parse(og[1].ToString());
                    // Update
                    UpdateLöcher();
                    return;
                }
            }
        }

        public void Update(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr, string og)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid && lr.ZweigGuid == zr.ZweigGuid)
                {
                    lr.ILOs = int.Parse(og[0].ToString());
                    lr.Fit = int.Parse(og[1].ToString());
                    // Update
                    UpdateLöcher();
                    return;
                }
            }
        }

        public void Clear(KnotenDataSet.KnotenRow kr)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid)
                {
                    lr.Delete();
                }
            }
            UpdateLöcher();
        }

        public void Clear(KnotenDataSet.KnotenRow kr, ZweigDataSet.ZweigRow zr)
        {
            foreach (AnglerDataSet.LöcherRow lr in MyLöcher)
            {
                if (lr.KnotenGuid == kr.KnotenGuid && lr.ZweigGuid == zr.ZweigGuid)
                {
                    lr.Delete();
                }
            }
            UpdateLöcher();
        }

        #endregion
    }
}