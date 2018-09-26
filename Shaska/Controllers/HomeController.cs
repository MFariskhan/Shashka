using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Shaska.Models;
using Shaska.ViewModel;
namespace Shaska.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController() {
            _context = new ApplicationDbContext();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("CanManageApplication"))
                return View("AdminPanel");

            if (User.IsInRole("Rider"))
                return RedirectToAction("OrdersForRider", "Orders");

            var memberId = User.Identity.GetUserId();
            var user = _context.Users.Where(c => c.Id == memberId).SingleOrDefault();

            var productCategories = _context.ProductCategories.ToList();
            ProductCategory dhamakaoffer = new ProductCategory()
            {
                Id = 4,
                CategoryName = "Dhamaka Offers",
                Priority = 4
            };
            productCategories.Add(dhamakaoffer);

            var recomendations = _context.Recommendations.SingleOrDefault(c => c.ApplicationUserId == memberId && c.Count == 1);
            if (recomendations != null)
            {
                var id = recomendations.ProductId;
                var recommendedId = _context.Products.Where(c => c.ProductId == id).Select(c => c.ProductCategoryId).SingleOrDefault();
                if (recommendedId != 0)
                {
                    ProductCategory recommended = new ProductCategory()
                    {
                        Id = recommendedId,
                        CategoryDescription = "Recommendations For You",
                        Priority = 5
                    };

                    productCategories.Add(recommended);
                }
            }
            //for search history
            recomendations = _context.Recommendations.SingleOrDefault(c => c.ApplicationUserId == memberId && c.Count == 0);
            if (recomendations != null)
            {
                var id = recomendations.ProductId;
                var recommendedId = _context.Products.Where(c => c.ProductId == id).Select(c => c.ProductCategoryId).SingleOrDefault();
                if (recommendedId != 0)
                {
                    ProductCategory recommended = new ProductCategory()
                    {
                        Id = recommendedId,
                        CategoryDescription = "We inspire from your search",
                        Priority = 8
                    };
                    productCategories.Add(recommended);
                }
            }

            productCategories = productCategories.OrderBy(x => x.Priority).ToList();

            CategoryProductsViewModel model = new CategoryProductsViewModel()
            {
                UserId = "",
                UserName = "Please Login",
            };

            if(user!=null){
                model.UserId = user.Id;
                model.UserName = user.FirstName;
            }
            List<Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel> cp = new List<CategoryProductsViewModel.CategoryProductViewModel>();

            for (int a = 0; a < productCategories.Count; a++)
            {
                int id = productCategories[a].Id;
                List<Product> listofproducts = _context.Products.Where(c => c.ProductCategoryId == id).ToList();
                for (int c1 = 0; c1 < listofproducts.Count; c1++)
                {
                    id = listofproducts[c1].ProductId;
                    listofproducts[c1].Image = _context.Images.Where(c => c.ProductId == id).ToList();
                }

                Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel categoryproduct = new Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel()
                {
                    ProductCategory = productCategories[a],
                    Products = listofproducts
                };
                cp.Add(categoryproduct);
            }

            model.CategoriesProductViewModel = cp;

            return View("Home",model);
        }
      [AllowAnonymous]
        public ActionResult Departments()
        {
            var productCategories = _context.ProductCategories.ToList();
            productCategories = productCategories.OrderBy(x => x.Priority).ToList();

            CategoryProductsViewModel model = new CategoryProductsViewModel();

            List<Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel> cp = new List<CategoryProductsViewModel.CategoryProductViewModel>();

            for (int a = 0; a < productCategories.Count; a++)
            {
                int id = productCategories[a].Id;
                List<Product> listofproducts = _context.Products.Where(c => c.ProductCategoryId == id).ToList();
                for (int c1 = 0; c1 < listofproducts.Count; c1++)
                {
                    id = listofproducts[c1].ProductId;
                    listofproducts[c1].Image = _context.Images.Where(c => c.ProductId == id).ToList();
                }

                Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel categoryproduct = new Shaska.ViewModel.CategoryProductsViewModel.CategoryProductViewModel()
                {
                    ProductCategory = productCategories[a],
                    Products = listofproducts
                };
                cp.Add(categoryproduct);
            }

            model.CategoriesProductViewModel = cp;
            
            return View(model);
        }
        [Authorize(Roles = RoleName.CanManageApplication)]
        public ActionResult AdminPanel()
        {
            
            return View();
        }
        [Authorize]
        public ActionResult SearchHistory(string Id)
        {
            var ProductIds = _context.Recommendations.Where(c => c.ApplicationUserId == Id).Select(c => c.ProductId).ToList();
            List<Product> Products = new List<Product>();
            for (int a = 0; a < ProductIds.Count();a++)
            {
                int id = ProductIds[a];
                var product = _context.Products.SingleOrDefault(c => c.ProductId ==id);
                if (product != null)
                {
                    product.Image = _context.Images.Where(c => c.ProductId == id).ToList();
                    Products.Add(product);
                }
            }
            return View(Products);
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}