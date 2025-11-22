// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using Ajax;
using OliEngine;
using OliEngine.OliMiddleTier.OLIs;
using OliEngine.OliMiddleTier.OLIx;
using OliWeb.Klassen;

namespace OliWeb.Controls.AjaxWortraum
{
    ///<summary>
    ///    Zusammenfassung f�r AjaxWortraumControl.
    ///</summary>
    [ComVisible(false)]
    public partial class AjaxWortraumControl : UserControl
    {
        // Member
        // ------
        private const int INDENT = 20;
        private readonly string imgroot = OliCommon.RootPath;

        private readonly string Nimg = "<img src=\"" + OliCommon.RootPath +
                                       "images/icons/Wortraum/N16.gif\" style=\"border:0px;\" />&nbsp;";

        private readonly string Kimg = "<img src=\"" + OliCommon.RootPath +
                                       "images/icons/Wortraum/K16.gif\" style=\"border:0px;\" />&nbsp;";

        private readonly string Bimg = "<img src=\"" + OliCommon.RootPath +
                                       "images/icons/Wortraum/B16.gif\" style=\"border:0px;\" />&nbsp;";

        private readonly string Zimg = "<img src=\"" + OliCommon.RootPath +
                                       "images/icons/Wortraum/Z16.gif\" style=\"border:0px;\" />&nbsp;";

        private readonly string wNimg = "<img src=\"" + OliCommon.RootPath +
                                        "images/icons/Wortraum/wN16.gif\" style=\"vertical-align:middle;border:0px;\" />&nbsp;";

        private readonly string wBimg = "<img src=\"" + OliCommon.RootPath +
                                        "images/icons/Wortraum/wB16.gif\" style=\"vertical-align:middle;border:0px;\" />&nbsp;";

        //		string CStyle = "background-image:url(" + OliEngine.OliCommon.RootPath + "images/icons/Symbole/Stamm.gif); background-repeat:no-repeat;background-position:center center; ";
        //		string AStyle = "background-image:url(" + OliEngine.OliCommon.RootPath + "images/icons/Symbole/Angler.gif); background-repeat:no-repeat;background-position:center center; ";

        // Methoden
        // --------

        /// <summary>
        ///     beim Laden der Seite wird das Control f�r die verwendung mit Ajax angemeldet
        ///     und das Lebel mit dem Root-Wortraum gef�llt
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof (AjaxWortraumControl));
            if (!Markierbar)
            {
                WortraumPanel.BackColor = Color.WhiteSmoke;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WortraumLabel.Text = makeNetz(Netz.GetRoot().NetzRow.NetzGuid.ToString(), 0, false);
            if (Markierer != null)
            {
                if (Markierer is Code)
                {
                    WortraumPanel.Attributes.Remove("style");
                    //					WortraumPanel.Attributes.Add("style", CStyle);
                }
                if (Markierer is Angler)
                {
                    WortraumPanel.Attributes.Remove("style");
                    //					WortraumPanel.Attributes.Add("style", AStyle);
                }
            }
        }

        /// <summary>
        ///     man kann den CodeMarkierer bzw. AnglerMarkierer angeben, der auf den Daten arbeitet
        /// </summary>
        public IMarkierer Markierer
        {
            get { return (IMarkierer) HttpContext.Current.Session["markierer"]; }
            set { HttpContext.Current.Session["markierer"] = value; }
        }

        /// <summary>
        ///     soll der CodeMarkierer bzw. AnglerMarkierer ver�ndert werden k�nnen
        /// </summary>
        public bool Markierbar
        {
            get
            {
                if (Markierer == null)
                {
                    return false;
                }
                if (HttpContext.Current.Session["markierbar"] != null)
                {
                    return bool.Parse(HttpContext.Current.Session["markierbar"].ToString());
                }
                return false;
            }
            set { HttpContext.Current.Session["markierbar"] = value; }
        }

        /// <summary>
        ///     man kann einstellen ob die Vorgabewerte f�r die bunten Punkte angezeigt werden sollen
        /// </summary>
        public bool ShowVgb
        {
            get
            {
                if (HttpContext.Current.Session["showVgb"] == null)
                {
                    return false;
                }
                return bool.Parse(HttpContext.Current.Session["showVgb"].ToString());
            }
            set { HttpContext.Current.Session["showVgb"] = value; }
        }

        /// <summary>
        ///     man kann einstellen ob die Vorgabewerte f�r die bunten Punkte angezeigt werden sollen
        /// </summary>
        public bool Werbefrei
        {
            get
            {
                if (HttpContext.Current.Session["werbefrei"] == null)
                {
                    return false;
                }
                return bool.Parse(HttpContext.Current.Session["werbefrei"].ToString());
            }
            set { HttpContext.Current.Session["werbefrei"] = value; }
        }

        /// <summary>
        ///     Auswahlfeld f�r bunte Punkte eines Knoten
        /// </summary>
        /// <param name="og"> </param>
        /// <returns> </returns>
        private string knotenPunktSelect(string og)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                "<select style=\"z-index:3;position:absolute;\" size='5' name='PunktSelect' onclick=\"javascript:KnotenUpdate(this);\">");
            sb.Append("<option " + (og == "33" ? "selected" : "") + " value='33'>3-3</option>");
            sb.Append("<option " + (og == "32" ? "selected" : "") + " value='32'>3-2</option>");
            sb.Append("<option " + (og == "22" ? "selected" : "") + " value='22'>2-2</option>");
            sb.Append("<option " + (og == "20" ? "selected" : "") + " value='20'>2-0</option>");
            sb.Append("<option " + (og == "10" ? "selected" : "") + " value='10'>1-0</option>");
            sb.Append("</select>");
            return sb.ToString();
        }

        /// <summary>
        ///     Auswahlfeld f�r bunte Punkte bei einem Zweig
        /// </summary>
        /// <param name="og"> </param>
        /// <returns> </returns>
        private string zweigPunktSelect(string og)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                "<select style=\"z-index:3;position:absolute;\" size='5' name='PunktSelect' onclick=\"javascript:ZweigUpdate(this);\">");
            sb.Append("<option " + (og == "33" ? "selected" : "") + " value='33'>3-3</option>");
            sb.Append("<option " + (og == "32" ? "selected" : "") + " value='32'>3-2</option>");
            sb.Append("<option " + (og == "22" ? "selected" : "") + " value='22'>2-2</option>");
            sb.Append("<option " + (og == "20" ? "selected" : "") + " value='20'>2-0</option>");
            sb.Append("<option " + (og == "10" ? "selected" : "") + " value='10'>1-0</option>");
            sb.Append("</select>");
            return sb.ToString();
        }

        /// <summary>
        ///     erstellt den Html Code f�r ein Netz mit seinen Knoten
        /// </summary>
        /// <param name="nguidstr"> </param>
        /// <param name="ebene"> </param>
        /// <param name="onlymarked"> </param>
        /// <returns> </returns>
        private string makeNetz(string nguidstr, int ebene, bool onlymarked)
        {
            int anzK = 0;
            Guid nguid = new Guid(nguidstr);
            StringBuilder sb = new StringBuilder();
            Netz n = new Netz(nguid);
            Knoten kn = new Knoten(n.NetzRow);

            // Netz
            sb.Append("<div style=\"padding-left:" + ebene*INDENT + "px;\"");
            sb.Append("class=\"netz\">");
            // Werbe Bild
            if (!Werbefrei && !n.NetzRow.IsDateiNull() && n.NetzRow.Datei.Length > 3)
            {
                sb.Append("<img style=\"float:right; width:50px;\" src=\"" + OliUtil.MakeImageSrc(n.NetzRow.Datei) +
                          "\"/>");
            }

            // Tooltipp
            if (Common.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                sb.Append("<div title='" + (n.NetzRow.Isen_descriptionNull() ? "" : n.NetzRow.en_description) + "'>");
                sb.Append(Nimg);
                sb.Append(n.NetzRow.Isen_nameNull() ? n.NetzRow.Netz : n.NetzRow.en_name);
            }
            else
            {
                sb.Append("<div title='" + (n.NetzRow.IsBeschreibungNull() ? "" : n.NetzRow.Beschreibung) + "'>");
                sb.Append(Nimg);
                sb.Append(n.NetzRow.Netz);
            }

            sb.Append("</div>");
            sb.Append("</div>");

            // Knoten
            foreach (KnotenDataSet.KnotenRow kr in kn.Knoten)
            {
                string og = "";
                // Markierung
                if (Markierer != null)
                {
                    if (Markierer is Code)
                    {
                        Code c = (Code) Markierer;
                        og = c.IsInString(kr);
                    }
                    if (Markierer is Angler)
                    {
                        Angler a = (Angler) Markierer;
                        og = a.IsInString(kr);
                    }
                }
                // wenn nur markierte und ist markiert sonst alle
                if (onlymarked && og.Length > 0 || !onlymarked)
                {
                    sb.Append(makeKnoten(kr, ebene, true));
                    anzK++;

                    // Child-div (wenn markiert dann weiter f�llen)
                    if (og.Length > 0)
                    {
                        string mwk = MakeWeiterKnoten(kr.KnotenGuid.ToString(), true);
                        if (mwk.Length > 0)
                        {
                            sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:block;\"");
                            sb.Append("ID=\"" + kr.KnotenGuid + "\">");
                            sb.Append(mwk);
                        }
                        else
                        {
                            sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:none;\"");
                            sb.Append("ID=\"" + kr.KnotenGuid + "\">");
                        }
                    }
                    else
                    {
                        sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:none;\"");
                        sb.Append("ID=\"" + kr.KnotenGuid + "\">");
                    }
                    sb.Append("</div>");
                }
            }
            // wenn keine Knoten dann nix zur�ckgeben
            if (anzK == 0)
            {
                return "";
            }
            else
            {
                return sb.ToString();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="kr"> </param>
        /// <param name="ebene"> </param>
        /// <param name="onlymarked"> </param>
        /// <returns> </returns>
        private string makeKnoten(KnotenDataSet.KnotenRow kr, int ebene, bool onlymarked)
        {
            StringBuilder sb = new StringBuilder();
            string og = "";
            // Markierung
            if (Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    og = c.IsInString(kr);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    og = a.IsInString(kr);
                }
            }

            sb.Append("<div style=\"white-space:nowrap;padding-left:" + ebene*INDENT + "px;\" ");
            sb.Append("ID=\"K" + kr.KnotenGuid + "\" ");
            sb.Append(">");
            sb.Append("<a class=\"knoten\" href='javascript:ToggleKnoten(\"" + kr.KnotenGuid + "\",\"" + onlymarked +
                      "\");'");
            if (Common.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                sb.Append("title='" + (kr.Isen_descriptionNull() ? "" : kr.en_description) + "'>");
            }
            else
            {
                sb.Append("title='" + (kr.IsBeschreibungNull() ? "" : kr.Beschreibung) + "'>");
            }
            sb.Append(Kimg);
            if (Common.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                sb.Append((kr.en_name) + "&nbsp;");
            }
            else
            {
                sb.Append((kr.Knoten) + "&nbsp;");
            }
            // Vorgabe Werte f�r bunte Punkte
            if (ShowVgb)
            {
                sb.Append("<img style=\"vertical-align:middle;border:none;\" width=\"6px\" src=\"" + imgroot +
                          "images/punkte/OLIs/" + kr.VgbOLIs + ".gif\"/>&nbsp;");
                sb.Append("<img style=\"vertical-align:middle;border:none;\" width=\"6px\" src=\"" + imgroot +
                          "images/punkte/ILOs/" + kr.VgbGet + ".gif\"/>&nbsp;");
                sb.Append("<img style=\"vertical-align:middle;border:none;\" width=\"6px\" src=\"" + imgroot +
                          "images/punkte/ILOs/" + kr.VgbILOs + ".gif\"/>&nbsp;");
                sb.Append("<img style=\"vertical-align:middle;border:none;\" width=\"6px\" src=\"" + imgroot +
                          "images/punkte/OLIs/" + kr.VgbFit + ".gif\"/>&nbsp;");
            }
            // weiter Pfeile
            if (!kr.IsweiterBaumGuidNull()) sb.Append(wBimg);
            if (!kr.IsweiterNetzGuidNull()) sb.Append(wNimg);

            sb.Append("</a>");

            // Markierung wenn es nicht in einen Baum geht
            if (og.Length > 0 && kr.IsweiterBaumGuidNull())
            {
                sb.Append(Markierbar ? "<a href='javascript:Edit(\"Edit" + kr.KnotenGuid + "\");'>" : "");
                // OLIs Get
                if (Markierer is Code)
                {
                    sb.Append("<img style=\"border:none;vertical-align:middle;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/OLIs/" + og[0] + ".gif\"/>&nbsp;");
                    sb.Append("<img style=\"border:none;vertical-align:middle;\"width=\"10px\" src=\"" + imgroot +
                              "images/punkte/ILOs/" + og[1] + ".gif\"/>&nbsp;");
                }
                // ILOs fit
                if (Markierer is Angler)
                {
                    sb.Append("<img style=\"border:none;vertical-align:middle;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/ILOs/" + og[0] + ".gif\"/>&nbsp;");
                    sb.Append("<img style=\"border:none;vertical-align:middle;\"width=\"10px\" src=\"" + imgroot +
                              "images/punkte/OLIs/" + og[1] + ".gif\"/>&nbsp;");
                }
                sb.Append(Markierbar ? "</a>" : "");

                // Markierung Editieren und L�schen
                if (Markierbar)
                {
                    sb.Append("<div style=\"display:none;\" ID=\"Edit" + kr.KnotenGuid + "\">" + knotenPunktSelect(og) +
                              "</div>");
                    sb.Append("&nbsp;<a href='javascript:ClearKnoten(\"" + kr.KnotenGuid + "\")'>");
                    sb.Append("<img style=\"vertical-align:middle;border:0px;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/x.gif\"/>&nbsp;");
                    sb.Append("</a>");
                }
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        ///     erstellt den Html Code f�r einen Baum mit seinen Zweigen
        /// </summary>
        /// <param name="lastKnoten"> </param>
        /// <param name="bguidstr"> </param>
        /// <param name="ebene"> </param>
        /// <param name="onlymarked"> </param>
        /// <returns> </returns>
        private string makeBaum(KnotenDataSet.KnotenRow lastKnoten, string bguidstr, int ebene, bool onlymarked)
        {
            int anzZ = 0;
            Guid bguid = new Guid(bguidstr);
            StringBuilder sb = new StringBuilder();
            Baum b = new Baum(bguid);
            Zweig zw = new Zweig(b.BaumRow);

            // Baum
            sb.Append("<div style=\"padding-left:" + ebene*INDENT + "px;\"");
            sb.Append("class=\"baum\">");
            // Werbe Bild
            if (!Werbefrei && !b.BaumRow.IsDateiNull() && b.BaumRow.Datei.Length > 3)
            {
                sb.Append("<img style=\"float:right; width:50px;\" src=\"" + OliUtil.MakeImageSrc(b.BaumRow.Datei) +
                          "\"/>");
            }
            // Tooltipp
            if (Common.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                sb.Append("<div title='" + (b.BaumRow.Isen_descriptionNull() ? "" : b.BaumRow.en_description) + "'>");
                sb.Append(Bimg);
                sb.Append(b.BaumRow.Isen_nameNull() ? b.BaumRow.Baum : b.BaumRow.en_name);
            }
            else
            {
                sb.Append("<div title='" + (b.BaumRow.IsBeschreibungNull() ? "" : b.BaumRow.Beschreibung) + "'>");
                sb.Append(Bimg);
                sb.Append(b.BaumRow.Baum);
            }
            sb.Append("</div>");
            sb.Append("</div>");

            // Zweige
            foreach (ZweigDataSet.ZweigRow zr in zw.Zweig)
            {
                string og = "";
                // Markierung
                if (Markierer != null)
                {
                    if (Markierer is Code)
                    {
                        Code c = (Code) Markierer;
                        og = c.IsInString(lastKnoten, zr);
                    }
                    if (Markierer is Angler)
                    {
                        Angler a = (Angler) Markierer;
                        og = a.IsInString(lastKnoten, zr);
                    }
                }
                // wenn nur markierte und ist markiert sonst alle
                if (onlymarked && og.Length > 0 || !onlymarked)
                {
                    sb.Append(makeZweig(lastKnoten, zr, ebene, true));
                    anzZ++;

                    // Child-div
                    if (og.Length > 0)
                    {
                        string mwz = MakeWeiterZweig(lastKnoten.KnotenGuid.ToString(), zr.ZweigGuid.ToString(), true);
                        if (mwz.Length > 0)
                        {
                            sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:block;\"");
                            sb.Append("ID=\"" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\">");
                            sb.Append(mwz);
                        }
                        else
                        {
                            sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:none;\"");
                            sb.Append("ID=\"" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\">");
                        }
                    }
                    else
                    {
                        sb.Append("<div style=\"padding-left:" + (ebene + 1)*INDENT + "px;display:none;\"");
                        sb.Append("ID=\"" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\">");
                    }
                    sb.Append("</div>");
                }
            }
            // ohne Zweige nichts zur�ckgeben
            if (anzZ == 0)
            {
                return "";
            }
            else
            {
                return sb.ToString();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="zr"> </param>
        /// <param name="ebene"> </param>
        /// <param name="onlymarked"> </param>
        /// <returns> </returns>
        private string makeZweig(KnotenDataSet.KnotenRow lastKnoten, ZweigDataSet.ZweigRow zr, int ebene,
                                 bool onlymarked)
        {
            string og = "";
            StringBuilder sb = new StringBuilder();
            // Markierung
            if (Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    og = c.IsInString(lastKnoten, zr);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    og = a.IsInString(lastKnoten, zr);
                }
            }

            sb.Append("<div style=\"white-space:nowrap;padding-left:" + ebene*INDENT + "px;\" ");
            sb.Append("ID=\"Z" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\" ");
            sb.Append(">");
            sb.Append("<a class=\"zweig\" href='javascript:ToggleZweig(\"" + lastKnoten.KnotenGuid + "\",\"" +
                      zr.ZweigGuid + "\",\"" + onlymarked + "\");'>");
            sb.Append(Zimg);
            if (Common.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                sb.Append(zr.Isen_nameNull() ? zr.Zweig : zr.en_name + "&nbsp;");
            }
            else
            {
                sb.Append(zr.Zweig + "&nbsp;");
            }
            // weiter Pfeile
            if (!zr.IsweiterBaumGuidNull()) sb.Append(wBimg);
            if (!zr.IsweiterNetzGuidNull()) sb.Append(wNimg);
            sb.Append("</a>");

            if (og.Length > 0)
            {
                sb.Append(Markierbar
                              ? "<a href='javascript:Edit(\"Edit" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\");'>"
                              : "");
                // OLIs Get
                if (Markierer is Code)
                {
                    sb.Append("<img style=\"border:none;vertical-align:middle;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/OLIs/" + og[0] + ".gif\"/>&nbsp;");
                    sb.Append("<img style=\"border:none;vertical-align:middle;\"width=\"10px\" src=\"" + imgroot +
                              "images/punkte/ILOs/" + og[1] + ".gif\"/>&nbsp;");
                }
                // ILOs fit
                if (Markierer is Angler)
                {
                    sb.Append("<img style=\"border:none;vertical-align:middle;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/ILOs/" + og[0] + ".gif\"/>&nbsp;");
                    sb.Append("<img style=\"border:none;vertical-align:middle;\"width=\"10px\" src=\"" + imgroot +
                              "images/punkte/OLIs/" + og[1] + ".gif\"/>&nbsp;");
                }
                sb.Append(Markierbar ? "</a>" : "");

                // Markierung Editieren und L�schen
                if (Markierbar)
                {
                    sb.Append("<div style=\"display:none;\" ID=\"Edit" + lastKnoten.KnotenGuid + zr.ZweigGuid + "\">" +
                              zweigPunktSelect(og) + "</div>");

                    sb.Append("&nbsp;<a href='javascript:ClearZweig(\"" + lastKnoten.KnotenGuid + "\",\"" + zr.ZweigGuid +
                              "\");'>");
                    sb.Append("<img style=\"vertical-align:middle;border:0px;\" width=\"10px\" src=\"" + imgroot +
                              "images/punkte/x.gif\"/>&nbsp;");
                    sb.Append("</a>");
                }
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        // ************************************************
        //               Ajax Methoden
        // *************************************************

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string MakeKnoten(string kguidstr)
        {
            Knoten k = new Knoten(new Guid(kguidstr));
            return makeKnoten(k.KnotenRow, 0, false);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string MakeZweig(string lastknoten, string zguidstr)
        {
            Knoten k = new Knoten(new Guid(lastknoten));
            Zweig z = new Zweig(new Guid(zguidstr));
            return makeZweig(k.KnotenRow, z.ZweigRow, 0, false);
        }

        /// <summary>
        ///     Ajax Methode, die f�r einen Knoten zur�ckgibt, wie es hinter ihm weitergeht.
        ///     Gibt ein ganzes Netz oder Baum aus
        /// </summary>
        /// <param name="kguidstr"> </param>
        /// <param name="onlymarked"> sollen nur markierte Knoten gezeigt werden </param>
        /// <returns> Netz oder Baum Html </returns>
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string MakeWeiterKnoten(string kguidstr, bool onlymarked)
        {
            Guid kguid = new Guid(kguidstr);
            Knoten k = new Knoten(kguid);

            // wenn es in einen Baum f�hrt -> diesen anzeigen und aus
            if (!k.KnotenRow.IsweiterBaumGuidNull())
            {
                return makeBaum(k.KnotenRow, k.KnotenRow.weiterBaumGuid.ToString(), 0, onlymarked);
            }

            // ******* Markieren
            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Markiere(k.KnotenRow);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Markiere(k.KnotenRow);
                }
            }
            if (!k.KnotenRow.IsweiterNetzGuidNull())
            {
                return makeNetz(k.KnotenRow.weiterNetzGuid.ToString(), 0, onlymarked);
            }

            return "";
        }

        /// <summary>
        ///     Ajax Methode, die f�r einen Zweig zur�ckgibt, wie es hinter ihm weitergeht
        /// </summary>
        /// <param name="lastKnoten"> letzter Knoten von dem zu diesem Zweig gegangen wurde </param>
        /// <param name="zguidstr"> Zweig Guid </param>
        /// <param name="onlymarked"> sollen nur markierte Zweige gezeigt werden </param>
        /// <returns> </returns>
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string MakeWeiterZweig(string lastKnoten, string zguidstr, bool onlymarked)
        {
            Guid zguid = new Guid(zguidstr);
            Zweig z = new Zweig(zguid);
            Knoten k = new Knoten(new Guid(lastKnoten));

            // ******* Markieren
            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Markiere(k.KnotenRow, z.ZweigRow);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Markiere(k.KnotenRow, z.ZweigRow);
                }
            }

            if (!z.ZweigRow.IsweiterNetzGuidNull())
            {
                return makeNetz(z.ZweigRow.weiterNetzGuid.ToString(), 0, onlymarked);
            }
            if (!z.ZweigRow.IsweiterBaumGuidNull())
            {
                return makeBaum(k.KnotenRow, z.ZweigRow.weiterBaumGuid.ToString(), 0, onlymarked);
            }
            return "";
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void UpdateKnoten(string kguid, string og)
        {
            Knoten k = new Knoten(new Guid(kguid));
            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Update(k.KnotenRow, og);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Update(k.KnotenRow, og);
                }
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void UpdateZweig(string kguid, string zguid, string og)
        {
            Knoten k = new Knoten(new Guid(kguid));
            Zweig z = new Zweig(new Guid(zguid));

            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Update(k.KnotenRow, z.ZweigRow, og);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Update(k.KnotenRow, z.ZweigRow, og);
                }
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void ClearKnoten(string kguidstr)
        {
            Guid kguid = new Guid(kguidstr);
            Knoten k = new Knoten(kguid);

            // L�schen
            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Clear(k.KnotenRow);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Clear(k.KnotenRow);
                }
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void ClearZweig(string kguidstr, string zguidstr)
        {
            Guid kguid = new Guid(kguidstr);
            Knoten k = new Knoten(kguid);
            Guid zguid = new Guid(zguidstr);
            Zweig z = new Zweig(zguid);

            // L�schen
            if (Markierbar && Markierer != null)
            {
                if (Markierer is Code)
                {
                    Code c = (Code) Markierer;
                    c.Clear(k.KnotenRow, z.ZweigRow);
                }
                if (Markierer is Angler)
                {
                    Angler a = (Angler) Markierer;
                    a.Clear(k.KnotenRow, z.ZweigRow);
                }
            }
        }

        /// <summary>
        ///     erm�glich dem Clientscript die Daten nur neu anzufordern wenn sie sich �ndern k�nnen
        /// </summary>
        /// <returns> </returns>
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public bool IsMarkierbar()
        {
            return Markierbar;
        }

        //#region Vom Web Form-Designer generierter Code

        //protected override void OnInit(EventArgs e)
        //{
        //    //
        //    // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
        //    //
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        /////<summary>
        /////    Erforderliche Methode f�r die Designerunterst�tzung
        /////    Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /////</summary>
        //private void InitializeComponent()
        //{
        //}

        //#endregion
    }
}
