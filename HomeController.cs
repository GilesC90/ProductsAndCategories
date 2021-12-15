using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;
using System.Linq;
using System;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        public Product ProductInDB()
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == HttpContext.Session.GetInt32("ProductId"));
        }
        public Category CatInDB()
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == HttpContext.Session.GetInt32("CategoryId"));
        }
        [HttpGet("categories")]
        public ViewResult Cats()
        {
            CatView ViewModel = new CatView()
            {
                AllCategories = _context.Categories
                .Include(cat => cat.ProductsIn)
                .ToList()
            };
            return View("CategoryDash", ViewModel);
        }
        [HttpPost("category/new")]
        public IActionResult NewCat(CatView NotFromForm)
        {
            if(ModelState.IsValid)
            {
                Category fromForm = NotFromForm.Form;
                _context.Add(fromForm);
                _context.SaveChanges();
                return RedirectToAction("Cats");
            }
            else 
            {
                return Cats();
            }
        }
        [HttpGet("products")]
        public ViewResult Prods()
        {
            ProdView ViewModel = new ProdView()
            {
                AllProducts = _context.Products
                .Include(prod => prod.CategoryHome)
                .ToList()
            };
            return View("ProductDash", ViewModel);
        }
        [HttpPost("product/new")]
        public IActionResult NewProd(ProdView NotFromForm)
        {
            Product fromForm = NotFromForm.Form;
            if(ModelState.IsValid)
            {
                _context.Add(fromForm);
                _context.SaveChanges();
                return RedirectToAction("Prods");
            }
            else 
            {
                return Prods();
            }
        }
        [HttpGet("product/{productId}")]
        public IActionResult ProductInfo(int productId)
        {
            OneProdView ViewModel = new OneProdView()
            {
                ToRender = _context.Products
                    .Include(c => c.CategoryHome)
                        .ThenInclude(p => p.CategoryWithProducts)
                    .FirstOrDefault(pi=> pi.ProductId == productId),
                CatToAssign = _context.Categories
                    .Include(p => p.ProductsIn)
                    .Where(p => !p.ProductsIn.Any(ch => ch.ProductId == productId))
                    .ToList()
            };
            
            
            if(_context.Products.Include(p => p.CategoryHome).ThenInclude(c => c.CategoryWithProducts).FirstOrDefault(ci=> ci.ProductId == productId) == null)
            {
                return RedirectToAction("Prods");
            }
            return View("ProductInformation", ViewModel);
        }
        [HttpPost("product/{productId}/assign")]
        public IActionResult AssignCategory(int productId, OneProdView viewModel)
        {
            if(ModelState.IsValid)
            {
                Association fromForm = viewModel.Form;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("ProductInfo", new { productId = productId });
            }
            else
            {
                return ProductInfo(productId);
            }
        }
        [HttpGet("category/{categoryId}")]
        public IActionResult CategoryInfo(int categoryId)
        {
            OneCatView ViewModel = new OneCatView()
            {
                ToRender = _context.Categories
                    .Include(p => p.ProductsIn)
                        .ThenInclude(c => c.ProductInCategory)
                    .FirstOrDefault(ci=> ci.CategoryId == categoryId),
                ProdToAssign = _context.Products
                    .Include(p => p.CategoryHome)
                    .Where(p => !p.CategoryHome.Any(ch => ch.CategoryId == categoryId))
                    .ToList()
            };
            
            
            if(_context.Categories.Include(p => p.ProductsIn).ThenInclude(c => c.ProductInCategory).FirstOrDefault(ci=> ci.CategoryId == categoryId) == null)
            {
                return RedirectToAction("Cats");
            }
            return View("CategoryInformation", ViewModel);
        }
        [HttpPost("category/{categoryId}/assign")]
        public IActionResult AssignProduct(int categoryId, OneCatView viewModel)
        {
            if(ModelState.IsValid)
            {
                Association fromForm = viewModel.Form;
                _context.Add(fromForm);
                _context.SaveChanges();

                return RedirectToAction("CategoryInfo", new { categoryId = categoryId });
            }
            else
            {
                return CategoryInfo(categoryId);
            }
        }
    }
}