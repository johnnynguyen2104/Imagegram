using Dapper.Contrib.Extensions;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public string Caption { get; set; }

        public int PostBy { get; set; }
    }
}
