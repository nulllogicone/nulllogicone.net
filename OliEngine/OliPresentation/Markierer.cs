using System;
using System.Collections;
using System.Data;

using OliEngine.OliDataAccess;
using OliEngine.OliMiddleTier.OLIs;

namespace OliEngine.OliPresentation
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Markierer.
	/// </summary>
	public class Markierer
	{
		protected myStringsDataSet myStrings;
		protected OliEngine.OliDataAccess.ShortCuts SC;

		public Markierer()
		{
			myStrings = new myStringsDataSet();
		}


		public void Markiere(myStrings strings)
		{
			myStringsDataSet.myStringsRow newSR = (myStringsDataSet.myStringsRow)myStrings.myStrings.NewRow();
			if (strings != null)
			{
				newSR.N  = strings.N;
				newSR.Netz = strings.Netz ;
				newSR.K = strings.K;
				newSR.Knoten = strings.Knoten;

				newSR.B = strings.B;
				newSR.Baum = strings.Baum;
				newSR.Z = strings.Z;
				newSR.Zweig = strings.Zweig;

				newSR.Verb = strings.Verb;
				newSR.Attrib = strings.Attrib;
			}		
			try
			{
				myStrings.myStrings.AddmyStringsRow(newSR);
			}
			catch (System.Data.ConstraintException)
			{
				// wurde wohl doppelte nkbz eingefügt
			}

		}

		public DataSet Zeilen
		{
			get
			{
				return(myStrings);
			}
		}
	}
}
