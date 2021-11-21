using Dapper.Contrib.Extensions;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("PostResources")]
    public class PostResource : BaseEntity
    {
        public int PostId { get; set; }

        public string ResourceUrl { get; set; }

        public int ResourceTypeId { get; set; }

        public int ResourceIndex { get; set; }
    }
}
