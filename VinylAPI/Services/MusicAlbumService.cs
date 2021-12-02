using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Data;
using VinylAPI.Entities;
using VinylAPI.Middleware.Exceptions;
using VinylAPI.Models;

namespace VinylAPI.Services
{
    public interface IMusicAlbumService
    {
        IEnumerable<GetMusicAlbumDto> GetAll(int bandId);
        GetMusicAlbumDto GetAlbumById(int bandId, int albumId);
        int Create(int bandId, CreateAlbumDto dto);
        void Update(int bandId, UpdateAlbumDto dto);
        void Delete(int bandId, int albumId);
    }
    public class MusicAlbumService : IMusicAlbumService
    {
        private readonly VinylAPIDbContext _context;
        private readonly IMapper _mapper;
        public MusicAlbumService(VinylAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int bandId, CreateAlbumDto dto)
        {
            var band = _context.Bands.FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException("Podany zespół nie istnieje");

            MusicAlbum album = _mapper.Map<MusicAlbum>(dto);

            album.BandId = bandId;

            _context.MusicAlbums.Add(album);
            _context.SaveChanges();

            return album.Id;

        }

        public void Delete(int bandId, int albumId)
        {
            var band = _context.Bands
               .Include(x => x.Albums)
               .FirstOrDefault(x => x.Id == bandId);

            if (band == null)
                throw new NotFoundException($"Podany zespół nie istnieje");
            var album = band.Albums.FirstOrDefault(x => x.Id == albumId);

            if(album == null)
                throw new NotFoundException($"Album o id {albumId} nie istnieje");

             _context.MusicAlbums.Remove(album);
            _context.SaveChanges();
        }

        public GetMusicAlbumDto GetAlbumById(int bandId, int albumId)
        {
            var band = _context.Bands
                .Include(x => x.Albums)
                .FirstOrDefault(x => x.Id == bandId);

            if (band == null)
                throw new NotFoundException($"Podany zespół nie istnieje");
            
            var album = band.Albums.FirstOrDefault(album => album.Id == albumId);

            if (album == null)
                throw new NotFoundException("Album o danym id nie istnieje");

            return _mapper.Map<GetMusicAlbumDto>(album);
        }

        public IEnumerable<GetMusicAlbumDto> GetAll(int bandId)
        {
            var band = _context.Bands
                .Include(x => x.Albums)
                .FirstOrDefault(x => x.Id == bandId);

            if (band == null)
                throw new NotFoundException($"Podany zespół nie istnieje");

            return _mapper.Map<IEnumerable<GetMusicAlbumDto>>(band.Albums);
        }

        public void Update(int bandId, UpdateAlbumDto dto)
        {
            var band = _context.Bands.Include(x => x.Albums).FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException("Podany zespół nie istnieje");

            var album = band.Albums
                .FirstOrDefault(a => a.Id == dto.Id);

            if (album == null)
                throw new NotFoundException("Podany album nie istnieje");

            if(dto.PublicationYear != null)
                album.PublicationYear = (int)dto.PublicationYear;
            if(!string.IsNullOrEmpty(dto.Genre))
                album.Genre = dto.Genre;
            if(!string.IsNullOrEmpty(dto.Name))
                album.Name = dto.Name;
            if (dto.AmountOfSongs != null)
                album.AmountOfSongs = (int)dto.AmountOfSongs;

            _context.SaveChanges();
        }
    }
}
