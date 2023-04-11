using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportLib
{
    public class Connection : DbContext
    {
        public Connection() : base()
        {
            Database.EnsureCreated();//проверяет есть ли БД
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=K-405-10\SQLEXPRESS;
                                          Initial Catalog=Bread;
                                          Integrated Security=SSPI");
            base.OnConfiguring(optionsBuilder);
        }
       

        public DbSet<Product> Products { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<ShopToCharacteristic> ShopToCharacteristics { get; set; }

    }
}
