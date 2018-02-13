
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KvMarktApi.Data;
using KvMarktApi.Models;

namespace KvMarktApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class PlaceController : BaseController<Place>
    {
        public PlaceController(ApplicationDbContext context) : base(context)
        { }

    }

}