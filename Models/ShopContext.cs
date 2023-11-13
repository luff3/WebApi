﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace lab04.Models
{
    public class ShopContext  : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext>options) : base (options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
    }
}
