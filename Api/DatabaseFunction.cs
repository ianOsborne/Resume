using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker.Http;
using System;

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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            [SqlInput(commandText: "SELECT [Name]  FROM [SalesLT].[Product] where ProductID = 680",
            connectionStringSetting: "connection-string"
            )]
            string outputData)
        {

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(outputData);

            return response;
        }
    }
}
