namespace AgriEnergyConnect.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }

        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }
    }
}
