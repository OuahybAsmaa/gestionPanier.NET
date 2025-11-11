using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projetPanier.Services;
using projetPanier.Models;

namespace projetPanier.Pages
{
    public class PanierModel : PageModel
    {
        private readonly PanierService _panierService;

        // Liste des produits de test
        private static List<Produit> produitsTest = new List<Produit>
        {
            new Produit { Id = 1, Nom = "Stylo", Prix = 5.50m, Image = "/images/1.jpg" },
            new Produit { Id = 2, Nom = "Cahier", Prix = 12.00m, Image = "/images/3.jpg" },
            new Produit { Id = 3, Nom = "Sac à dos", Prix = 150.00m, Image = "/images/2.webp" }
        };

        public PanierModel(PanierService panierService)
        {
            _panierService = panierService;
        }

        public List<Produit> Produits { get; set; } = new();

        public void OnGet()
        {
            Produits = _panierService.ObtenirProduits();
        }

        // Modifier quantité
        public IActionResult OnPostModifierQuantite(int id, string action)
        {
            var produits = _panierService.ObtenirProduits();
            var produit = produits.FirstOrDefault(p => p.Id == id);

            if (produit != null)
            {
                if (action == "augmenter")
                {
                    produit.Quantite++;
                }
                else if (action == "diminuer")
                {
                    if (produit.Quantite > 1)
                    {
                        produit.Quantite--;
                    }
                    else
                    {
                        produits.Remove(produit); // Supprime si quantité = 1
                    }
                }

                _panierService.Sauvegarder(produits);
            }

            return RedirectToPage();
        }

        // Supprimer TOUT le produit
        public IActionResult OnPostSupprimerTout(int id)
        {
            _panierService.SupprimerProduit(id, 999); 
            return RedirectToPage();
        }

        // Vider le panier
        public IActionResult OnPostVider()
        {
            _panierService.ViderPanier();
            return RedirectToPage();
        }
    }
}