// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-19:06
// --------------------------
//  

using System;
using System.Globalization;

namespace OliEngine
{
    /// <summary>
    ///     OliUtil.
    /// </summary>
// TODO: die meisten der hier angebotenen Funktionen formatiert Werte in HTML
// deshalb sollten sie eigentlich in der GUI angeboten werden und haben
// in der Engine nichts verloren
// 2022-08-08: Falsch, Ich will die Bedeutung der 'magic numbers' definiert haben,
// nicht im UI sondern tief verborgen
    public abstract class OliUtil
    {
        //private enum StammZust
        //{
        //    Urheber,
        //    IchAuch,
        //    Delegierer,
        //    Uebersetzer,
        //    Antworter
        //};

        private enum PostItZust
        {
            neu,
            delegiert,
            uebersetzt,
            kommentiert
        };

        //private enum CodeZust
        //{
        //};

        // IsNumeric(string)
        /// <summary>
        ///     prüft ob die übergebene Zeichenfolge eine int-Zahl darstellt
        /// </summary>
        /// <param name="value"> Zeichenfolge, die auf Zahl geprüft werden soll </param>
        /// <returns> Gibt true zurück wenn es sich um eine Zahl handelt </returns>
        //public static bool IsNumeric(string value)
        //{
        //    try
        //    {
        //        int.Parse(value);
        //        return (true);
        //    }
        //    catch
        //    {
        //        return (false);
        //    }
        //}

        //MakeRedKooK
        public static string MakeRedKook(string kook)
        {
            if (kook.Length == 0) kook = "0";
            var k = decimal.Parse(kook);
            return MakeRedKook(k);
        }

        // MakeRedKooK
        public static string MakeRedKook(decimal kook)
        {
            if (kook < 0)
            {
                return ("<strong><font color='red'>" + kook.ToString("0.00") + "</font></strong>");
            }
            else
            {
                return (kook.ToString("0.00"));
            }
        }

         //MakeImageHtml(string)
        public static string MakeImageHtml(string datei)
        {
            var s = "";

            if (!string.IsNullOrEmpty(datei))
            {
                s = @"<img src='" + MakeImageSrc(datei);
                s += "' border='0px'";
                s += " alt='" + datei + "'>";
            }

            return (s);
        }

        // MakeImageHtml(string, width)
        // TODO: diese Funktion die obige verwenden lassen
        public static string MakeImageHtml(string datei, int width)
        {
            var s = "";

            if (!string.IsNullOrEmpty(datei))
            {
                s = @"<img src='" + MakeImageSrc(datei);
                s += @"' width='" + width + "px' border='0px'";
                s += " alt='" + datei + "' />";
            }

            return (s);
        }

        // MakeImageSrc()
        public static string MakeImageSrc(string datei)
        {
            var src = "";

            if (!string.IsNullOrEmpty(datei))
            {
                if (datei.StartsWith("http"))
                {
                    src = datei;
                }
                else
                {
                    if (!datei.StartsWith("/"))
                    {
                        datei = "/" + datei;
                    }
                    datei = datei.Replace("//", "/");
                    src = OliCommon.bilderOrdner + datei;
                }
            }
            return src;
        }

        /// <summary>
        ///     Erstellt ein html Fragment, das eine anklickbare Weltkugel darstellt.
        /// </summary>
        /// <param name="url"> </param>
        /// <returns> </returns>
        [Obsolete(
            "diese 'hardcoded' html Methode sollte über design mit css-Weltkugel-Design gelöst werden. Dann hat ein Link ein Ziel und eine Darstellung"
            )]
        public static string MakeLinkHtml(string url)
        {
            var s = "";
            if (!string.IsNullOrEmpty(url))
            {
                if (!(url.StartsWith("http")))
                {
                    url = "https://" + url;
                }
                s += "<a href='" + url + "' title='" + url + "' rel='nofollow' >";
                s += "<img alt='" + url + "' src='" + OliCommon.imagesOrdner +
                     "weltrotring.jpg' width='30px' border='0px'/>";
                s += "</a>";
            }
            return (s);
        }

        // MakeHtmlLineBreak(string)
        public static string MakeHtmlLineBreak(string inString)
        {
            return (inString.Replace("\n", "<br />"));
        }

        // MakeHtmlLineBreak(object)
        public static string MakeHtmlLineBreak(object o)
        {
            var inString = o.ToString();
            return (inString.Replace("\n", "<br>"));
        }

        // FirstXWords(string)
        public static string FirstXWords(string source, int anz)
        {
            if (source.Length > 0)
            {
                var pos = 0;
                for (var i = 0; i < anz; i++)
                {
                    pos = source.IndexOf(" ", pos + 1);
                    if (pos == -1)
                    {
                        return (source);
                    }
                }
                return (source.Substring(0, pos) + " [...]");
            }
            return ("");
        }

        // MakeDateTimeDiff
        public static string MakeDateTimeDiff(DateTime date)
        {
            DateTimeFormatInfo dtfi = CultureInfo.CurrentCulture.DateTimeFormat;

            var ret = "";
            TimeSpan diff = DateTime.Now - date;
            var fut = false;

            // Zukunft
            if (diff.Milliseconds < 0)
            {
                fut = true;
                diff = -diff;
            }

            // < 24 h
            if (diff.Days == 0)
            {
                // < 12 h
                if (diff.Hours < 12)
                {
                    ret = "<font color='darkred' style='font-bold:true;'>" + date.ToLongTimeString() + "</font>";
                }
                else
                {
                    ret = "<font color='darkred'>" + date.ToLongTimeString() + "</font>";
                }
            }
            // > 24 h
            if (diff.Days == 1)
            {
                ret = "<font color='darkgreen'>" + date.ToShortTimeString() + "</font>";
            }
            // diese Woche
            if (diff.Days > 1 && diff.Days <= 7)
            {
                DayOfWeek dow = date.DayOfWeek;
                ret = "<font color='darkblue'>";
                ret += dtfi.GetAbbreviatedDayName(dow) + " </font>" + date.ToShortTimeString();
            }
            // dieses Jahr
            if (diff.Days > 7 && diff.Days <= 365)
            {
                ret = "<font color='darkblue'>";
                ret += date.Day + " " + dtfi.GetAbbreviatedMonthName(date.Month);
                ret += "</font>";
            }
            // letztes Jahr
            if (diff.Days > 365 && diff.Days < 730)
            {
                ret = "<font color='darkblue'>";
                ret += dtfi.GetMonthName(date.Month);
                ret += "</font> " + date.Year;
            }
            // vorletztes Jahr
            if (diff.Days >= 730 && diff.Days < 1095)
            {
                ret = dtfi.GetAbbreviatedMonthName(date.Month) + " " + date.Year;
            }
            if (diff.Days > 1095)
            {
                ret = date.Year.ToString();
            }

            if (fut)
            {
                ret = "<span style='background-color:lightblue'>" + ret + "</span>";
            }

            // ToolTip
            ret = "<span title='" + date + "' >" + ret + "</span>";

            return (ret);
        }

        // MakeDateTimeDiff(object)
        public static string MakeDateTimeDiff(object source)
        {
            try
            {
                DateTime dt = DateTime.Parse(source.ToString());
                return (MakeDateTimeDiff(dt));
            }
            catch
            {
                return ("");
            }
        }

        // MakeDbDate
        public static string MakeDbDate(DateTime date)
        {
            var s = "";
            s += date.Year + ".";
            s += date.Month + ".";
            s += date.Day + " ";
            s += date.Hour + ":";
            s += date.Minute + ":";
            s += date.Second.ToString();
            return s;
        }
    }
}