using Persistence.Entities;
using Persistence.Repository;
using System;
using System.Net;
using System.Web.Mvc;

namespace LegacyProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly AddressRepository _addressRepository;

        public CustomersController(
            CustomerRepository customerRepository,
            AddressRepository addressRepository
        )
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }


        // GET: Customers
        public ActionResult Index()
        {
            return View(_customerRepository.All());
        }

        // GET: Customers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerRepository.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (customer.CustomerFirstname == "")
            {
                ModelState.AddModelError("CustomerFirstname", "Bitte geben Sie einen Vornamen ein!");
            }
            if (customer.CustomerSurname == "")
            {
                ModelState.AddModelError("CustomerFirstname", "Bitte geben Sie einen Nachnamen ein!");
            }

            if (ModelState.IsValid)
            {
                _customerRepository.Add(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerRepository.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (customer.CustomerFirstname == "")
            {
                ModelState.AddModelError("CustomerFirstname", "Bitte geben Sie einen Vornamen ein!");
            }
            if (customer.CustomerSurname == "")
            {
                ModelState.AddModelError("CustomerFirstname", "Bitte geben Sie einen Nachnamen ein!");
            }

            if (ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerRepository.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _customerRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
