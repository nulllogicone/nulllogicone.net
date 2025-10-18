using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliEngine.EventGrid
{
    internal class EventType
    {
        public EventType(string value) => Value = value;
        public string Value { get; private set; }
        public static EventType PostItUpdated { get { return new EventType("PostIt-Updated"); } }
        public static EventType StammUpdated { get { return new EventType("Stamm-Updated"); } }
    }
}
