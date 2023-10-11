using AutoMapper;
using BusinessLayer.Constants;
using BusinessLayer.Services.Abstract;
using CoreLayer.Utilities.Results;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.DTOs.Info;
using EntityLayer.DTOs.SocialMedia;
using EntityLayer.Models;

namespace BusinessLayer.Services.Concrete;

public class InfoManager : IInfoService
{
    private readonly IInfoDAL _infoDAL;
    private readonly IMapper _mapper;

    public InfoManager(IInfoDAL infoDAL, IMapper mapper)
    {
        _infoDAL = infoDAL;
        _mapper = mapper;
    }

    public async Task<IDataResult<InfoGetDto>> GetInfoAsync()
    {
        try
        {
            // Retrieve the Information entity with related SocialMediaAccounts
            var info = await _infoDAL.GetAsync(p => p.Id == 1,"SocialMediaAccounts");

            if (info is null)
                return new ErrorDataResult<InfoGetDto>("Information is null");

            var infoDto = _mapper.Map<InfoGetDto>(info);

            var socialMediaAccountDtos = info.SocialMediaAccounts
                .Select(s => _mapper.Map<GetSocialMediaDto>(s))
                .ToList();

            infoDto.SocialMediaAccounts = socialMediaAccountDtos;

            return new SuccessDataResult<InfoGetDto>(infoDto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<InfoGetDto>("An error occurred while fetching the information");
        }
    }


    public async Task<IResult> UpdateInfo(int id, InfoPostDto info)
    {
        // Check if the entity with the given ID exists
        var existingInfo = await _infoDAL.GetAsync(i => i.Id == id,  "SocialMediaAccounts");
        if (existingInfo is null)
        {
            return new ErrorResult(Messages.NotFound);
        }

        // Check and update each property if provided
        if (!string.IsNullOrWhiteSpace(info.AgencyLocation))
        {
            existingInfo.AgencyLocation = info.AgencyLocation;
        }

        if (!string.IsNullOrWhiteSpace(info.AgencyPhone))
        {
            existingInfo.AgencyPhone = info.AgencyPhone;
        }

        if (!string.IsNullOrWhiteSpace(info.AgencyMail))
        {
            existingInfo.AgencyMail = info.AgencyMail;
        }

        // Save changes to the database
        _infoDAL.Update(existingInfo);

        return new SuccessResult(Messages.Updated);
    }

}
