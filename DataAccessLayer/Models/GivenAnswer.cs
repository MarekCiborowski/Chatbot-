using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("GivenAnswer")]
    public class GivenAnswer
    {
        [Key]
        public int givenAnswerID { get; set; }
        public string answer { get; set; } // udzielona odpowiedź
        public bool isCorrect { get; set; }
        public int points { get; set; }

        [ForeignKey("test")]
        public int testID { get; set; }
        public Test test { get; set; }
        [ForeignKey("question")]
        public int questionID { get; set; }
        public Question question { get; set; }


    }
}
