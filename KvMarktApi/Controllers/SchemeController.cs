using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KvMarktApi.Data;
using KvMarktApi.Models;
using KvMarktApi.Responses;
using KvMarktApi.Services;

namespace KvMarktApi.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class SchemeController : BaseController<Scheme>
    {

        public SchemeController(
            ApplicationDbContext context
        ) : base(context)
        { }

        private static readonly Expression<Func<Scheme, SchemeDto>> AsSchemeDto =
            x => new SchemeDto(x)
            { 
                AuthorName =    x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
                Category =      x.CategoryId,
                CategoryName =  x.Category.Name,
                Place =         x.PlaceId,
                PlaceName =     x.Place.Name
            };

        public override async Task<IActionResult> List() {
            var contributor = await GetContributor(User);
            var result = await _dbSet
                .Include(s => s.Author)
                .Select(x => new SchemeDto(x)
                {
                    IsFavorite = x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null,
                    AuthorName = x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
                    Category = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Place = x.PlaceId,
                    PlaceName = x.Place.Name
                })
                .ToListAsync();
            return base.ReturnResult(result);
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetOwn() {
            var contributor = await GetContributor(User);
            var result = await _dbSet
                .Where(s => s.AuthorId == contributor.Id)
                .Include(s => s.Author)
                .Select(x => new SchemeDto(x)
                {
                    IsFavorite = x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null,
                    AuthorName = x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
                    Category = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Place = x.PlaceId,
                    PlaceName = x.Place.Name
                })
                .ToListAsync();
            return base.ReturnResult(result);
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var contributor = await GetContributor(User);
            var result =  await _dbSet
                .Where(x => x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null)
                .Include(s => s.Author)
                .Select(x => new SchemeDto(x)
                {
                    IsFavorite = x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null,
                    AuthorName = x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
                    Category = x.CategoryId,
                    CategoryName = x.Category.Name,
                    Place = x.PlaceId,
                    PlaceName = x.Place.Name
                })
                .ToListAsync();
           
            return base.ReturnResult(result);
        }



        [HttpGet("{id:long}/fans")]
        public async Task<IActionResult> GetFans(long id)
        {
            var schemeList = new List<SchemeDto>();

            var result = await _dbSet//.FindAsync(id)
                .Select(s => new { 
                    SchemeId = s.Id,
                    Fans = s.ContributorFavoriteSchemes.Select(f => f.ContributorId) 
                })
                .ToListAsync();
            return base.ReturnResult(result);
        }

    }

}