using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenZapasPartialViewsPaginacion.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class Imagen
    {
        [Key]
        [Column("IDIMAGEN")]
        public int Id { get; set; }

        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }

        [Column("IMAGEN")]
        public string ImagenUrl {  get; set; }
    }
}
