using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Rentler.Interview.Client.Models;

namespace Rentler.Interview.Api.Data
{
    public class FoodRepository
    {
        private string ConnectionString { get; }

        public FoodRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<List<Food>> GetFood()
        {
            await using var connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();

                 const string query = @"
                    SELECT
                        Brand,
                        Description,
                        ServingSize,
                        Fat,
                        Carbohydrates,
                        Protein
                    FROM Foods";

                var result = await connection.QueryAsync<Food>(query);

                return result.ToList();
            }
            catch (Exception exc)
            {
                return new List<Food>();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<Food> InsertFood(Food food)
        {
            await using var connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();

                const string query = @"
                    INSERT INTO Foods (Brand, Description, ServingSize, Calories, Fat, Carbohydrates, Protein)
                    VALUES
                        (@Brand, @Description, @ServingSize, @Calories, @Fat, @Carbohydrates, @Protein);
                    SELECT SCOPE_IDENTITY();";

                var resultId = await connection.ExecuteScalarAsync<int>(query, food);
                food.Id = resultId;

                return food;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
