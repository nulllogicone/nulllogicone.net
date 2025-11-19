using Microsoft.AspNetCore.Http;

namespace NulllogiconeCore.Extensions;

public static class ContentNegotiationExtensions
{
    public static IResult? TryNegotiate(this HttpContext context, string uiPath, string? rdfPath = null)
    {
        var accept = context.Request.Headers.Accept.ToString();

        // 1. Browser / UI Request
        // We check this first because browsers send text/html.
        if (accept.Contains("text/html"))
        {
            return Results.Redirect(uiPath);
        }

        // 2. RDF Request (if supported)
        // We check for standard RDF/XML mimetypes.
        if (!string.IsNullOrEmpty(rdfPath) &&
           (accept.Contains("application/rdf+xml") || accept.Contains("text/xml") || accept.Contains("application/xml")))
        {
            return Results.Redirect(rdfPath);
        }

        // 3. Default (JSON)
        // Includes application/json, text/json, or any other unhandled type.
        return null;
    }
}
