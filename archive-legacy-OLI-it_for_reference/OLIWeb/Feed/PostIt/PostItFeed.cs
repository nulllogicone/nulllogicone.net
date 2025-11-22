using System;
using System.Data;



using OliEngine.DataSetTypes;
using OliEngine.OliDataAccess;

namespace OliWeb.Feed.PostIt
{
	/// <summary>
	/// PostItFeed.
	/// </summary>
	public class PostItFeed : OliWeb.Feed.OLIitFeed
	{

		public PostItFeed()
		{
		}

//		public PostItFeed(Guid sguid) : base(sguid)
//		{
//			PostItList pl = new PostItList(s.StammRow);
//			this._table = pl.PostIt;
//		}

		public Guid StammGuid
		{
			set
			{
				this.sguid = value;
				this.s = new Stamm(value);
				PostItList pl = new PostItList(s.StammRow);
				this._table = pl.PostIt;

			}
		}
		
		public DataTable DataSource
		{
			set
			{
				this._table = value;
			}
		}

		public override string ToString()
		{
			string s = "";
			foreach(DataRow dr in this._table.Rows)
			{
				PostItDataSet.PostItRow pr = (PostItDataSet.PostItRow)dr;
				
				s += "<div class=postit>";
				if(!pr.IsTitelNull())
				{
					s += "<div class=titel>" + pr.Titel + "</div>";
				}
				s += pr.PostIt ;
				s+= "</div>";
			}
			return s;
		}

		public string ToXml()
		{
			string s = "";


			return s;
		}


	}
}

