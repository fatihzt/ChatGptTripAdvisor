using ChatGptBot.Business.Manager;
using ChatGptBot.Business.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using System;

namespace ChatGptBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistroyController : ControllerBase
    {
        private readonly IChatHistoryService _chatHistoryService;
        private readonly HttpClient _client;
        private readonly IOpenAIService _openAIService;
        
        public ChatHistroyController(IChatHistoryService chatHistoryService,IOpenAIService openAIService,HttpClient client)
        {
            _chatHistoryService = chatHistoryService;
            _client = client;
            this._openAIService = openAIService;
        }
        [HttpGet("GetCityName")]
        public async Task<IActionResult> GetCityName()
        {
            string apikey = "1cd672a228msh23352867f930791p170d3ajsn138709139db9";
            CitiesManager citiesManager = new CitiesManager(apikey);
            string[] cityName = await citiesManager.GetCityNames();
            return Ok(cityName);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string prompt)
        {
            

            var result=await _openAIService.Completions.CreateCompletion(new()
            {
                Prompt = $"{prompt} için yol tarifi verir misin ?",
                MaxTokens = 500

            },Models.TextDavinciV3);
            return Ok(result.Choices[0].Text);
        }
        

    }
}
