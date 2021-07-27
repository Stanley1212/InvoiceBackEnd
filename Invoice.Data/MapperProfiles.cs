using AutoMapper;
using Invoice.Core.Dtos;
using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Data
{
   public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            #region Unit
            CreateMap<UnitCreateDto, Unit>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.UserCreated, o => o.MapFrom(s => ""))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Active, o => o.MapFrom(s => true));

            CreateMap<UnitUpdateDto, Unit>()
              .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
              .ForMember(d => d.UserUpdated, o => o.MapFrom(s => ""))
              .ForMember(d => d.UpdatedDate, o => o.MapFrom(s => DateTime.UtcNow))
              .ForMember(d => d.Active, o => o.MapFrom(s => true));
            #endregion

        }

    }
}
