// --------------------------------------------------------------------------------
// [OliWeb] [OliWeb] CommandBase.cs
// Created: 2012-08-13
// Changed: 2014-06-14-19:32
// User: Frederic, 
// --------------------------------------------------------------------------------
//  

using System.Drawing;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command
{
    /// <summary>
    /// OLI-it abstrahiert Kommunikation unterschiedlichster Problemfelder
    /// (Frage-Antwort / Kurz-Und-F�ndig / Supporter-Hausfrau / Partnervermittlung).
    /// <p>
    /// Deshalb war von Anfang an vorgesehen die Beschriftungen der Entit�ten
    /// <a href="SAPCT.htm">SAPCT</a>
    /// (Frager, Anbieter, Suchender, ....) und iher Detailansichten
    /// (meine Fragen, Anzeigen, Angebote) individuell einzustellen.
    /// </p>
    /// </summary>
    [ComVisible(false)]
    public class CommandBase : MasterControl
    {
        /// <summary>
        /// Die unterschiedlichen Beschriftungssprachen liegen auf der Datenbank in
        /// der Q-Tabelle (einer der wenigen Buchstaben, die noch frei waren)
        /// <p>
        ///     <i>
        /// nach SAPCT RL NKBZ ogif w x
        /// <a href="Glossar.htm">Glossar</a>
        ///     </i>
        /// </p>
        /// </summary>
        private enum BeschriftungsTyp
        {
            Individuell,
            Allgemein
        }

        /// <summary>
        /// Hilfsfunktion, die den HyperLink sucht und den anderen Eigenschaften
        /// zur Verf�gung stellt.
        /// <p>
        /// Alle Controls, die
        /// von dieser Klasse abgeleitet sind, m�ssen ein HyperLink mit
        /// dem Namen
        /// <b>HyperLink1</b>
        /// enthalten.
        /// </p>
        /// </summary>
        private HyperLink hl
        {
            get { return (HyperLink) FindControl("HyperLink1"); }
        }

        /// <summary>
        /// der kleine, gelb hinterlegte Hilfetext, wenn die Maus �ber dem
        /// Control verweilt.
        /// </summary>
        public string ToolTip
        {
            get { return hl.ToolTip; }
            set { hl.ToolTip = value; }
        }

        /// <summary>
        /// eigentlich stellt jedes von dieser Klasse abgeleitete Control diese
        /// Eigenschaft selbst ein (mit Anzhal der Details in Klammern).
        /// </summary>
        public string Text
        {
            get { return hl.Text; }
            set { hl.Text = value; }
        }

        /// <summary>
        /// eigentlich stellt jedes von dieser Klasse abgeleitete Control diese
        /// Eigenschaft selbst ein.
        /// Man kann auf der aspx Seite mit einem Attribut diese Eigenschaft
        /// festlegen - vor allem bei den <see cref="ExitCommand" />, die noch
        /// einen Parameter ben�tigen. (cmd=exitS)
        /// </summary>
        public string NavigateUrl
        {
            get { return hl.NavigateUrl; }
            set { hl.NavigateUrl = value; }
        }

        /// <summary>
        /// wenn ein GetCommandControl auf einer Seite platziert wird, kann
        /// man eine individuelle Css Klasse angeben
        /// <code>&lt;uc1:StammPostItCommand id="StammPostItCommand1" runat="server" CssClass="">&lt;/uc1:StammPostItCommand></code>
        /// </summary>
        public string CssClass
        {
            get { return hl.CssClass; }
            set { hl.CssClass = value; }
        }

        public bool Selected
        {
            set
            {
                if (value)
                {
                    hl.BackColor = Color.White;
                }
            }
        }
    }
}
