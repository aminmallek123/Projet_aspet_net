namespace Projet_aspet.Models.Help
{
    public class ListeCart
    {
        public List<Item> Items { get; private set; }
        public static readonly ListeCart Instance;
        static ListeCart()
        {
            Instance = new ListeCart();
            Instance.Items = new List<Item>();
        }
        protected ListeCart() { }
        public void AddItem(Movie prod)
        {
            Boolean iswhat = false;
            // Create a new item to add to the cart
            foreach (Item a in Items)
            {
                if (a.Prod.Id == prod.Id)
                {
                    a.quantite++;
                    iswhat = true;
                    return;
                }
            }
            if (iswhat == false)
            {
                Item newItem = new Item(prod);
                newItem.quantite = 1;
                Items.Add(newItem);
            }
        }
        public void setToNUll()
        { }
        public void SetLessOneItem(Movie produit)
        {
            foreach (Item a in Items)
            {
                if (a.Prod.Id == produit.Id)
                {
                    if (a.quantite <= 0)
                    {
                        RemoveItem(a.Prod);
                        return;
                    }
                    else
                    {
                        a.quantite--;
                        return;
                    }
                }
            }
        }
        public void SetItemQuantity(Movie produit, int quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(produit);
                return;
            }
            foreach (Item a in Items)
            {
                if (a.Prod.Id == produit.Id)
                {
                    a.quantite = quantity;
                    return;
                }
            }
        }
        public void RemoveItem(Movie produit)
        {
            Item t = null;
            foreach (Item a in Items)
            {
                if (a.Prod.Id == produit.Id)
                {
                    t = a;
                }
            }
            if (t != null)
            {
                Items.Remove(t);
            }
        }
        public double GetSubTotal()
        {
            double subTotal = 0;
            foreach (Item i in Items)
                subTotal += i.TotalPrice;
            return (double)subTotal;
        }

    }
}
