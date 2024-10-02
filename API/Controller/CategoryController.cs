using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.CategoryRepo;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<CategoriesHirarichy> _repo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _cp;
        public CategoryController(IGenericRepository<CategoriesHirarichy> repo,
            IMapper mapper,
            ICategoryRepository cp)
        {
            _repo = repo;
            _mapper = mapper;
            _cp = cp;
        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<CategoriesHirarichy>>> GetCategories()
        {
            var cat = await _cp.GetAllCategories();

            var catDtos = _mapper.Map<List<CategoriesHirarichyDto>>(cat);

            return Ok(catDtos);
        }
    }
}
