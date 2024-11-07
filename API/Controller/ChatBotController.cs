using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.ItemRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IItemRepository _itemRepository;

        public ChatBotController(HttpClient httpClient, IItemRepository itemRepository)
        {
            _httpClient = httpClient;
            _itemRepository = itemRepository;
        }

        // DTO for chat request message
        public class ChatRequest
        {
            public string Message { get; set; }
        }

        // GET: api/ChatBot/GetItemsByKeyword
        [HttpGet("GetItemsByKeyword")]
        public async Task<IActionResult> GetItemsByKeyword([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword cannot be empty.");
            }

            // Call the repository method to fetch items based on the keyword
            var items = await _itemRepository.GetItemsByKeywordForChatbotAsync(keyword);

            // Return the list of items in JSON format
            return Ok(items);
        }
    }
}
