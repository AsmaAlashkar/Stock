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
        private readonly IGenericRepository<Category> _repo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IGenericRepository<Category> repo,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var cat = await _categoryRepository.GetCategories();
            var catDtos = _mapper.Map<List<CategoryDto>>(cat);
            return Ok(catDtos);
        }
    }
}
