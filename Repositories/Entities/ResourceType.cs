using Dapper.Contrib.Extensions;

namespace Imagegram.Functions.Repositories.Entities
{
    [Table("ResourceTypes")]
    public class ResourceType : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
