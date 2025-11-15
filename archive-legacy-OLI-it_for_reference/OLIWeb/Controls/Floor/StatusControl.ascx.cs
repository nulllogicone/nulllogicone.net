// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using System.Drawing;
using System.Web.UI;
using OliEngine;

namespace OliWeb.Controls.Floor
{
    ///<summary>
    ///    Zusammenfassung f�r StatusControl.
    ///</summary>
    public partial class StatusControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConnectionLabel.Text = OliCommon.OLIsConnection.DataSource;

            if (Request.Cookies["oliweb"] != null)
            {
                OliWebCookieLabel.Text = Request.Cookies["oliweb"].Values[0] + Request.Cookies["oliweb"].Values[1] +
                                         Request.Cookies["oliweb"].Values[2];
            }

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                AspSessionLabel.Text = Request.Cookies["ASP.NET_SessionId"].Value;
                AspSessionLabel.ForeColor = Color.Green;
            }
            else
            {
                AspSessionLabel.Text = "kein Session Cookie";
                AspSessionLabel.ForeColor = Color.Red;
            }

            if (Session["java"] != null)
            {
                JavaLabel.Text = Session["java"].ToString();
            }
            else
            {
                JavaLabel.Text = " -";
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode f�r die Designerunterst�tzung
        ///    Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}
