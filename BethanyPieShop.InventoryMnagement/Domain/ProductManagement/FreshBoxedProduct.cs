using BethanyPieShop.InventoryMnagement.Domain.General;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement
{
    public class FreshBoxedProduct : BoxedProduct
    {

        #region Constructors
        public FreshBoxedProduct(int id, 
                                string name, 
                                string? description, 
                                Price price, int maxAmountInStock, int amountPerBox) 
                                : base(id, name, description, price, maxAmountInStock, amountPerBox)
        {
        }
        #endregion
        #region Methods
        public void UseFreshBoxedProduct(int items)
        {
            //UseBoxedProduct(items);
        }
        #endregion

    }
}
