/*
 * Auteur      : big71 (http://www.codeproject.com/aspnet/ASPNET2XHTML.asp)
 * Mise à jour et adaptation : Sébastien FERRAND (http://www.vbmaf.net)
 * Description   : Transforme le code HTML généré par asp.net pour le rendre
 *              compatible avec XHTML.
 * Version      : 1.0
 * Date         : 05 octobre 2004
 * ------------------------------------------------------------------------------
 * Révision :
 * Version		: 1.1
 * Date			: 03 novembre 2004
 * Mise à jour des méthodes ConvertToLowerCase(), SingleTagToLowerCase() et 
 * PropertiesToLowerCase() pour la prise en charge des tags avec ou sans propriété
 * et les propriétés multiples pour un tag
 * --
 * Add support for tags with/without properties and multiproperties tags.
 * ------------------------------------------------------------------------------
 * Licence      : Cette classe est livrée telle quelle. Le ou les auteurs ne sont
 *              en rien responsable de l'utilisation qui en sera fait.
 *              Vous êtes libre de distribuer ou d'utiliser cette classe dans 
 *              vos projets à condition que cet entête reste présente.
 */

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace OliWeb 
{
	/// <summary>
	/// Summary description for XHTMLPage.
	/// </summary>
	public class XHTMLPage : System.Web.UI.Page 
	{
		public enum _XHTMLFormat 
		{
			XHTML10_Strict,
			XHTML10_Transitional,
			XHTML10_Frameset
		}

		
		private string m_sXHTML;
		private _XHTMLFormat m_XHTMLFormat;
		private Encoding m_Encoding;
		private string m_sLanguage;
		private bool m_bXmlCDATA;
		private DateTime m_StartTime;

		public _XHTMLFormat XHTMLFormat 
		{
			get {return m_XHTMLFormat;}
			set {m_XHTMLFormat = value;}
		}

		public Encoding Encoding 
		{
			get {return m_Encoding;}
			set {m_Encoding = value;}
		}
		
		public string Language 
		{
			get {return m_sLanguage;}
			set {m_sLanguage = value;}
		}

		public bool XmlCDATA 
		{
			get {return m_bXmlCDATA;}
			set {m_bXmlCDATA = value;}
		}

		public XHTMLPage() 
		{
			//
			// TODO: Add constructor logic here
			//
			m_sXHTML = "";
			m_XHTMLFormat = _XHTMLFormat.XHTML10_Transitional;
			m_Encoding = Encoding.UTF8;
			m_sLanguage = "de";
			m_bXmlCDATA = false;
			m_StartTime = DateTime.Now;
		}

		protected override void Render(HtmlTextWriter output) 
		{
			StringWriter w;
			w = new StringWriter();
			
			HtmlTextWriter myoutput = new HtmlTextWriter(w);
			base.Render(myoutput);

			myoutput.Close();

			m_sXHTML = w.GetStringBuilder().ToString();
			
			ReplaceDocType();

			switch (m_XHTMLFormat) 
			{
				case _XHTMLFormat.XHTML10_Strict:
					ConvertToXHTMLStrict();
					break;

				case _XHTMLFormat.XHTML10_Transitional:
					ConvertToXHTMLTransitional();
					break;

				case _XHTMLFormat.XHTML10_Frameset:
					ConvertToXHTMLFrameset();
					break;
			}

			// frederic@luchting.de
			// Fußzeile anhängen
			AppendFooter();
			output.Write(m_sXHTML);
		}

		private void ConvertToXHTMLFrameset() 
		{
			ConvertToLowerCase();
			AddSelfClose("meta");
			FixHtml();
		}

		private void ConvertToXHTMLTransitional() 
		{
			ConvertToLowerCase();
			AddSelfClose("meta");
			AddSelfClose("link");
			AddSelfClose("img");
			AddSelfClose("hr");

			FixScript();
			FixBr();
			FixStyle();
			FixHtml();
			
		}

		private void ConvertToXHTMLStrict() 
		{
			ConvertToLowerCase();
			AddSelfClose("meta");
			AddSelfClose("link");
			AddSelfClose("img");
			AddSelfClose("hr");

			FixScript();
			RemoveAttribute("form", "name");
			FixInput();
			FixBr();
			FixStyle();
			FixHtml();
			maskScript();
		}

		/// <summary>
		/// Fügt vor das Ende /form /body /html noch eine OLI-it Fußzeile ein
		/// </summary>
		private void AppendFooter()
		{
			int nStart = m_sXHTML.IndexOf("</form>",0);
			if (nStart > 0)
			{
				m_sXHTML = m_sXHTML.Insert(nStart, this.FussZeile());
			}
		}

		#region output putzen - Funktionen um die Response XHTML konform zu gestalten

		private void ReplaceDocType() 
		{
			// delete the current DOCTYPE
			int nStart = m_sXHTML.IndexOf("<!DOCTYPE", 0);
			if ( nStart > 0 ) 
			{
				int nEnd = m_sXHTML.IndexOf(">", nStart + 1);
				if ( nEnd > 0 ) 
				{
					m_sXHTML = m_sXHTML.Remove(nStart, nEnd-nStart+1);

					switch (m_XHTMLFormat) 
					{
						case _XHTMLFormat.XHTML10_Strict:
							m_sXHTML = m_sXHTML.Insert(0, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
							break;

						case _XHTMLFormat.XHTML10_Transitional:
							m_sXHTML = m_sXHTML.Insert(0, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
							break;

						case _XHTMLFormat.XHTML10_Frameset:
							m_sXHTML = m_sXHTML.Insert(0, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">");
							break;
					}
					
					m_sXHTML = m_sXHTML.Insert(0, "<?xml version=\"1.0\" encoding=\""+ m_Encoding.HeaderName +"\"?>\r\n");
				}
			}	
		}

		private void ConvertToLowerCase() 
		{
			// Make all tag to lower case
			//			m_sXHTML = Regex.Replace(m_sXHTML, "<(/?)([a-zA-Z]+)(\\s*)>", new MatchEvaluator(SingleTagToLowerCase), RegexOptions.IgnoreCase);
			
			/// Update 03/11/2004 : Add support for Tags with properties
			/// Author : Sébastien FERRAND (mailto:sebastien.ferrand@vbmaf.net)
			m_sXHTML = Regex.Replace(m_sXHTML, "<(/?)([a-zA-Z0-9]+)[ ]*(.*?)>", 
				new MatchEvaluator(SingleTagToLowerCase), RegexOptions.IgnoreCase);
			
			/// Update 03/11/2004 : Update to match correctly tag with more one propertie
			/// Author : Sébastien FERRAND (mailto:sebastien.ferrand@vbmaf.net)
			// Make all properties to lower case
			m_sXHTML = Regex.Replace(m_sXHTML, "<([a-zA-Z0-9]+)(\\s+[a-zA-Z]+)(=\".+?>)",  
				new MatchEvaluator(PropertiesToLowerCase), RegexOptions.IgnoreCase);
		}

		private string SingleTagToLowerCase(Match m) 
		{
			/// Update 03/11/2004 : Add support for Tags with multi-properties
			/// Author : Sébastien FERRAND (mailto:sebastien.ferrand@vbmaf.net)
			if (m.Groups[3].ToString().Trim() == String.Empty )
				return "<" + m.Groups[1].ToString().ToLower() + m.Groups[2].ToString().ToLower() + ">";
			else
				return "<" + m.Groups[1].ToString().ToLower() + m.Groups[2].ToString().ToLower() + " " + m.Groups[3].ToString() + ">";
		}

		private string PropertiesToLowerCase(Match m) 
		{
			string szReplace = "";
			szReplace = "<" + m.Groups[1].ToString() + m.Groups[2].ToString().ToLower();

			// Search another property in tag
			if (Regex.Match(m.Groups[3].ToString(), "(.*?\")(\\s+\\w+)(=\".+>)",
				RegexOptions.IgnoreCase).Success) 
			{
				szReplace += Regex.Replace(m.Groups[3].ToString(),
					"(.*?\")(\\s+\\w+)(=\".+>)", new MatchEvaluator(nextProperty),
					RegexOptions.IgnoreCase);
			} 
			else 
			{
				szReplace += m.Groups[3].ToString();
			}

			return szReplace ; 
		}

		/// <summary>
		/// Recursively search for property in tag
		/// </summary>
		/// <param name="m">Match of the regular expression</param>
		/// <returns>tag with lower case properties</returns>
		private string nextProperty(Match m) 
		{
			string szReplace = "";
			szReplace = m.Groups[1].ToString() + m.Groups[2].ToString().ToLower();

			// Search another property in tag
			// Ignore if tag contains __VIEWSTATE... prevent long time calculation.
			if (Regex.Match(m.Groups[3].ToString(), "(.*?\")(\\s+\\w+)(=\".+>)",
				RegexOptions.IgnoreCase).Success && m.Groups[3].ToString().IndexOf("__VIEWSTATE")==-1) 
			{
				System.Diagnostics.Debug.WriteLine("Match OK","nextProperty");
				szReplace += Regex.Replace(m.Groups[3].ToString(),
					"(.*?\")(\\s+\\w+)(=\".+>)", new MatchEvaluator(nextProperty),
					RegexOptions.IgnoreCase);
			} 
			else 
			{
				System.Diagnostics.Debug.WriteLine("Match NOK","nextProperty");
				szReplace += m.Groups[3].ToString();
			}
			return szReplace;
		}

		private string HTMLTag(Match m) 
		{
			return "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\""+ m_sLanguage +"\">";
		}

		private void FixHtml() 
		{
			m_sXHTML = Regex.Replace(m_sXHTML, "<html>", new MatchEvaluator(HTMLTag), RegexOptions.IgnoreCase);
		}

		private void FixBr() 
		{
			m_sXHTML = m_sXHTML.Replace("<br>", "<br />");
		}

		private void FixScript() 
		{
			m_sXHTML = m_sXHTML.Replace("<script language=\"javascript\">", "<script type=\"text/javascript\">");
		}

		private void FixStyle() 
		{
			m_sXHTML = Regex.Replace(m_sXHTML, "style=\".+\"", new MatchEvaluator(ToLowerCase), RegexOptions.IgnoreCase);

			//			// Add <![CDATA[ ... ]]> to mask style
			m_sXHTML = Regex.Replace(m_sXHTML, 
				@"(<style[^<]*>){1}(?(?=\s*<!--)(\s*<!--)(\s*.*?)(//\s*-->)|(\s*.*?))\s*(</style>){1}",
				new MatchEvaluator(FixStyleAndScript), 
				RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}

		private void FixInput() 
		{
			int nStart = 0;
			int nPos = 0;
			
			while ( nPos >= 0 ) 
			{
				string sSearch = "<input type=\"hidden\"";
				nPos = m_sXHTML.IndexOf(sSearch, nStart);
				if ( nPos > 0 ) 
				{
					nStart = nPos + sSearch.Length;
					m_sXHTML = m_sXHTML.Insert(nPos, "<pre>");

					int nEnd = m_sXHTML.IndexOf(">", nStart);
					if ( nEnd > 0 ) 
					{
						m_sXHTML = m_sXHTML.Insert(nEnd+1, "</pre>");
					}
				}
			}
		}

		private void AddSelfClose(string sTagName) 
		{
			int nStart = 0;
			int nPos = 0;
			
			while ( nPos >= 0 ) 
			{
				string sSearch = "<" + sTagName;
				nPos = m_sXHTML.IndexOf(sSearch, nStart);
				if ( nPos > 0 ) 
				{
					nStart = nPos + 1;
					int nEnd = m_sXHTML.IndexOf(">", nStart);
					if ( nEnd > 0 ) 
					{
						if ( m_sXHTML[nEnd-1] != '/' ) 
						{
							m_sXHTML = m_sXHTML.Insert(nEnd, " /");
						}
					}
				}
			}
		}

		private void RemoveAttribute(string sTagName, string sAttrName) 
		{
			int nStart = 0;
			int nPos = 0;
			
			while ( nPos >= 0 ) 
			{
				string sSearch = "<" + sTagName + " " + sAttrName + "=\"";
				nPos = m_sXHTML.IndexOf(sSearch, nStart);
				if ( nPos > 0 ) 
				{
					// start of attribute
					nStart = nPos + sTagName.Length + 1;

					// search the " of the end of the value
					int nEnd = m_sXHTML.IndexOf("\"", nPos + sSearch.Length + 1);
					if ( nEnd > 0 ) 
					{
						m_sXHTML = m_sXHTML.Remove(nStart, nEnd-nStart+1);
					}
				}
			}
		}

		private void maskScript() 
		{
			// Add <![CDATA[ ... ]]> to mask script
			m_sXHTML = Regex.Replace(m_sXHTML, 
				@"(<script[^<]*>){1}(?(?=\s*<!--)(\s*<!--)(\s*.*?)(//\s*-->)|(\s*.*?))\s*(</script>){1}",
				new MatchEvaluator(FixStyleAndScript), 
				RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}

		private string FixStyleAndScript(Match m) 
		{
			string ret="";
			string st, ed;

			if (m_bXmlCDATA) 
			{
				st = "\r\n<![CDATA[\r\n";
				ed = "\r\n]]>\r\n";
			} 
			else 
			{
				st = "\r\n<!--\r\n";
				ed = "\r\n//-->\r\n";
			}

			if (m.Groups[2].ToString().Trim()=="" && m.Groups[4].ToString().Trim()=="")
				st = ed = "";

			ret = m.Groups[1].ToString() + st;
			ret += m.Groups[2].ToString() + m.Groups[4].ToString() + ed + m.Groups[5].ToString();
			return ret;
		}

		private string ToLowerCase(Match m) 
		{
			return m.ToString().ToLower();
		}
		#endregion


		/// <summary>
		/// SchreibFussZeile hängt an jede Antwort ein Copyright und Impressum dran.
		/// Wird von  aufgerufen.
		/// </summary>
		private string FussZeile()
		{
			
			string baseLink = OliWeb.Klassen.Helper.MakeBaseLink();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			// break
			sb.Append("<br />");
			sb.Append("<hr size='1px'></hr>");
			sb.Append("<div align='left' style='font-size: x-small; clear:both;'>");


			// Home
			sb.Append(" <a href='" + baseLink + "default.aspx'><img border='0px' alt='home' src='" + baseLink + "images/oli-it_36.jpg' /></a>");
			
			// CopyRight
			sb.Append(OliEngine.OliCommon.CopyRight );
			
			// Nutzungsbedingungen 
			sb.Append(" | <a href='" + baseLink + "nutzungsbedingungen.aspx'>Nutzungsbedingungen</a>");

			// Impressum
			sb.Append(" | <a href='" + baseLink + "Impressum.aspx'>Impressum</a>");
			
			//			if(user != null && user.BookMark.Length > 1)
			//			{
			//				sb.Append(" | <span class=bookmark>");
			//				sb.Append("<a href=" + baseLink + "/Sites/Elemente/LinkToThisPage.aspx" + user.BookMark);
			//				sb.Append(" target='oliitlinkpage' title='Direkt auf diese Seite verlinken'>Link to this page</a></span>");
			//			}

			// aktuelle Systemzeit
			sb.Append(" | " + DateTime.Now.ToString());

			// validate xhtml
			sb.Append(" | <a href='http://validator.w3.org/check?uri=referer'>XHTML 1.0</a>"); 

			// xhtml prozess time
			TimeSpan ts = DateTime.Now - m_StartTime;
			sb.Append(" | " + ts.TotalMilliseconds.ToString() + " ms");
			
			// Schlusszeile zu  ------------------- danach kommt /form /body /html OUT und VORBEI
			sb.Append("</div>");
			return(sb.ToString());
		}
	}
}
