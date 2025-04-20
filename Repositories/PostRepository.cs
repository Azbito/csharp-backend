using backend.Config;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var post = _context.Posts.Find(id);

        if (post != null)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
