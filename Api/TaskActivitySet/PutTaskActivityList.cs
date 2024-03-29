﻿using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskActivitySet
{
    public class PutTaskActivityList : Function
    {
        private TaskActivityService _taskActivityService;

        public PutTaskActivityList(AuthorizationService authorizationService, TaskActivityService taskActivityService)
            : base(authorizationService) { _taskActivityService = taskActivityService; }

        [FunctionName("PutTaskActivitySet")]
        public async Task Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "put", Route = "taskactivityset"),] HttpRequest req, ILogger log, CancellationToken ct)
        {
            log.LogTrace($"SetTaskActivityList function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!requestBody.Contains("[")) requestBody = $"[{requestBody}]";

            var data = JsonConvert.DeserializeObject<List<TaskActivity>>(requestBody);
            var userIdentifier = await GetTargetUserIdentifier(req);
            if (data[0].UserIdentifier != userIdentifier && ! await IsParent(req))
            {
                throw new SecurityException("Invalid attempt to access a record by an invalid user");
            }

            try
            {
                await _taskActivityService.UpdateList(data);
            }
            catch (Exception exception)
            {

                log.LogError($"Exception {exception.Message} occurred");
                throw;
            }

        }
    }
}
