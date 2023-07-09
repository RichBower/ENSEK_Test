﻿using interview.test.ensek.Core.Domain.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace interview.test.ensek.Infrastructure.SqlServer;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        
    }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<MeterReadingEntity> MeterReadings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>().ToTable("Account");
        modelBuilder.Entity<MeterReadingEntity>().ToTable("MeterReading");

    }
}
