using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.ItemRepo;
using Repository;
using Standard.Entities;
using Standard.DTOs;
using Standard.DTOs.ItemDtos;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IGenericRepository<Item> _repo;
        private readonly IMapper _mapper;
        private readonly IItemRepository _item;
        public ItemController(
            IGenericRepository<Item> repo,
            IMapper mapper,
            IItemRepository item)
        {
            _repo = repo;
            _mapper = mapper;
            _item = item;
        }
        [HttpGet("GetItems")]
        public async Task<ActionResult<ItemDetailsResult>> GetItems([FromQuery] DTOPaging paging)
        {
            // Validate pagination inputs
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var itemsResult = await _item.GetItems(paging);

            if (!itemsResult.ItemsDetails.Any())
            {
                return NotFound("No items found.");
            }

            // Return the mapped ItemDetailsDto list
            return Ok(itemsResult);
        }


        [HttpGet("GetItemById/{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _item.GetItemById(id);

            if (item == null)
            {
                return NotFound("Item not found or has been deleted.");
            }

            return Ok(item);
        }


        [HttpGet("GetItemsByCategoryId/{catId}")]
        public async Task<ActionResult<List<ItemDetailsDto>>> GetItemsByCategoryId(int catId, [FromQuery] DTOPaging paging)
        {
            // Validate pagination inputs
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItemsByCategoryId(catId, paging);

            if (!items.Any())
            {
                return NotFound("No items found for the given category.");
            }

            return Ok(items); // Directly return the result since it's already in ItemDetailsDto format
        }


        [HttpGet("GetItemsBySubWHId/{subId}")]
        public async Task<ActionResult<List<ItemDetailsDto>>> GetItemsBySubWHId(int subId, [FromQuery] DTOPaging paging)
        {
            // Validate pagination inputs
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItemsBySubWHId(subId, paging);

            if (!items.Any())
            {
                return NotFound("No items found for the given sub-warehouse.");
            }

            return Ok(items); // Directly return the result since it's already in ItemDetailsDto format
        }


        [HttpGet("GetItemsByUnitId/{unitId}")]
        public async Task<ActionResult<List<ItemDetailsDto>>> GetItemsByUnitId(int unitId,[FromQuery] DTOPaging paging)
        {
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItemsByUnitId(unitId,paging);
            if (!items.Any())
            {
                return NotFound("No items found for the given sub-warehouse.");
            }

            return Ok(items);
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

        [HttpPost("CreateItem")]
        public async Task<ActionResult> CreateItem(CreateItemDto Item)
        {
            var newItem = _mapper.Map<Item>(Item);

            await _repo.CreateNew(newItem);
            return Ok("Item Created Successfully");

        }
    }
}
