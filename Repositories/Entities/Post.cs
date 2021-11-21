using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public string Caption { get; set; }

        public int PostBy { get; set; }

        [Write(false)]
        public IEnumerable<PostResource> Resources { get; set; }

        [Write(false)]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
