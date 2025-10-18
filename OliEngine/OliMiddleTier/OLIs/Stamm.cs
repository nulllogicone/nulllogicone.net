// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using OliEngine.DataSetTypes;
using OliEngine.DataSetTypes.Analysis;
using OliEngine.DataSetTypes.Views;
using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Analysis;
using OliEngine.OliDataAccess.Views;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     Stamm.
    /// </summary>
    public class Stamm
    {
        // Member
        // ------

        // mein DataAccess-Stamm-Objekt
        protected OliDataAccess.Stamm stamm;

        // ein Verweis auf das aktuell ausgewählte Objekt
        private readonly OliUser user;
        private Angler angler;
        private PostIt postIt;
        private TopLab topLab;
        private Tollis tollis;
        private ShortCuts shortCuts;
        private QDataSet.QRow q;
        private Extras extras;

        private StammAnglerDataSet.StammAnglerDataTable stammAngler;
        private StammPostItDataSet.StammPostItDataTable stammPostIt;
        private StammNewsDataSet.StammNewsDataTable stammNews;
        private StammInboxDataSet.StammInboxDataTable stammInbox;
        private StammTopLabDataSet.StammTopLabDataTable stammTopLab;
        private ShortCutsDataSet.ShortCutsDataTable stammShortCuts;
        private TollisDataSet.TollisDataTable stammTollis;
        private StammKontoDataSet.StammKontoDataTable stammKonto;
//		FristAbgelaufenDataSet.FristAbgelaufenDataTableDataTable fristAbgelaufen;

        // Werbefrei                   ******** 
        // TODO: diese Eigenschaft gehört zu der Stamm-Eigenschaften Tabelle
//		public bool werbefrei = true;
//		private Stamm.SichtbaresGrid sichtbareGrid = Stamm.SichtbaresGrid.None;

        // Konstruktor
        // ------------

        public Stamm(Guid sguid)
        {
            stamm = new OliDataAccess.Stamm(sguid);
        }

        public Stamm(OliUser user, Guid sguid)
        {
            this.user = user;
            stamm = new OliDataAccess.Stamm(sguid);
        }

        /// <summary>
        ///     Konstruktor für Stamm(mit user)
        ///     <param name = "user">Ein Stamm hängt immer an einem user-Objekt</param>
        ///     <see cref = "OliUser">Da kann man die Klasse sehen</seealso>
        /// </summary>
        public Stamm(OliUser user)
        {
            this.user = user;
            stamm = new OliDataAccess.Stamm();
            StammDataSet.StammRow sr = stamm.Stamm.NewStammRow();

            sr.Stamm = "";
            sr.Datum = DateTime.Now;
            sr.KooK = OliCommon.DefaultBoundKooK; // TODO: we need to add an entry in StammKonto table
            sr.Versionsnummer = OliCommon.WortraumVersion;
            sr.Beschreibung = "";
            sr.zuQID = 5;
            sr.StammGuid = Guid.NewGuid();

            stamm.Stamm.AddStammRow(sr);


        }

        #region Eigenschaften		

        // Eigenschaften
        // -------------

        // OliUser
        public OliUser OliUser
        {
            get { return (user); }
        }

        // StammRow
        public StammDataSet.StammRow StammRow
        {
            get { return (stamm.StammRow); }
        }

        // Angler
        public Angler Angler
        {
            get { return (angler); }
            set { angler = value; }
        }

        // PostIt
        public PostIt PostIt
        {
            get { return postIt; }
            set { postIt = value; }
        }

        // TopLab
        public TopLab TopLab
        {
            get { return (topLab); }
            set { topLab = value; }
        }

        // Tollis
        public Tollis Tollis
        {
            get { return (tollis); }
            set { tollis = value; }
        }

        // ShortCuts
        public ShortCuts ShortCuts
        {
            get { return (shortCuts); }
            set { shortCuts = value; }
        }

        // Q
        public QDataSet.QRow Q
        {
            get
            {
                if (q == null)
                {
                    q = new Q(StammRow).QRow;
                }
                return (q);
            }
            set { q = value; }
        }

        // Extras
        public Extras Extras
        {
            get
            {
                if (StammRow.RowState == DataRowState.Added)
                {
                    extras = new Extras(user);
                }
                else
                {
                    if (extras == null)
                    {
                        extras = new Extras(StammRow);
                    }
                }
                return (extras);
            }
            set { extras = value; }
        }

        // MyAngler
        public StammAnglerDataSet.StammAnglerDataTable MyAngler
        {
            get
            {
                if (stammAngler == null)
                {
                    stammAngler = new StammAngler(StammRow).StammAngler;
                }
                return (stammAngler);
            }
            set { stammAngler = value; }
        }

        // MyPostIt
        public StammPostItDataSet.StammPostItDataTable MyPostIt
        {
            get
            {
                if (stammPostIt == null)
                {
                    if (Extras.ExtrasRow != null && Extras.ExtrasRow.showclosed)
                    {
                        stammPostIt = new StammPostIt(StammRow, true).StammPostIt;
                    }
                    else
                    {
                        stammPostIt = new StammPostIt(StammRow).StammPostIt;
                    }
                }
                return (stammPostIt);
            }
            set { stammPostIt = value; }
        }

        // MyNews
        public StammNewsDataSet.StammNewsDataTable MyNews
        {
            get
            {
                if (stammNews == null)
                {
                    stammNews = new StammNews(StammRow).StammNews;
                }
                return (stammNews);
            }
            set { stammNews = value; }
        }

        // MyInbox
        public StammInboxDataSet.StammInboxDataTable MyInbox
        {
            get
            {
                if (stammInbox == null)
                {
                    stammInbox = new StammInbox(StammRow).StammInbox;
                }
                return (stammInbox);
            }
            set { stammInbox = value; }
        }

        // MyTopLab
        public StammTopLabDataSet.StammTopLabDataTable MyTopLab
        {
            get
            {
                if (stammTopLab == null)
                {
                    stammTopLab = new StammTopLab(StammRow).StammTopLab;
                }
                return (stammTopLab);
            }
            set { stammTopLab = value; }
        }

        // MyShortCuts
        public ShortCutsDataSet.ShortCutsDataTable MyShortCuts
        {
            get
            {
                if (stammShortCuts == null)
                {
                    stammShortCuts = new ShortCutsList(StammRow).ShortCuts;
                }
                return (stammShortCuts);
            }
            set { stammShortCuts = value; }
        }

        // MyTollis
        public TollisDataSet.TollisDataTable MyTollis
        {
            get
            {
                TollisList tl = new TollisList(StammRow);
                return (tl.Tollis);
            }
            set { stammTollis = value; }
        }

        // MyKonto
        public StammKontoDataSet.StammKontoDataTable MyKonto
        {
            get
            {
                StammKonto sk = new StammKonto(StammRow.StammGuid);
                return (sk.StammKonto);
            }
            set { stammKonto = value; }
        }

        // MyFristAbgelaufen
        public FristAbgelaufenDataSet.FristAbgelaufenDataTable MyFristAbgelaufen
        {
            get
            {
                FristAbgelaufen fa = new FristAbgelaufen(StammRow);
                return (fa.FristAbgelaufen);
            }
            set { }
        }

        // BinIchEingeloggt
        public bool BinIchEingeloggt
        {
            get
            {
                if (user.EingeloggterStamm != null &&
                    user.EingeloggterStamm.StammRow.StammGuid == StammRow.StammGuid)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        #endregion

        #region Methoden

        // Methoden
        // --------

        // ShowAngler
        public void ShowAngler(Guid aguid)
        {
            angler = new Angler(this, aguid);
        }

        // ShowPostIt
        public void ShowPostIt(Guid pguid)
        {
            postIt = new PostIt(this, pguid);

            try
            {
                // wenn standard bezahlt => user benachrichtigen
                if (postIt.StammZahlt == 1 && !postIt.StammClosed && BinIchEingeloggt)
                {
//					user.Nachricht = "Die Nachricht hat noch keinen besonderen Wert (KooK)";
//					user.Nachricht = "Sie können frankieren und eine Frist setzen";
                }
                if (postIt.StammClosed && BinIchEingeloggt)
                {
                    user.Nachricht = "message sent";
                }

                //HttpContext ctx = HttpContext.Current;
                //string kommentar = ctx.Request.RawUrl;

                // Tuerlog protokollieren
                //ProtokollTuerLog(kommentar);
            }
            catch 
            {
            }
        }

        // ShowTopLab
        public void ShowTopLab(Guid tguid)
        {
            topLab = new TopLab(this, tguid);

            //HttpContext ctx = HttpContext.Current;
            //string kommentar = ctx.Request.RawUrl;

            // Tuerlog protokollieren
            //ProtokollTuerLog(kommentar);
        }

        // ShowShortCuts
        public void ShowShortCuts(Guid scguid)
        {
            shortCuts = new ShortCuts(this, scguid);
        }

        // ShowTollis
        public void ShowTollis(StammDataSet.StammRow stammRow, TopLabDataSet.TopLabRow topLabRow)
        {
            tollis = new Tollis(stammRow, topLabRow);
        }

        // NewsGelesen
        public void NewsGelesen(Guid nguid)
        {
            if (BinIchEingeloggt)
            {
                News n = new News(nguid);
                n.NewsRow.gelesen = DateTime.Now;
                n.UpdateNews();
            }
        }

        // NewsGesehen
        public void NewsGesehen(Guid nguid)
        {
            if (BinIchEingeloggt)
            {
                News n = new News(nguid);
                n.NewsRow.gesehen = DateTime.Now;
                n.UpdateNews();
            }
        }

        // InboxGelesen
        public void InboxGelesen(Guid iguid)
        {
            if (BinIchEingeloggt)
            {
                Inbox ib = new Inbox(iguid);
                ib.InboxRow.gelesen = DateTime.Now;
                ib.UpdateInbox();
            }
        }

        // InboxGelesen
        public void InboxGelesen(TopLabDataSet.TopLabRow topLabRow)
        {
            if (BinIchEingeloggt)
            {
                Inbox ib = new Inbox(StammRow, topLabRow);
                if (ib.InboxRow != null)
                {
                    ib.InboxRow.gelesen = DateTime.Now;
                    ib.UpdateInbox();
                }
            }
        }

        // InboxGesehen
        public void InboxGesehen(TopLabDataSet.TopLabRow topLabRow)
        {
            if (BinIchEingeloggt)
            {
                Inbox ib = new Inbox(StammRow, topLabRow);
                if (ib.InboxRow != null)
                {
                    ib.InboxRow.gesehen = DateTime.Now;
                    ib.UpdateInbox();
                }
            }
        }

        // UpdateStamm()
        public int UpdateStamm()
        {
            if (stamm.StammRow.RowState == DataRowState.Added)
            {
                var sguid = stamm.StammRow.StammGuid.ToString();
                var result = stamm.UpdateStamm();

                var pwd = OliDb.GiveRows("oli.Stamm", "StammGuid", sguid)[0]["Unterschrift"].ToString();

                user.ShowStamm(stamm.StammRow.Stamm, pwd);
                return (result);
            }
            else if (BinIchEingeloggt)
            {
                return (stamm.UpdateStamm());
            }
            else
            {
                throw new Exception("Nicht eingeloggter Stamm versucht UpdateStamm");
            }
        }

        // UpdateStamm(freund)
        public int UpdateStamm(Stamm freund)
        {
            if (stamm.StammRow.RowState == DataRowState.Added)
            {
                string sguid = stamm.StammRow.StammGuid.ToString();
                int result = stamm.UpdateStamm();

                string pwd = OliDb.GiveRows("oli.Stamm", "StammGuid", sguid)[0]["Unterschrift"].ToString();

                user.ShowStamm(stamm.StammRow.Stamm, pwd);

                // Den Freund mit 10 KooK Gutschrift beschenken
                // TODO: 10 raus 
                freund.StammRow.KooK += 10;
                freund.UpdateStamm(freund);
                user.Nachricht = user.Freund.StammRow.Stamm + " sind 10 KooK gutgeschrieben worden";

                return (result);
            }
            else
            {
                return (stamm.UpdateStamm());
            }
        }

        // NewShortCuts
        public ShortCuts NewShortCuts()
        {
            ShortCuts = new ShortCuts(this);
            return ShortCuts;
        }

        // CopyShortCutsToCode
        public bool CopyShortCutsToCode(Guid scguid, Guid cguid)
        {
            OliDataAccess.ShortCuts shortCuts = new OliDataAccess.ShortCuts(scguid);
            OliDataAccess.Code code = new OliDataAccess.Code(cguid);

            foreach (DataRow dr in shortCuts.myStrings)
            {
                ShortCutsDataSet.StringsRow sr = (ShortCutsDataSet.StringsRow) dr;

                CodeDataSet.RingeRow rr = code.Ringe.NewRingeRow();
                rr.CodeGuid = cguid;
                rr.NetzGuid = sr.NetzGuid;
                rr.KnotenGuid = sr.KnotenGuid;
                if (!sr.IsBaumGuidNull() && !sr.IsZweigGuidNull())
                {
                    rr.BaumGuid = sr.BaumGuid;
                    rr.ZweigGuid = sr.ZweigGuid;
                }
                rr.OLIs = sr.Verb;
                rr.Get = sr.Attrib;
                rr.RingGuid = Guid.NewGuid();

                code.Ringe.AddRingeRow(rr);
                code.UpdateRinge();
            }

            return (true);
        }

//		// DeleteNachricht()
//		public void DeleteNachricht(Guid nguid)
//		{
//			if(this.BinIchEingeloggt)
//			{
//				Nachrichten n = new Nachrichten(user.Stamm.StammRow);
//				NachrichtenDataSet.NachrichtenRow nr = n.Nachrichten.FindByNachrichtenGuid(nguid);
//			
//				nr.Delete();
//				n.UpdateNachrichten();
//			}
//		}
//
//		// DeleteAllNachrichten
//		public void DeleteAllNachrichten()
//		{
//			if(this.BinIchEingeloggt)
//			{
//				Nachrichten n = new Nachrichten(user.Stamm.StammRow);
//				foreach(DataRow r in n.Nachrichten.Rows)
//				{
//					r.Delete();
//				}
//				n.UpdateNachrichten();
//			}
//		}

        private void ProtokollTuerLog(string kommentar)
        {
            // TuerLog protokollieren 
            try
            {
//				TuerLog tl = new TuerLog();

                // ip-Adresse
                string ip = OliUser.IpAdress;

                // eingeloggter Stamm
                Guid eglsguid;
                if (OliUser.EingeloggterStamm != null)
                {
                    eglsguid = OliUser.EingeloggterStamm.StammRow.StammGuid;
                }
                else
                {
                    eglsguid = new Guid();
                }

                // StammGuid
                Guid sguid = StammRow.StammGuid;

                // AnglerGuid
                Guid aguid;
                if (Angler != null)
                {
                    aguid = Angler.AnglerRow.AnglerGuid;
                }
                else
                {
                    aguid = new Guid();
                }

                // PostItGuid
                Guid pguid;
                if (PostIt != null)
                {
                    pguid = PostIt.PostItRow.PostItGuid;
                }
                else
                {
                    pguid = new Guid();
                }

                // TopLabGuid
                Guid tguid;
                if (TopLab != null)
                {
                    tguid = TopLab.TopLabRow.TopLabGuid;
                }
                else
                {
                    tguid = new Guid();
                }

//				tl.InsertEntry(ip,eglsguid,sguid, aguid, pguid, tguid, kommentar);
                //TuerLog.InsertEntry(ip, eglsguid, sguid, aguid, pguid, tguid, kommentar);
            }
            catch 
            {
            }
        }

        // ToString()
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<br>Stamm: ");
            sb.Append(StammRow.Stamm);

            sb.Append("<br>PostIt: ");
            if (PostIt != null)
            {
                sb.Append(OliUtil.FirstXWords(PostIt.PostItRow.PostIt, 10));
            }

            sb.Append("<br>Angler: ");
            if (Angler != null)
            {
                sb.Append(Angler.AnglerRow.Angler);
            }

            sb.Append("<br>TopLab: ");
            if (TopLab != null)
            {
                sb.Append(OliUtil.FirstXWords(TopLab.TopLabRow.TopLab, 10));
            }

//			sb.Append("<br>sichtbare Grid: ");
//			sb.Append(this.sichtbaresGrid.ToString());

            return sb.ToString();
        }

        /// <summary>
        ///     erstellt einen RDF Abschnitt, der diesen Code beschreibt
        /// </summary>
        /// <returns></returns>
        public string MakeStammRDF()
        {
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

            // -- Stamm --
            xw.WriteStartElement("nlo:Stamm"); // Description
            xw.WriteAttributeString("rdf:about", "Stamm/?" + StammRow.StammGuid); // Subject
            // Dublin Core
            xw.WriteComment("Dublin Core");
            xw.WriteComment("===========");
            xw.WriteElementString("dc:date", DateTime.Now.ToString("s"));
            xw.WriteStartElement("dc:publisher");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net");
            xw.WriteEndElement();
            // Stamm Elemente
            xw.WriteComment("Stamm Felder");
            xw.WriteComment("============");
            xw.WriteElementString("nlo:stammGuid", StammRow.StammGuid.ToString());
            xw.WriteElementString("nlo:name", StammRow.Stamm); // Predicate + Object
            xw.WriteElementString("nlo:datum", StammRow.Datum.ToString("s")); // Predicate + Object
            xw.WriteElementString("nlo:beschreibung", StammRow.IsBeschreibungNull() ? "" : StammRow.Beschreibung);
            // Predicate + Object
            xw.WriteStartElement("nlo:datei");
            xw.WriteAttributeString("rdf:resource", StammRow.IsDateiNull() ? "" : OliUtil.MakeImageSrc(StammRow.Datei));
            xw.WriteEndElement();
            xw.WriteElementString("nlo:boundKook", StammRow.KooK.ToString().Replace(",", ".")); // Predicate + Object

            // StammAngler
            xw.WriteComment("StammAngler - seine Filterprofile");
            xw.WriteComment("=================================");
            foreach (StammAnglerDataSet.StammAnglerRow  ar in MyAngler)
            {
                xw.WriteStartElement("nlo:stammAngler"); // Predicate
                xw.WriteStartElement("nlo:Angler");
                xw.WriteAttributeString("rdf:about", "Angler/?" + ar.AnglerGuid); // Object
                xw.WriteAttributeString("nlo:name", ar.Angler); // Predicate, Object
                xw.WriteAttributeString("nlo:anglerGuid", ar.AnglerGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Angler/" + ar.AnglerGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
            // StammPostIt
            xw.WriteComment("StammPostIt - seine Nachrichten");
            xw.WriteComment("===============================");
            foreach (StammPostItDataSet.StammPostItRow  pr in MyPostIt)
            {
                xw.WriteStartElement("nlo:stammPostIt");
                xw.WriteStartElement("nlo:PostIt"); // Predicate
                xw.WriteAttributeString("rdf:about", "PostIt/?" + pr.PostItGuid); // Object
//				xw.WriteAttributeString("nlo:StammZust", pr.StammZust.ToString() );
                xw.WriteAttributeString("nlo:postItGuid", pr.PostItGuid.ToString());
//				xw.WriteAttributeString("nlo:PostItZust", pr.IsPostItZustNull() ? "" : pr.PostItZust.ToString());
                xw.WriteAttributeString("nlo:flowKook", pr.bezahlt.ToString().Replace(",", "."));
                xw.WriteAttributeString("nlo:frist", pr.IsFristNull() ? "" : pr.Frist.ToString("s"));
//				xw.WriteAttributeString("nlo:StammClosed", pr.IsclosedNull() ? "" : pr.closed.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/PostIt/" + pr.PostItGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
            // StammTopLab
            xw.WriteComment("StammTopLab - seine Antworten");
            xw.WriteComment("=============================");
            foreach (StammTopLabDataSet.StammTopLabRow str in MyTopLab)
            {
                xw.WriteStartElement("nlo:stammTopLab");
                xw.WriteStartElement("nlo:TopLab");
                xw.WriteAttributeString("rdf:about", "TopLab/?" + str.TopLabGuid);
                xw.WriteAttributeString("nlo:topLabGuid", str.TopLabGuid.ToString());
                xw.WriteAttributeString("nlo:toll", str.IsDurchTollNull() ? "" : str.DurchToll.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/TopLab/" + str.TopLabGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
            xw.WriteEndElement();

            xw.WriteEndElement(); // ~</rdf:RDF>

            xw.Flush();
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string strOutput = sr.ReadToEnd();

            return strOutput;
        }

        #endregion
    }
}