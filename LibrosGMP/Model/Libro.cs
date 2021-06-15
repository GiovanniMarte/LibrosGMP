using SQLite;

namespace LibrosGMP.Models
{
    public class Libro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string TLibro { get; set; }
        public string Accion { get; set; }
    }
}
