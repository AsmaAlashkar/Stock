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
