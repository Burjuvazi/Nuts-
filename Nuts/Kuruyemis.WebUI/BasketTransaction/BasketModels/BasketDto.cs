namespace Kuruyemis.WebUI.BasketTransaction.BasketModels
{
    public class BasketDto
    {
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);

    }
}
