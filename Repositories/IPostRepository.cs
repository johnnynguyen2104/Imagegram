using Imagegram.Functions.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagegram.Functions.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IList<Post>> GetPostsWithComments(int fromId, int maxItemPerPage);
    }
}
