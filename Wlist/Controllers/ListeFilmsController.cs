using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wlist.Data;
using Wlist.Models;

namespace Wlist.Controllers
{
    public class ListeFilmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilisateur> _gestionaire;

        public ListeFilmsController(ApplicationDbContext context, UserManager<Utilisateur> gestionaire)
        {
            _context = context;
            _gestionaire = gestionaire;
        }

        private Task<Utilisateur> GetCurrentUserAsync() =>
            _gestionaire.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<string> RecupererIdUtilisateurCourant()
        {
            Utilisateur utilisateur = await GetCurrentUserAsync();
            return utilisateur?.Id;
        }

        public async Task<IActionResult> Index()
        {
            var id = await RecupererIdUtilisateurCourant();
            var filmsUtilisateur = _context.FilmsUtilisteur.Where(x => x.IdUtilisateur ==id);
            var modele = filmsUtilisateur.Select(x => new ModeleVueFilm
            {
                IdFilm = x.IdFilm,
                Titre = x.Film.Titre,
                Annee = x.Film.Annee,
                Vu = x.Vu,
                PresentDansListe = true,
                Note = x.Note
            }).ToList();
            return View(modele);
        }
    }
}
