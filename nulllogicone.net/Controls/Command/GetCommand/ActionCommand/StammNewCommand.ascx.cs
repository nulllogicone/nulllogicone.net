// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using OliWeb.Klassen;

namespace OliWeb.Controls.Command.GetCommand.ActionCommand
{
    ///<summary>
    ///    StammNewCommand.
    ///</summary>
    public class StammNewCommand : CommandBase
    {
        /// <summary>
        ///     das HyperLink Control.
        /// </summary>
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // allgemeinen Text einstellen
            HyperLink1.Text = "Stamm new";
            HyperLink1.NavigateUrl = Helper.MakeBaseLink() + "Sites/Elemente/NeuAnmelden.aspx";

            if (Stamm != null)
            {
                HyperLink1.Text = Stamm.Q.S + " new";
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
            Load += Page_Load;
        }

        #endregion
    }
}