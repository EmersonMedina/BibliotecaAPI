﻿using BibliotecaAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Utilidades
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            :base(dbContextOptions)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
    }
}