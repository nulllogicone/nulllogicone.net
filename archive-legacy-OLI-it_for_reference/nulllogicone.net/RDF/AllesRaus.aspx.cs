// --------------------------
// (c) frederic@luchting.de
// --------------------------
//  
using System;
using System.Text;

namespace nulllogicone.net.RDF
{
    /// <summary>
    ///     Zusammenfassung f�r AllesRaus.
    /// </summary>
    public class AllesRaus : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Button Button1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Hier Benutzercode zur Seiteninitialisierung einf�gen
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

        /// <summary>
        ///     Erforderliche Methode f�r die Designerunterst�tzung. 
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {
            Button1.Click += Button1_Click;
            Load += Page_Load;
        }

        #endregion

        private void Button1_Click(object sender, System.EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            OliEngine.OliDataAccess.Code codes = new OliEngine.OliDataAccess.Code();
            foreach (OliEngine.DataSetTypes.CodeDataSet.CodeRow cr in codes.Code.Rows)
            {
                OliEngine.OliMiddleTier.OLIs.Code c = new OliEngine.OliMiddleTier.OLIs.Code(cr.CodeGuid);
                sb.Append(c.MakeCodeRDF());
            }

            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/rdf+xml";
            Response.AddHeader("content-disposition", "attachment; filename=\"Stamm.rdf\"");
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}
