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
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var itemsResult = await _item.GetItems(paging);

            if (!itemsResult.ItemsDetails.Any())
            {
                return NotFound("No items found.");
            }

            return Ok(itemsResult);
        }
        [HttpGet("GetItemNames")]
        public async Task<ActionResult<List<ItemsNamesDto>>> GetItemsNames()
        {
                var itemNames = await _item.GetItemsNames();

                if (itemNames == null || itemNames.Count == 0)
                {
                    return NotFound("No items found.");
                }

                return Ok(itemNames);
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
        
        [HttpGet("GetItemDetailsBySubAsync/{itemId}/{subId}")]
        public async Task<ActionResult<ItemDto>> GetItemDetailsBySubAsync(int itemId, int subId)
        {
            var itemDetails = await _item.GetItemDetailsBySubAsync(itemId, subId);

            if (itemDetails == null)
            {
                return NotFound("Either the Item or SubWearhouse was not found.");
            }

            return Ok(itemDetails);
        }

        [HttpGet("GetItemsByCategoryId/{catId}")]
        public async Task<ActionResult<ItemDetailsResult>> GetItemsByCategoryId(int catId, [FromQuery] DTOPaging paging)
        {
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItemsByCategoryId(catId, paging);

            //if (!items.ItemsDetails.Any())
            //{
            //    return NotFound("No items found for the given category.");
            //}

            return Ok(items); 
        }


        [HttpGet("GetItemsBySubWHId/{subId}")]
        public async Task<ActionResult<List<ItemDetailsDto>>> GetItemsBySubWHId(int subId, [FromQuery] DTOPaging paging)
        {
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than zero.");
            }

            var items = await _item.GetItemsBySubWHId(subId, paging);

            if (!items.ItemsDetails.Any())
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
            if (!items.ItemsDetails.Any())
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
                var items = await _item.GetAllItemsWithDetailsAsync();

                if (items == null || !items.Any())
                {
                    return NotFound("No items found.");
                }

                return Ok(items); 
            }
            catch (Exception ex)
            {
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

        [HttpPut("UpdateItem/{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromBody] CreateItemDto Item)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"Item with ID {id} not found");
                }

                if (!string.IsNullOrEmpty(Item.ItemName))
                {
                    existingItem.ItemName = Item.ItemName;
                }
                if (!string.IsNullOrEmpty(Item.ItemCode))
                {
                    existingItem.ItemCode = Item.ItemCode;
                }

                if (Item.CatFk!=0)
                {
                    existingItem.CatFk = Item.CatFk;
                }
                if (Item.UniteFk != 0)
                {
                    existingItem.UniteFk = Item.UniteFk;
                }
                if (Item.ItemExperationdate.HasValue)
                {
                    existingItem.ItemExperationdate = Item.ItemExperationdate.Value;
                }

                await _repo.Update(existingItem);

                return Ok("Item updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating Item: {ex.Message}");
            }
        }

    }
}
