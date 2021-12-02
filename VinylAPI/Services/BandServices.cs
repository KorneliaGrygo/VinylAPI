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
    public interface IBandService
    {
        BandDto GetById(int id);
        IEnumerable<BandDto> GetAll();
        int Create(CreateBandDto dto);
        void Delete(int bandId);
        void UpdateBand(int bandId, UpdateBandDto dto);

    }
    public class BandServices : IBandService
    {
        private readonly VinylAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public BandServices(VinylAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BandDto GetById(int id)
        {
            var band = _dbContext
                        .Bands
                        .FirstOrDefault(band => band.Id == id);
            if (band is null)
                throw new NotFoundException("Nie znaleziono zespołu");


            var bandDto = _mapper.Map<BandDto>(band);
            return bandDto;
        }

        public IEnumerable<BandDto> GetAll()
        {
            var bands = _dbContext.Bands
                   .Include(x => x.Albums)
                   .ThenInclude(x => x.Songs)
                   .ToList();

            var bandsDtos = _mapper.Map<List<BandDto>>(bands);
            return bandsDtos;
        }

        public int Create(CreateBandDto dto)
        {
            var band = _mapper.Map<Band>(dto);

            _dbContext.Bands.Add(band);
            _dbContext.SaveChanges();
            return band.Id;
        }

        public void Delete(int bandId)
        {
            var band = _dbContext
                .Bands
                .FirstOrDefault(b => b.Id == bandId);
            if (band == null)
                throw new NotFoundException("Nie znaleziono zespołu");
                

            _dbContext.Bands.Remove(band);
            _dbContext.SaveChanges();
        }

        public void UpdateBand(int bandId, UpdateBandDto dto)
        {
            var band = _dbContext
                .Bands
                .FirstOrDefault(b => b.Id == bandId);
            if (band == null)
                throw new NotFoundException("Nie znaleziono zespołu");

            if (!string.IsNullOrEmpty(dto.Name))
                band.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description))
                band.Description = dto.Description;

            _dbContext.SaveChanges();

        }
    }
}
