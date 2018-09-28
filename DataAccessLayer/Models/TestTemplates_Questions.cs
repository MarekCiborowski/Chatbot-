using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("TestTemplates_Questions")]
    public class TestTemplates_Questions
    {
        [Key]
        public int testTemplates_QuestionsID { get; set; }

        [ForeignKey("testTemplate")]
        public int testTemplateID { get; set; }
        public TestTemplate testTemplate { get; set; }
        [ForeignKey("question")]
        public int questionID { get; set; }
        public Question question { get; set; }
    }
}
