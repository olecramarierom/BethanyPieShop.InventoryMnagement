﻿using BethanyPieShop.InventoryManagement.Domain.Contracts;
using BethanyPieShop.InventoryMnagement.Domain.General;
using BethanyPieShop.InventoryMnagement.Domain.ProductManagement;

namespace BethanyPieShop.InventoryManagement.Domain.ProductManagement
{
    public class RegularProduct : Product, ISavable
    {
        public RegularProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : base(id, name, description, price, unitType, maxAmountInStock)
        {
        }

        public override object Clone()
        {
            return new RegularProduct(0, Name, Description, new Price() {ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency }, this.UnitType, this.maxItemsInStock);
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};4;";
        }

        public override void IncreaseStock()
        {
            AmountInStock++;
        }
    }
}
