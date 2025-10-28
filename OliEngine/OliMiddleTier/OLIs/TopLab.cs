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
using OliEngine.DataSetTypes.Views;
using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Views;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     TopLab.
    /// </summary>
    public class TopLab
    {
        // Member
        // ------

        private OliDataAccess.TopLab topLab;
        private Tollis tollis;
        private readonly Stamm stamm;

        // hier geh�ren noch Eigenschaften daraus erstellt
        // TODO: - war nur grad zu sehr im Stress (nicht zu faul)
        public readonly Stamm MyStamm;
        public PostIt MyPostIt;

        // Konstruktor
        // -----------

        public TopLab(Guid tguid)
        {
            topLab = new OliDataAccess.TopLab(tguid);
        }

        public TopLab(Stamm stamm, Guid tguid)
        {
            this.stamm = stamm;
            topLab = new OliDataAccess.TopLab(tguid);

            MyStamm = new Stamm(null, topLab.TopLabRow.StammGuid);
            MyPostIt = new PostIt(null, topLab.TopLabRow.PostItGuid);
        }

        public TopLab(EingeloggterStamm eStamm, PostIt postIt)
        {
            stamm = eStamm;

            topLab = new OliDataAccess.TopLab();
            TopLabDataSet.TopLabRow tr = topLab.TopLab.NewTopLabRow();

            tr.StammGuid = eStamm.StammRow.StammGuid;
            tr.PostItGuid = postIt.PostItRow.PostItGuid;
            tr.TopLab = "";
            tr.Lohn = 0;
            tr.Datum = DateTime.Now;
            tr.TopLabGuid = Guid.NewGuid();
            tr.Typ = "txt";

            topLab.TopLab.AddTopLabRow(tr);
        }

        // Eigenschaften
        // -------------

        // TopLabRow
        public TopLabDataSet.TopLabRow TopLabRow
        {
            get { return topLab.TopLabRow; }
        }

        // MyTollis
        public TollisDataSet.TollisDataTable MyTollis
        {
            get
            {
                TollisList tl = new TollisList(TopLabRow);
                return (tl.Tollis);
            }
        }

        // MyTopLab
        public TopLabTopLabDataSet.TopLabTopLabDataTable MyTopLab
        {
            get
            {
                TopLabTopLab tt = new TopLabTopLab(TopLabRow);
                return (tt.TopLabTopLab);
            }
        }

        // BinIchMeinTopLab
        public bool BinIchMeinTopLab
        {
            get
            {
                try
                {
                    return (stamm.StammRow.StammGuid == TopLabRow.StammGuid);
                }
                catch
                {
                    return (false);
                }
            }
        }

        // Tollis
        public Tollis Tollis
        {
            get { return (tollis); }
            set { tollis = value; }
        }

        // Methoden
        // --------

        public void AddTopLab(OliUser user, string titel, string text)
        {
            OliDataAccess.TopLab tl = new OliDataAccess.TopLab();
            TopLabDataSet.TopLabRow tlr = tl.TopLab.NewTopLabRow();

            tlr.TopLabGuid = Guid.NewGuid();
            tlr.Typ = "txt";
            tlr.StammGuid = user.Stamm.StammRow.StammGuid;
            tlr.TopTopLabGuid = user.Stamm.TopLab.TopLabRow.TopLabGuid;
            tlr.PostItGuid = user.Stamm.PostIt.PostItRow.PostItGuid;

            tlr.Titel = titel;
            tlr.TopLab = text;
            tlr.Datum = DateTime.Now;
            tlr.Lohn = 0;

            tl.TopLab.Rows.Add(tlr);
            tl.UpdateTopLab();
        }

        // Update
        public int UpdateTopLab()
        {
            int result;

            if (topLab.TopLabRow.RowState == DataRowState.Added)
            {
                // rowguid merken
                Guid tguid = topLab.TopLabRow.TopLabGuid;

                // update
                result = topLab.UpdateTopLab();

                // Zeilen in News suchen
                Guid pguid = topLab.TopLabRow.PostItGuid;
                DataRow[] drs = stamm.MyNews.Select("PostItGuid='" + pguid + "'");

                // News gelesen
                foreach (DataRow dr in drs)
                {
                    StammNewsDataSet.StammNewsRow snr = (StammNewsDataSet.StammNewsRow) dr;
                    stamm.NewsGelesen(snr.NewsGuid);
                }

                topLab = new OliDataAccess.TopLab(tguid);
            }
            else
            {
                result = topLab.UpdateTopLab();
            }

            return (result);
        }

        // ShowTollis
        public void ShowTollis(StammDataSet.StammRow stammRow, TopLabDataSet.TopLabRow topLabRow)
        {
            tollis = new Tollis(stammRow, topLabRow);
        }

        public string MakeTopLabRDF()
        {
            TopLabDataSet.TopLabRow tr = TopLabRow;

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

            // -- TopLab --
            xw.WriteStartElement("nlo:TopLab"); // Description
            xw.WriteAttributeString("rdf:about", "TopLab/?" + tr.TopLabGuid); // Subject
            // Dublin Core
            xw.WriteComment("Dublin Core");
            xw.WriteComment("===========");
            xw.WriteElementString("dc:date", DateTime.Now.ToString("s"));
            xw.WriteStartElement("dc:publisher");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net");
            xw.WriteEndElement();

            // Felder
            xw.WriteComment("TopLab Felder");
            xw.WriteComment("=============");
            xw.WriteElementString("nlo:topLabGuid", tr.TopLabGuid.ToString());
            xw.WriteElementString("nlo:titel", tr.IsTitelNull() ? "" : tr.Titel);
            xw.WriteElementString("nlo:text", tr.TopLab);
            xw.WriteElementString("nlo:flowKook", tr.Lohn.ToString().Replace(",", "."));
            xw.WriteElementString("nlo:datum", tr.Datum.ToString("s"));
            if (!tr.IsDateiNull())
            {
                xw.WriteStartElement("nlo:datei");
                xw.WriteAttributeString("rdf:resource", OliUtil.MakeImageSrc(tr.Datei));
                xw.WriteEndElement();
            }
            if (!tr.IsURLNull() && tr.URL.Length > 3)
            {
                xw.WriteStartElement("nlo:link");
                xw.WriteAttributeString("rdf:resource", tr.URL);
                xw.WriteEndElement();
            }
            xw.WriteElementString("nlo:typ", tr.Typ);

            // Stamm
            xw.WriteStartElement("nlo:topLabStamm");
            xw.WriteStartElement("nlo:Stamm");
            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/Stamm/?" + TopLabRow.StammGuid);
            xw.WriteAttributeString("nlo:stammGuid", TopLabRow.StammGuid.ToString());
            xw.WriteAttributeString("nlo:name", DbDirect.GiveStamm(TopLabRow.StammGuid));
            xw.WriteStartElement("nlo:resource");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Stamm/" + TopLabRow.StammGuid + ".rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();

            // PostIt
            xw.WriteStartElement("nlo:topLabPostIt");
            xw.WriteStartElement("nlo:PostIt");
            xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/PostIt/?" + TopLabRow.PostItGuid);
            xw.WriteAttributeString("nlo:postItGuid", TopLabRow.PostItGuid.ToString());
            xw.WriteStartElement("nlo:resource");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/PostIt/" + TopLabRow.PostItGuid + ".rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();

            // Top - TopLab
            if (!tr.IsTopTopLabGuidNull())
            {
                xw.WriteComment("Top TopLab - �bergeordnete Antwort");
                xw.WriteComment("==================================");
                xw.WriteStartElement("nlo:topLabTopLab");
                xw.WriteStartElement("nlo:TopLab");
                xw.WriteAttributeString("rdf:about", "http://nulllogicone.net/TopLab/?" + tr.TopTopLabGuid);
                xw.WriteAttributeString("nlo:topLabGuid", tr.TopTopLabGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/TopLab/" + tr.TopTopLabGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

//			foreach(TollisDataSet.TollisRow tr in this.MyTollis)
//			{
//				xw.WriteStartElement("nlo:toll");
//				xw.WriteElementString("nlo:text", tr.TollText);
//				xw.WriteElementString("nlo:datum", tr.IsdatumNull() ? "" : tr.datum);
//				xw.WriteElementString("nlo:
//			}

            xw.WriteEndElement(); // ~TopLab
            xw.WriteEndElement(); // ~</rdf:RDF>

            xw.Flush();
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string strOutput = sr.ReadToEnd();

            return strOutput;
        }
    }
}
