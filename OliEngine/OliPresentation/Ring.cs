using System;

namespace OliEngine.OliPresentation
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Ringe.
	/// </summary>
	public class Ring 
	{
		protected int cid;		
		protected myStrings nkbz;
		protected int O;
		protected int g;
		protected int vonK;
		protected int vonZ;

		public Ring(int cid)
		{
			this.cid = cid;
		}

		public Ring(int cid, myStrings nkbz)
		{
			this.cid = cid;
			this.nkbz = nkbz;
		}

		public int CID
		{
			get{return(cid);}
		}

		public int OLIs
		{
			get{return(O);}
			set{O=value;}
		}
		public int get
		{
			get{return(g);}
			set{g=value;}
		}
	}
}
