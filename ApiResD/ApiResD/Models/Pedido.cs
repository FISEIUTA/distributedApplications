namespace ApiResD.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public required string Product { get; set; }
        public required int Amount { get; set; }
    }
}
