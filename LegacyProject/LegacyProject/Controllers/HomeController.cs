using Persistence.Repository;
using System.Web.Mvc;
using LegacyProject.Models;

namespace LegacyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly AddressRepository _addressRepository;

        public HomeController(
            CustomerRepository customerRepository,
            AddressRepository addressRepository
        )
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public ActionResult Index()
        {
            return View(new HomeIndexViewModel());
        }
    }
}