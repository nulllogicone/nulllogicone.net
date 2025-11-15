using System;

namespace OliEngine.OliPresentation
{
	public enum StringArt {Code, Angler}

	/// <summary>
	/// Zusammendfassende Beschreibung f�r String.
	/// </summary>
	public class myStrings 
	{
		protected StringArt sa;

		protected int n;
		protected int k;
		protected int b;
		protected int z;

		protected string netz;
		protected string knoten;
		protected string baum;
		protected string zweig;

		protected int verb;
		protected int attrib;

		public myStrings()
		{
		}

		public StringArt StringArt
		{
			get{return(sa);}
			set{sa = value;}
		}
		public int N
		{
			get{return(n);}
			set{n=value;}
		}
		public int K
		{
			get{return(k);}
			set{k=value;}
		}
		public int B
		{
			get{return(b);}
			set{b=value;}
		}
		public int Z
		{
			get{return(z);}
			set{z=value;}
		}

		public string Netz
		{
			get{return(netz);}
			set{netz=value;}
		}
		public string Knoten
		{
			get{return(knoten);}
			set{knoten=value;}
		}
		public string Baum
		{
			get{return(baum);}
			set{baum=value;}
		}
		public string Zweig
		{
			get{return(zweig);}
			set{zweig=value;}
		}

		// Steht f�r OLIs oder ILOs
		public int Verb
		{
			get{return(verb);}
			set{verb=value;}
		}
		// Steht f�r get oder fit
		public int Attrib
		{
			get{return(attrib);}
			set{attrib=value;}
		}
	}
}

