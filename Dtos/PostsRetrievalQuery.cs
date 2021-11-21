namespace Imagegram.Functions.Dtos
{
    public class PostsRetrievalQuery
    {
        public string ContinuationToken { get; set; }

        public int MaxItemPerPage { get; set; } = 10;
    }
}
