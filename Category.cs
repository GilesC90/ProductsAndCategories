using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProductsAndCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name {get; set; }

        // Nav Property for the Many to Many Relationship between Products and Categories
        public List<Association> ProductsIn {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}