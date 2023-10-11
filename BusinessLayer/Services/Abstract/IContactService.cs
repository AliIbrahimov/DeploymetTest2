using CoreLayer.Utilities.Results;
using EntityLayer.DTOs.Contact;
using EntityLayer.Models;

namespace BusinessLayer.Services.Abstract;

public interface IContactService
{
    Task<IDataResult<List<ContactGetDto>>> GetAllContacts();
    Task<IDataResult<List<ContactGetDto>>> GetAllPaginateAsync(int page, int size);
    Task<IDataResult<ContactGetDto>> GetByIdAsync(int id);
    Task<IDataResult<Contact>> CreateContact(ContactPostDTO contactPostDto);
    Task<IDataResult<Contact>> HardDeleteByIdAsync(int id);




}
