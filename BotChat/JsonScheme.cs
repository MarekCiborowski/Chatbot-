using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using BotChat.Interfaces;
using BusinessLogicLayer.Interfaces.IUser;
using BusinessLogicLayer.Services.UserServices;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Newtonsoft.Json.Linq;

namespace BotChat
{
    [Serializable]
    public class JsonScheme
    {

        //Conversation.Container.Resolve<IJsonScheme>();
        private IUserServices _userService;
        private IJsonProvider _jsonProvider;
        public static int testID { get; set; }

        public JsonScheme(IUserServices userServices)
        {
            _userService = userServices;
            _jsonProvider = new JsonProvider(userServices);
        }
        //private IJsonProvider jsonProvider;
        //public JsonScheme(IUserServices _userService, IJsonProvider _jsonProvider)
        //{
        //    userService = _userService;
        //    jsonProvider = _jsonProvider;
        //}

        public IForm<JObject> ValidateUser()
        {

            OnCompletionAsyncDelegate<JObject> endingResult = async (context, state) =>
            {
                string testKey = String.Empty;
                foreach (JProperty item in (JToken)state)
                {
                    testKey = item.Value.ToString();
                }

                context.UserData.SetValue("tescik", testKey);
            };
            //This one is starter Form and is calling the second one

            var schema = JObject.Parse(_jsonProvider.GetValidationJson());
            return new FormBuilderJson(schema)
                .AddRemainingFields()
                .OnCompletion(endingResult)
                .Build();
        }

        public IForm<JObject> BuildJsonForm()
        {
            OnCompletionAsyncDelegate<JObject> processResult = async (context, state) =>
            {
                state.Remove("Confirmation");
                //List<int> IDquestions = new List<int>();
                List<HelperClass> list = new List<HelperClass>();

                int j = 0;
                // Iterate the responses and do what you like here
                foreach (JProperty item in (JToken)state)
                {

                    int questionID = Int32.Parse(item.Name.ToString()) - 100;
                    string answer = item.Value.ToString();

                    int questionTypeID = _userService.GetQuestionTypeID(questionID);

                    // int questionTypeID = questionType.questionTypeID;
                    if (questionTypeID == 3)//"wielokrotnego wyboru")
                    {
                        string an = answer.Remove(0, 6);//.Remove(answer.Length - 1, 1);
                        string answ = an.Remove(an.Length - 4, 4);

                        string[] answers = answ.Split(',');
                        if (answers.Count() != 1)
                            for (int i = 0; i < answers.Count(); i++)
                            {
                                string oneAnswer = answers[i];
                                if (i == 0)
                                {
                                    an = oneAnswer.Remove(oneAnswer.Length - 1, 1);
                                    answers[i] = an;
                                    HelperClass helper = new HelperClass(questionID, an);
                                    list.Add(helper);
                                }

                                else if (i == answers.Count() - 1)
                                {
                                    an = oneAnswer.Remove(0, 5);
                                    answers[i] = an;
                                    HelperClass helper = new HelperClass(questionID, an);
                                    list.Add(helper);
                                }
                                else
                                {
                                    an = oneAnswer.Remove(0, 5);
                                    oneAnswer = an;
                                    an = oneAnswer.Remove(oneAnswer.Length - 1, 1);
                                    answers[i] = an;
                                    HelperClass helper = new HelperClass(questionID, an);
                                    list.Add(helper);
                                }
                            }
                        else
                        {
                            HelperClass helper = new HelperClass(questionID, answ);
                            list.Add(helper);
                        }
                    }
                    else if (questionTypeID == 2)//jednokrotnego wyboru
                    {
                        HelperClass helper = new HelperClass(questionID, answer);
                        list.Add(helper);
                    }
                    else if (questionTypeID == 1)//Pytanie otwarte
                    {
                        HelperClass helper = new HelperClass(questionID, answer);
                        list.Add(helper);
                    }
                    else//Pytanie otwarte i jednokrotnego wyboru
                    {
                        await context.PostAsync("Coś się zjebało się");
                    }
                }
                foreach(HelperClass item in list)
                {
                    GivenAnswer givenAnswer = _userService.CreateGivenAnswer(testID, item.questionID, item.answer);
                    _userService.AddGivenAnswer(givenAnswer);
                }
                _userService.FinalizingTest(testID);//, TimeSpan.MinValue);
                await context.PostAsync("xd");
                // await context.PostAsync(msg);
                await context.PostAsync("Thanks for completing our test");


            };


            var schema = JObject.Parse(_jsonProvider.GetJsonForTest(testID));
            return new FormBuilderJson(schema)
                .AddRemainingFields()
                .OnCompletion(processResult)
                .Build();


            //using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BotChat.schema.json"))
            //{
            //    JsonProvider.GetJsonForTest();
            //    var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());
            //    return new FormBuilderJson(schema)
            //        .AddRemainingFields()
            //        .Build();
            //}

        }
    }
}