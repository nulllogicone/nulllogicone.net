using Microsoft.AspNetCore.Http;

namespace NulllogiconeCore.Extensions;

public enum RepresentationFormat
{
    Html,
    Json,
    Rdf
}

public static class ContentNegotiationExtensions
{
    public static RepresentationFormat DetermineFormat(this HttpContext context, string? extension)
    {
        // 1. Check Extension (Explicit Override)
        if (extension == ".json") return RepresentationFormat.Json;
        if (extension == ".rdf") return RepresentationFormat.Rdf;

        // 2. Check Accept Header (Content Negotiation)
        var accept = context.Request.Headers.Accept.ToString();

        if (accept.Contains("text/html"))
        {
            return RepresentationFormat.Html;
        }

        if (accept.Contains("application/rdf+xml") || accept.Contains("text/xml") || accept.Contains("application/xml"))
        {
            return RepresentationFormat.Rdf;
        }

        // 3. Default
        return RepresentationFormat.Json;
    }
}
