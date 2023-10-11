using CoreLayer.Utilities.Results;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Info;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.Abstract;

public interface IInfoService
{
    Task<IDataResult<InfoGetDto>> GetInfoAsync();
    Task<CoreLayer.Utilities.Results.IResult> UpdateInfo(int id, InfoPostDto info);
}
