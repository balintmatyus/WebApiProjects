using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;

namespace LangTutor.Controllers
{
    [ApiController]
    public class TutorController : Controller
    {

        [HttpPost]
        [Route("api/tutor")]
        public async Task<IActionResult> Method1([FromBody] Tuple<string, string>[] promptHistory)
        {
            string result = string.Empty;

            var openai = new OpenAIAPI("");
            ChatRequest request = new ChatRequest();

            request.Messages = new List<ChatMessage>()
            {
                new ChatMessage(ChatMessageRole.System, "You are a helpful math teacher, who teaches at Corvinus University of Budapest. You answer in detail and ask questions, if more input is required for a precise answer."),
            };

            foreach (var message in promptHistory)
            {
                request.Messages.Add(new ChatMessage(ChatMessageRole.FromString(message.Item1), message.Item2));
            }

            request.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
            request.MaxTokens = 1024;
            request.Temperature = 0.5;

            var completions = await openai.Chat.CreateChatCompletionAsync(request);

            foreach (var choices in completions.Choices)
            {
                result += choices.Message.Content;
            }

            return Ok(result);

        }
    }
}
