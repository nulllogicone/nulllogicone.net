using System;
using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class PostItRdfMapper
    {
        // Writes a StammPostIt (view) fragment similar to legacy MakeStammRDF
        public static void WriteXml(XmlWriter xw, StammPostIt postIt)
        {
            if (xw == null) return;
            if (postIt == null) return;

            xw.WriteStartElement("nlo", "stammPostIt", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteStartElement("nlo", "PostIt", "http://nulllogicone.net/schema.rdfs#");
            if (postIt.PostItGuid.HasValue)
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"PostIt/?{postIt.PostItGuid}");
            xw.WriteAttributeString("nlo", "postItGuid", "http://nulllogicone.net/schema.rdfs#", postIt.PostItGuid?.ToString() ?? string.Empty);
            xw.WriteAttributeString("nlo", "flowKook", "http://nulllogicone.net/schema.rdfs#", (postIt.Bezahlt).ToString().Replace(',', '.'));
            xw.WriteAttributeString("nlo", "frist", "http://nulllogicone.net/schema.rdfs#", postIt.Frist.HasValue ? postIt.Frist.Value.ToString("s") : string.Empty);
            xw.WriteStartElement("nlo", "resource", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"http://nulllogicone.net/PostIt/{postIt.PostItGuid}.rdf");
            xw.WriteEndElement(); // nlo:resource
            xw.WriteEndElement(); // nlo:PostIt
            xw.WriteEndElement(); // nlo:stammPostIt
        }
    }
}

