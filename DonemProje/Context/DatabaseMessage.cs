using DonemProje.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProje.Context
{
    public class DatabaseMessage :DbContext
    {
   
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0VVF11F; Initial Catalog = MesajData; Integrated Security = True; Trust Server Certificate=True");
                base.OnConfiguring(optionsBuilder);
            }

            public DbSet<MessageProperties> Messages { get; set; }


       
    }
}
