using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationUpload.Data
{
    public class WebApplicationUploadDbContext : DbContext
    {
        public WebApplicationUploadDbContext(DbContextOptions<WebApplicationUploadDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }
    }

}
