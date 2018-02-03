using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1.Controllers {

    public class BaseController<T> : Controller, IBaseController<T> where T : BaseObject {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseController(
            ApplicationDbContext context
        )
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var result = await _dbSet.ToListAsync();
            return ReturnResult(result);
        }

        [HttpGet("{id:long}")] //, Name = "GetItem"
        public virtual async Task<IActionResult> Get(long id)
        {
            var result = await _dbSet.FindAsync(id);              
            return ReturnResult(result);
        }

        public IActionResult ReturnResult(object result) {
            if (result == null) {
                return NotFound(new ApiResponse(404, "No item found"));
            }
            return Ok(new ApiOkResponse(result));
        }

        protected async Task<Contributor> GetContributor(ClaimsPrincipal User) {
            var mail = User.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var contributor = await _context.Contributor.Where(x => x.Email == mail).FirstOrDefaultAsync();
            return contributor;
        }
    }


    interface IBaseController<T> {
        Task<IActionResult> List();
        Task<IActionResult> Get(long id);
    }


    public class ApiValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
            }

            base.OnActionExecuting(context);
        }
    }

    public class ErrorController {
        [Route("error/{code}")]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }

}