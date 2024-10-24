using App.Data;
using OpenAI;
using OpenAI.Embeddings;

namespace App.Services;

public class OpenAIService
{
    private readonly IConfiguration _config;
    private readonly OpenAIClient _provider;
    private readonly EmbeddingClient _emb;


    public OpenAIService(IConfiguration config, VectorDB vectorDB)
    {
        _config = config;
        _provider = new OpenAIClient(_config["OpenAI:ApiKey"]);
        _emb = new EmbeddingClient("", _config["OpenAI:ApiKey"]);
    }

    // public async Task<ChatCompletion> Chat(List<ChatMessage> messages, string model, float temperature = 0.5f)
    // {
    //     ChatCompletionOptions options = new ChatCompletionOptions()
    //     {
    //         Temperature = temperature,
    //         TopP = 0.95f
    //     };
    //     ChatCompletion completion = await _client.GetChatClient(model).CompleteChatAsync(messages, options);
    //     return completion;
    // }
}