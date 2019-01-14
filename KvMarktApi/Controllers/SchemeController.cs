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

namespace KvMarktApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class SchemeController : BaseController<Scheme>
    {

        private readonly DbSet<ContributorFavoriteScheme> _favoriteSet;
        private readonly DbSet<SchemeSelectedPlace> _schemeSelectedPlacesSet;

        public SchemeController(
            ApplicationDbContext context
        ) : base(context)
        {
            _favoriteSet = context.Set<ContributorFavoriteScheme>();
            _schemeSelectedPlacesSet = context.Set<SchemeSelectedPlace>();
        }


        private IQueryable<SchemeDto> _getSchemeObject(Contributor contributor)
        {
            return _dbSet
                .Include(s => s.Author)
                .Select(x => new SchemeDto(x)
                {
                    IsFavorite = x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null,
                    Places = x.SchemeSelectedPlaces.Where(sp => sp.Scheme.Id == x.Id).Select(sp => sp.Place.Id).ToList(),
                    AuthorName = x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
                    Category = x.CategoryId,
                    CategoryName = x.Category.Name,
                })
                .AsNoTracking();
        }
        // private IQueryable<SchemeDto> _getSchemeObject()
        // {
        //     return _dbSet
        //         .Include(s => s.Author)
        //         .Select(x => new SchemeDto(x)
        //         {
        //             // IsFavorite = x.ContributorFavoriteSchemes.Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null,
        //             // AuthorName = x.Author != null ? (x.Author.Firstname + (x.Author.Firstname != null ? " " : "") + x.Author.Lastname) : null,
        //             Category = x.CategoryId,
        //             // CategoryName = x.Category.Name,
        //             // Place = x.PlaceId,
        //             // PlaceName = x.Place.Name
        //         })
        //         .AsNoTracking();
        // }

        public override async Task<IActionResult> List()
        {
            var contributor = await GetContributor(User);
            var result = await _getSchemeObject(contributor).ToListAsync();
            return base.ReturnResult(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var contributor = await GetContributor(User);
            var result = await _getSchemeObject(contributor)
                .SingleOrDefaultAsync(s => s.Id == id);
            return base.ReturnResult(result);
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetOwn()
        {
            var contributor = await GetContributor(User);
            var result = await _getSchemeObject(contributor)
                .Where(s => s.Author == contributor.Id)
                .AsNoTracking()
                .ToListAsync();
            return base.ReturnResult(result);
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var contributor = await GetContributor(User);
            var result = await _getSchemeObject(contributor)
                .Where(x => x.IsFavorite) // .Where(f => f.ContributorId == contributor.Id).FirstOrDefault() != null
                .AsNoTracking()
                .ToListAsync();

            return base.ReturnResult(result);
        }

        [HttpGet("{id:long}/favorites/add")]
        public async Task<IActionResult> AddToFavorites(long id)
        {
            var contributor = await GetContributor(User);
            var searchResult = await _favoriteSet
                .AsNoTracking()
                .FirstOrDefaultAsync(fs => fs.Contributor.Id == contributor.Id && fs.SchemeId == id);
            if (searchResult != null)
            {
                return base.ReturnResult(new { isFavorite = true, message = "Nothing Changed" });
            }
            var cfs = new ContributorFavoriteScheme
            {
                Scheme = await _dbSet.SingleAsync(s => s.Id == id),
                Contributor = await _context.Set<Contributor>().SingleAsync(c => c.Id == contributor.Id)
            };
            _favoriteSet.Add(cfs);
            var result = await _context.SaveChangesAsync();
            return base.ReturnResult(new
            {
                isFavorite = (result == 1)
            });
        }

        [HttpGet("{id:long}/favorites/remove")]
        public async Task<IActionResult> RemoveFromFavorites(long id)
        {
            var contributor = await GetContributor(User);
            var searchResult = await _favoriteSet
                .AsNoTracking()
                .FirstOrDefaultAsync(fs => fs.Contributor.Id == contributor.Id && fs.SchemeId == id);
            if (searchResult == null)
            {
                return base.ReturnResult(new { isFavorite = false, message = "Nothing Changed" });
            }
            var cfs = new ContributorFavoriteScheme
            {
                Scheme = await _dbSet.SingleAsync(s => s.Id == id),
                Contributor = await _context.Set<Contributor>().SingleAsync(c => c.Id == contributor.Id)
            };
            _favoriteSet.Remove(cfs);
            var result = await _context.SaveChangesAsync();
            return base.ReturnResult(new
            {
                isFavorite = !(result == 1)
            });
        }

        [HttpGet("{id:long}/fans")]
        public async Task<IActionResult> GetFans(long id)
        {
            var schemeList = new List<SchemeDto>();

            var result = await _dbSet//.FindAsync(id)
                .Select(s => new
                {
                    SchemeId = s.Id,
                    Fans = s.ContributorFavoriteSchemes.Select(f => f.ContributorId)
                })
                .AsNoTracking()
                .ToListAsync();
            return base.ReturnResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchemeDto model)
        {
            var contributor = await GetContributor(User);
            // var searchResult = await _dbSet
            //     .FirstOrDefaultAsync(s => s.Id == model.Id);

            var scheme = new Scheme
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Author = contributor,
                Category = _context.Set<Category>().Single(c => c.Id == model.Category),
                AgeStart = model.AgeStart,
                AgeEnd = model.AgeEnd
            };
            await addPlacesToScheme(model.Places, scheme, contributor);

            var schemeResult = await _dbSet.AddAsync(scheme);
            var result = await _context.SaveChangesAsync();
            return base.ReturnResult(new SchemeDto(scheme)
            {
                IsFavorite = false,
                AuthorName = $"{contributor.Firstname} {contributor.Lastname}",
                Category = scheme.CategoryId,
                CategoryName = scheme.Category.Name,
                Places = scheme.SchemeSelectedPlaces.Where(sp => sp.Scheme.Id == scheme.Id).Select(sp => sp.Place.Id).ToList(),
            });
        }

        private async Task addPlacesToScheme(IEnumerable<long> places, Scheme scheme, Contributor contributor)
        {
            foreach (var item in places)
            {
                var cfs = new SchemeSelectedPlace
                {
                    Scheme = scheme,
                    Place = await _context.Set<Place>().SingleAsync(p => p.Id == item)
                };
                await _schemeSelectedPlacesSet.AddAsync(cfs);
            }
            // var schemePlacesResult = await _context.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] SchemeDto model)
        {
            var contributor = await GetContributor(User);
            // var searchResult = await _dbSet
            //     .FirstOrDefaultAsync(s => s.Id == model.Id);
            var scheme = new Scheme
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Author = contributor,
                Category = _context.Set<Category>().Single(c => c.Id == model.Category),
                AgeStart = model.AgeStart,
                AgeEnd = model.AgeEnd
            };

            try
            {
                _context.Update(scheme);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
            }

            return base.ReturnResult(new SchemeDto(scheme)
            {
                IsFavorite = false,
                AuthorName = $"{contributor.Firstname} {contributor.Lastname}",
                Category = scheme.CategoryId,
                CategoryName = scheme.Category.Name,
            });
        }

    }

}