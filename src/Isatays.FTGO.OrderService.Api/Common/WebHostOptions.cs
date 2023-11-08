namespace Isatays.FTGO.OrderService.Api.Common;

public class WebHostOptions
{
    /// <summary>
    /// Создает экземпляр <see cref="WebHostOptions"/>
    /// </summary>
    /// <param name="instanceName"></param>
    /// <param name="webAddress"></param>
    internal WebHostOptions(string instanceName, string webAddress)
    {
        InstanceName = instanceName;
        WebAddress = webAddress;
    }

    /// <summary>Имя секции в appsettings.json</summary>
    internal const string SectionName = nameof(WebHostOptions);

    /// <summary>Возвращает имя проекта</summary>
    internal string InstanceName { get; }

    /// <summary>Возвращает веб адресс (домен) проекта</summary>
    internal string WebAddress { get; }
}
