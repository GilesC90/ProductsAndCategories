using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description {get; set; }
        [Required(ErrorMessage = "Please enter a price")]
        public decimal Price {get; set; }

        // Nav Property for the Many to Many Relationship between Products and Categories
        public List<Association> CategoryHome {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}