

using Kuruyemis.WebUI.BasketTransaction.BasketModels;

namespace Kuruyemis.WebUI.BasketTransaction
{
    public interface IBasketTransaction
    {
        BasketDto GetOrCreateBasket();
        void SaveUpdateBasketItem(BasketItemDto basketItem);
        void RemoveOrDecrease(int notebookId);
        void DeleteItem(int notebookId);
        void DeleteBasket();
    }
}
