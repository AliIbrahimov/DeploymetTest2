using AutoMapper;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Contact;
using EntityLayer.DTOs.Info;
using EntityLayer.DTOs.SocialMedia;
using EntityLayer.DTOs.Statistic;
using EntityLayer.Models;

namespace BusinessLayer.Utilities.Profiles;

public class Mapper:Profile
{
	public Mapper()
	{
		CreateMap<BlogPostDTO, Blog>().ReverseMap();
		CreateMap<ContactPostDTO, Contact>().ReverseMap();
		CreateMap<BlogGetDTO, Blog>().ReverseMap();
		CreateMap<ContactGetDto, Contact>().ReverseMap();
		CreateMap<Information,InfoGetDto>().ReverseMap();
		CreateMap<SocialMediaAccount,GetSocialMediaDto>().ReverseMap();
		CreateMap<Statistic, PostStatisticDto>().ReverseMap();

	}
}
