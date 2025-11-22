using System;
using System.Web;
using System.Collections;

using OliEngine.OliMiddleTier.OLIx;

namespace OliWeb.Controls.BlaetterWald
{
	/// <summary>
	/// NetzKette.
	/// Sie ist Singleton in einer Session und nimmt die �bergeordneten
	/// Netze auf.
	/// </summary>
	public class NetzKette
	{
		ArrayList _nk = new ArrayList();

		private NetzKette()
		{
		}

		public static NetzKette Instance()
		{
			HttpContext ctx = HttpContext.Current;
			if(ctx.Session["netzkette"] == null)
			{
				ctx.Session["netzkette"] = new NetzKette();
			}
			return (NetzKette)ctx.Session["netzkette"];
		}

		public void Push(Netz netz)
		{
			// Wenn das Netz noch nicht enthalten ist
			// wird es eingef�gt 
			int idx = IndexOf(netz);

			if(idx == -1)
			{
				_nk.Add(netz);
			}
												  
		}

		// Index eines Netz
		private int IndexOf(Netz netz)
		{
			int idx = -1;
			Netz n;
			foreach(object o in _nk)
			{
				n = (Netz)o;
				if(n.NetzRow.NetzGuid == netz.NetzRow.NetzGuid)
				{
					idx = _nk.IndexOf(o);
					return idx;
				}
			}
			return idx;
		}

		public Netz GiveTopNetz()
		{
			return (Netz)_nk[_nk.Count - 1];
		}

		public void CutBehind(Netz netz)
		{
			int idx = IndexOf(netz);
			if(idx >= 0 && idx < _nk.Count)
			{
				try
				{
					_nk.RemoveRange(idx + 1, _nk.Count - idx - 1);	
				}
				catch(Exception ex)
				{
					string exep = ex.Message;
				}
			}
		}

		public string MakeHtml()
		{
			string s = "";
			Netz n;
			foreach(object o in _nk)
			{
				n = (Netz)o;
				s += " > " + MakeLink(n);
			}
			return s;
		}

		private string MakeLink(Netz netz)
		{
			string s = "";
			s += "<a href=BlaetterWald.aspx?nguid=";
			s += netz.NetzRow.NetzGuid.ToString();
			s += ">" + netz.NetzRow.Netz + "</a>";
			return s;
		}
		

	}
}

