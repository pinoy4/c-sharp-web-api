using Microsoft.EntityFrameworkCore;
using MWTest.Model;
using System;

namespace MWTest.Db
{
    public class MWTestDb : DbContext
    {
        public MWTestDb(DbContextOptions<MWTestDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
