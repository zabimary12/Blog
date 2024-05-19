namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IArticleRepository Articles { get; }
        ICommentRepository Comments { get; }
        ITagRepository Tags { get; }
        IUserRepository Users { get; }
    }
}