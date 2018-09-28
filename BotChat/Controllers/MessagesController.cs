using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BusinessLogicLayer.Services.UserServices;
using BusinessLogicLayer.Interfaces.IUser;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Builder.FormFlow;
using System.Web.Http.Description;
using System.Diagnostics;
using BotChat.Dialogs;
using BotChat.Interfaces;
using System;

namespace BotChat
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {

        private static UserService _userService;
        static MessagesController()
        {
            IUserServices _userService = (UserService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(UserService));
        }
        //internal static IDialog<JObject> MakeRootDialog()
        //{

        //    return Chain.From(() => FormDialog.FromForm(JsonScheme.BuildJsonForm));
        //}

        //internal static IDialog<JObject> MakeValidationDialog()
        //{

        //    return Chain.From(() => FormDialog.FromForm(JsonScheme.ValidateUser));
        //}

        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Microsoft.Bot.Connector.Activity activity)
        {
            if (activity != null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:


                       
                        await Conversation.SendAsync(activity, () =>  new RootDialog(_userService));
                        //await Conversation.SendAsync(activity, MakeRootDialog);


                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;

        }


        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        //public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        //{
        //    if (activity.GetActivityType() == ActivityTypes.Message)
        //    {
        //        await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
        //    }
        //    else
        //    {
        //        HandleSystemMessage(activity);
        //    }
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    return response;
        //}

        private Microsoft.Bot.Connector.Activity HandleSystemMessage(Microsoft.Bot.Connector.Activity message)
        {
            string messageType = message.GetActivityType();
            if (messageType == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (messageType == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (messageType == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (messageType == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (messageType == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}