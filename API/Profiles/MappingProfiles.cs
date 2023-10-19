using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Views;

namespace API.Profiles;
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Laboratorio,LaboratorioDto>().ReverseMap();
            CreateMap<Medicamento,MedicamentoDto>().ReverseMap();
            CreateMap<Veterinario,VeterinarioDto>().ReverseMap();
            CreateMap<Mascota,MascotaDto>().ReverseMap();
            CreateMap<Mascota,MascotaxRazaDto>().ReverseMap();
            CreateMap<Raza,RazaDto>().ReverseMap();
            CreateMap<Especie,EspecieDto>().ReverseMap();
            CreateMap<Propietario,PropietarioDto>().ReverseMap();
            CreateMap<Propietario,PropietarioxMascotasDto>().ReverseMap();
            CreateMap<Raza,RazaxMascotasDto>().ReverseMap();
            CreateMap<Especie,EspeciexMascotasDto>().ReverseMap();
            CreateMap<Cita,CitaxMascotaDto>().ReverseMap();
            CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            CreateMap<SpeciesxPets,SpeciesxPetDto>().ReverseMap();
            CreateMap<Usuario,UsuarioDto>().ReverseMap();
            CreateMap<MovimientoTotal,MovimientoTotalDto>().ReverseMap();
            CreateMap<Cita,CitaDto>().ReverseMap();
            CreateMap<MovimientoMedicamento,MovimientoDto>().ReverseMap();
        }
    }
