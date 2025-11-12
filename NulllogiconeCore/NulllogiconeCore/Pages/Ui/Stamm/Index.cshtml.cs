using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NulllogiconeCore.Data;
using NulllogiconeCore.Models;

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

        public void OnGet()
        {
            if (Guid.HasValue)
            {
                Entity = _db.Stamms.FirstOrDefault(s => s.StammGuid == Guid.Value);
            }
            else
            {
                // Handle case when guid is not provided
                // Entity = null or default;
            }
        }
    }
}

