using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Imagegram.Functions.Orchestrators;
using Imagegram.Functions.Dtos;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using System;
using Imagegram.Functions.Extensions;

namespace Imagegram.Functions.Clients
{
    public static class PostClients
    {
        [FunctionName(nameof(CreateSinglePostClient))]
        public static async Task<HttpResponseMessage> CreateSinglePostClient(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                SinglePostCreation input = (await req.ReadFormAsync()).BindToModel<SinglePostCreation>();

                using (var ms = new MemoryStream())
                {
                    req.Form.Files["Image"].CopyTo(ms);
                    input.Image = ms.ToArray();
                }

                string instanceId = await starter.StartNewAsync(nameof(PostOrchestrators.CreateSinglePostOrchestrator), input);

                return await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(new HttpRequestMessageFeature(req.HttpContext).HttpRequestMessage, instanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
