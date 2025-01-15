using BethanyPieShop.InventoryManagement.Domain.Contracts;
using BethanyPieShop.InventoryMnagement.Domain.General;
using BethanyPieShop.InventoryMnagement.Domain.ProductManagement;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement
{
    public class BulkProduct : Product, ISavable
    {
        public BulkProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : base(id, name, description, price, unitType, maxAmountInStock)
        {

        }

        public override object Clone()
        {
            return new BulkProduct(0, this.Name, this.Description, new Price() { ItemPrice = this.Price.ItemPrice, Currency = Price.Currency }, this.UnitType, this.maxItemsInStock);
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};3;";
        }

        public override void IncreaseStock()
        {
            AmountInStock++;
        }
    }
}
