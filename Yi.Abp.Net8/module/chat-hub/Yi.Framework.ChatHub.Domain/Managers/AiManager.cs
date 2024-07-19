using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Yi.Framework.ChatHub.Domain.Shared.Options;

namespace Yi.Framework.ChatHub.Domain.Managers
{
    public class AiManager : DomainService, ISingletonDependency
    {
        public AiManager(IOptions<AiOptions> options)
        {
            this.OpenAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = options.Value.ApiKey,
                BaseDomain = options.Value.BaseDomain
            });
        }
        private OpenAIService OpenAIService { get; }

        public async IAsyncEnumerable<string> ChatAsStreamAsync()
        {
            var completionResult = OpenAIService.ChatCompletion.CreateCompletionAsStream(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromUser("特朗普是谁？"),
                },
                Model = Models.Gpt_4,
            });

            await foreach (var result in completionResult)
            {
                if (result.Successful)
                {
                    yield return result.Choices.FirstOrDefault()?.Message.Content??string.Empty;
                }
            }

        }
    }
}
