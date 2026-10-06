using Microsoft.AspNetCore.Mvc;
using firstASP.Data;
using firstASP.Models;

namespace firstASP.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomersController(ApplicationDbContext db) { _db = db; }

        // shows the list (with search)
        public IActionResult Index(string searchString)
        {
            var customers = _db.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.CustomerName.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["searchString"] = searchString;
            return View(customers.ToList());
        }

        // shows the empty add-form
        public IActionResult Create()
        {
            return View();
        }

        // saves a new customer
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT - show the edit form
        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);
        }

        // EDIT - save the changes
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - remove the customer
        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}