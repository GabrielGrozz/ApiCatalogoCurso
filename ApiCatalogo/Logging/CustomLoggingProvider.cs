using System.Collections.Concurrent;

namespace ApiCatalogo.Logging
{
    //essa classe implementa a interface ILoggerProvider, que nos da a possibilidade de criar logs personalizados
    public class CustomLoggingProvider : ILoggerProvider
    {
        //nossa classe de configuração
        readonly CustomLoggingProviderConfiguration loggerConfig;

        readonly ConcurrentDictionary<string , CustomLogging> loggers = new ConcurrentDictionary<string, CustomLogging> ();

        //no construtor ele recebe uma classe de configuração de log
        public CustomLoggingProvider(CustomLoggingProviderConfiguration config)
        {
            loggerConfig = config;
        }

        //criar um logger para uma categoria expecifica
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomLogging(name , loggerConfig));
        }

        public void Dispose()
        {
            loggers.Clear ();
        }
    }
}
