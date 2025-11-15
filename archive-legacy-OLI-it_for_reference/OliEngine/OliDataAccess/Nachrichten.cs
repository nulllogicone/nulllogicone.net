using System;
using System.Data;
using System.Data.SqlClient;

using OliEngine.DataSetTypes;

namespace OliEngine.OliDataAccess
{
	/// <summary>
	/// Zusammendfassende Beschreibung f�r Nachrichten.
	/// </summary>
	public class Nachrichten : NachrichtenDataSet
	{
		SqlDataAdapter nad;

		public Nachrichten()
		{
			SqlConnection con = OliEngine.OliCommon.OLIsConnection ;

			nad = new SqlDataAdapter("Select * From Nachrichten ORDER BY Datum DESC", con);
			SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
			nad.Fill(this.Nachrichten );
		}

		public Nachrichten(StammDataSet.StammRow stammRow)
		{
			SqlConnection con = OliEngine.OliCommon.OLIsConnection ;

			nad = new SqlDataAdapter("Select * From Nachrichten WHERE StammGuid='" + stammRow.StammGuid + "' ORDER BY Datum DESC", con);
			SqlCommandBuilder ncb = new SqlCommandBuilder(nad);
			nad.Fill(this.Nachrichten );
		}

		public int UpdateNachrichten()
		{
			try
			{
				return(nad.Update(this.Nachrichten));
			}
			catch
			{
				return 0;
			}
		}

		public static void NeueNachricht(Guid anSguid, string text)
		{
			Nachrichten n = new Nachrichten();
			NachrichtenDataSet.NachrichtenRow nr = n.Nachrichten.NewNachrichtenRow();
			nr.NachrichtenGuid = Guid.NewGuid();
			nr.Datum = DateTime.Now;
			nr.StammGuid = anSguid;
			nr.Nachricht = text;
			n.Nachrichten.AddNachrichtenRow(nr);
			n.UpdateNachrichten();
		}
	}
}

