// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Data;
using System.Text;
using System.Web;
using OliEngine.DataSetTypes.Views;
using OliEngine.OliDataAccess;
using OliEngine.OliDataAccess.Views;

namespace OliEngine.OliMiddleTier.OLIs
{
    /// <summary>
    ///     OliUser.
    ///     Dieses Objekt wird zu Beginn einer Arbeitssitzung erstellt und
    ///     beh�lt den Zustand des eingeloggten und angezeigtem Stammes,
    ///     den gezeigten PostIt, Angler oder Detailtabellen (Code, ShortCuts)
    ///     Es stellt einen created-Zeitstempel und Nachrichten an den OliUser
    ///     zur Verf�gung sowie die gew�hlte Button-Beschriftung.
    ///     In der Webanwendung wird es vom SessionManager gespeichert.
    /// </summary>
    public class OliUser
    {
        // Member
        // ------

        private Stamm stamm;
        private EingeloggterStamm eingeloggterStamm;

        private string nachricht = DateTime.Now.ToShortTimeString() + " ";
        // und dann von �berall zusammengesetzt - danach gel�scht

        private readonly DateTime created = DateTime.Now;
        private Q qList;

        // Konstruktor
        // ----------

        // Eigenschaften
        // -------------

        // Created
        public DateTime Created
        {
            get { return (created); }
        }

        // Stamm
        public Stamm Stamm
        {
            get { return stamm; }
            set { stamm = value; }
        }

        // EingeloggterStamm
        public EingeloggterStamm EingeloggterStamm
        {
            get { return eingeloggterStamm; }
            set { eingeloggterStamm = value; }
        }

        // Eingeloggt
        public bool Eingeloggt
        {
            get { return (eingeloggterStamm != null); }
        }

        /// <summary>
        ///     Mitteilungen an den aktuellen 'Arbeitssitzungs-User'.
        /// 
        ///     Durch die Zuweisung eines Strings an user.Nachricht = "bla bla bla"
        ///     wird er an einen bestehenden String hinten angeh�ngt und mit &ltbr> abgeschlossen.
        /// 
        ///     Nachdem die Nachricht auf der Anwendungsoberfl�che angezeigt wurde,
        ///     wird sie auf "" gesetzt.
        /// 
        ///     TODO: Die Zuweisung sollte mit dem �berladenen Operator += erfolgen
        /// </summary>
        public string Nachricht
        {
            get { return (nachricht); }
            // wenn Zeichen dann als Html Aufz�hlung aneinanderh�ngen
            // sonst leer
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    nachricht += value + "<br />";
                }
                else
                {
                    nachricht = "";
                }
            }
        }

        // Freund
        public Stamm Freund { get; set; }

        // BookMark
        public string BookMark
        {
            get
            {
                string bm = "?";
                if (stamm != null)
                {
                    bm += "sguid=" + stamm.StammRow.StammGuid + "&";

                    if (stamm.Angler != null)
                    {
                        bm += "aguid=" + stamm.Angler.AnglerRow.AnglerGuid + "&";
                    }

                    if (stamm.PostIt != null)
                    {
                        bm += "pguid=" + stamm.PostIt.PostItRow.PostItGuid + "&";
                    }

                    if (stamm.TopLab != null)
                    {
                        bm += "tguid=" + stamm.TopLab.TopLabRow.TopLabGuid + "&";
                    }

                    // das letzte & entfernen
                    bm = bm.Substring(0, bm.Length - 1);
                }
                return (bm);
            }
        }

        // QLIst
        public Q QList
        {
            get
            {
                if (qList == null)
                {
                    qList = new Q();
                }
                return (qList);
            }
        }

        // IpAdress
        public string IpAdress
        {
            get
            {
                string ip = "";
                HttpContext ctx = HttpContext.Current;
                ip = ctx.Request.UserHostAddress;
                return ip;
            }
        }

        // Methoden
        // --------

        ///<summary>
        ///    ShowStamm(sguid)
        ///    Diese Methode gibt ein Mittelschicht - Stamm - Objekt zur�ck.
        ///    Daf�r muss die Guid bekannt sein (entweder aus Email-Links oder
        ///    aus der Oberfl�che (Stamm suchen, Journal)
        ///    Diese Aufrufe werden in die Tabelle tblTuerLog protokolliert *
        ///    <param name = "sguid"></param>
        ///    <returns>Stamm</returns>
        ///</summary>
        public Stamm ShowStamm(Guid sguid)
        {
            // Guid merken
            Guid pguid = Guid.Empty;
            Guid tguid = Guid.Empty;

            try
            {
                pguid = Stamm.PostIt.PostItRow.PostItGuid;
            }
            catch
            {
                // ignored
            }

            try
            {
                tguid = Stamm.TopLab.TopLabRow.TopLabGuid;
            }
            catch
            {
                // ignored
            }

            // StammGuid zeigen
            stamm = new Stamm(this, sguid);

            // vorige guid wieder herstellen
            if (pguid != Guid.Empty)
            {
                stamm.ShowPostIt(pguid);
            }

            if (tguid != Guid.Empty)
            {
                stamm.ShowTopLab(tguid);
            }

            return (Stamm);
        }

        /// <summary>
        ///     ShowStamm (stamm, pwd)
        /// 
        ///     Mit dieser �berladenen Methode wird ein Stamm eingeloggt
        ///     (wenn das Kennwort passt)
        /// </summary>
        /// <param name = "stamm"></param>
        /// <param name = "pwd">Das Kennwort f�r diesen Stamm</param>
        /// <param name = "ip"></param>
        /// <returns></returns>
        public bool ShowStamm(string stamm, string pwd)
        {
            Guid sguid = Guid.Empty;

            StammList slGenau = new StammList(stamm, false);
            if (slGenau.Stamm.Count == 1)
            {
                sguid = slGenau.StammRow.StammGuid;
            }

            // Einloggversuch
            try
            {
                // DataAccess Stamm bekommt man mit Name und Passwort
                var direktStamm = new OliDataAccess.Stamm(stamm, pwd);
                sguid = direktStamm.StammRow.StammGuid;

                // Eingeloggter Stamm wird mit Guid und Passwort erstellt
                EingeloggterStamm = new EingeloggterStamm(this, direktStamm.StammRow.StammGuid, pwd);

                // user.Nachricht
                Nachricht = "eingeloggt";
                return (true);
            }
            catch 
            {
                // user.Nachricht
                Nachricht = "nicht eingeloggt";
                return (false);
            }
            finally
            {
                if (sguid != Guid.Empty)
                {
                    ShowStamm(sguid);
                }
            }
        }

        // ShowPostIt ()
        public void ShowPostIt(Guid pguid)
        {
            // wenn kein Stamm bisher angezeigt wird 
            if (Stamm == null)
            {
                OliDataAccess.PostIt p = new OliDataAccess.PostIt(pguid);
                PostItStamm ps = new PostItStamm(p.PostItRow);

                // wird der Urheber der Nachricht gezeigt
                DataRow[] dr = ps.PostItStamm.Select("StammZust=1");
                if (dr.Length > 0)
                {
                    PostItStammDataSet.PostItStammRow psr = (PostItStammDataSet.PostItStammRow) dr[0];
                    Guid sguid = psr.StammGuid;
                    ShowStamm(sguid);
                }
            }

            // der Stamm zeigt das gew�nschte PostIt
            Stamm.ShowPostIt(pguid);

            // wenn noch eine alte Antwort rumh�ngt - entfernen
            if (Stamm.TopLab != null)
            {
                if (Stamm.TopLab.TopLabRow.PostItGuid != pguid)
                {
                    Stamm.TopLab = null;
                }
            }
        }

        // ShowAngler (mit Urheber)
        public void ShowAngler(Guid aguid)
        {
            OliDataAccess.Angler a = new OliDataAccess.Angler(aguid);

//			if(this.Stamm != null)
//			{
//				stamm.ShowAngler(aguid);
//			}
//			else
//			{
            ShowStamm(a.AnglerRow.StammGuid);
            Stamm.ShowAngler(aguid);
//			}
        }

        // ShowTopLab (mit PostIt)
        public void ShowTopLab(Guid tguid)
        {
            // soll der Urheber des TopLab angezeigt werden
            OliDataAccess.TopLab t = new OliDataAccess.TopLab(tguid);

            // wenn kein Stamm gezeigt wird
            if (Stamm == null)
            {
                ShowStamm(t.TopLabRow.StammGuid);
            }
            Stamm.ShowPostIt(t.TopLabRow.PostItGuid);
            Stamm.ShowTopLab(tguid);
        }

        // NewStamm
        public Stamm NewStamm()
        {
            Stamm stamm = new Stamm(this);
            return stamm;
        }

        // Ausloggen
        public void Ausloggen()
        {
            eingeloggterStamm = null;
        }

        // ToString
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("OliUser (" + Created + "):");

            sb.Append("<br>eingeloggt: ");
            if (EingeloggterStamm != null)
            {
                sb.Append("<font color=red><b>" + eingeloggterStamm.StammRow.Stamm + "</b></font>");
            }

            if (stamm != null)
            {
//				sb.Append("Stamm: " + this.Stamm.StammRow.Stamm);
                sb.Append(Stamm.ToString());
            }

            return sb.ToString();
        }
    }
}
