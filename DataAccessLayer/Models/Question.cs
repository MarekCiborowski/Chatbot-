using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int questionID { get; set; }
        public string value { get; set; }
        public int points { get; set; }
        [ForeignKey("category")]
        public int categoryID { get; set; }
        public Category category { get; set; }
        [ForeignKey("questionType")]
        public int questionTypeID { get; set; }
        public QuestionType questionType { get; set; }
        public ICollection<TestTemplates_Questions> testTemplates_Questions { get; set; } = new Collection<TestTemplates_Questions>();
        public ICollection<Answer> answer { get; set; } = new Collection<Answer>();
        public ICollection<GivenAnswer> givenAnswer { get; set; } = new Collection<GivenAnswer>();
    }
}
