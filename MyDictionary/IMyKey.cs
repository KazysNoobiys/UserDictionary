namespace MyDictionary
{
    public interface IMyKey<TId, TName>
    {
        TId Id { get; set; }
        TName Name { get; set; }
    }

}