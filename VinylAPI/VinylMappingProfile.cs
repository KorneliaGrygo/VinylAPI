using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Entities;
using VinylAPI.Models;

namespace VinylAPI
{
    public class VinylMappingProfile : Profile
    {
        public VinylMappingProfile()
        {
            CreateMap<Band, BandDto>();
            CreateMap<CreateBandDto, Band>();
            CreateMap<GetMusicAlbumDto, MusicAlbum>().ReverseMap();
            CreateMap<CreateAlbumDto, MusicAlbum>().ReverseMap();
            CreateMap<MusicAlbum, MusicAlbumDto>();
            CreateMap<UpdateAlbumDto, MusicAlbum>().ReverseMap();
            CreateMap<Song, SongDto>();
            CreateMap<Song, CreateSongDto>().ReverseMap();

        }
    }
}
