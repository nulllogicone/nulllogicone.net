// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    AnglerNewCommand.
    ///</summary>
    public partial class AnglerNewCommand : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // zuerst alles deaktivieren
            HyperLink1.Text = string.Format("<b>+ {0}</b>", GetGlobalResourceObject("SAPCT", "A"));
            HyperLink1.ToolTip = "neues Filterprofil erstellen, einen neuen Empfänger für diesen Stamm.";
            HyperLink1.NavigateUrl = "";
            HyperLink1.Visible = false;

            // dann aktivieren
            if (Stamm != null && Stamm.BinIchEingeloggt)
            {
                HyperLink1.NavigateUrl = "~/Sites/Edit/AnglerEdit.aspx?cmd=newA&prevSessionId=" +
                                         Session.SessionID;
                HyperLink1.Visible = true;
            }
        }

    }
}