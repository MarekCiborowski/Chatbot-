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
    
    
    [Table("Category")]
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public string categoryName { get; set; }

        public ICollection<Question> questions { get; set; } = new Collection<Question>();
    }
}
