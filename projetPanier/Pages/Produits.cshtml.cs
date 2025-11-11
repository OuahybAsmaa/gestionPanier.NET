using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projetPanier.Data;
using projetPanier.Models;
using projetPanier.Services;

namespace projetPanier.Pages
{
    public class ProduitsModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly PanierService _panierService;

        public List<Produit> Produits { get; set; } = new();

        public ProduitsModel(AppDbContext context, PanierService panierService)
        {
            _context = context;
            _panierService = panierService;
        }

        private static List<Produit> produitsTest = new List<Produit>
        {
           new Produit { Id = 1, Nom = "Stylo", Prix = 5.50m,Image = "/images/1.jpg" },
           new Produit { Id = 2, Nom = "Cahier", Prix = 12.00m,Image = "/images/3.jpg"  },
           new Produit { Id = 3, Nom = "Sac à dos", Prix = 150.00m,Image = "/images/2.webp"  }
        };

        public void OnGet()
        {
            Produits = produitsTest;
        }

        public void OnPostAjouter(int id, int quantite)
        {
            var produit = produitsTest.FirstOrDefault(p => p.Id == id);
            if (produit != null)
                _panierService.AjouterProduit(produit, quantite);

            Produits = produitsTest;
        }


    }
}