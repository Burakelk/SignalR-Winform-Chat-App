using DonemProje.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProje
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0VVF11F; Initial Catalog = ProjeData; Integrated Security = True; Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Users> Users { get; set; }
       

    }


}
