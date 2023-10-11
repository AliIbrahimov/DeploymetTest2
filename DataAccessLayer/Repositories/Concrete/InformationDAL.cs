using CoreLayer.DAL.EntityFramework;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Models;

namespace DataAccessLayer.Repositories.Concrete;

public class InformationDAL : BaseRepository<Information, AppDbContext>, IInfoDAL
{
    public InformationDAL(AppDbContext context) : base(context)
    {
    }
}
