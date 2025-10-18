// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using System.Web.UI.WebControls;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor.Suche
{
    /// <summary>
    ///     für eine gefundene Stämmen mit Bild und Trefferanzahl im Titel
    /// </summary>
    public partial class StammListsControl : MasterControl
    {
        /// <summary>
        ///     die Ausgabetabelle für Stammbild und Name
        /// </summary>
        /// <remarks>
        ///     <h2>Zu den anderen DataGrids noch ein summary hinzufügen!</h2>
        /// </remarks>
        protected DataGrid StammDataGrid;

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

        /// <summary>
        ///     Titel mit Trefferanzahl
        /// </summary>
        protected Label TitelLabel;

        // Eigenschaften
        // -------------

        /// <summary>
        ///     Die Daten werden diesem Control von außen übergeben, da es für
        ///     unterschiedliche Werte verwendet werden kann. Danach bindet es
        ///     sein darstellendes Steuerelement (DataGrid oder Repeater).
        ///     <p>Hier wird auch noch der Titel mit der Anzahl angepasst</p>
        /// </summary>
        public object DataSource
        {
//			get
//			{
//				return StammDataGrid.DataSource;
//			}
            set
            {
                DataTable dt = (DataTable) value;
                StammDataGrid.DataSource = dt;
                StammDataGrid.DataBind();
                TitelLabel.Text = "Stamm (" + dt.Rows.Count + ")";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}