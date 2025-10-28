// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Web.UI;
using OliEngine.OliDataAccess.Views;

namespace OliWeb.Sites.Elemente
{
    /// <summary>
    ///     Buch.
    /// </summary>
    public partial class Buch : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StammTopLab st = new StammTopLab();
            BuchRepeater.DataSource = st.StammTopLab;
            DataBind();
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

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}
