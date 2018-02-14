using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KvMarktApi.Data;
using KvMarktApi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KvMarktApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ContributorController : BaseController<Contributor>
    {
        public ContributorController(ApplicationDbContext context) : base(context)
        { }

        public override async Task<IActionResult> Get(long id)
        {
            var result = await _dbSet
                .Include(c => c.Association)
                .Select(c => new
                {
                    Id = c.Id,
                    email = c.Email,
                    firstname = c.Firstname,
                    lastname = c.Lastname,
                    association = c.Association.Id,
                    associationName = c.Association.Name
                })
                .SingleOrDefaultAsync(c => c.Id == id );
                
            return ReturnResult(result);
        }


    }

}