using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        PageResult<BandDto> GetAll(Query query);
        int Create(CreateBandDto dto);
        void Delete(int bandId);
        void UpdateBand(int bandId, UpdateBandDto dto);

    }
    public class BandServices : IBandService
    {
        private readonly VinylAPIDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;

        public BandServices(VinylAPIDbContext dbContext, IMapper mapper, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextService = contextService;
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
        public PageResult<BandDto> GetAll(Query query)
        {
            var baseQuery = _dbContext.Bands
                   .Include(x => x.Albums)
                   .ThenInclude(x => x.Songs)
                   .Where(x => query.SearchPhrase == null ||
                   (x.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                   || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Band, object>>>
                {
                    {nameof(Band.Name), r=>r.Name },
                    {nameof(Band.Description), r=>r.Description },
                };
                var selectedColumn = columnsSelector[query.SortBy];
                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn):
                    baseQuery.OrderByDescending(selectedColumn);
            }

            var bands = baseQuery.Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
            var bandsDtos = _mapper.Map<List<BandDto>>(bands);

            return new PageResult<BandDto>(bandsDtos, bandsDtos.Count(), query.PageSize, query.PageNumber);
        }

        public int Create(CreateBandDto dto)
        {
            IsInRole(Roles.USER);
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
        private void IsInRole(string roleName)
        {
            var isAdmin = _contextService.User.IsInRole(roleName);
            if (!isAdmin)
                throw new ForbiddenException("Brak dostępu do zasobu");
        }
    }
}
