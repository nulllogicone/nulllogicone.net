using System;
using System.IO;
using System.Text;
using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    public static class PostItRdfMapper
    {
        /// <summary>
        /// Generates a complete RDF/XML document for a PostIt entity.
        /// </summary>
        public static string ToRdfXml(PostIt postIt)
        {
            if (postIt == null) throw new ArgumentNullException(nameof(postIt));

            using var stream = new MemoryStream();
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = false
            };

            using (var xw = XmlWriter.Create(stream, settings))
            {
                // start document
                xw.WriteStartDocument();

                // <rdf:RDF ...>
                xw.WriteStartElement("rdf", "RDF", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                xw.WriteAttributeString("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("xmlns", "nlo", null, "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("xml", "base", null, "http://nulllogicone.net/");

                // <nlo:PostIt rdf:about="PostIt/{guid}">
                xw.WriteStartElement("nlo", "PostIt", "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"PostIt/{postIt.PostItGuid}");

                // Dublin Core: date + publisher
                xw.WriteComment("Dublin Core");
                xw.WriteElementString("dc", "date", "http://purl.org/dc/elements/1.1/", DateTime.UtcNow.ToString("s"));
                xw.WriteStartElement("dc", "publisher", "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", "http://nulllogicone.net");
                xw.WriteEndElement();

                // PostIt fields
                xw.WriteComment("PostIt Felder");
                xw.WriteElementString("nlo", "postItGuid", "http://nulllogicone.net/schema.rdfs#", postIt.PostItGuid.ToString());
                xw.WriteElementString("nlo", "titel", "http://nulllogicone.net/schema.rdfs#", postIt.Titel ?? string.Empty);
                xw.WriteElementString("nlo", "postIt", "http://nulllogicone.net/schema.rdfs#", postIt.PostIt1 ?? string.Empty);
                xw.WriteElementString("nlo", "datum", "http://nulllogicone.net/schema.rdfs#", postIt.Datum.ToString("s"));
                xw.WriteElementString("nlo", "kooK", "http://nulllogicone.net/schema.rdfs#", postIt.KooK.ToString().Replace(',', '.'));
                xw.WriteElementString("nlo", "postItZust", "http://nulllogicone.net/schema.rdfs#", postIt.PostItZust?.ToString() ?? string.Empty);
                xw.WriteElementString("nlo", "url", "http://nulllogicone.net/schema.rdfs#", postIt.Url ?? string.Empty);
                xw.WriteElementString("nlo", "hits", "http://nulllogicone.net/schema.rdfs#", postIt.Hits.ToString());
                xw.WriteElementString("nlo", "typ", "http://nulllogicone.net/schema.rdfs#", postIt.Typ ?? string.Empty);

                // Datei resource if present
                xw.WriteStartElement("nlo", "datei", "http://nulllogicone.net/schema.rdfs#");
                if (!string.IsNullOrWhiteSpace(postIt.Datei))
                {
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", postIt.Datei);
                }
                else
                {
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", string.Empty);
                }
                xw.WriteEndElement();

                // Close PostIt element
                xw.WriteEndElement(); // nlo:PostIt

                // Close RDF element
                xw.WriteEndElement(); // rdf:RDF

                xw.WriteEndDocument();
                xw.Flush();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        // Writes a StammPostIt (view) fragment similar to legacy MakeStammRDF
        public static void WriteXml(XmlWriter xw, StammPostIt postIt)
        {
            if (xw == null) return;
            if (postIt == null) return;

            xw.WriteStartElement("nlo", "stammPostIt", "http://nulllogicone.net/schema.rdfs#");
            xw.WriteStartElement("nlo", "PostIt", "http://nulllogicone.net/schema.rdfs#");
            if (postIt.PostItGuid.HasValue)
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"PostIt/{postIt.PostItGuid}");
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

