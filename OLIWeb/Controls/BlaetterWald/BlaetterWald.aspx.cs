using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using OliEngine.OliMiddleTier.OLIx;
using OliEngine.OliMiddleTier.ZellHaufen;
using OliWeb.Klassen;

namespace OliWeb.Controls.BlaetterWald
{
	/// <summary>
	/// BlaetterWald.
	/// </summary>
	public class BlaetterWald : MasterPage
	{
		protected OliWeb.Controls.BlaetterWald.NetzControl NetzControl1;
		protected OliWeb.Controls.BlaetterWald.BaumControl BaumControl1;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist für den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			NetzKette nk = NetzKette.Instance();
			BaumKette bk = BaumKette.Instance();
			Guid nguid;
			Guid bguid;

			// nguid im Querystring
			if(Request["nguid"] == null)
			{
				// root
				nguid = new Guid("76035F19-F4AE-4D58-A388-4BBC72C51CEF");
			}
			else
			{
				nguid = new Guid(Request["nguid"]);
			}

			Netz n = new Netz(nguid);
			nk.Push(n);
			nk.CutBehind(n);

			// Setzen der Eigenschaft im NetzControl
			NetzControl1.NetzGuid = n.NetzRow.NetzGuid ;

			// bguid im Querystring
			if(Request["bguid"] != null)
			{
				bguid = new Guid(Request["bguid"]);
				BaumControl1.BaumGuid = bguid;
				Baum b = new Baum(bguid);
				bk.Push(b);
				bk.CutBehind(b);
			}
			else
			{
				bk.Clear();
			}





		}


	}
}
