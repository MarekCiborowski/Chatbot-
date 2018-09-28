using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
   
    [Table("QuestionType")]
    public class QuestionType
    {
        [Key]
        public int questionTypeID { get; set; }
        public string typeName { get; set; }
        public ICollection<Question> question { get; set; } = new Collection<Question>();
        
    }
}
