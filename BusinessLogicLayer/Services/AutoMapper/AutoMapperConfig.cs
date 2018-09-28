using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.AutoMapper
{
    class AutoMapperConfig : IAutoMapperConfig
    {
        public AutoMapperConfig()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            
            var mapper = config.CreateMapper();

        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, QuestionVM>();
            CreateMap<QuestionVM, Question>();

            CreateMap<Answer, AnswerVM>();
            CreateMap<AnswerVM, Answer>();

            CreateMap<Test, TestVM>();
            CreateMap<TestVM, Test>();

            CreateMap<GivenAnswer, GivenAnswerVM>();
            CreateMap<GivenAnswerVM, GivenAnswer>();

        }
    }
}
