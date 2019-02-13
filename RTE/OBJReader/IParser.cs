namespace RTE.OBJReader
{
    public interface IParser<out T> where T : IType
    {
        T Parse(string line);
    }
}
