using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shaska.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.IO;


namespace Shaska.Controllers
{
   
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        public ProductsController() {
            _context = new ApplicationDbContext();
        
        }
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = RoleName.CanManageApplication)]
        public ActionResult Edit(int id)
        {
            var Product = _context.Products.SingleOrDefault(c => c.ProductId == id);

            if (Product == null)
                return HttpNotFound();
            var viewmodel = new ProductSizeCategoryViewModel()
            {
                product =Product,
                productCategory = _context.ProductCategories.ToList()
            };
            
            return View("CategoryForm", viewmodel);
        }

        [Authorize(Roles = RoleName.CanManageApplication)]
        public ActionResult CategoryForm()
        {
            var s = _context.ProductCategories.ToList();
            var viewmodel = new ProductSizeCategoryViewModel()
            {
                 product=new Product(),
                productCategory = s
            };
            
            viewmodel.product.Size = new List<Size>();
            
           return View(viewmodel);
        }

        [Authorize(Roles = RoleName.CanManageApplication)]
        public ActionResult ProductForm(ProductSizeCategoryViewModel viewmodel)
        { 
            if (viewmodel.product.ProductCategoryId==0)
            {
            var s = _context.ProductCategories.ToList();
            var Tviewmodel = new ProductSizeCategoryViewModel()
            {
                product =new Product(),
                productCategory = s
            };
              return View("CategoryForm", Tviewmodel);
            }
            return View(viewmodel.product);
        }


        [Authorize(Roles = RoleName.CanManageApplication)]
        [HttpPost]
        public ActionResult SizeAndImages(Product product)
        {
            
            if (!ModelState.IsValid)
            {
                return View("ProductForm", product);
            }
            if (product.ProductId != 0)
            {
                product.Size = _context.Sizes.Where(c => c.ProductId == product.ProductId).ToList();
            }
            else
            {
                product.Size = new List<Size>();
            }
            if (product.NoOfSizes != product.Size.Count )
            {
                if(product.NoOfSizes==0)
                    product.Size.Clear();

                product.Size = new List<Size>();
               
                for (int a = 0; a < product.NoOfSizes;a++ )
                {
                    product.Size.Add(new Shaska.Models.Size());
                }
            }
            return View(product);
        }
        [Authorize(Roles = RoleName.CanManageApplication)]
        [HttpPost]
        public ActionResult UploadImages(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("SizeAndImages", product);
            }
            //delete all existing images
            System.IO.DirectoryInfo di = new DirectoryInfo("C:/Projects/Shaska/Shaska/TempUploads");
            foreach (FileInfo file in di.GetFiles())
            {

                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {

                dir.Delete(true);
            }
            if (product.ProductId != 0)
            {
                product.Image = _context.Images.Where(c => c.ProductId == product.ProductId).ToList();
            }
            return View(product);
        }

        [Authorize(Roles = RoleName.CanManageApplication)]
        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Finish(Product product)
        {
      
            product.ProductDate = DateTime.Now;
            if (product.ProductId == 0)
            {
                
                
                var sizeId = _context.GetID.Single(c => c.Id == 1);
                
                product.Image=CopyFromTempToPerm(sizeId.SavedSizeAndImageId);

                _context.Products.Add(product);

                _context.SaveChanges();
            
                sizeId.SavedSizeAndImageId = sizeId.SavedSizeAndImageId + 1;
                _context.SaveChanges();
            }
            else
            {
                //update size database
                var listofdeletesizes = _context.Sizes.Where(c => c.ProductId == product.ProductId).ToList();
                for (int a = 0; a < listofdeletesizes.Count; a++)
                {
                    _context.Sizes.Remove(listofdeletesizes[a]);
                    _context.SaveChanges();
                }
                //update image database
                var listofdeleteImages = _context.Images.Where(c => c.ProductId == product.ProductId).ToList();
                    for (int a = 0; a < listofdeleteImages.Count; a++)
                    {
                        if (System.IO.File.Exists(@"C:/Projects/Shaska/Shaska/Peruploads/" + listofdeleteImages[a].ImagePath))
                        {
                            System.IO.File.Delete(@"C:/Projects/Shaska/Shaska/Peruploads/" + listofdeleteImages[a].ImagePath);
                        }
                        _context.Images.Remove(listofdeleteImages[a]);
                        _context.SaveChanges();
                    }
                    product.Image=CopyFromTempToPerm(product.ProductId);
                

                var ProductInDb = _context.Products.Single(m => m.ProductId == product.ProductId);
                ProductInDb.Manufacturer = product.Manufacturer;
                ProductInDb.ProductCategoryId = product.ProductCategoryId;
                ProductInDb.ProductDate = product.ProductDate;
                ProductInDb.ProductName = product.ProductName;
                ProductInDb.ProductPrice = product.ProductPrice;
                ProductInDb.NoOfSizes = product.NoOfSizes;
                ProductInDb.Size = product.Size;
                ProductInDb.Image = product.Image;
                _context.SaveChanges();
          
            }
            return RedirectToAction("Products/CategoryForm");
        }


        [Authorize(Roles = RoleName.CanManageApplication)]
              public ActionResult Add()
              {
                  for (int i = 0;
                           i < Convert.ToInt32(Request.Form["PackageFileCount"]);
                           i++)
                  {
                      if (Request.Form["File0Mode_" + i] != "sourceFile")
                          throw
                            new Exception("Uploader expects to send original files.");
                     //store new uploaded images
                      Storage _storage = new Storage();
                      _storage.SaveUploadedFile(Server.MapPath("/Tempuploads"),
                                                 Request.Files["File0_" + i]);

                  }
                  Response.Write("Upload Complete");
                  return null;
              }

        //functionforcopyimagestemptopermanantfolderandsaveintodatabase
              public List<Image> CopyFromTempToPerm(int ImageId)
              {
                  List<Image> list = new List<Image>();
                  String TImgId= "";
                  TImgId = "" + ImageId;
                  string sourcePath = @"C:/Projects/Shaska/Shaska/Tempuploads";
                  string targetPath = @"C:/Projects/Shaska/Shaska/Peruploads";

                  if (System.IO.Directory.Exists(sourcePath))
                  {
                      string[] files = System.IO.Directory.GetFiles(sourcePath);
                      int c = 0;
                      // Copy the files and overwrite destination files if they already exist. 
                      foreach (string s in files)
                      {
                          c++;
                          var fileExt = System.IO.Path.GetExtension(s);
                          // Use static Path methods to extract only the file name from the path.
                          var destFile = System.IO.Path.Combine(targetPath, TImgId + c +fileExt);
                          System.IO.File.Copy(s, destFile, true);

                          list.Add(new Shaska.Models.Image());
                          list[c-1].ImagePath=TImgId +""+ c+fileExt ;
                         
                      }
                  }
                  return list;
              }
    //view of products
        [AllowAnonymous]
              public ActionResult ShowProduct(int id)
              {
                  var product = _context.Products.SingleOrDefault(c => c.ProductId == id);
                  if (product == null)
                      return HttpNotFound();
                   
                  var memberId = User.Identity.GetUserId();

                  var OldSearchHistory = _context.Recommendations.SingleOrDefault(c=> c.ApplicationUserId == memberId &&  c.Count==0);
                  if (OldSearchHistory != null)
                  {
                      OldSearchHistory.Count = -1;
                      _context.SaveChanges();
                  }

                  var recommendation = _context.Recommendations.SingleOrDefault(c=> c.ProductId==id && c.ApplicationUserId==memberId);
                  if (product != null && recommendation == null)
                  {
                      recommendation = new Recommendation()
                      {
                          ApplicationUserId = memberId,
                          ProductId = id,
                          Count = 0
                      };
                      _context.Recommendations.Add(recommendation);
                      _context.SaveChanges();
                  }
                  else
                  {
                      recommendation.Count = 0;
                      _context.SaveChanges();
                  }

                  
                 
                  product.Size = _context.Sizes.Where(c=> c.ProductId==product.ProductId).ToList();
                  product.Image = _context.Images.Where(c => c.ProductId == product.ProductId).ToList();

                  return View(product);
              }
        //view of categories
         [AllowAnonymous]
              public ActionResult ShowProductsByCategory(int id)
              {
                
                  var products = _context.Products.Where(c => c.ProductCategoryId == id).ToList();
                  if (products == null)
                      return HttpNotFound();
                  var CategoryName = _context.ProductCategories.SingleOrDefault(c => c.Id == id);
                  ViewBag.Name = CategoryName.CategoryName;
                  foreach(var pro in products){
                      pro.Image = _context.Images.Where(c=> c.ProductId==pro.ProductId).ToList();
                  }

                  return View(products);
              }
        //Cart
        [Authorize]
        [HttpPost]
        public ActionResult AddProductToCart(Product product)
              {
                  var memberId = User.Identity.GetUserId();
                  var cartInDb = _context.Carts.SingleOrDefault(m=> m.UserId==memberId && m.ProductId==product.ProductId);
                  if (cartInDb == null)
                  {
                      Cart cart = new Cart()
                      {
                          ProductId = product.ProductId,
                          UserId = memberId.ToString(),
                          Quantity = 1
                      };

                      _context.Carts.Add(cart);
                      
                  }
                  else {
                      cartInDb.UserId = memberId;
                      cartInDb.ProductId = product.ProductId;
                      cartInDb.Quantity = 2;
                  }
                  _context.SaveChanges();
                  return RedirectToAction("ShowProductsByCategory/"+product.ProductCategoryId,"Products");
              }
        [Authorize]
        public ActionResult Cart()
        {
            return View();
        }

    }
}