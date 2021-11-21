using Imagegram.Functions.Dtos;
using Imagegram.Functions.Repositories;
using Imagegram.Functions.Repositories.Entities;
using Imagegram.Functions.Storages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagegram.Functions.Activities
{
    public class PostActivities
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<PostResource> _postResourceRepository;
        private readonly IRepository<ResourceType> _resourceTypeRepository;
        private readonly IStorage _storage;

        public PostActivities(IRepository<Post> postRepository, IRepository<PostResource> postResourceRepository, IRepository<ResourceType> resourceTypeRepository, IStorage storage)
        {
            _postRepository = postRepository;
            _postResourceRepository = postResourceRepository;
            _resourceTypeRepository = resourceTypeRepository;
            _storage = storage;
        }


        public static string CreateSinglePost = nameof(CreateSinglePostActivity);

        [FunctionName(nameof(CreateSinglePostActivity))]
        public async Task CreateSinglePostActivity([ActivityTrigger] SinglePostCreation singlePost)
        {
            int createdPostId = await _postRepository.Insert(new Post() { PostBy = singlePost.PostBy, Caption = singlePost.Caption });
            ResourceType resourceType = (await _resourceTypeRepository.Query("Select id FROM ResourceTypes WHERE Code = @TypeCode", new { TypeCode = "IMG" })).FirstOrDefault();

            if (resourceType == null)
            {
                throw new Exception("No IMG resource type was found.");
            }

            string imageEndpoint = await _storage.Upload(singlePost.Image);

            await _postResourceRepository.Insert(new PostResource() { PostId = createdPostId, ResourceIndex = 0, ResourceUrl = imageEndpoint, ResourceTypeId = resourceType.Id });
        }
    }
}
