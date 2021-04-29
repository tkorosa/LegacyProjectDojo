using Persistence.Entities;
using Persistence.Repository;
using System;
using System.Net;
using System.Web.Mvc;
using Persistence.Validation;
using Service;
using Service.Validation;

namespace LegacyProject.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressRepository _addressRepository;
        private readonly AddressService _addressService;

        public AddressController(AddressRepository addressRepository, AddressService addressService)
        {
            _addressRepository = addressRepository;
            _addressService = addressService;
        }
        public ActionResult Index()
        {
            var addresses = _addressRepository.All();
            _addressService.SetDeletable(addresses);
            return View(addresses);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addressRepository.Find(id.Value);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address address)
        {
            var validation = new AddressValidation(address);
            validation.Validate();

            if (validation.HasError)
            {
                foreach (var errorDescription in validation.Errors)
                {
                    ModelState.AddModelError(errorDescription.Field, errorDescription.Error);
                }
            }
            else
            {
                try
                {
                    _addressService.Create(address);
                }
                catch (FieldValidationException error)
                {
                    ModelState.AddModelError(error.Field, error.Error);
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(address);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addressRepository.Find(id.Value);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Address address)
        {
            var error = EditAddressValidator.NotHasZeroErrors(address, out var errField);
            if (!error && !error && ModelState.IsValid)
            {
                _addressRepository.Update(address);
                return RedirectToAction("Index");
            }
            if (errField != null)
            {
                ModelState.AddModelError(errField, "Fehler bei Eingabe des Feldes!");
            }
            return View(address);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = _addressRepository.Find(id.Value);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _addressRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
