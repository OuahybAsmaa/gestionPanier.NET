using projetPanier.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projetPanier.Services
{
    public class PanierService
    {
        private const string COOKIE_NAME = "PanierProduits";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PanierService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Produit> ObtenirProduits()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null || !context.Request.Cookies.ContainsKey(COOKIE_NAME))
                return new List<Produit>();

            var json = context.Request.Cookies[COOKIE_NAME];
            return JsonSerializer.Deserialize<List<Produit>>(json!) ?? new List<Produit>();
        }

        public void AjouterProduit(Produit produit, int quantite)
        {
            var produits = ObtenirProduits();

            var existant = produits.FirstOrDefault(p => p.Id == produit.Id);
            if (existant != null)
            {
                existant.Quantite += quantite;
            }
            else
            {
                produit.Quantite = quantite;
                produits.Add(produit);
            }

            Sauvegarder(produits);
        }

        public void SupprimerProduit(int id, int quantiteASupprimer)
        {
            var produits = ObtenirProduits();
            var existant = produits.FirstOrDefault(p => p.Id == id);

            if (existant != null)
            {
                existant.Quantite -= quantiteASupprimer;
                if (existant.Quantite <= 0)
                    produits.Remove(existant);
            }

            Sauvegarder(produits);
        }

        public void ViderPanier()
        {
            var context = _httpContextAccessor.HttpContext;
            context?.Response.Cookies.Delete(COOKIE_NAME);
        }

        // Méthode pour sauvegarder dans le cookie
        public void Sauvegarder(List<Produit> produits)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            var json = JsonSerializer.Serialize(produits);

            context.Response.Cookies.Append(COOKIE_NAME, json, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                HttpOnly = false,
                Secure = false,
                IsEssential = true
            });
        }
    }
}