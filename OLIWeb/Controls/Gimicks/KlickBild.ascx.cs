// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using OliEngine;

namespace OliWeb.Controls.Gimicks
{
    ///<summary>
    ///    stellt ein Bild mit angegebener Breite dar und öffnet es bei Klick in einem eigenen
    ///    Browserfenster in Originalgröße
    ///</summary>
    public partial class KlickBild : UserControl
    {


        private string bildName = "";

        /// <summary>
        ///     Bildname ausgehend vom OliUpload Ordner - also mit vorangestellter
        ///     StammGuid/Bildname.ext
        /// </summary>
        public string BildName
        {
            get { return (bildName); }
            set { bildName = value; }
        }

        /// <summary>
        ///     Breite in px in der das Bild dargestellt werden soll
        /// </summary>
        public int Breite { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     das Bild wird in gewünschter Breite in einen Link gepackt
        ///     und dargestellt.
        ///     <code>&lt;a href=Bild target=klickbild>&lt;img src=Bild>&lt;/a></code>
        /// </summary>
        /// <param name="e"> </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (BildName.Length > 0)
            {
                HtmlImage i = new HtmlImage();
                i.Alt = BildName;
                i.Src = OliUtil.MakeImageSrc(BildName);
                i.Border = 0;

                if (Breite > 0)
                    i.Width = Breite;

                HtmlAnchor a = new HtmlAnchor();
                a.HRef = OliUtil.MakeImageSrc(BildName);
                a.Target = "klickbild";
                a.Controls.Add(i);

                Controls.Add(a);
            }
        }
    }
}