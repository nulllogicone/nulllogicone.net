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

using OliEngine.OliMiddleTier.Markierer;
using OliEngine.OliMiddleTier.ZellHaufen;

namespace OliWeb.Controls.Wortraum
{
	/// <summary>
	/// WortraumEdit.
	/// </summary>
	public class WortraumEdit : System.Web.UI.Page
	{
		protected OliWeb.Controls.Wortraum.WortraumController WortraumController1;
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Dieser Aufruf ist f�r den ASP.NET Web Form-Designer erforderlich.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Erforderliche Methode f�r die Designerunterst�tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		ZellBuilder zb;


		private void Page_Load(object sender, System.EventArgs e)
		{
			zb = (ZellBuilder)Session["zb"];
			if (zb==null) 
			{
				NullMarkierer nm = new NullMarkierer();
				zb = new ZellBuilder();
				zb.Markierer = nm;
				Session["zb"] = zb;
			}
			WortraumController1.Markierbar = true;
			WortraumController1.ZellBuilder = zb;
			WortraumController1.ShowEdit = true;
			WortraumController1.Werbefrei = false;
			
		}

	}
}

