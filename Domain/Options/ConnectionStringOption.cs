namespace Domain.Options;

public class ConnectionStringOption
{

    public const string Key = "ConnectionStrings";

    //isimlendirme yaparken appsettings.json dosyasındaki isimle aynı olmalıdır.
    public string SqlServer { get; set; } = default!; //ConnectionStrings{SqlServer:***}


}