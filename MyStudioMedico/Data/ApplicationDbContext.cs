using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStudioMedico.Models;

namespace MyStudioMedico.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyStudioMedico.Models.Dottore> Dottore { get; set; }
        public DbSet<MyStudioMedico.Models.Appuntamento> Appuntamento { get; set; }
    }
}