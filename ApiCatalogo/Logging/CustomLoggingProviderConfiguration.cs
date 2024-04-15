namespace ApiCatalogo.Logging
{
    //classe de configuração do log
    //essa classe define o nível do logging, sendo Warning como default e define o id do evento dele, sendo como default 0
    public class CustomLoggingProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
        public int EventId { get; set; } = 0;
    }
}
