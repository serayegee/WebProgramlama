﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using odevweb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;



namespace odevweb.Models
{
    public class KuaforContext : DbContext
    {
        public KuaforContext() { }
        public KuaforContext(DbContextOptions<KuaforContext> options) : base(options) { } 
   
        public DbSet<Islem> Islems { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<PersonelMusaitlik> PersonelMusaitliks { get; set; }
        public DbSet<Randevu> Randevus { get; set; }    
        public DbSet<Kullanici> Kullanicis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QQFBS32\\SQLEXPRESS;Database=KuaforDb;Trusted_Connection=True");
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RandevuIslem>()
                .HasKey(ri => new{ri.RandevuId, ri.IslemId});

            modelBuilder.Entity<RandevuPersonel>()
                .HasKey(rp => new { rp.RandevuId, rp.PersonelId });

           /* modelBuilder.Entity<Personel>()
                .HasOne(c => c.Islem)
                .WithMany(d => d.Personels)
           
            .HasForeignKey(p => p.IslemId) // Foreign Key tanımlaması
            .OnDelete(DeleteBehavior.Restrict); // Silme işlemi kısıtlı (işlem silinirse personeller silinmez)
           
            base.OnModelCreating(modelBuilder);
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          modelBuilder.Entity<Personel>()
        .HasOne(p => p.Islem)
        .WithMany(i => i.Personels)
        .HasForeignKey(p => p.IslemId)
        .OnDelete(DeleteBehavior.Cascade); 

        }

    }


}


