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
    }
}
