using System.Web.Http;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Autofac;
using Microsoft.Bot.Connector;
using System.Reflection;
using Autofac.Integration.WebApi;
using BusinessLogicLayer.Services.UserServices;
using BusinessLogicLayer.Interfaces.IUser;
using BusinessLogicLayer.RepositoryModules;
using BotChat.Interfaces;
using BotChat.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace BotChat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
           

            Conversation.UpdateContainer(
            builder =>
            {
                builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));
                builder.RegisterModule(new UserRepositoryModule());
                //builder.RegisterType<JsonScheme>().As<IJsonScheme>();
                //builder.RegisterType<JsonProvider>().As<IJsonProvider>();
                builder
            .RegisterType<RootDialog>()
            .Keyed<RootDialog>(FiberModule.Key_DoNotSerialize)
            .AsSelf()
            .As<RootDialog>()
            .SingleInstance();
                builder
            .RegisterType<JsonScheme>()
            .Keyed<JsonScheme>(FiberModule.Key_DoNotSerialize)
            .AsSelf()
            .As<JsonScheme>()
            .SingleInstance();

                // Bot Storage: Here we register the state storage for your bot. 
                // Default store: volatile in-memory store - Only for prototyping!
                // We provide adapters for Azure Table, CosmosDb, SQL Azure, or you can implement your own!
                // For samples and documentation, see: [https://github.com/Microsoft/BotBuilder-Azure](https://github.com/Microsoft/BotBuilder-Azure)
                var store = new InMemoryDataStore();

                // Other storage options
                // var store = new TableBotDataStore("...DataStorageConnectionString..."); // requires Microsoft.BotBuilder.Azure Nuget package 
                // var store = new DocumentDbBotDataStore("cosmos db uri", "cosmos db key"); // requires Microsoft.BotBuilder.Azure Nuget package 

                //Autofac config
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
                

                //End of autofac config

                builder.Register(c => store)
                    .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                    .AsSelf()
                    .SingleInstance();
            });
            


            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
            
        }
    }
}
