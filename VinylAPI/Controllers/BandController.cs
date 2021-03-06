using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Data;
using VinylAPI.Entities;
using VinylAPI.Models;
using VinylAPI.Services;

namespace VinylAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IBandService _serivce;

        public BandController(IBandService serivce)
        {
            _serivce = serivce;
        }

        [HttpGet("Query")]
        public IActionResult GetAll([FromQuery] BandQuery query)
        {
            var bandsDtos = _serivce.GetAll(query);

            return Ok(bandsDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var band = _serivce.GetById(id);
            return Ok(band);
        }
        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateBand([FromBody] CreateBandDto dto)
        {
            var bandId = _serivce.Create(dto);

            return Created($"/api/band/{bandId}", null);
        }
        [HttpPut("{id}/update")]
        [Authorize]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateBandDto dto)
        {
            _serivce.UpdateBand(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}/delete")]
        [Authorize]
        public IActionResult Delete([FromRoute] int id)
        {
            _serivce.Delete(id);
            return NoContent();
        }
       
    }
}
