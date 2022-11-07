namespace WebApiRestaurante2.DTOs
{
    public class AcompañamientoDTOConPlatillo: GETAcompañamientoDTO
    {
        public List<GETPlatilloDTO> Platillos { get; set; }
    }
}
