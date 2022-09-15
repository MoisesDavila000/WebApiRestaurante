namespace WebApiRestaurante.Entidades
{
    public class Acompañamiento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int PlatilloId { get; set; }

        public Platillos Platillo { get; set; }  
    }
}
