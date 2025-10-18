using OliEngine.DataSetTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OliEngine.DataSetTypes.PostItDataSet;

namespace OliEngine.EventGrid.DataObjects
{
    internal class PostIt
    {
        public PostIt()
        {

        }
        public PostIt(PostItRow postItRow)
        {
            Title = postItRow.Titel;
        }
        public string Title {  get; set; }
    }
}
