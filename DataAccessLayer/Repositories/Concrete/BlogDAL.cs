using CoreLayer.DAL.EntityFramework;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Models;

namespace DataAccessLayer.Repositories.Concrete;

public class BlogDAL : BaseRepository<Blog, AppDbContext>, IBlogDAL
{
    public BlogDAL(AppDbContext context) : base(context)
    {
    }
}
 