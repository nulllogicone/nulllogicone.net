using System;



namespace OliEngine.OliPresentation
{
	/// <summary>
	/// Zusammendfassende Beschreibung für WeiterKletternEventArgs.
	/// Diese EreignisArgumente werden benötigt
	/// wenn von Knoten oder Zweigen in
	/// Netze oder Bäume weitergeklettert wird	
	/// </summary>
	
	public class WeiterKletternEventArgs : EventArgs
	{
		public enum KletternIn {Netz, Baum};	
		
		private KletternIn _inWas;
		private int _inID;
		


		protected myStrings _vonString;

		public WeiterKletternEventArgs()
		{}



		public myStrings VonString
		{
			get {return _vonString;}
			set {_vonString = value;}
		}

		public KletternIn inWas
		{
			get{return(_inWas);}
			set{_inWas = value;}
		}
		public int inID
		{
			get{return(_inID);}
			set{_inID = value;}
		}



	}
}
