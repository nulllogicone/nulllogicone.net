// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System;
using System.Web.UI.WebControls;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    StammNewCommand.
    ///</summary>
    public partial class StammNewCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected HyperLink HyperLink1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // allgemeinen Text einstellen
            HyperLink1.Text = "Stamm new";
            HyperLink1.NavigateUrl = "~/Sites/Elemente/NeuAnmelden.aspx";

            if (Stamm != null)
            {
                HyperLink1.Text = Stamm.Q.S + " new";
            }
        }

    }
}
