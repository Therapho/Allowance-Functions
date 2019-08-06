﻿using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.Lookups
{
    public class GetStatusList : LookupService<Status>
    {
        public GetStatusList(DatabaseContext context) : base(context) { }

        [FunctionName("GetStatusList")]
        public async Task<List<Status>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lookups/statusset"),] HttpRequest req, ILogger log)
        {
            return RunInternal(log);
        }
    }
}
