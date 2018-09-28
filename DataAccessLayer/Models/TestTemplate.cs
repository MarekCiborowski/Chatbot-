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
    [Table("TestTemplate")]
    public class TestTemplate
    {
        [Key]
        public int testTemplateID { get; set; }
        public string templateName { get; set; }
        public string description { get; set; }

        public ICollection<TestTemplates_Questions> testTemplates_Questions { get; set; } = new Collection<TestTemplates_Questions>();
        public ICollection<Test> test { get; set; } = new Collection<Test>();
    }
}
