using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KvMarktApi.Data;
using KvMarktApi.Models;

namespace KvMarktApi.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class AssociationController : BaseController<Association>
    {
        public AssociationController(ApplicationDbContext context) : base(context)
        { }

        public override async Task<IActionResult> List() {
            var result = await _dbSet
                .Include(a => a.Teams)
                .Select(a => new { 
                    name = a.Name,
                    teams = a.Teams.Select(t => new { id = t.Id, name = t.Name, t.LogoUrl})
                })
                .ToListAsync();
            return base.ReturnResult(result);
        }

    }

}