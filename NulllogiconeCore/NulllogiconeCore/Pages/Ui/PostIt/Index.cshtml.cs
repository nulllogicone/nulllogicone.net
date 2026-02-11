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
        public Guid Guid { get; set; }

        public Models.PostIt? Entity { get; set; }

        public async Task OnGetAsync()
        {

                Entity = _db.PostIts
                    .Include(p => p.Codes)
                    .Include(p => p.TopLabs)
                    .Include(p => p.Provisions)
                    .Include(p => p.Wurzelns)
                    .FirstOrDefault(p => p.PostItGuid == Guid);

           
           
        }
    }
}
