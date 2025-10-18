using System;
using System.Collections;

namespace OliEngine.OliPresentation
{
	/// <summary>
	/// Zusammendfassende Beschreibung für NetzKette.
	/// 
	/// Die Klasse verhält sich wie ein Stack.
	/// Es können NetzKettenGlieder (eingebettete Klasse)
	/// eingefügt werden (ohne doppelte NetzID)
	/// 
	/// </summary>
	public class NetzKette
	{
		ArrayList _nk;

		public NetzKette() 
		{
			_nk = new ArrayList();
		}

		// KettenGlieder gibt ein Array von NetzKettenGliedern zurück
		public ArrayList KettenGlieder
		{
			get{return(_nk);}
		}

		// Count der Glieder
		public int Count
		{
			get{return(_nk.Count);}
		}
		
		// neues KettenGlied dem Array hinzufügen
		// gelingt nur wenn NetzID noch nicht vorhanden
		public void Push(NetzKettenGlied nkg)
		{
			if (nkg != null)
			{
				try
				{
					if(nkg.Netz.Length>0)
					{
						bool gibts = false;
						foreach(NetzKettenGlied n in _nk)
						{
							if (n.NID == nkg.NID)
								gibts = true;
						}
						if(!gibts)
							_nk.Add(nkg);	
						
					}
				}
				catch
				{}
			}
		}

		// Gibt das zuletzt eingefügte KettenGlied zurück
		// und entfert es aus dem Array
		public NetzKettenGlied Pop()
		{
			NetzKettenGlied nkg;
			nkg = (NetzKettenGlied) _nk[_nk.Count-1];
			_nk.Remove(nkg);
			return(nkg);
		}
	}

	//
	// NetzKettenGlied
	//
	// eigene Klasse (wird von NetzKette verwendet)
	public class NetzKettenGlied
	{
		string _netz;
		int _nid;

		public NetzKettenGlied()
		{}

		public NetzKettenGlied(string netz, int nid)
		{
			_netz = netz;
			_nid = nid;
		}

		public string Netz
		{
			get{return(_netz);}
			set{_netz = value;}
		}

		public int NID
		{
			get{return(_nid);}
			set{_nid = value;}
		}

	}
}
