﻿using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AllowanceFunctions.Api.TaskActivitySet
{
    public class GetTaskActivityListByDay : Function    
    {
        private TaskActivityService _taskActivityService;

        public GetTaskActivityListByDay(AuthorizationService authorizationService, TaskActivityService taskActivityService)
            : base(authorizationService) { _taskActivityService = taskActivityService; }

        [FunctionName("GetTaskActivityList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "taskactivityset"),] HttpRequest req, ILogger log)
        {
            var taskWeekId = req.Query.GetValue<int>("taskweekid");
            var userIdentifier = req.GetUserIdentifier();

            Ensure.That(taskWeekId).IsGt(0);

            log.LogTrace($"GetTaskActivityListByDay function processed a request for taskWeekId={taskWeekId}.");

            

            List<TaskActivity> result = null;
            try
            {
                result = await _taskActivityService.GetList(userIdentifier, taskWeekId);
            }
            catch (Exception exception)
            {
               
                return new BadRequestObjectResult($"Error trying to execute GetActivityList with TaskWeekId:{taskWeekId} and userIdentifier:{userIdentifier}.  {exception.Message}");
            }
            
            return new OkObjectResult( result);
        }
    }
}
