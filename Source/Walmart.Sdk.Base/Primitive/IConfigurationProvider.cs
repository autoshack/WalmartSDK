namespace Walmart.Sdk.Base.Primitive
{
    public interface IConfigurationProvider
    {
        string this[string key] { get; set; }
    }
}