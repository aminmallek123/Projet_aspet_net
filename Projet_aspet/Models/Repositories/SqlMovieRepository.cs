using Microsoft.EntityFrameworkCore;
using Projet_aspet.data;

namespace Projet_aspet.Models.Repositories
{
    public class SqlMovieRepository : IRepository<Movie>
    {
        private readonly AppDbContext contexte;
        public SqlMovieRepository(AppDbContext context)
        {
            this.contexte = context;
        }
        public Movie Add(Movie t)
        {
            contexte.Movies.Add(t);
            contexte.SaveChanges();
            return t;
        }

        public Movie Delete(int Id)
        {
            Movie P = contexte.Movies.Find(Id);
            if (P != null)
            {
                contexte.Movies.Remove(P);
                contexte.SaveChanges();
            }
            return P;
        }

        public Movie Get(int Id)
        {
            return contexte.Movies.Find(Id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return contexte.Movies;
        }

        public Movie Update(Movie t)
        {
            var Product =contexte.Movies.Attach(t);
            Product.State = EntityState.Modified;
            contexte.SaveChanges();
            return t;
        }
        public IEnumerable<Movie> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
                return contexte.Movies.Where(a => a.Name.Contains(term));
            else
                return contexte.Movies;
        }
    }
}
