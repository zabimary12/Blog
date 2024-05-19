using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _db;
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _userRepository;

        public EfUnitOfWork(BlogContext db)
        {
            _db = db;
        }

        public IArticleRepository Articles => _articleRepository ?? (_articleRepository = new ArticleRepository(_db));

        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_db));

        public ITagRepository Tags => _tagRepository ?? (_tagRepository = new TagRepository(_db));

        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_db));
    }
}