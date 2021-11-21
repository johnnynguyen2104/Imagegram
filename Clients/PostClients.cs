using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Imagegram.Functions.Orchestrators;
using Imagegram.Functions.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using System;
using Imagegram.Functions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Imagegram.Functions.Clients
{
    public static class PostClients
    {
        [FunctionName(nameof(CreateSinglePostClient))]
        public static async Task<HttpResponseMessage> CreateSinglePostClient(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            try
            {
                SinglePostCreation input = (await req.ReadFormAsync()).BindToModel<SinglePostCreation>();

                if (req.Form.Files["Image"] == null)
                {
                    throw new Exception("Missing image for the post.");
                }

                List<string> extensions = new List<string>() { ".png", ".jpg", ".bmp" };
                if (!extensions.Any(e => e == Path.GetExtension(req.Form.Files["Image"].Name)))
                {
                    throw new Exception("Only png, jpg and bmp are accepted.");
                }

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

        [FunctionName(nameof(CreateSingleCommentClient))]
        public static async Task<HttpResponseMessage> CreateSingleCommentClient(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
           [DurableClient] IDurableOrchestrationClient starter,
           ILogger log)
        {
            try
            {
                SingleCommentCreation input = await req.Content.ReadAsAsync<SingleCommentCreation>();

                string instanceId = await starter.StartNewAsync(nameof(PostOrchestrators.CreateSingleCommentOrchestrator), input);

                return await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FunctionName(nameof(GetPostsClient))]
        public static async Task<HttpResponseMessage> GetPostsClient(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req,
          [DurableClient] IDurableOrchestrationClient starter,
          ILogger log)
        {
            try
            {
                var queries = req.RequestUri.ParseQueryString();
                PostsRetrievalQuery input = new PostsRetrievalQuery() { ContinuationToken = queries.Get("ContinuationToken"), MaxItemPerPage = int.Parse(queries.Get("MaxItemPerPage") ?? "0") };

                string instanceId = await starter.StartNewAsync(nameof(PostOrchestrators.GetPostsOrchestrator), input);

                return await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
