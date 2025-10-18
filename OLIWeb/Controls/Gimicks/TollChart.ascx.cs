// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OliWeb.Controls.Gimicks
{
    ///<summary>
    ///    die durchschnittliche Toll-Bewertung der Antworten wird als grüner Balken auf
    ///    einem grauen Bereich dargestellt. Bei Mousehover wird der exakte Wert als 
    ///    Tooltipp angezeigt.
    ///</summary>
    public partial class TollChart : UserControl
    {
        /// <summary>
        ///     ein dunkelgrünes, 1x1 px großes image auf dem ascx Control
        /// </summary>
        protected Image TollImage;

        /// <summary>
        ///     ein graues, 1x1 px großes image auf dem ascx Control
        /// </summary>
        protected Image RestImage;

        private int tollWert;
        private int breite = 150;
        private int hoehe = 10;

        /// <summary>
        ///     der durchschnittliche Wert aller Bewertungen (0-100) muss angegeben werden
        /// </summary>
        public int TollWert
        {
            get { return (tollWert); }
            set
            {
                if (tollWert >= 0 && tollWert <= 100)
                {
                    tollWert = value;
                }
            }
        }

        /// <summary>
        ///     die Gesamtbreite des Controls. Der Tollwert wird als prozentualer Bereich dargestellt.
        /// </summary>
        public int Breite
        {
            get { return (breite); }
            set
            {
                if (value > 0 && value < 500)
                {
                    breite = value;
                }
            }
        }

        /// <summary>
        ///     Die gewünschte anzuzeigende Höhe in px
        /// </summary>
        public int Hoehe
        {
            get { return (hoehe); }
            set { hoehe = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Tollwert in grün, der Rest in grau werden als zwei images nebeneinander dargestellt
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            TollImage.Visible = false;
            RestImage.Visible = false;

            if (TollWert >= 0 && TollWert <= 100)
            {
                TollImage.Width = (TollWert*Breite)/100;
                RestImage.Width = Breite - (TollWert*Breite)/100;

                TollImage.ToolTip = "Bewertung: " + TollWert;
                RestImage.ToolTip = "Bewertung: " + TollWert;

                TollImage.Height = Hoehe;
                RestImage.Height = Hoehe;

                TollImage.Visible = true;
                RestImage.Visible = true;
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung.
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}