using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Expenses.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> User { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=../Expenses.WebApi/ExpensesDB.db");
        }
        
    }

}

