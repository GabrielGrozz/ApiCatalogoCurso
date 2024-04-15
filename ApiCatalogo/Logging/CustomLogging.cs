
namespace ApiCatalogo.Logging
{
    //herda de ILogger, que nos fornece os métodos para registro
    public class CustomLogging : ILogger
    {
        public string loggerName;
        readonly CustomLoggingProviderConfiguration configuration;

        public CustomLogging(string name, CustomLoggingProviderConfiguration config)
        {
            loggerName = name;
            configuration = config;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        //verifica se o nivel de log está "ativado"(caso o nivel seja o minimo desejado para realizar o log)
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == configuration.LogLevel;
        }

        //método que faz o registro do log
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"{logLevel.ToString()} : {eventId} - {formatter(state, exception)}";
            WriteLogInArchive(message);
        }

        private void WriteLogInArchive(string message)
        {
            string path = @"D:\dev\ApiCatalogo\ProjectLog.txt";

            using (StreamWriter writer = new StreamWriter(path, true)) 
            {
                try
                {
                    writer.WriteLine(message);
                    writer.Close();

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
