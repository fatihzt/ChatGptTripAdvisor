using ChatGptBot.Business.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;

namespace ChatGptBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistroyController : ControllerBase
    {
        private readonly IChatHistoryService _chatHistoryService;
        private readonly HttpClient _client;
        private readonly OpenAIService _openAIService;
        public ChatHistroyController(IChatHistoryService chatHistoryService,HttpClient client, OpenAIService openAIService)
        {
            _chatHistoryService = chatHistoryService;
            _client = client;
            _openAIService = openAIService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string prompt)
        {
            var result=await _openAIService.Completions.CreateCompletion(new()
            {
                Prompt = prompt,
                MaxTokens = 500

            });
            return Ok(result);
        }
    }
}
