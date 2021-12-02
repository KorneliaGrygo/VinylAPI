﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VinylAPI.Data;
using VinylAPI.Entities;
using VinylAPI.Middleware.Exceptions;
using VinylAPI.Models;

namespace VinylAPI.Services
{
    public interface ISongService
    {
        IEnumerable<SongDto> GetAll(int bandId, int albumId);
        SongDto GetById(int bandId, int albumId, int songId);
        int CreateSong(int bandId, int albumId, CreateSongDto dto);
        void UpdateSong(int bandId, int albumId, UpdateSongDto dto);
    }
    public class SongService : ISongService
    {
        private readonly VinylAPIDbContext _context;
        private readonly IMapper _mapper;

        public SongService(VinylAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CreateSong(int bandId, int albumId, CreateSongDto dto)
        {
            var band = _context.Bands
                              .Include(x => x.Albums)
                              .ThenInclude(x => x.Songs)
                              .FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException($"Zespól o id {bandId} nieistnieje");

            var album = band.Albums.FirstOrDefault(x => x.Id == albumId);
            if (album == null)
                throw new NotFoundException($"Album o id {albumId} nie istnieje");

            var song = _mapper.Map<Song>(dto);
            song.MusicAlbumId = albumId;

            _context.Songs.Add(song);
            _context.SaveChanges();

            return song.Id;
        }

        public IEnumerable<SongDto> GetAll(int bandId, int albumId)
        {
            var band = _context.Bands
                                .Include(x => x.Albums)
                                .ThenInclude(x => x.Songs)
                                .FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException($"Zespól o id {bandId} nieistnieje");
            var album = band.Albums.FirstOrDefault(x => x.Id == albumId);
            if (album == null)
                throw new NotFoundException($"Album o id {albumId} nie istnieje");

            return _mapper.Map<IEnumerable<SongDto>>(album.Songs.ToList());
        }

        public SongDto GetById(int bandId, int albumId, int songId)
        {
            var band = _context.Bands
                               .Include(x => x.Albums)
                               .ThenInclude(x => x.Songs)
                               .FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException($"Zespól o id {bandId} nieistnieje");

            var album = band.Albums.FirstOrDefault(x => x.Id == albumId);
            if (album == null)
                throw new NotFoundException($"Album o id {albumId} nieistnieje");

            var song = _mapper.Map<SongDto>(album.Songs.FirstOrDefault(x => x.Id == songId));
            if (song == null)
                throw new NotFoundException($"Piosenka o id {albumId} nieistnieje");

            return song;
        }

        public void UpdateSong(int bandId, int albumId, UpdateSongDto dto)
        {
            var band = _context.Bands
                            .Include(x => x.Albums)
                            .ThenInclude(x => x.Songs)
                            .FirstOrDefault(x => x.Id == bandId);
            if (band == null)
                throw new NotFoundException($"Zespól o id {bandId} nieistnieje");

            var album = band.Albums.FirstOrDefault(x => x.Id == albumId);
            if (album == null)
                throw new NotFoundException($"Album o id {albumId} nieistnieje");

            var song = album.Songs.FirstOrDefault(x => x.Id == dto.Id);
            if (song == null)
                throw new NotFoundException($"Piosenka o id {albumId} nieistnieje");
            if (!string.IsNullOrEmpty(dto.Name))
                song.Name = dto.Name;
            if (dto.Lenght != null)
                song.Lenght = (double)dto.Lenght;

            _context.SaveChanges();
        }
    }
}
