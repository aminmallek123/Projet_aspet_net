using Microsoft.AspNetCore.Mvc;
using Projet_aspet.Models.Repositories;
using Projet_aspet.Models;
using Projet_aspet.Models.Help;
using Microsoft.AspNetCore.Authorization;

namespace Projet_aspet.Controllers
{
    public class PanierController : Controller
    {
        readonly IRepository<Movie> MovieRepository;
        public PanierController(IRepository<Movie> produitRepository)
        {
            this.MovieRepository = produitRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
        [Authorize]
        public ActionResult AjouterProduit(int id)
        {
            Movie pp = MovieRepository.Get(id);
            ListeCart.Instance.AddItem(pp);
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
        [HttpPost]
        public ActionResult PlusProduit(int Id)
        {
            Movie pp = MovieRepository.Get(Id);
            ListeCart.Instance.AddItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.Id == pp.Id)
                    trouve = a;
            }
            var results = new
            {
                ct = 1,
                Total = ListeCart.Instance.GetSubTotal(),
                Quatite = trouve.quantite,
                TotalRow = trouve.TotalPrice
            };
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult MoinsProduit(int id)
        {
            Movie pp = MovieRepository.Get(id);
            ListeCart.Instance.SetLessOneItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.Id == pp.Id)
                    trouve = a;
            }
            if (trouve != null)
            {
                var results = new
                {
                    Total = ListeCart.Instance.GetSubTotal(),
                    Quatite = trouve.quantite,
                    TotalRow = trouve.TotalPrice,
                    ct = 1
                };
                return Json(results);
            }
            else
            {
                var results = new
                {
                    ct = 0
                };
                return Json(results);
            }
            return null;
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection collection)
        {
            ListeCart.Instance.Items.Clear();
            ViewBag.Message = "Commande effectuée zvec succès";
            return View();
        }
        [HttpPost]
        public ActionResult SupprimerProduit(int id)
        {
            Movie pp = MovieRepository.Get(id);
            ListeCart.Instance.RemoveItem(pp);
            var results = new
            {
                Total = ListeCart.Instance.GetSubTotal(),
            };
            return RedirectToAction("Index", "Home");
        }
    }
}
