using System;
using System.Threading;
using System.Threading.Tasks;
using BotChat.Interfaces;
using BusinessLogicLayer.Interfaces.IUser;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace BotChat.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private IUserServices _userServices;
        //IJsonScheme jsonScheme;
        public RootDialog(IUserServices userServices)
        {
            _userServices = userServices;
        }


        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            JsonScheme jsonScheme = new JsonScheme(_userServices);
            var myform = new FormDialog<JObject>(new JObject(), jsonScheme.ValidateUser, FormOptions.PromptInStart, null);
            context.Call(myform,TestDialog);
        }

        private async Task TestDialog(IDialogContext context, IAwaitable<object> result)
        {
            JsonScheme jsonScheme = new JsonScheme(_userServices);
            
            //bool isValidated=false;
            string testKey;
            int? testID;

            context.UserData.TryGetValue("tescik",out testKey);
            testID = _userServices.GetTestByKey(testKey);

            if (testID == -1)
            {
                await context.PostAsync("Sorry, but your key is incorrect.");

                
                var form = new FormDialog<JObject>(new JObject(), jsonScheme.ValidateUser, FormOptions.PromptInStart, null);
                context.Call(form, MessageReceivedAsync);
            }
            else if (testID == -2)
            {
                await context.PostAsync("Sorry, but your test was expired.");
                var form = new FormDialog<JObject>(new JObject(), jsonScheme.ValidateUser, FormOptions.PromptInStart, null);
                context.Call(form, MessageReceivedAsync);
            }
            else if (testID == -3)
            {
                await context.PostAsync("Sorry, but your test was solved");
                var form = new FormDialog<JObject>(new JObject(), jsonScheme.ValidateUser, FormOptions.PromptInStart, null);
                context.Call(form, MessageReceivedAsync);
            }
            else if(testID == null)
            {
                await context.PostAsync("Unexpected error.");
                var form = new FormDialog<JObject>(new JObject(), jsonScheme.ValidateUser, FormOptions.PromptInStart, null);
                context.Call(form, MessageReceivedAsync);
            }

            //isValidated = true;
            ////await context.PostAsync(testKey);
            //if (!isValidated)
            //{
            //    await context.PostAsync("We cannot match test key you entered.");
            //    var form = new FormDialog<JObject>(new JObject(), new JsonScheme(_userServices).ValidateUser, FormOptions.PromptInStart, null);
            //    context.Call(form, MessageReceivedAsync);

            //}
            JsonScheme.testID = testID.GetValueOrDefault(); // nie działa poprzez konstruktor 😢
            var myform = new FormDialog<JObject>(new JObject(), jsonScheme.BuildJsonForm, FormOptions.PromptInStart, null);
            context.Call(myform, EndDialog);
            //bool isValidated = false;
            //int testID = -4;
            //while (!isValidated)
            //{

            //    var key = await result;
            //    //testID = _userServices.GetTestByKey(key.ToString());
            //    if (testID == -1)
            //    {
            //        isValidated = true;
            //        break;
            //    }
            //    testID++;
            //};
            //// Return our reply to the user


            //context.Wait(MessageReceivedAsync);
        }
        private async Task EndDialog(IDialogContext context, IAwaitable<object> result)
        {
            
            context.Done(this);
            
        }
    }
}