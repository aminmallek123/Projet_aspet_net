using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_aspet.data.Enums;
using Projet_aspet.Models;
using Projet_aspet.Models.Repositories;
using Projet_aspet.ViewModels;

namespace Projet_aspet.Controllers
{
    public class MovieController : Controller
    {
        readonly IRepository<Movie> MovieRepository;
        public MovieController(IRepository<Movie> movieRepository)
        {
            MovieRepository = movieRepository;
        }

        // GET: MovieController
        [Authorize]
        public ActionResult Index()
        {
            var movie = MovieRepository.GetAll();
            return View(movie);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            Movie movie = MovieRepository.Get(id);
            return View(movie);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Movie newMovie = new Movie
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImageURL = model.ImageURL,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    MovieCategory = model.MovieCategory,
                    cinemaName = model.cinemaName,
                    ProducedName = model.ProducedName
                };
                MovieRepository.Add(newMovie);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = MovieRepository.Get(id);
            EditViewModel newMovie = new EditViewModel
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                cinemaName = movie.cinemaName,
                ProducedName = movie.ProducedName
            };

            return View(newMovie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Movie m = MovieRepository.Get(model.Id);
                m.Name=model.Name;
                m.Price=model.Price;
                m.Description=model.Description;
                m.ImageURL=model.ImageURL;
                m.StartDate=model.StartDate;
                m.EndDate=model.EndDate;
                m.MovieCategory=model.MovieCategory;
                m.cinemaName=model.cinemaName;
                m.ProducedName = model.ProducedName;
                Movie Mov = MovieRepository.Update(m);
                if (Mov != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();
            }
            return View(model);
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
             MovieRepository.Delete(id);
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Home()
        {
            var movie = MovieRepository.GetAll();
            return View(movie);
        }
        public ActionResult Search(string term)
        {
            var result = MovieRepository.Search(term);
            return View("Home", result);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = MovieRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allMovies.Where(n => n.Name.ToUpper().Contains(searchString.ToUpper())).ToList();

                return View("Home", filteredResultNew);
            }

            return View("Home", allMovies);
        }
        public async Task<IActionResult> Categ(string id)
        {
            var allMovies = MovieRepository.GetAll();
            if (!string.IsNullOrEmpty(id))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allMovies.Where(n => n.Description.ToUpper().Contains(id.ToUpper())).ToList();

                return View("Home", filteredResultNew);
            }

            return View("Home", allMovies);
        }
    }
}
