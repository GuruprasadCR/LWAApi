using Microsoft.EntityFrameworkCore;

namespace LWAApi.Models
{
    public class DatabaseContexta:DbContext
    {

        public DatabaseContexta(DbContextOptions<DatabaseContexta> option)
                : base(option)
        {
        }

        public  DbSet<Category>? CategoriesTable { get; set; }
        public  DbSet<Product>? ProductsTable { get; set; }

    }
}




//public DatabaseContexta()
//{
//}
