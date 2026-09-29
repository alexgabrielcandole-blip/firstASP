using Microsoft.AspNetCore.Mvc;

using firstASP.Data;

using firstASP.Models;

 

namespace firstASP.Controllers

{

    public class ProductsController : Controller

    {

        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db) { _db = db; }

 

        // shows the list

        public IActionResult Index()

        {

            var products = _db.Products.ToList();

            return View(products);

        }

 

        // shows the empty add-form

        public IActionResult Create()

        {

            return View();

        }

 

        // saves a new product

        [HttpPost]

        public IActionResult Create(Product product)

        {

            _db.Products.Add(product);

            _db.SaveChanges();

            return RedirectToAction("Index");

        }

         // EDIT - show the edit form
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        // EDIT - save the changes
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - remove the product
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }

}