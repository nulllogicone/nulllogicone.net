using System;
using System.Web;
using System.Collections;

using OliEngine.OliMiddleTier.OLIx;

namespace OliWeb.Controls.BlaetterWald
{
	/// <summary>
	/// BaumKette.
	/// </summary>
	public class BaumKette
	{
		ArrayList _bk = new ArrayList();

		private BaumKette()
		{
		}

		public static BaumKette Instance()
		{
			HttpContext ctx = HttpContext.Current;
			if(ctx.Session["baumkette"] == null)
			{
				ctx.Session["baumkette"] = new BaumKette();
			}
			return (BaumKette)ctx.Session["baumkette"];
		}

		public void Push(Baum baum)
		{

			// Wenn der Baum noch nicht drin ist
			// wird er eingef�gt
			int idx = IndexOf(baum);

			if(idx == -1)
			{
				_bk.Add(baum);
			}
		}

		// Index eines Baum
		private int IndexOf(Baum baum)
		{
			int idx = -1;
			Baum b;
			foreach(object o in _bk)
			{
				b = (Baum)o;
				if(b.BaumRow.BaumGuid == baum.BaumRow.BaumGuid)
				{
					idx = _bk.IndexOf(o);
					return idx;
				}
			}
			return idx;
		}

		public void CutBehind(Baum baum)
		{
			int idx = IndexOf(baum);
			if(idx >= 0 && idx < _bk.Count)
			{
				try
				{
					_bk.RemoveRange(idx + 1, _bk.Count - idx - 1);	
				}
				catch(Exception ex)
				{
					string exep = ex.Message;
				}
			}
		}

		public void Clear()
		{
			_bk.Clear();
		}

		public string MakeHtml()
		{
			string s = "";
			Baum b;
			foreach(object o in _bk)
			{
				b = (Baum)o;
				s += " > " + MakeLink(b);
			}
			return s;
		}

		private string MakeLink(Baum baum)
		{
			string s = "";
			s += "<a href=BlaetterWald.aspx?nguid=";
			s += NetzKette.Instance().GiveTopNetz().NetzRow.NetzGuid.ToString();
			s += "&bguid=" + baum.BaumRow.BaumGuid + ">";
			s += baum.BaumRow.Baum + "</a>";
			return s;
		}

	}
}

