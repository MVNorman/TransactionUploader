using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace TransactionUploader.Extensions
{
    public static class SerilogExtension
    {
        public static IConfigurationRoot ConfigureSerilog(this IConfigurationRoot configurationRoot)
        {
            var sinkOptions = new SinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };

            var columnOpts = new ColumnOptions();

            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Remove(StandardColumn.MessageTemplate);
            columnOpts.Store.Remove(StandardColumn.LogEvent);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    configurationRoot.GetConnectionString("UploaderConnection"),
                    sinkOptions,
                    columnOptions: columnOpts,
                    appConfiguration: configurationRoot
                ).CreateLogger();

            return configurationRoot;
        }
    }
}
