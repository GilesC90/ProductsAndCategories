using System.Collections.Generic;

namespace ProductsAndCategories.Models
{
    public class OneCatView
    {
        public Category ToRender {get; set; }
        public List<Product> ProdToAssign {get; set; }
        public Association Form {get; set; }
    }
}