using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    public class OneProdView
    {
        public Product ToRender {get; set; }
        public List<Category> CatToAssign {get; set; }
        public Association Form {get; set; }
    }
}