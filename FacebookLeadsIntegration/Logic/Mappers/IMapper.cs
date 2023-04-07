namespace Logic.Mappers
{
    public interface IMapper<Source, Target>
    {
        Target Map(Source source);
    }
}

