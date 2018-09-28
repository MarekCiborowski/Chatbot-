using BotChat.Interfaces;
using BusinessLogicLayer.Interfaces.IUser;
using BusinessLogicLayer.Services.UserServices;
using DataAccessLayer.Models;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BotChat
{
    public class JsonProvider : IJsonProvider
    {
        private IUserServices _userService;
        public  JsonProvider(IUserServices userServices)
        {
            _userService = userServices;
        }

        //public JsonProvider (IUserServices userService)
        //{
        //    _userService = userService;
        //}


        public string GetJsonForTest(int testID)
        {
           
            // ID pytania jest inkrementowane o 100;
            // int testID = _userService.GetTestByKey(testKey);

           // Category cat1 = new Category { categoryID = 1, categoryName = "C#" };
           // Category cat2 = new Category { categoryID = 2, categoryName = "C++" };
           // Category cat3 = new Category { categoryID = 3, categoryName = "Java" };
           // Category cat4 = new Category { categoryID = 4, categoryName = "JavaScript" };
           // Category cat5 = new Category { categoryID = 5, categoryName = "Python" };

           // QuestionType qt1 = new QuestionType { questionTypeID = 1, typeName = "Otwarte" };
           // QuestionType qt2 = new QuestionType { questionTypeID = 2, typeName = "Jednokrotnego wyboru" };
           // QuestionType qt3 = new QuestionType { questionTypeID = 3, typeName = "Wielokrotnego wyboru" };

           // Question qu3 = new Question { questionID = 1, value = "Pytanie 1", categoryID = cat1.categoryID, points = 1, questionTypeID = qt1.questionTypeID };
           // Question qu2 = new Question { questionID = 2, value = "Pytanie 2", categoryID = cat2.categoryID, points = 2, questionTypeID = qt2.questionTypeID };
           // Question qu1 = new Question { questionID = 3, value = "Pytanie 3", categoryID = cat3.categoryID, points = 3, questionTypeID = qt3.questionTypeID };

           // Answer a1 = new Answer { answerID = 1, value = "odpowiedz 1", isCorrect = true, questionID = 2 };
           // Answer a2 = new Answer { answerID = 2, value = "odpowiedz 2", isCorrect = false, questionID = 2 };

           // Answer a3 = new Answer { answerID = 3, value = "odpowiedz 1", isCorrect = true, questionID = 3 };
           // Answer a4 = new Answer { answerID = 4, value = "odpowiedz 2", isCorrect = false, questionID = 3 };
           // Answer a5 = new Answer { answerID = 5, value = "odpowiedz 3", isCorrect = true, questionID = 3 };
           // Answer a6 = new Answer { answerID = 6, value = "odpowiedz 3", isCorrect = false, questionID = 3 };
           // Answer a7 = new Answer { answerID = 7, value = "odpowiedz 4", isCorrect = false, questionID = 3 };
           // Answer a8 = new Answer { answerID = 8, value = "odpowiedz 5", isCorrect = false, questionID = 3 };
           // Answer a9 = new Answer { answerID = 9, value = "odpowiedz 6", isCorrect = true, questionID = 3 };

           // qu2.answer.Add(a1);
           // qu2.answer.Add(a2);

           // qu3.answer.Add(a3);
           // qu3.answer.Add(a4);
           // //qu3.answer.Add(a5);
           // qu3.answer.Add(a6);
           // qu3.answer.Add(a7);
           // qu3.answer.Add(a8);
           // qu3.answer.Add(a9);

            //IEnumerable<Question> questions = new List<Question> { qu1, qu2, qu3 };
            IEnumerable<Question> questions = _userService.GetQuestionsByTestId(testID);


            StringBuilder sb = new StringBuilder();
            sb.Append("{\n\t\"type\": \"object\",\n\t\"required\": [\"Confirmation\",");
            foreach(Question question in questions)
            {
                sb.Append("\"").Append( question.questionID+100).Append("\",");
            }
            sb.Remove(sb.Length - 1, 1);

            sb.Append("],\n\t\"Templates\": {\n\t\t\"NotUnderstood\": {\n\t\t\t\"Patterns\": [\"I do not understand. Try again.\"]\n\t\t}\n\t},");
            sb.Append("\n\t\"properties\":{");

            foreach(Question question in questions)
            {
                sb.Append("\n\t\t\"").Append(question.questionID+100).Append("\":{\n\t\t\t\"Prompt\": {\n\t\t\t\t\"Patterns\": [");
                int questionType = question.questionTypeID;
                //1. Pytanie otwarte, 2. Pytanie jednokrotnego wyboru, 3. Pytanie wielokrotnego wyboru
                switch (questionType){
                    case 1:
                        sb.Append("\"").Append(question.value).Append(" (enter your answer)").Append("\"").
                            Append("]\n\t\t\t},\n\t\t\t\"Describe\": \"").Append(question.value).Append("\",\n\t\t\t\"type\": [ \"string\", \"null\" ]\n\t\t},");
    
                        break;
                    case 2:
                        IEnumerable<Answer> answersForOneOptionQuestion = _userService.GetAnswersForQuestion(question.questionID);
                        //IEnumerable<Answer> answersForOneOptionQuestion = new List<Answer> { a1, a2 };
                        sb.Append("\"").Append(question.value).Append(" (click correct answer)").Append(" {||}\"").
                            Append("]\n\t\t\t},\n\t\t\t\"Describe\": \"").Append(question.value).Append("\",\n\t\t\t\"type\": \"string\",\n\t\t\t\"enum\": [");
                        foreach(Answer answer in answersForOneOptionQuestion)
                        {
                            sb.Append("\n\t\t\t\t\"").Append(answer.value).Append("\",");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n\t\t\t]\n\t\t},");


                        break;
                    case 3:
                        IEnumerable<Answer> answersForManyOptionsQuestion = _userService.GetAnswersForQuestion(question.questionID);
                        //IEnumerable<Answer> answersForManyOptionsQuestion = new List<Answer> { a3, a4, a6, a7, a8, a9 };
                        sb.Append("\"").Append(question.value).Append(" (enter answers' numbers, for instance: 1,2,3)").Append(" {||}\"").
                           Append("]\n\t\t\t},\n\t\t\t\"Describe\": \"").Append(question.value).Append("\",\n\t\t\t\"type\": \"array\",").
                           Append("\n\t\t\t\"items\": {\n\t\t\t\t\"type\": \"string\",\n\t\t\t\t\"enum\": [ ");
                        foreach (Answer answer in answersForManyOptionsQuestion)
                        {
                            sb.Append("\n\t\t\t\t\t\"").Append(answer.value).Append("\",");
                        }
                        
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n\t\t\t\t]\n\t\t\t}\n\t\t},");
                        break;




                }
                
            }


            sb.Append("\n\t\t\"").Append("Confirmation").Append("\":{\n\t\t\t\"Prompt\": {\n\t\t\t\t\"Patterns\": [").
                Append("\"To check your answers enter (status), to change your answers enter number of question: ");
            foreach (Question question in questions)
            {
                sb.Append("\n\t\t\t\t<br/>").Append(question.questionID + 100).Append(": ").Append(question.value);
            }
            sb.Append("<br/>To go back to confirmation screen enter (Confirmation) {||}\"]\n\t\t\t},").Append("\n\t\t\t\"Describe\": \"Confirmation status\",").
                Append("\n\t\t\t\"type\": \"string\",").Append("\n\t\t\t\"enum\": [ ").
                Append("\n\t\t\t\t\"Finish him\"").Append("\n\t\t\t]").Append("\n\t\t}");



            //sb.Remove(sb.Length - 1, 1);
            //sb.Append("\n\t},\n\t\"OnCompletion\": \"await context.PostAsync(\\\"WIDZICIE DZIAŁA TEN BOT\\\");\"\n}");
            sb.Append("\n\t}\n}");
            using (StreamWriter sw = new StreamWriter("D:\\CDriveDirs.txt"))
            {
                sw.WriteLine(sb.ToString());
            }

            return sb.ToString();


        }

        public string GetValidationJson()
        {
            
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n\t\"type\": \"object\",\n\t\"required\": [\"testKey\"");
            sb.Append("],\n\t\"Templates\": {\n\t\t\"NotUnderstood\": {\n\t\t\t\"Patterns\": [\"I do not understand. Try again.\"]\n\t\t}\n\t},");
            sb.Append("\n\t\"properties\":{").Append("\n\t\t\"testKey\": {").Append("\n\t\t\t\"Prompt\": {");
            sb.Append("\n\t\t\t\t\"Patterns\": [\"Enter received test key\"]").Append("\n\t\t\t},").Append("\n\t\t\t\"Describe\": \"Value of test key\",");
            sb.Append("\n\t\t\t\"type\" : [ \"string\", \"null\" ]").Append("\n\t\t}").Append("\n\t}").Append("\n}");
            using (StreamWriter sw = new StreamWriter("D:\\Validacyjka.txt"))
            {
                sw.WriteLine(sb.ToString());
            }
            return sb.ToString();
        }

    }
}