using Dapper.Contrib.Extensions;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("Comments")]
    public class Comment : BaseEntity
    {
        public string CommentText { get; set; }

        public int PostId { get; set; }

        public int CommentBy { get; set; }
    }
}
