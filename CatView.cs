using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    public class CatView
    {
        public List<Category> AllCategories {get; set; }
        public Category Form {get; set; }
    }
}