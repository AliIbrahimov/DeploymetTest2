using CoreLayer.DAL.EntityFramework;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Models;

namespace DataAccessLayer.Repositories.Concrete;

public class ContactDAL : BaseRepository<Contact, AppDbContext>, IContactDAL
{
    public ContactDAL(AppDbContext context) : base(context)
    {
    }
}
