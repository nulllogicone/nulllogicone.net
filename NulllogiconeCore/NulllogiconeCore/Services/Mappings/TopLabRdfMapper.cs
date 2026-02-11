using System;
using System.IO;
using System.Text;
using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class TopLabRdfMapper
    {
        // Full RDF/XML for a TopLab entity
        public static string ToRdfXml(TopLab topLab)
        {
            if (topLab == null) throw new ArgumentNullException(nameof(topLab));

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

                xw.WriteStartElement("rdf", "RDF", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                xw.WriteAttributeString("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("xmlns", "nlo", null, "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("xml", "base", null, "http://nulllogicone.net/");

                xw.WriteStartElement("nlo", "TopLab", "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"TopLab/{topLab.TopLabGuid}");

                // Dublin Core
                xw.WriteElementString("dc", "date", "http://purl.org/dc/elements/1.1/", DateTime.UtcNow.ToString("s"));
                xw.WriteStartElement("dc", "publisher", "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", "http://nulllogicone.net");
                xw.WriteEndElement();

                // Fields
                xw.WriteElementString("nlo", "topLabGuid", "http://nulllogicone.net/schema.rdfs#", topLab.TopLabGuid.ToString());
                xw.WriteElementString("nlo", "stammGuid", "http://nulllogicone.net/schema.rdfs#", topLab.StammGuid.ToString());
                xw.WriteElementString("nlo", "postItGuid", "http://nulllogicone.net/schema.rdfs#", topLab.PostItGuid.ToString());
                xw.WriteElementString("nlo", "titel", "http://nulllogicone.net/schema.rdfs#", topLab.Titel ?? string.Empty);
                xw.WriteElementString("nlo", "topLab", "http://nulllogicone.net/schema.rdfs#", topLab.TopLab1 ?? string.Empty);
                xw.WriteElementString("nlo", "url", "http://nulllogicone.net/schema.rdfs#", topLab.Url ?? string.Empty);
                xw.WriteElementString("nlo", "lohn", "http://nulllogicone.net/schema.rdfs#", topLab.Lohn.ToString().Replace(',', '.'));
                xw.WriteElementString("nlo", "datum", "http://nulllogicone.net/schema.rdfs#", topLab.Datum.ToString("s"));
                xw.WriteElementString("nlo", "typ", "http://nulllogicone.net/schema.rdfs#", topLab.Typ ?? string.Empty);

                // Datei resource
                xw.WriteStartElement("nlo", "datei", "http://nulllogicone.net/schema.rdfs#");
                if (!string.IsNullOrWhiteSpace(topLab.Datei))
                {
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", topLab.Datei);
                }
                else
                {
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", string.Empty);
                }
                xw.WriteEndElement();

                xw.WriteEndElement(); // nlo:TopLab
                xw.WriteEndElement(); // rdf:RDF
                xw.WriteEndDocument();
                xw.Flush();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        // Fragment writer used within Stamm RDF mapping
        public static void WriteXml(XmlWriter xw, StammTopLab topLab)
        {
            if (xw == null || topLab == null) return;

            xw.WriteStartElement("nlo", "stammTopLab", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteStartElement("nlo", "TopLab", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"TopLab/{topLab.TopLabGuid}");
            xw.WriteAttributeString("nlo", "topLabGuid", "http://nulllogicone.net/schema.rdfs#", topLab.TopLabGuid.ToString());
            xw.WriteAttributeString("nlo", "toll", "http://nulllogicone.net/schema.rdfs#", topLab.Lohn.HasValue ? topLab.Lohn.Value.ToString().Replace(',', '.') : string.Empty);
            xw.WriteStartElement("nlo", "resource", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"http://nulllogicone.net/TopLab/{topLab.TopLabGuid}.rdf");
            xw.WriteEndElement();
            xw.WriteEndElement();
            xw.WriteEndElement();
        }
    }
}

