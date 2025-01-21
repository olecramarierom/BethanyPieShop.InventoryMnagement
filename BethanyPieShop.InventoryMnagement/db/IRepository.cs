﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanyPieShop.InventoryManagement.db
{
    public interface IRepository<T>
    {
        void AddProduct(T entity);
        IList<T> GetAllProducts();
    }
}
