using Microsoft.EntityFrameworkCore;

namespace AchadosApi.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Animais> Animais { get; set; }
        public DbSet<Observacoes> Observações { get; set; }

    }
}
    