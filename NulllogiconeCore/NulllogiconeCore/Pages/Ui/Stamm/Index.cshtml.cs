using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NulllogiconeCore.Pages.Ui.Stamm
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(ApplicationDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
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
                var client = _httpClientFactory.CreateClient("BackendApi");
                var response = await client.GetAsync($"/test/{Guid.Value}");
                if (response.IsSuccessStatusCode)
                {
                    JsonResponse = await response.Content.ReadAsStringAsync();
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

