using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class AnglerRdfMapper
    {
        public static void WriteXml(XmlWriter xw, Angler angler)
        {
            if (xw == null) return;
            if (angler == null) return;

            xw.WriteStartElement("nlo", "stammAngler", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteStartElement("nlo", "Angler", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"Angler/?{angler.AnglerGuid}");
            xw.WriteAttributeString("nlo", "name", "http://nulllogicone.net/schema.rdfs#", angler.Angler1 ?? string.Empty);
            xw.WriteAttributeString("nlo", "anglerGuid", "http://nulllogicone.net/schema.rdfs#", angler.AnglerGuid.ToString());
            xw.WriteStartElement("nlo", "resource", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"http://nulllogicone.net/Angler/{angler.AnglerGuid}.rdf");
            xw.WriteEndElement(); // nlo:resource
            xw.WriteEndElement(); // nlo:Angler
            xw.WriteEndElement(); // nlo:stammAngler
        }
    }
}

