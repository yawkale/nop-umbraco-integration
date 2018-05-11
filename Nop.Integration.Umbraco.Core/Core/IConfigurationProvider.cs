namespace Nop.Integration.Umbraco.Core.Core
{
    public interface IConfigurationProvider
    {
        T GetCongurationValue<T>(string key, T defaultValue);
    }
}
