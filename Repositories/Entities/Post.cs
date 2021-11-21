using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public string Caption { get; set; }

        public int PostBy { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
