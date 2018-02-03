using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : BaseController<Team>
    {
        public TeamController(ApplicationDbContext context) : base(context)
        { }

    }

}