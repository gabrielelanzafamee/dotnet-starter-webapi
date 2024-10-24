using OpenAI.Embeddings;

namespace App.Data;


public enum VectorDBTypes
{
    SqLite,
    MySQL,
    MongoDB,
    InMemory
}


public class VectorDB
{
    public VectorDB(VectorDBTypes type)
    {
    }

    public async void Load() {}
    public async void Store(OpenAIEmbedding embedding, DataSource source) {}
    public async void FindSimilarities(OpenAIEmbedding embedding) {}
}