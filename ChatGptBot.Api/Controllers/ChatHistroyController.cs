using ChatGptBot.Business.Manager;
using ChatGptBot.Business.Service;
using ChatGptBot.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using System;

namespace ChatGptBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatHistroyController : ControllerBase
    {
        private readonly IChatHistoryService _chatHistoryService;
        private readonly HttpClient _client;
        private readonly IOpenAIService _openAIService;
        private readonly IUserService _userService;
        
        public ChatHistroyController(IChatHistoryService chatHistoryService,IOpenAIService openAIService,HttpClient client,IUserService userService)
        {
            _chatHistoryService = chatHistoryService;
            _client = client;
            this._openAIService = openAIService;
            this._userService = userService;
        }
        

        [HttpPost("trip-advisor")]
        public async Task<IActionResult> Create([FromQuery] string prompt)
        {
            var userId= await GetCurrentUserId();
            var user = _userService.Get(u => u.Id.ToString() == userId);
            if (user == null) { return BadRequest("Not Registered"); }
            string API_KEY = "5ae2e3f221c38a28845f05b65b1c348121dbfc76b26afedfbfdd3349";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.opentripmap.com/0.1/en/places/geoname?name={prompt}&apikey={API_KEY}");
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(result);
            var type = (string)jsonObject["type"];

            if (result.Count()<1)
            {
                return BadRequest("Lütfen geçerli bir konum giriniz.");
            }

            var completionResult = await _openAIService.Completions.CreateCompletion(new()
            {
                Prompt = $"{prompt} için tarihi,turistik ve kesinlikle görülmesi gereken yerlerin listesini çıkarır mısın ?",
                MaxTokens = 500

            }, Models.TextDavinciV3);
            ChatHistory entity = new()
            {
                History = completionResult.Choices[0].Text,
                UserId = user.Id,
            };
            _chatHistoryService.Add(entity);

            return Ok(completionResult.Choices[0].Text);
        }

        //[HttpPost("soru-sor")]
        //public async Task<IActionResult> Soru([FromQuery] string promp) 
        //{
        //    var result = await _openAIService.Completions.CreateCompletion(new()
        //    {
        //        Prompt = promp,
        //        MaxTokens = 5000
        //    }, Models.TextDavinciV2);
        //    return Ok(result.Choices[0].Text);
        //}
        private async Task<string> GetCurrentUserId()
        {

            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value.ToString();
            return userId;
        }
    }
}
