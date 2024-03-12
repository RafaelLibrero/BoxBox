﻿using BoxBox.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxBox.Data
{
    public class BoxBoxContext: DbContext
    {
        public BoxBoxContext(DbContextOptions<BoxBoxContext> options)
        :base (options) { }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Race> Races { get; set; }

    }
}
