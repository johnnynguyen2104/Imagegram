using Imagegram.Functions.Activities;
using Imagegram.Functions.Dtos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;

namespace Imagegram.Functions.Orchestrators
{
    public class PostOrchestrators
    {
        [FunctionName(nameof(CreateSinglePostOrchestrator))]
        public static async Task CreateSinglePostOrchestrator(
           [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            SinglePostCreation input = context.GetInput<SinglePostCreation>();

            await context.CallActivityAsync(PostActivities.CreateSinglePost, input);
        }

        [FunctionName(nameof(CreateSingleCommentOrchestrator))]
        public static async Task CreateSingleCommentOrchestrator(
          [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            SingleCommentCreation input = context.GetInput<SingleCommentCreation>();

            await context.CallActivityAsync(PostActivities.CreateSinglePost, input);
        }

        [FunctionName(nameof(GetPostsOrchestrator))]
        public static async Task<PostsRetrievalResponse> GetPostsOrchestrator(
         [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            PostsRetrievalQuery input = context.GetInput<PostsRetrievalQuery>();

            return await context.CallActivityAsync<PostsRetrievalResponse>(PostActivities.GetPosts, input);
        }
    }
}
