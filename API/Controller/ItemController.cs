using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.CategoryRepo;
using Repository;
using Standard.Entities;
using Repository.ItemRepo;
using Standard.DTOs;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IGenericRepository<Item> _repo;
        private readonly IMapper _mapper;
        private readonly IItemRepository _item;
        public ItemController(IGenericRepository<Item> repo,
            IMapper mapper,
            IItemRepository item)
        {
            _repo = repo;
            _mapper = mapper;
            _item = item;
        }
        [HttpGet("GetItems")]
        public async Task<ActionResult<List<ItemDto>>> GetItems()
        {
            var item = await _item.GetItems();

            var itemDtos = _mapper.Map<List<Item>>(item);

            return Ok(itemDtos);
        }
    }
}
