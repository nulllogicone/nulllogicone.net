using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;

namespace NulllogiconeCore.Pages.Ui.Angler
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

        public Models.Angler? Entity { get; set; }
        public string? JsonResponse { get; set; }

        public async Task OnGetAsync()
        {
            if (Guid.HasValue)
            {
                Entity = _db.Anglers
                    .Include(a => a.Stamm)
                    .Include(a => a.News)
                   
                    .FirstOrDefault(a => a.AnglerGuid == Guid.Value);

                var client = _api.CreateClient("BackendApi");
                // Using the unified endpoint for JSON data
                var response = await client.GetAsync($"/angler/{Guid.Value}.json");
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
