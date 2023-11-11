namespace Kuruyemis.WebUI.BasketTransaction.BasketModels
{
    public class BasketItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId  { get; set; }
        public string? UserName { get; set; }
    }
}
