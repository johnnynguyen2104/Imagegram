using Imagegram.Functions.Repositories.Entities;
using System.Collections.Generic;

namespace Imagegram.Functions.Dtos
{
    public class PostsRetrievalResponseItem
    {
        public int PostId { get; set; }

        public string Caption { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
