using Login.APİ.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.APİ.DbContext
{
    public class MyContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
    
}
