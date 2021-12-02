using AutoMapper;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var bandsDtos = _serivce.GetAll();

            return Ok(bandsDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var band = _serivce.GetById(id);
            return Ok(band);
        }
        [HttpPost("create")]
        public IActionResult CreateBand([FromBody] CreateBandDto dto)
        {
            var bandId = _serivce.Create(dto);

            return Created($"/api/band/{bandId}", null);
        }
        [HttpPut("{id}/update")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateBandDto dto)
        {
            _serivce.UpdateBand(id, dto);
            return NotFound();
        }
        [HttpDelete("{id}/delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _serivce.Delete(id);
            return NotFound();
        }
       
    }
}
