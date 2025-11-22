using System;
using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class TopLabRdfMapper
    {
        // Writes a StammTopLab (view) fragment similar to legacy MakeStammRDF
        public static void WriteXml(XmlWriter xw, StammTopLab topLab)
        {
            if (xw == null) return;
            if (topLab == null) return;

            xw.WriteStartElement("nlo", "stammTopLab", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteStartElement("nlo", "TopLab", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"TopLab/?{topLab.TopLabGuid}");
            xw.WriteAttributeString("nlo", "topLabGuid", "http://nulllogicone.net/schema.rdfs#", topLab.TopLabGuid.ToString());
            xw.WriteAttributeString("nlo", "toll", "http://nulllogicone.net/schema.rdfs#", topLab.Lohn.HasValue ? topLab.Lohn.Value.ToString().Replace(',', '.') : string.Empty);
            xw.WriteStartElement("nlo", "resource", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"http://nulllogicone.net/TopLab/{topLab.TopLabGuid}.rdf");
            xw.WriteEndElement(); // nlo:resource
            xw.WriteEndElement(); // nlo:TopLab
            xw.WriteEndElement(); // nlo:stammTopLab
        }
    }
}

