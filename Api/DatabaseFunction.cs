using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace Api
{
    public class DatabaseFunction
    {
        private readonly ILogger _logger;

        public DatabaseFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DatabaseFunction>();
        }

        [Function("DatabaseFunction")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest req,
            [SqlInput(commandText: "SELECT * FROM [SalesLT].[Customer] where CustomerID = 1",
                parameters: "@Id={Query.id}",
                connectionStringSetting: "DATABASE_CONNECTION_STRING")]
            string outputData
            )
        {
            return outputData;
        }
    }
}
