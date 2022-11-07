namespace WebApiRestaurante2.DTOs
{
    public class PlatilloDTOConAcompañamiento: GETPlatilloDTO
    {
        public List<GETAcompañamientoDTO> Acompañamiento { get; set; }
    }
}
