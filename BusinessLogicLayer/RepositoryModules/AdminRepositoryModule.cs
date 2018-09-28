using Autofac;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.IAdmin;
using BusinessLogicLayer.Interfaces.IMyAuthentication;
using BusinessLogicLayer.Services.AdminServices;
using BusinessLogicLayer.Services.AuthenticationService;
using BusinessLogicLayer.Services.AutoMapper;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer;
using DataAccessLayer.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.RepositoryModules
{
    public class AdminRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AdminRepository(new DatabaseContext())).As<IAdminRepository>();
            
            builder.RegisterType<AnswerAdmin>().As<IAnswerAdmin>();
            builder.RegisterType<QuestionAdmin>().As<IQuestionAdmin>();
            builder.RegisterType<TestAdmin>().As<ITestAdmin>();
            builder.RegisterType<TestSchemeAdmin>().As<ITestSchemeAdmin>();
            builder.RegisterType<AuthenticationAdmin>().As<IAuthenticationAdmin>();
            builder.RegisterType<DropDownListHelper>().As<IDropDownListHelper>();
                        
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }       
    }
}
