using Microsoft.EntityFrameworkCore;
using ProductFinder.Entities;

namespace ProductFinder.DataAccess
{
    public class ProductDbContext : DbContext
    {
        //Burada ilk yapacağımız işlem OnConfiguring metodunu override edip ConnectionStringimizi vermek.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=JARVIS\\SQLEXPRESS; Database=ProductDb; Integrated Security=True; MultipleActiveResultSets=False; Encrypt=False; TrustServerCertificate=False;");
            //Buraya Dbmizin ismini verdik ama Db'de böyle bir tablo yok.
            //Bu tabloyu da migration ile sağlayacağız.
        }

        public DbSet<Product> Products { get; set; }
        //Buradaki Product ile biz Entity katmanını buraya dahil ettik.
    }
}
