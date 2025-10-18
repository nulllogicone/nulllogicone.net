// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;

namespace OliWeb.Controls.Command.SpecialCommand.GetCommand
{
    ///<summary>
    ///    schließt eine DetailDatenansicht
    ///</summary>
    public partial class CloseHyperLink : CommandBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einfügen
        }

//		public string NavigateUrl
//		{
//			get
//			{
//				return HyperLink1.NavigateUrl;
//			}
//			set
//			{
//				HyperLink1.NavigateUrl = value;
//			}
//		}

//		public string ToolTip
//		{
//			get
//			{
//				return HyperLink1.ToolTip;
//			}
//			set
//			{
//				HyperLink1.ToolTip = value;
//			}
//		}

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
    }
}