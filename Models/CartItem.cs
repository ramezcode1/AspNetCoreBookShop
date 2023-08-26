namespace BookStore.Models
{
    public class CartItem
    {
        public Book Book { get; set; } = new Book();
        public int Quantity { get; set; }
        
        private decimal _SubTotal;
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = Convert.ToDecimal(Book.Price * Quantity); }
        }
    }
}
