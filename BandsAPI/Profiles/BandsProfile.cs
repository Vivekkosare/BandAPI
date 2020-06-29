
using AutoMapper;
using BandsAPI.Entities;
using BandsAPI.Helpers;
using BandsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.Profiles
{
    public class BandsProfile : Profile
    {
        public BandsProfile()
        {
            CreateMap<Band, BandDto>()
                .ForMember(dest => dest.FoundYearsAgo,
                opt => opt.MapFrom(src => $"{src.Founded.ToString("yyyy")}({src.Founded.GetYearsAgo()} years ago)"));

            CreateMap<BandForCreatingDto, Band>();
        }
    }
}
