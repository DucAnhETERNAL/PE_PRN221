using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        [Required(ErrorMessage = "Book name is required")]
        public string BookName { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryID { get; set; }
        public Categories Category { get; set; }
        public ICollection<Ships > Ships { get; set; }
    }
}
