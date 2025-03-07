using ExamenZapasPartialViewsPaginacion.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenZapasPartialViewsPaginacion.Data
{
    public class ZapasContext:DbContext
    {
        public ZapasContext(DbContextOptions<ZapasContext> options) :base(options){ }

        public DbSet<Imagen> Imagenes {  get; set; }

        public DbSet<Zapas> Zapatillas { get; set; }

    }
}
