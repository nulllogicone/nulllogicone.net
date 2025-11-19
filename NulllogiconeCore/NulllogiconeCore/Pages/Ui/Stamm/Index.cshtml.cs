using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NulllogiconeCore.Data;

namespace NulllogiconeCore.Pages.Ui.Stamm
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

        public Models.Stamm? Entity { get; set; }
        public string? JsonResponse { get; set; }

        public async Task OnGetAsync()
        {
            if (Guid.HasValue)
            {
                Entity = _db.Stamms.FirstOrDefault(s => s.StammGuid == Guid.Value);
                var client = _api.CreateClient("BackendApi");
                var response = await client.GetAsync($"/test/{Guid.Value}");
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
                // Handle case when guid is not provided
                // Entity = null or default;
                JsonResponse = "No GUID provided.";
            }
        }
    }
}

