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
}