using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;

namespace NulllogiconeCore.Pages.Ui.PostIt
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _api;

        public IndexModel(ApplicationDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _api = httpClientFactory;
        }

        [FromRoute]
        public Guid? Guid { get; set; }

        public Models.PostIt? Entity { get; set; }
        public string? JsonResponse { get; set; }

        public async Task OnGetAsync()
        {
            if (Guid.HasValue)
            {
                Entity = _db.PostIts
                    .Include(p => p.Codes)
                    .Include(p => p.TopLabs)
                    .Include(p => p.Provisions)
                    .Include(p => p.Wurzelns)
                    .FirstOrDefault(p => p.PostItGuid == Guid.Value);

                var client = _api.CreateClient("BackendApi");
                // Using the unified endpoint for JSON data
                var response = await client.GetAsync($"/postit/{Guid.Value}.json");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var jsonElement = System.Text.Json.JsonSerializer.Deserialize<object>(jsonString);
                        JsonResponse = System.Text.Json.JsonSerializer.Serialize(jsonElement, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    }
                    catch
                    {
                        JsonResponse = jsonString; // fallback if not valid JSON
                    }
                }
                else
                {
                    JsonResponse = $"Error: {response.StatusCode}";
                }
            }
            else
            {
                JsonResponse = "No GUID provided.";
            }
        }
    }
}
