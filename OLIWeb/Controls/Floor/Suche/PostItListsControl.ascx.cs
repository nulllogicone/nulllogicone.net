// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Data;
using OliWeb.Klassen;

namespace OliWeb.Controls.Floor.Suche
{
    ///<summary>
    ///    für gefundener Nachrichten mit Bild und der Trefferanzahl im Titel
    ///</summary>
    public partial class PostItListsControl : MasterControl
    {
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
        ///     Die Daten werden diesem Control von außen übergeben, da es für
        ///     unterschiedliche Werte verwendet werden kann. Danach bindet es
        ///     sein darstellendes Steuerelement (DataGrid oder Repeater).
        ///     <p>Hier wird auch noch der Titel mit der Anzahl angepasst</p>
        /// </summary>
        public object DataSource
        {
            get { return PostItDataGrid.DataSource; }
            set
            {
                DataTable dt = (DataTable) value;
                PostItDataGrid.DataSource = dt;
                PostItDataGrid.DataBind();
                TitelLabel.Text = "Nachrichten (" + dt.Rows.Count + ")";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}