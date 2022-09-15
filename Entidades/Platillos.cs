namespace WebApiRestaurante.Entidades
{
    public class Platillos
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public string Nombre { get; set; }

        public int Precio { get; set; }

        public string Descricpion { get; set; }

        public List<Acompañamiento> Acompañamientos { get; set; }
    }
}
