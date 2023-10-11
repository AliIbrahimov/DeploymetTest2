using CoreLayer.DAL.EntityFramework;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Models;

namespace DataAccessLayer.Repositories.Concrete;

public class StatisticDal : BaseRepository<Statistic, AppDbContext>, IStatisticDal
{
    public StatisticDal(AppDbContext context) : base(context)
    {
    }
}
