using Imagegram.Functions.Dtos;
using Imagegram.Functions.Repositories;
using Imagegram.Functions.Repositories.Entities;
using Imagegram.Functions.Storages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Functions.Activities
{
    public class PostActivities
    {
        private readonly IPostRepository _postRepository;
        private readonly IRepository<PostResource> _postResourceRepository;
        private readonly IRepository<ResourceType> _resourceTypeRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IStorage _storage;

        public PostActivities(IPostRepository postRepository, IRepository<PostResource> postResourceRepository, IRepository<ResourceType> resourceTypeRepository, IRepository<Comment> commentRepository, IStorage storage)
        {
            _postRepository = postRepository;
            _postResourceRepository = postResourceRepository;
            _resourceTypeRepository = resourceTypeRepository;
            _commentRepository = commentRepository;
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

        public static string CreateSingleComment = nameof(CreateSingleCommentActivity);

        [FunctionName(nameof(CreateSingleCommentActivity))]
        public async Task CreateSingleCommentActivity([ActivityTrigger] SingleCommentCreation comment)
        {
            await _commentRepository.Insert(new Comment() { CommentText = comment.Comment, PostId = comment.PostId, CommentBy = comment.CommentBy });
        }

        public static string GetPosts = nameof(GetPostsActivity);

        [FunctionName(nameof(GetPostsActivity))]
        public async Task<PostsRetrievalResponse> GetPostsActivity([ActivityTrigger] PostsRetrievalQuery query)
        {
            IEnumerable<Post> posts;
            int maxItemPerPage = query.MaxItemPerPage;

            if (string.IsNullOrEmpty(query.ContinuationToken))
            {
                posts = await _postRepository.GetPostsWithComments(0, maxItemPerPage);
            }
            else
            {
                Dictionary<string, int> values = Utility.ReadContinuationToken(query.ContinuationToken);
                maxItemPerPage = values["MaxItemPerPage"];

                posts = await _postRepository.GetPostsWithComments(values["FromId"], maxItemPerPage);
            }

            return new PostsRetrievalResponse()
            {
                // If the posts result doesn't match with the maxItemPerPage or empty, then no continuation token will be generated
                ContinuationToken = posts.Count() < maxItemPerPage || !posts.Any() ? string.Empty : Utility.GenerateContinuationToken(new Dictionary<string, int>() { { "FromId", posts.Last().Id }, { "MaxItemPerPage", maxItemPerPage } }),
                Posts = posts.Select(p => new PostsRetrievalResponseItem() 
                {
                    PostId = p.Id,
                    Caption = p.Caption,
                    ImageUrl = p.ImageUrl,
                    Comments = p.Comments
                })
            };
        }
    }
}
