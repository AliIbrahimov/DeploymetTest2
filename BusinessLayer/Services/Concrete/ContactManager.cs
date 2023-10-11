using AutoMapper;
using BusinessLayer.Constants;
using BusinessLayer.Services.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using CoreLayer.Utilities.Results;
using DataAccessLayer.Repositories.Abstract;
using DataAccessLayer.Repositories.Concrete;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Contact;
using EntityLayer.Models;

namespace BusinessLayer.Services.Concrete;

public class ContactManager : IContactService
{
    private readonly IContactDAL _contactDAL;
    private readonly IMapper _mapper;
    private readonly ContactValidator validator = new ContactValidator();

    public ContactManager(IContactDAL contactDAL, IMapper mapper)
    {
        _contactDAL = contactDAL;
        _mapper = mapper;
    }

    public async Task<IDataResult<Contact>> CreateContact(ContactPostDTO contactPostDto)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(contactPostDto);
            if (!validationResult.IsValid)
            {
                string errorMessages = string.Join("\n", validationResult.Errors.Select(error => error.ErrorMessage));
                return new ErrorDataResult<Contact>(errorMessages);
            }
            Contact contact = _mapper.Map<Contact>(contactPostDto);
            await _contactDAL.CreateAsync(contact);

            return new SuccessDataResult<Contact>(contact, Messages.Added);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<Contact>($"Not added! Error: {ex.Message}");
        }
    }

    public async Task<IDataResult<List<ContactGetDto>>> GetAllContacts()
    {
        try
        {
            var contacts = _mapper.Map<List<ContactGetDto>>(await _contactDAL.GetAllAsync(d => !d.isDeleted));
            if (contacts is null)
                return new ErrorDataResult<List<ContactGetDto>>("Empty");
            return new SuccessDataResult<List<ContactGetDto>>(contacts,Messages.Listed);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<ContactGetDto>>("Somthing went wrong!");
        }
    }

    public async Task<IDataResult<List<ContactGetDto>>> GetAllPaginateAsync(int page, int size)
    {
        try
        {
            var contacts = _mapper.Map<List<ContactGetDto>>(await _contactDAL.GetAllPaginateAsync(page, size, b => !b.isDeleted));
            if (contacts.Count is 0)
                return new ErrorDataResult<List<ContactGetDto>>(Messages.NotFound);
            return new SuccessDataResult<List<ContactGetDto>>(contacts);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<ContactGetDto>>("Somthing went wrong!");
        }
    }

    public async Task<IDataResult<ContactGetDto>> GetByIdAsync(int id)
    {
        try
        {
            if (!(await _contactDAL.IsExistAsync(b => b.Id == id)))
                return new ErrorDataResult<ContactGetDto>(Messages.NotFound);

            var existingContact = await _contactDAL.GetAsync(p => p.Id == id && !p.isDeleted);
            var contact = _mapper.Map<ContactGetDto>(existingContact);
            return new SuccessDataResult<ContactGetDto>(contact,Messages.Listed);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<ContactGetDto>("No blog found on this id");
        }
    }

    public async Task<IDataResult<Contact>> HardDeleteByIdAsync(int id)
    {
        try
        {
            if (await _contactDAL.IsExistAsync(c=>c.Id==id))
            {
                Contact existingContact = await _contactDAL.GetAsync(p => p.Id == id);
                _contactDAL.Delete(existingContact);
                return new SuccessDataResult<Contact>(existingContact, Messages.Deleted);
            }
            else
                return new ErrorDataResult<Contact>(Messages.NotFound);

        }
        catch (Exception ex)
        {

            return new ErrorDataResult<Contact>(Messages.Notdeleted);
        }
    }
}
