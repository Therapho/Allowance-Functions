using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.Lookups
{
    public class GetTaskGroupList : LookupFunction<TaskGroup>
    {
        public GetTaskGroupList(DatabaseContext databaseContext) : base(databaseContext) { }

        [FunctionName("GetTaskGroupList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "lookups/taskgroupset"), ] HttpRequest req, ILogger log)
        {
            return await RunInternal(log);
        }
    }
}
