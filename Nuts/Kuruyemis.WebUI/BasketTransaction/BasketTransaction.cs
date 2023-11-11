
using Kuruyemis.WebUI.BasketTransaction;
using Kuruyemis.WebUI.BasketTransaction.BasketModels;
using Newtonsoft.Json;

namespace Kuruyemis.WebUI.BasketTransaction
{
    public class BasketTransaction : IBasketTransaction
    {
        const string basketName = "basket";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketTransaction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public BasketDto GetOrCreateBasket()
        {
           bool response = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
           return response ? 
                JsonConvert.DeserializeObject<BasketDto>(_httpContextAccessor.HttpContext.Request.Cookies[basketName]) 
                : new BasketDto() { BasketItems = new List<BasketItemDto>() };
        }
        public void SaveUpdateBasketItem(BasketItemDto basketItem)
        {
            BasketDto basketDto = GetOrCreateBasket();
            if (basketDto.BasketItems.Any(x => x.ProductId == basketItem.ProductId))
            {
                BasketItemDto basketItemDto = basketDto.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
                basketItemDto.Quantity += 1;
            }
            else basketDto.BasketItems.Add(basketItem);
            string basketSerialize = JsonConvert.SerializeObject(basketDto);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, basketSerialize);
        }

        public void RemoveOrDecrease(int id)
        {
            bool response = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName); 
            if (response)
            {
                BasketDto basketDto = GetOrCreateBasket();
                for (int i = 0; i < basketDto.BasketItems.Count; i++)
                {
                    if (basketDto.BasketItems[i].ProductId == id && basketDto.BasketItems[i].Quantity > 1)
                    {
                        basketDto.BasketItems[i].Quantity -= 1;
                    }
            else if (basketDto.BasketItems[i].ProductId == id && basketDto.BasketItems[i].Quantity == 1) basketDto.BasketItems.Remove(basketDto.BasketItems[i]);
                    
                }
                string basketSerialize = JsonConvert.SerializeObject(basketDto);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, basketSerialize);
            }
        }

        public void DeleteItem(int id)
        {
            bool response = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
            if (response)
            {
                BasketDto basketDto = GetOrCreateBasket();
                BasketItemDto basketItemDto = basketDto.BasketItems.FirstOrDefault(x=>x.ProductId == id);
                basketDto.BasketItems.Remove(basketItemDto);
                string basketSerialize = JsonConvert.SerializeObject(basketDto);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName,basketSerialize);
            }
        }

        public void DeleteBasket() => _httpContextAccessor.HttpContext.Response.Cookies.Delete(basketName);
       
    }
}
