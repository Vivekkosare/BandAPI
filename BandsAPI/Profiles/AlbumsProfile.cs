using AutoMapper;
using BandsAPI.Entities;
using BandsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.Profiles
{
    public class AlbumsProfile:Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<AlbumForCreatingDto, Album>();
            CreateMap<AlbumForUpdatingDto, Album>().ReverseMap();
        }
    }
}
