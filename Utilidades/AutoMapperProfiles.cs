using AutoMapper;
using WebApiRestaurante2.DTOs;
using WebApiRestaurante2.Entidades;

namespace WebApiRestaurante2.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PlatilloDTO, Platillos>();
            CreateMap<Platillos, GETPlatilloDTO>();
            CreateMap<Platillos, PlatilloDTOConAcompañamiento>()
                .ForMember(platillo => platillo.Acompañamiento, opciones => opciones.MapFrom(MapPLatilloDTOAcompañamiento));
            CreateMap<AcompañamientoDTO, Acompañamiento>()
                .ForMember(acompañamiento => acompañamiento.PlatilloAcompañamiento, opciones => opciones.MapFrom(MapPlatilloAcompañamiento));
            CreateMap<Acompañamiento, GETEmpleadoDTO>();
            CreateMap<Acompañamiento, AcompañamientoDTOConPlatillo>()
                .ForMember(acompañamiento => acompañamiento.Platillos, opciones => opciones.MapFrom(MapAcompañamientoDTOPlatillos));
            CreateMap<TurnoDTO, Turnos>();
            CreateMap<Turnos, GETTurnoDTO>();
            CreateMap<EmpleadoDTO, Empleados>();
            CreateMap<Empleados, GETEmpleadoDTO>();
        }

        private List<GETAcompañamientoDTO> MapPLatilloDTOAcompañamiento(Platillos platillo, GETPlatilloDTO getPlatilloDTO)
        {
            var resultado = new List<GETAcompañamientoDTO>();

            if (platillo.PlatilloAcompañamientos == null) { return resultado; }

            foreach (var platilloAcompañamiento in platillo.PlatilloAcompañamientos)
            {
                resultado.Add(new GETAcompañamientoDTO()
                {
                    Id = platilloAcompañamiento.AcompañamientoId,
                    Nombre = platilloAcompañamiento.Acompañamiento.Nombre,
                    Descripcion = platilloAcompañamiento.Acompañamiento.Descripcion
                }); 
            }

            return resultado;
        }

        private List<GETPlatilloDTO> MapAcompañamientoDTOPlatillos(Acompañamiento acompañamiento, GETAcompañamientoDTO getAcompañamientoDTO)
        {
            var resultado = new List<GETPlatilloDTO>();

            if (acompañamiento.PlatilloAcompañamiento == null)
            {
                return resultado;
            }

            foreach (var platilloAcompañamiento in acompañamiento.PlatilloAcompañamiento)
            {
                resultado.Add(new GETPlatilloDTO()
                {
                    Id = platilloAcompañamiento.PlatillosId,
                    Tipo = platilloAcompañamiento.Platillos.Tipo,
                    Nombre = platilloAcompañamiento.Platillos.Nombre,
                    Precio = platilloAcompañamiento.Platillos.Precio,
                    Descricpion = platilloAcompañamiento.Platillos.Descricpion

                });
            }

            return resultado;
        }

        private List<PlatilloAcompañamiento> MapPlatilloAcompañamiento(AcompañamientoDTO acompañamientoDTO, Acompañamiento acompañamiento)
        {
            var resultado = new List<PlatilloAcompañamiento>();

            if (acompañamientoDTO.PlatillosIds == null) { return resultado; }
            foreach (var platilloId in acompañamientoDTO.PlatillosIds)
            {
                resultado.Add(new PlatilloAcompañamiento() { PlatillosId = platilloId });
            }
            return resultado;
        }
    }
}