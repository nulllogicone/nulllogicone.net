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
using OliEngine.OliDataAccess.Functions;
using OliEngine.OliDataAccess.Views;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     PostIt.
    /// </summary>
    public class PostIt
    {
        // Member
        // ------

        private OliDataAccess.PostIt postIt;
        private Code code;
        private readonly Stamm stamm;
        private Wurzeln _w;

        private PostItCodeDataSet.PostItCodeDataTable postItCode;
        private PostItStammDataSet.PostItStammDataTable postItStamm;
        private PostItAnglerDataSet.PostItAnglerDataTable postItAngler;
        private PostItTopLabDataSet.PostItTopLabDataTable postItTopLab;
        private PostItKontoDataSet.PostItKontoDataTable postItKonto;

        /// <summary>
        ///     Initializes a new instance of the <see cref = "PostIt" /> class.
        /// </summary>
        /// <param name = "pguid">The pguid.</param>
        public PostIt(Guid pguid)
        {
            postIt = new OliDataAccess.PostIt(pguid);
            
            Stamm s = new Stamm(UrheberStamm.StammRow.StammGuid);
            stamm = s;
        }

        public PostIt(Stamm stamm, Guid pguid)
        {
            this.stamm = stamm;

            // TODO: aus diesen drei zeilen eine sp und einfachen
            // Command Aufruf machen
            postIt = new OliDataAccess.PostIt(pguid);

            postIt.HitPostIt();
        }

        // Konstruktor (EingeloggterStamm)
        // erstellt einen neuen Datensatz in Tabelle PostIt.
        //
        // In der Update Methode wird geprüft, ob es ein neues
        // PostIt ist und dann mit dem Stamm über einen neuen Eintrag 
        // in die Wurzeltabelle verknüpft und ein leerer Code erstellt
        public PostIt(EingeloggterStamm eingeloggterStamm)
        {
            stamm = eingeloggterStamm;
            // neue DataAccess PostItRow holen
            postIt = new OliDataAccess.PostIt();
            PostItDataSet.PostItRow pr = postIt.PostIt.NewPostItRow();

            // Vorgabe Werte setzen
            pr.PostIt = "";
            pr.PostItGuid = Guid.NewGuid();
            pr.KooK = OliCommon.DefaultFlowKooK;
            pr.Datum = DateTime.Now;
            pr.Hits = 0;
            pr.Typ = "txt";

            // Reihe hinzufügen
            postIt.PostIt.AddPostItRow(pr);
        }

        // Eigenschaften
        // -------------

        // PostItRow
        public PostItDataSet.PostItRow PostItRow
        {
            get { return (postIt.PostItRow); }
        }

        /// <summary>
        /// </summary>
        private Wurzeln w
        {
            get
            {
                if (_w == null)
                {
                    _w = new Wurzeln(stamm.StammRow.StammGuid, PostItRow.PostItGuid);
                }
                return _w;
            }
            set { _w = value; }
        }

        // StammZahlt
        public decimal StammZahlt
        {
            get
            {
                //				Wurzeln w = new Wurzeln(this.stamm.StammRow.StammGuid,this.PostItRow.PostItGuid);
                if (w.WurzelnRow != null)
                {
                    return (w.WurzelnRow.bezahlt);
                }
                else
                {
                    return 0;
                }
            }
        }

        // StammFrist
        public DateTime StammFrist
        {
            get
            {
                //				Wurzeln w = new Wurzeln(this.stamm.StammRow.StammGuid, this.PostItRow.PostItGuid);
                try
                {
                    return (w.WurzelnRow.Frist);
                }
                catch
                {
                    return (DateTime.MinValue);
                }
            }
            set
            {
                //				Wurzeln w = new Wurzeln(this.stamm.StammRow.StammGuid, this.PostItRow.PostItGuid);
                w.WurzelnRow.Frist = value;
                w.UpdateWurzeln();
            }
        }

        // StammClosed
        public bool StammClosed
        {
            get
            {
                //				Wurzeln w = new Wurzeln(this.stamm.StammRow.StammGuid,this.PostItRow.PostItGuid);
                try
                {
                    return (w.WurzelnRow.closed);
                }
                catch
                {
                    return (false);
                }
            }
        }

        // StammZust
        public int StammZust
        {
            get
            {
                //				Wurzeln w = new Wurzeln(this.stamm.StammRow.StammGuid,this.PostItRow.PostItGuid);
                return (w.WurzelnRow.StammZust);
            }
        }

        // Code
        public Code Code
        {
            get { return (code); }
            set { code = value; }
        }

        // MyCode
        public PostItCodeDataSet.PostItCodeDataTable MyCode
        {
            get
            {
                if (postItCode == null)
                {
                    postItCode = new PostItCode(postIt.PostItRow).PostItCode;
                }
                return (postItCode);
            }
            set { postItCode = value; }
        }

        /// <summary>
        ///     Gets or sets my stamm.
        /// </summary>
        /// <value>My stamm.</value>
        public PostItStammDataSet.PostItStammDataTable MyStamm
        {
            get
            {
                if (postItStamm == null)
                {
                    postItStamm = new PostItStamm(postIt.PostItRow).PostItStamm;
                }
                return (postItStamm);
            }
            set { postItStamm = value; }
        }

        // MyEmpfaenger
        public PostItAnglerDataSet.PostItAnglerDataTable MyEmpfaenger
        {
            get
            {
                if (postItAngler == null)
                {
                    postItAngler = new PostItAngler(postIt.PostItRow).PostItAngler;
                }
                return (postItAngler);
            }
        }

        // MyFeedback
        public PostItTopLabDataSet.PostItTopLabDataTable MyTopLab
        {
            get
            {
                if (postItTopLab == null)
                {
                    postItTopLab = new PostItTopLab(postIt.PostItRow).PostItTopLab;
                }
                return (postItTopLab);
            }
            set { postItTopLab = value; }
        }

        // MyKonto
        public PostItKontoDataSet.PostItKontoDataTable MyKonto
        {
            get
            {
                if (postItKonto == null)
                {
                    postItKonto = new PostItKonto(postIt.PostItRow.PostItGuid).PostItKonto;
                }
                return (postItKonto);
            }
            set { postItKonto = value; }
        }

        // MyStammHtmlList
        // TODO: Das geht doch inzwischen wirklich einfacher. Bitte mit LINQ einen Dreizeiler machen! 
        // Dafür ist die Dokumentation allerdings etwas dürftig (von 2004) also kein Unit-Test!
        // Aber es soll *irgendwie* eine Liste der Urheber einer Nachricht als Html erstellt werden.
        public string MyStammHtmlList
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (PostItStammDataSet.PostItStammRow psr in MyStamm)
                {
                    string link = "?sguid=" + psr.StammGuid;

                    // aktueller Stamm? => .current
                    string cssClass = stamm.StammRow.StammGuid == psr.StammGuid ? "current " : "";

                    // StammZustand == 1 => .urheber
                    if (psr.StammZust == 1)
                    {
                        cssClass += " urheber";
                    }

                    // UrheberLinks aneinander setzen
                    sb.AppendFormat("<a class='{0}' href='{1}'> {2} </a> ", cssClass, link, DbDirect.GiveStamm(psr.StammGuid));
                    sb.AppendLine();
                }


                return sb.ToString();
            }
        }

        /// <summary>
        ///     wenn der aktuelle Stamm ein Urheber dieser Nachricht ist (kann sich
        ///     auch nachträglich angewurzelt haben)
        /// </summary>
        public bool BinIchMeinPostIt
        {
            get
            {
                bool bia = false;
                Guid sguid = stamm.StammRow.StammGuid;

                foreach (DataRow dr in MyStamm)
                {
                    PostItStammDataSet.PostItStammRow psr = (PostItStammDataSet.PostItStammRow)dr;
                    if (psr.StammGuid == sguid)
                    {
                        bia = true;
                        break;
                    }
                }
                return (bia);
            }
        }

        /// <summary>
        ///     Der Ur-Urheber dieser Nachricht (StammZust=1)
        /// </summary>
        public OliDataAccess.Stamm UrheberStamm
        {
            get
            {
                try
                {
                    PostItStamm ps = new PostItStamm(PostItRow);
                    DataRow[] dr = ps.PostItStamm.Select("StammZust=1");
                    if (dr[0] != null)
                    {
                        PostItStammDataSet.PostItStammRow psr = (PostItStammDataSet.PostItStammRow)dr[0];
                        Guid sguid = psr.StammGuid;
                        return (new OliDataAccess.Stamm(sguid));
                    }
                    else
                    {
                        return (null);
                    }
                }
                catch
                {
                    return (null);
                }
            }
        }

        /// <summary>
        ///     wenn dieser Urheber noch eine Frist in der Zukunft hat
        /// </summary>
        public bool IsOpen
        {
            get
            {
                bool io = false;

                foreach (PostItStammDataSet.PostItStammRow psr in MyStamm)
                {
                    if (!psr.IsFristNull() && psr.Frist > DateTime.Now)
                    {
                        io = true;
                        break;
                    }
                }
                return io;
            }
        }

        // Methoden
        // --------

        // ShowCode
        public void ShowCode(Guid cguid)
        {
            code = new Code(cguid);
        }

        /// <summary>
        ///     den eingeloggten Stamm an diese Nachricht anwurzeln
        ///     (neuer Eintrag in der wurzel-Tabelle zwischen Stamm und PostIt
        ///     mit StammZust=2.
        ///     <strong>Außerdem wird 1 KooK bezahlt</strong> und die Frist auf Standard-Offset
        ///     gesetzt
        /// </summary>
        /// <param name = "eingeloggterStamm"></param>
        public void Anwurzeln(EingeloggterStamm eingeloggterStamm)
        {
            WurzelnDataSet.WurzelnRow wr = w.Wurzeln.NewWurzelnRow();

            wr.StammGuid = eingeloggterStamm.StammRow.StammGuid;
            wr.PostItGuid = PostItRow.PostItGuid;
            wr.StammZust = 2;
            wr.bezahlt = 0;
            wr.Frist = DateTime.Now.AddDays(OliCommon.FristOffset);
            wr.gemailt = false;
            wr.closed = false;

            w.Wurzeln.AddWurzelnRow(wr);
            w.UpdateWurzeln();

            // anwurzeln bezahlen!
            Zahlmeister zm = new Zahlmeister(stamm.StammRow, postIt.PostItRow);
            zm.Bezahlen(1, DateTime.Now.AddDays(OliCommon.FristOffset), "Anwurzeln");

            stamm.OliUser.Nachricht = "Sie wurden angewurzelt und 1 KooK übertragen";
        }

        /// <summary>
        ///     Die verbindung dieser Nachricht zum aktuellen Stamm wird abgebrochen = gelöscht!
        ///     Es werden keine KooK (zurück) übertragen sondern einfach die n:m Zeile in der
        ///     Wurzel Tabelle gelöscht
        /// </summary>
        /// <param name = "eingeloggterStamm"></param>
        public void Abwurzeln(EingeloggterStamm eingeloggterStamm)
        {
            if (UrheberStamm.StammRow.StammGuid != eingeloggterStamm.StammRow.StammGuid)
            {
                if (StammZahlt >= 0)
                {
                    Guid sguid = eingeloggterStamm.StammRow.StammGuid;
                    Guid pguid = PostItRow.PostItGuid;

                    w.WurzelnRow.Delete();
                    w.UpdateWurzeln();

                    stamm.OliUser.Nachricht = "Sie wurden erfolgreich wieder abgewurzelt";
                }
                else
                {
                    stamm.OliUser.Nachricht =
                        "Wenn sie einen negativen Betrag bezahlt haben, können Sie sich nicht abwurzeln";
                }
            }
            else
            {
                throw (new Exception("Urheber versucht sein PostIt abzuwurzeln"));
            }
        }

        // Frankieren
        public void Frankieren(EingeloggterStamm eStamm, decimal betrag, DateTime frist)
        {
            if (eStamm.StammRow.StammGuid == stamm.StammRow.StammGuid)
            {
                Zahlmeister zm = new Zahlmeister(stamm.StammRow, PostItRow);
                zm.Bezahlen(betrag, frist, "Frankieren");
            }
        }

        /// <summary>
        ///     neuer, leerer Code
        /// </summary>
        /// <returns></returns>
        public Code NewCode()
        {
            // neuen Code erstellen lassen (mit update)
            Code = new Code(stamm, this);

            // meinen neuen Code zeigen
            ShowCode(Code.CodeRow.CodeGuid);

            // und zurückgeben
            return Code;
        }

        /// <summary>
        ///     neuer Code mit den automatiscen ShortCuts
        /// </summary>
        /// <param name = "mitShortCuts"></param>
        /// <returns></returns>
        public Code NewCode(bool mitShortCuts)
        {
            // neuen Code erstellen lassen (mit update)
            Code = new Code(stamm, this);

            ShortCutsDataSet.ShortCutsDataTable scdt = stamm.MyShortCuts;
            foreach (ShortCutsDataSet.ShortCutsRow scr in scdt.Rows)
            {
                if (scr.auto)
                {
                    stamm.CopyShortCutsToCode(scr.ShortCutsGuid, Code.CodeRow.CodeGuid);
                }
            }

            // meinen neuen Code zeigen
            ShowCode(Code.CodeRow.CodeGuid);

            // und zurückgeben
            return Code;
        }

        // DeleteCode
        public bool DeleteCode(Guid codeGuid)
        {
            OliDataAccess.Code c = new OliDataAccess.Code(codeGuid);
            c.CodeRow.Delete();
            c.UpdateCode();

            return (true);
        }

        /// <summary>
        ///     Speichert das PostIt in der Datenbank.
        ///     Wenn es sich um ein neues PostIt handelt, wird es über einen neuen
        ///     Eintrag in der Wurzeltabelle mit dem Stamm verknüpft und ein
        ///     neuer Code wird erstellt.
        /// </summary>
        /// <returns></returns>
        public int UpdatePostIt()
        {
            // eine neue PostItRow wird mit einem neuen Eintrag in der Wurzel-Tabelle
            // an den Stamm angewurzelt und erhält einen neuen Code
            if (PostItRow.RowState == DataRowState.Added)
            {
                Guid pguid = PostItRow.PostItGuid;

                // update
                int result = postIt.UpdatePostIt();

                //				// Datengrundlage erneuern
                //				this.postIt = new OliDataAccess.PostIt(pguid);

                // Wurzel Reihe holen
                Wurzeln w = new Wurzeln();
                WurzelnDataSet.WurzelnRow wr = w.Wurzeln.NewWurzelnRow();

                // Wurzel - Feldwerte setzen
                wr.PostItGuid = pguid;
                wr.StammGuid = stamm.StammRow.StammGuid;
                wr.StammZust = 1;
                wr.bezahlt = 0;
                wr.Frist = DateTime.Now.AddDays(OliCommon.FristOffset);
                wr.gemailt = false;
                wr.closed = false;

                // Wurzel hinzufügen und update
                w.Wurzeln.AddWurzelnRow(wr);
                w.UpdateWurzeln();

                // Add default FlowKook to PostItKonto
                var pk = new PostItKonto(pguid);
                pk.AddPosten(OliCommon.DefaultFlowKooK, "Startwert");

                // Code Reihe holen
                OliDataAccess.Code c = new OliDataAccess.Code();
                CodeDataSet.CodeRow cr = c.Code.NewCodeRow();

                // Code Werte setzen
                cr.PostItGuid = pguid;
                cr.StammGuid = stamm.StammRow.StammGuid;
                Stamm myS = new Stamm(stamm.StammRow.StammGuid);
                cr.Kommentar = "Markierung von " + myS.StammRow.Stamm;
                Guid cguid = Guid.NewGuid();
                cr.CodeGuid = cguid;
                cr.gescannt = false;

                // Code hinzufügen und update
                c.Code.AddCodeRow(cr);
                c.UpdateCode();

                // ShortCuts einfügen
                ShortCutsDataSet.ShortCutsDataTable scdt = stamm.MyShortCuts;
                foreach (ShortCutsDataSet.ShortCutsRow scr in scdt.Rows)
                {
                    if (scr.auto)
                    {
                        stamm.CopyShortCutsToCode(scr.ShortCutsGuid, cr.CodeGuid);
                    }
                }

                // Neue Nachricht wird von Stamm bezahlt Kooks und setzt erste Frist
                Zahlmeister zm = new Zahlmeister(stamm.StammRow, postIt.PostItRow);
                zm.Bezahlen(OliCommon.DefaultTransferKooK, DateTime.Now.AddDays(OliCommon.FristOffset), "Neue Nachricht");

                // Datengrundlage erneuern
                postIt = new OliDataAccess.PostIt(pguid);
                ShowCode(cguid);

                return (result);
            }
            else if (BinIchMeinPostIt && stamm.BinIchEingeloggt)
            {
                return (postIt.UpdatePostIt());
            }
            else
            {
                throw new Exception("ein nicht eingeloggter versucht PostIt zu ändern");
            }
        }

        /// <summary>
        ///     gibt die Beschreibungen zu dieser Nachricht als RDF aus
        /// </summary>
        public string MakePostItRDF()
        {
            PostItDataSet.PostItRow pr;
            pr = PostItRow;
            Guid urheberGuid = stamm.StammRow.StammGuid;

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

            // -- PostIt --
            xw.WriteStartElement("nlo:PostIt"); // Description
            xw.WriteAttributeString("rdf:about", "PostIt/?" + pr["PostItGuid"]); // Subject

            // Dublin Core
            xw.WriteComment("Dublin Core");
            xw.WriteComment("===========");
            xw.WriteElementString("dc:date", DateTime.Now.ToString("s"));
            xw.WriteStartElement("dc:publisher");
            xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net");
            xw.WriteEndElement();

            // -- PostIt Felder
            xw.WriteComment("PostIt Felder");
            xw.WriteComment("=============");
            xw.WriteElementString("nlo:postItGuid", pr.PostItGuid.ToString());
            xw.WriteElementString("nlo:titel", pr.IsTitelNull() ? "" : pr.Titel);
            xw.WriteElementString("nlo:text", pr.PostIt);
            xw.WriteElementString("nlo:flowKook", pr.KooK.ToString().Replace(",", "."));
            xw.WriteElementString("nlo:datum", pr.Datum.ToString("s"));
            if (!pr.IsDateiNull())
            {
                xw.WriteStartElement("nlo:datei");
                xw.WriteAttributeString("rdf:resource", pr.IsDateiNull() ? "" : OliUtil.MakeImageSrc(pr.Datei));
                xw.WriteEndElement();
            }
            if (!pr.IsURLNull())
            {
                xw.WriteStartElement("nlo:link");
                xw.WriteAttributeString("rdf:resource", pr.IsURLNull() ? "" : pr.URL);
                xw.WriteEndElement();
            }
            xw.WriteElementString("nlo:typ", pr.Typ);

            // -- PostItStamm
            xw.WriteComment("PostItStamm (Urheber=1, angewurzelt=2)");
            xw.WriteComment("======================================");
            foreach (PostItStammDataSet.PostItStammRow psr in MyStamm)
            {
                xw.WriteStartElement("nlo:postItStamm"); // Predicate
                xw.WriteStartElement("nlo:Stamm");
                xw.WriteAttributeString("rdf:about", "Stamm/?" + psr.StammGuid); // Object
                xw.WriteAttributeString("nlo:stammGuid", psr.StammGuid.ToString());
                xw.WriteAttributeString("nlo:name", DbDirect.GiveStamm(psr.StammGuid));
                xw.WriteElementString("nlo:zustand", psr.StammZust.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Stamm/" + psr.StammGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            // -- PostItCode
            xw.WriteComment("PostItCode - die Markierungen");
            xw.WriteComment("=============================");
            foreach (PostItCodeDataSet.PostItCodeRow pcr in MyCode)
            {
                xw.WriteStartElement("nlo:postItCode"); // Predicate
                xw.WriteStartElement("nlo:Code");
                xw.WriteAttributeString("rdf:about", "Code/?" + pcr.CodeGuid); // Object
                xw.WriteAttributeString("nlo:codeGuid", pcr.CodeGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Code/" + pcr.CodeGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            // -- PostItAngler
            xw.WriteComment("PostItAngler - die Empfänger");
            xw.WriteComment("============================");
            foreach (PostItAnglerDataSet.PostItAnglerRow par in MyEmpfaenger)
            {
                xw.WriteStartElement("nlo:postItAngler"); // Predicate
                xw.WriteStartElement("nlo:Angler");
                xw.WriteAttributeString("rdf:about", "Angler/?" + par.AnglerGuid); // Object
                xw.WriteAttributeString("nlo:anglerGuid", par.AnglerGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/Angler/" + par.AnglerGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            // -- PostItTopLab
            xw.WriteComment("PostItTopLab - die Antworten");
            xw.WriteComment("============================");
            foreach (PostItTopLabDataSet.PostItTopLabRow ptr in MyTopLab)
            {
                xw.WriteStartElement("nlo:postItTopLab"); // Predicate
                xw.WriteStartElement("nlo:TopLab");
                xw.WriteAttributeString("rdf:about", "TopLab/?" + ptr.TopLabGuid); // Object
                xw.WriteAttributeString("nlo:topLabGuid", ptr.TopLabGuid.ToString());
                xw.WriteStartElement("nlo:resource");
                xw.WriteAttributeString("rdf:resource", "http://nulllogicone.net/TopLab/" + ptr.TopLabGuid + ".rdf");
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            xw.WriteEndElement(); // rdf:Description
            xw.WriteEndElement(); // ~</rdf:RDF>

            xw.Flush();
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string strOutput = sr.ReadToEnd();

            return strOutput;
        }
    }
}