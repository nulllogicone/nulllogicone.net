// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Data;
using System.Linq;
using OliWeb.Sites;

namespace OliWeb.Controls.Command.GetCommand.DetailCommand
{
    ///<summary>
    ///    StammInboxCommand.
    ///</summary>
    public partial class StammInboxCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = "StammInbox";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;
            HyperLink1.ToolTip = "+ neue Antworten auf meine Nachrichten";

            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                // meine News (Neu/Gesamt)
                var myIn = Stamm.MyInbox.AsEnumerable();
                int allIn = myIn.Count();
                int newIn = myIn
                    .Where(n => !n.Field<DateTime?>("gesehen").HasValue)
                    .Where(n => !n.Field<DateTime?>("gelesen").HasValue)
                    .Count();
                string newString = newIn > 0 ? string.Format("<b>{0}</b>", newIn) : "";
                Text = string.Format("Inbox ({1}) - {0}", newString, allIn);

                //this.Text = "Inbox (" + Stamm.MyInbox.Rows.Count.ToString() + ")";
                HyperLink1.NavigateUrl = "~/Sites/StammInboxSite.aspx?sguid=" + Stamm.StammRow.StammGuid;
                HyperLink1.Visible = true;
            }

            if (Page is StammInboxSite)
            {
                HyperLink1.CssClass = "ButtonSel";
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///    Erforderliche Methode für die Designerunterstützung
        ///</summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}