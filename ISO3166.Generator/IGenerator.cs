using System.Threading.Tasks;

namespace ISO3166.Generator
{
    public interface IGenerator
    {
        Task Generate(Country[] countries);
    }
}