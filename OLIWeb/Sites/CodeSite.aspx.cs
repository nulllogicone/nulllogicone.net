// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:19
// --------------------------
//  

using System;
using OliWeb.Klassen;

namespace OliWeb.Sites
{
    /// <summary>
    ///     CodeSite.
    /// </summary>
    public partial class CodeSite : MasterPostItPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PostIt.Code != null)
            {
                RdfHyperLink.NavigateUrl = "http://nulllogicone.net/Code/?" + PostIt.Code.CodeRow.CodeGuid;
            }
            else
            {
                Response.Redirect("~/Sites/PostItSite.aspx");
            }
        }

        #region Vom Web Form-Designer generierter Code

        protected override void OnInit(EventArgs e)
        {
            // CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}