using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NulllogiconeCore.Data;

namespace NulllogiconeCore.Pages.Ui.Stamm
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [FromRoute]
        public Guid? Guid { get; set; }

        public Models.Stamm? Entity { get; set; }

        public async Task OnGetAsync()
        {
            if (Guid.HasValue)
            {
                Entity = _db.Stamms
                    .Include(s => s.Anglers)
                    .FirstOrDefault(s => s.StammGuid == Guid.Value);
            }

        }
    }
}

