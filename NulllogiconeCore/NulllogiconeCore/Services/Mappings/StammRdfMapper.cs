using System;
using System.IO;
using System.Text;
using System.Xml;
using NulllogiconeCore.Models;

namespace NulllogiconeCore.Services.Mappings
{
    /// <summary>
    /// Maps a Stamm entity to an RDF/XML string.
    /// This is a lightweight migration of the older MakeStammRDF logic adapted
    /// to use the API model types instead of DataRows/DataTables.
    /// </summary>
    public static class StammRdfMapper
    {
        public static string ToRdfXml(Stamm stamm, IEnumerable<StammPostIt>? postIts = null, IEnumerable<StammTopLab>? topLabs = null)
        {
            if (stamm == null) throw new ArgumentNullException(nameof(stamm));

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

                // <nlo:Stamm rdf:about="Stamm/{guid}">
                xw.WriteStartElement("nlo", "Stamm", "http://nulllogicone.net/schema.rdfs#");
                xw.WriteAttributeString("rdf", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", $"Stamm/{stamm.StammGuid}");

                // Dublin Core: date + publisher
                xw.WriteComment("Dublin Core");
                xw.WriteElementString("dc", "date", "http://purl.org/dc/elements/1.1/", DateTime.UtcNow.ToString("s"));
                xw.WriteStartElement("dc", "publisher", "http://purl.org/dc/elements/1.1/");
                xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", "http://nulllogicone.net");
                xw.WriteEndElement();

                // Stamm fields
                xw.WriteComment("Stamm Felder");
                xw.WriteElementString("nlo", "stammGuid", "http://nulllogicone.net/schema.rdfs#", stamm.StammGuid.ToString());
                xw.WriteElementString("nlo", "name", "http://nulllogicone.net/schema.rdfs#", stamm.Stamm1 ?? string.Empty);
                xw.WriteElementString("nlo", "datum", "http://nulllogicone.net/schema.rdfs#", stamm.Datum.ToString("s"));
                xw.WriteElementString("nlo", "beschreibung", "http://nulllogicone.net/schema.rdfs#", stamm.Beschreibung ?? string.Empty);

                // Datei resource if present
                xw.WriteStartElement("nlo", "datei", "http://nulllogicone.net/schema.rdfs#");
                if (!string.IsNullOrWhiteSpace(stamm.Datei))
                {
                    // keep as-is; the old code used OliUtil.MakeImageSrc which is not available here
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", stamm.Datei);
                }
                else
                {
                    xw.WriteAttributeString("rdf", "resource", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", string.Empty);
                }
                xw.WriteEndElement();

                // boundKook (KooK)
                if (stamm.KooK.HasValue)
                {
                    // format with dot as decimal separator
                    var kook = stamm.KooK.Value.ToString().Replace(',', '.');
                    xw.WriteElementString("nlo", "boundKook", "http://nulllogicone.net/schema.rdfs#", kook);
                }

                // Anglers (references)
                xw.WriteComment("StammAngler - seine Filterprofile");
                if (stamm.Anglers != null)
                {
                    foreach (var ag in stamm.Anglers)
                    {
                        AnglerRdfMapper.WriteXml(xw, ag);
                    }
                }

                // StammPostIt - seine Nachrichten (if provided)
                xw.WriteComment("StammPostIt - seine Nachrichten");
                if (postIts != null)
                {
                    foreach (var pr in postIts)
                    {
                        PostItRdfMapper.WriteXml(xw, pr);
                    }
                }

                // StammTopLab - seine Antworten (if provided)
                xw.WriteComment("StammTopLab - seine Antworten");
                if (topLabs != null)
                {
                    foreach (var tl in topLabs)
                    {
                        TopLabRdfMapper.WriteXml(xw, tl);
                    }
                }

                // close nlo:Stamm
                xw.WriteEndElement();

                // close rdf:RDF
                xw.WriteEndElement();
                xw.WriteEndDocument();
                xw.Flush();
            }

            stream.Position = 0;
            using var sr = new StreamReader(stream, Encoding.UTF8);
            return sr.ReadToEnd();
        }
    }
}

