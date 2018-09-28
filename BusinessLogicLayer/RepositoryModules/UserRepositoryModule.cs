using Autofac;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using BusinessLogicLayer.Services.AdminServices;
using BusinessLogicLayer.Services.AuthenticationService;
using BusinessLogicLayer.Interfaces.IAdmin;
using BusinessLogicLayer.Interfaces.IMyAuthentication;
using BusinessLogicLayer.Services.UserServices;
using BusinessLogicLayer.Interfaces.IUser;

namespace BusinessLogicLayer.RepositoryModules
{
    public class UserRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.Register(c => new UserRepository(new DatabaseContext())).As<IUserRepository>();
            
            
            builder.RegisterType<UserService>().As<IUserServices>();
            
            
        }

    }
}
