using System;
using System.Web;

namespace nulllogicone
{
	public class Handler : IHttpHandler 
	{
    
		
		public void ProcessRequest(HttpContext context) 
		{
			HttpResponse Response = context.Response;
			Response.Clear();

			int posS = context.Request.RawUrl.IndexOf("Stamm/");
			if (posS > 0)
			{
				string sguidstr = context.Request.RawUrl.Substring(posS + 6, 36);
				Guid sguid = new Guid(sguidstr);
				OliEngine.OliMiddleTier.OLIs.Stamm s = new OliEngine.OliMiddleTier.OLIs.Stamm(sguid);

				Response.Write(s.MakeStammRDF());
				return;
			}

			int posA = context.Request.RawUrl.IndexOf("Angler/");
			if (posA > 0)
			{
				string aguidstr = context.Request.RawUrl.Substring(posA + 7, 36);
				Guid aguid = new Guid(aguidstr);
				OliEngine.OliMiddleTier.OLIs.Angler a = new OliEngine.OliMiddleTier.OLIs.Angler(aguid);

				Response.Write(a.MakeAnglerRDF());
				return;
			}

			int posP = context.Request.RawUrl.IndexOf("PostIt/");
			if (posP > 0)
			{
				string pguidstr = context.Request.RawUrl.Substring(posP + 7, 36);
				Guid pguid = new Guid(pguidstr);
				OliEngine.OliMiddleTier.OLIs.PostIt p = new OliEngine.OliMiddleTier.OLIs.PostIt(pguid);

				Response.Write(p.MakePostItRDF());
				return;
			}

			int posC = context.Request.RawUrl.IndexOf("Code/");
			if (posC > 0)
			{
				string cstr = context.Request.RawUrl.Substring(posC + 5, 36);
				Guid cguid = new Guid(cstr);
				OliEngine.OliMiddleTier.OLIs.Code c = new OliEngine.OliMiddleTier.OLIs.Code(cguid);

				Response.Write(c.MakeCodeRDF());
				return;
			}

			int posT = context.Request.RawUrl.IndexOf("TopLab/");
			if (posT > 0)
			{
				string tguidstr = context.Request.RawUrl.Substring(posT + 7, 36);
				Guid tguid = new Guid(tguidstr);
				OliEngine.OliMiddleTier.OLIs.TopLab t = new OliEngine.OliMiddleTier.OLIs.TopLab (tguid);

				Response.Write(t.MakeTopLabRDF());
				return;
			}

			int posNKBZ = context.Request.RawUrl.IndexOf("NKBZ/");
			if(posNKBZ > 0)
			{
				OliEngine.OliMiddleTier.OLIx.NKBZ nkbz = OliEngine.OliMiddleTier.OLIx.NKBZ.Instance();
				Response.Write(nkbz.MakeWortraumRDF());
				return;
			}

		}

		public bool IsReusable 
		{
			get 
			{
				return true;
			}
		} 
	}
}
