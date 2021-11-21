using Dapper.Contrib.Extensions;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string Username { get; set; }
    }
}
