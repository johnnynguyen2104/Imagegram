using System;
using System.Collections.Generic;
using System.Text;

namespace Imagegram.Functions.Dtos
{
    public class PostsRetrievalResponse
    {
        public IEnumerable<PostsRetrievalResponseItem> Posts { get; set; }

        public string ContinuationToken { get; set; }
    }
}
