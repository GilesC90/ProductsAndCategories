using System.ComponentModel.DataAnnotations;
using System;

namespace ProductsAndCategories.Models
{
    public class Association
    {
        [Key]
        public int AssociationId {get; set; }

        //Foreign Key and Nav Property for Product
        [Required]
        public int ProductId {get; set; }
        public Product ProductInCategory {get; set; }

        //Foreign Key and Nav Property for Category
        [Required]
        public int CategoryId {get; set; }
        public Category CategoryWithProducts {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}