using Microsoft.Extensions.DependencyInjection;
using UserService.Repository.Implementation;
using UserService.Repository.Interface;
using Service.Implementation;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.Middleware
{
    public  class ServiceRegisteryExtension
    {
        //public static IServiceCollection ServiceRegistery(this IServiceCollection service)

        //{
        //    service.AddScoped<IUserService, UserService>();
        //    //service.AddScoped<IMusicService, MusicService>();
        //   // service.AddScoped<IUserRepository, UserReposiroty>();
        //   // service.AddScoped<IMusicRepository, MusicRepository>();
        //    service.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();



        //    return service;
        //}
    }
}
