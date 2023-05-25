namespace Projet_aspet.Models.Help
{
    public class Item
    {
        public int quantite { get; set; }
        private int _ProduitId;
        public Movie _produit = null;
        public Movie Prod
        {
            get { return _produit; }
            set { _produit = value; }
        }
        public string Description
        {
            get { return _produit.Name; }
        }
        public double UnitPrice
        {
            get { return _produit.Price; }
        }
        public double TotalPrice
        {
            get { return _produit.Price * quantite; }
        }
        public Item(Movie p)
        {
            this.Prod = p;
        }
        public bool Equals(Item item)
        {
            return item.Prod.Id == this.Prod.Id;
        }

    }
}
