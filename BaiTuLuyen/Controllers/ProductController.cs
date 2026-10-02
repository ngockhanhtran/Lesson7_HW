using BaiTuLuyen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaiTuLuyen.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" },
            new Category { Id = 3, Name = "Linh kiện & Phụ kiện" }
        };
        private static List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop Gaming ASUS ROG",
                Image = "sample-laptop.jpg",
                Price = 25000000,
                SalePrice = 20000000,
                CategoryId = 2,
                Description = "Laptop chơi game cấu hình cao với màn hình 144Hz mượt mà."
            }
        };

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        private void LoadCategoriesViewBag(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(_categories, "Id", "Name", selectedId);
        }

        // INDEX
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CategoryList = _categories;
            return View(_products);
        }

        // DETAILS
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryName = _categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
            return View(product);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategoriesViewBag();
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }

                    product.Image = uniqueFileName;
                }

                product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
                _products.Add(product);

                return RedirectToAction(nameof(Index));
            }

            LoadCategoriesViewBag(product.CategoryId);
            return View(product);
        }

        // EDIT - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            LoadCategoriesViewBag(product.CategoryId);
            return View(product);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return NotFound();

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }

                    existingProduct.Image = uniqueFileName;
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.SalePrice = product.SalePrice;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Description = product.Description;

                return RedirectToAction(nameof(Index));
            }

            LoadCategoriesViewBag(product.CategoryId);
            return View(product);
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
