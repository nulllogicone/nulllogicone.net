// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI.WebControls;
using OliEngine.DataSetTypes;
using OliWeb.Controls.Koerper.Organ;
using OliWeb.Klassen;

namespace OliWeb.Controls.Koerper
{
    ///<summary>
    ///    aktueller Stamm in der Mittelschicht. An ihm h�ngen PostIt, Angler und TopLab.
    ///    Er wird durch das <see cref="Organ.StammOrgan" /> Control mit Bild, Name, Beschreibung, Erstelldatum und seinem Verm�gen (KooK)
    ///    angezeigt. Oben rechts mit x - kann man ihn schlie�en.
    ///</summary>
    public partial class StammKoerper : MasterControl
    {
        /// <summary>
        ///     die Beschriftung f�r die Buttons wird aus einer individuell einstellbaren
        ///     Tabelle genommen. Zuerst wird der Name des Sprachmusters genannt
        /// </summary>
        protected Label QLabel;

        /// <summary>
        ///     Der Name f�r die Stamm-Entit�t (Urheber von Fragen, Verk�ufer von Angeboten)
        /// </summary>
        protected Label QQLabel;

        /// <summary>
        ///     stellt die Datenfelder eines Stammes dar (Name, Bild, KooK, ...)
        /// </summary>
        protected StammOrgan StammOrgan1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // meine Q-Row zum beschriften holen
            if (Stamm != null)
            {
                QDataSet.QRow Q = OliUser.Stamm.Q;

                QLabel.Text = Q.S;
                QQLabel.Text = "(" + Q.Q + ")";

                if (Stamm.BinIchEingeloggt)
                {
                    EditHyperLink.Visible = true;
                }
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        #endregion
    }
}
