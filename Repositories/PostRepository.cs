using Dapper;
using Imagegram.Functions.Repositories.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Functions.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public async Task<IList<Post>> GetPostsWithComments(int fromId, int maxItemPerPage)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                using (var batch = await connection.QueryMultipleAsync("dbo.GetPostsWithComments", new { FromPostId = fromId, MaxItemPerPage = maxItemPerPage }, commandType: System.Data.CommandType.StoredProcedure))
                {
                    IList<Post> posts = batch.Read<Post>().ToList();
                    IEnumerable<PostResource> resources = batch.Read<PostResource>();
                    IEnumerable<Comment> comments = batch.Read<Comment>();

                    foreach (Post item in posts)
                    {
                        item.Resources = resources.Where(r => r.PostId == item.Id);
                        item.Comments = new List<Comment>(comments.Where(c => c.PostId == item.Id));
                    }

                    return posts;
                }
            }
        }
    }
}
