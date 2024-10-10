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
        public async Task<ActionResult<List<ItemDto>>> GetItems([FromQuery] DTOPaging paging)
        {
            // Validate pagination inputs
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItems(paging);

            if (!items.Any())
            {
                return NotFound("No items found.");
            }

            return Ok(_mapper.Map<List<ItemDto>>(items));
        }
        [HttpGet("GetItemById/{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _item.GetItemById(id);

            if (item == null)
            {
                return NotFound("Item not found or has been deleted.");
            }

            return Ok(_mapper.Map<ItemDto>(item));
        }
        [HttpGet("GetItemsByCategoryId/{id}")]
        public async Task<ActionResult<List<ItemDto>>> GetItemsByCategoryId(int id)
        {
            var item = await _item.GetItemsByCategoryId(id);

            return Ok(_mapper.Map<List<Item>>(item));
        }
        [HttpGet("GetItemsBySubWHId/{id}")]
        public async Task<ActionResult<List<ItemDto>>> GetItemsBySubWHId(int id)
        {
            var item = await _item.GetItemsBySubWHId(id);

            return Ok(_mapper.Map<List<Item>>(item));
        }
        [HttpGet("GetItemsByUnitId/{id}")]
        public async Task<ActionResult<List<ItemDto>>> GetItemsByUnitId(int id)
        {
            var item = await _item.GetItemsByUnitId(id);


            return Ok(_mapper.Map<List<Item>>(item));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItemsWithDetails()
        {
            try
            {
                // Fetch all items with their associated details
                var items = await _item.GetAllItemsWithDetailsAsync();

                // Check if the list is empty
                if (items == null || !items.Any())
                {
                    return NotFound("No items found.");
                }

                return Ok(items);  // Return the list of items
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
