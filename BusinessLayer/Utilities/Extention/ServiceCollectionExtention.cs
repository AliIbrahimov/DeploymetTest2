using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstract;
using DataAccessLayer.Repositories.Concrete;
using EntityLayer.DTOs.Blog;
using EntityLayer.DTOs.Contact;
using EntityLayer.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace BusinessLayer.Utilities.Extention;

public static class ServiceCollectionExtention
{
    public static IServiceCollection ServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBlogDAL,BlogDAL>();
        services.AddScoped<IBlogService,BlogManager>();
        services.AddScoped<IContactService,ContactManager>();
        services.AddScoped<IContactDAL, ContactDAL>();
        services.AddScoped<IInfoService,InfoManager>();
        services.AddScoped<IInfoDAL, InformationDAL>();
        services.AddScoped<IStatisticService,StatisticManager>();
        services.AddScoped<IStatisticDal, StatisticDal>();
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        services.AddScoped<IValidator<BlogPostDTO>, BlogValidator>();
        services.AddScoped<IValidator<ContactPostDTO>, ContactValidator>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                              policy =>
                              {
                                  policy
                                  .AllowAnyMethod()
                                  .AllowAnyOrigin()
                                  .AllowAnyHeader();
                              });
        });
        return services;
        
    }
}
