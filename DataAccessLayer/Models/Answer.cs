using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        
        public int answerID { get; set; }
        public string value { get; set; }
        public bool isCorrect { get; set; }

        [ForeignKey("question")]
        public int questionID { get; set; }
        public Question question  { get; set; }


    }
}
