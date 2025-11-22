using System;
using System.Xml;
using System.IO;
using System.Text;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class AnglerRdfMapper
    {
        public static string ToRdfXml(Angler angler)
        {
            if (angler == null) throw new ArgumentNullException(nameof(angler));

            using var stream = new MemoryStream();
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = false
            };

            using (var xw = XmlWriter.Create(stream, settings))
            {
                xw.WriteStartDocument();

                // <rdf:RDF ...>
                xw.WriteStartElement("rdf", "RDF", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                xw.WriteAttributeString("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("xmlns", "nlo", null, "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("xml", "base", null, "http://nulllogicone.net/");

                // <nlo:Angler rdf:about="Angler/?{guid}">
                xw.WriteStartElement("nlo", "Angler", "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"Angler/?{angler.AnglerGuid}");

                // Dublin Core
                xw.WriteComment("Dublin Core");
                xw.WriteElementString("dc", "date", "http://purl.org/dc/elements/1.1/", DateTime.UtcNow.ToString("s"));

                // Angler fields
                xw.WriteComment("Angler Felder");
                xw.WriteElementString("nlo", "anglerGuid", "http://nulllogicone.net/schema.rdfs#", angler.AnglerGuid.ToString());
                xw.WriteElementString("nlo", "name", "http://nulllogicone.net/schema.rdfs#", angler.Angler1 ?? string.Empty);
                xw.WriteElementString("nlo", "datum", "http://nulllogicone.net/schema.rdfs#", angler.Datum.ToString("s"));
                xw.WriteElementString("nlo", "beschreibung", "http://nulllogicone.net/schema.rdfs#", angler.Beschreibung ?? string.Empty);

                // Close nlo:Angler
                xw.WriteEndElement();

                // Close rdf:RDF
                xw.WriteEndElement();
                xw.WriteEndDocument();
                xw.Flush();
            }

            stream.Position = 0;
            using var sr = new StreamReader(stream, Encoding.UTF8);
            return sr.ReadToEnd();
        }

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

