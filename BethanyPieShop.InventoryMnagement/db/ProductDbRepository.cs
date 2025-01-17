using BethanyPieShop.InventoryMnagement.Domain.ProductManagement;
using Microsoft.Data.SqlClient;

namespace BethanyPieShop.InventoryManagement.db
{
    public class ProductDbRepository: IRepository<Product>
    {

        private readonly DatabaseConnection _connection;
        private readonly string connectionString = "Server=localhost,1433;Database=BethanyPieShop;User Id=SA;Password=YourPassword123;Encrypt=False;";


        public ProductDbRepository()
        {
            _connection = new DatabaseConnection(connectionString);
        }

        public void AddProduct(Product entity)
        {
            var query = "INSERT INTO Product (Name, Description, AmountInStock, Price, CurrencyID, UnitTypeID, ProductTypeID, MaxAmountInStock) " +
                "        VALUES (@Name, @Description, @AmountInStock, @Price, @CurrencyID, @UnitTypeID, @ProductTypeID, @MaxAmountInStock)";

            using (var connection = _connection.GetConnection()) 
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@AmountInStock", entity.AmountInStock);
                command.Parameters.AddWithValue("@Price", entity.Price.ItemPrice);
                command.Parameters.AddWithValue("@CurrencyID", (int)entity.Price.Currency);
                command.Parameters.AddWithValue("@UnitTypeID", (int)entity.UnitType + 1);
                command.Parameters.AddWithValue("@ProductTypeID", entity.ProductType);
                command.Parameters.AddWithValue("@MaxAmountInStock", entity.MaxItemInStock);

                connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}
