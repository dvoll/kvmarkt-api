using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KvMarktApi.Data;
using KvMarktApi.Models;

namespace KvMarktApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : BaseController<Team>
    {
        public TeamController(ApplicationDbContext context) : base(context)
        { }

    }

}