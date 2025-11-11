namespace projetPanier.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Quantite { get; set; } = 1;
        public string Image { get; set; } = string.Empty;
    }
}
