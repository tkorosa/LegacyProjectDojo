using Persistence.Entities;
using Persistence.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Persistence.Validation;
using Service;
using Service.Validation;

namespace LegacyProject.Controllers
{
    // Copied from AddressController... Mea Culpa
    // Problem: DB war so doof vorgeben mit den FavouriteShopBla Feldern im Customer :(
    // TODO: PBB fragen, wie man das besser machen kann, wenn er vom Urlaub zurückkommt
    public class FavouriteShopController : Controller
    {
        private readonly CustomerRepository _addressRepository;

        public FavouriteShopController(CustomerRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public ActionResult Index()
        {
            return View(
                _addressRepository.All().Where(c => !string.IsNullOrEmpty(c.FavouriteShopStreetText))
            );
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer address = _addressRepository.Find(id.Value);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }
        
        public ActionResult Edit(Guid? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer address)
        {
            var error = 
                EditAddressValidator.NotHasZeroErrors(
                    new Address
                    {
                        City = address.FavouriteShopCity,
                        Country = address.FavouriteShopCountry,
                        StreetText = address.FavouriteShopStreetText,
                        Zip = address.FavouriteShopZip
                    }, 
                    out var errField
                );
            if (!error && !error && ModelState.IsValid)
            {
                _addressRepository.UpdateFavouriteShop(address);
                return RedirectToAction("Index");
            }
            if (errField != null)
            {
                ModelState.AddModelError("FavouriteShop"+errField, "Fehler bei Eingabe des Feldes!");
            }
            return View(address);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer address = _addressRepository.Find(id.Value);
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
            _addressRepository.RemoveFavouriteShop(id);
            return RedirectToAction("Index");
        }
    }
}
