using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Models;
using VinylAPI.Services;

namespace VinylAPI.Controllers
{
    [Route("api/bands/{bandId}/album")]
    [ApiController]
    public class MusicAlbumController : ControllerBase
    {
        private readonly IMusicAlbumService _musicAlbumService;
        public MusicAlbumController(IMusicAlbumService musicAlbumService)
        {
            _musicAlbumService = musicAlbumService;
        }

        [HttpGet]
        public IActionResult GetAllMusicAlbums([FromRoute] int bandId)
        {
            var albums = _musicAlbumService.GetAll(bandId);
            return Ok(albums);
        }

        [HttpGet("{albumId}")]
        public IActionResult GetMusicAlbum([FromRoute] int bandId, [FromRoute] int albumId)
        {
            var album = _musicAlbumService.GetAlbumById(bandId, albumId);
            return Ok(album);
        }
        [HttpPost("create")]
        public IActionResult CreateAlbum([FromRoute] int bandId, [FromBody] CreateAlbumDto dto)
        {
            var albumdId = _musicAlbumService.Create(bandId, dto);
            return Created($"api/bands/{bandId}/album/{albumdId}",null);
        }
        [HttpPut]
        public IActionResult UpdateAlbum([FromRoute] int bandId, [FromBody] UpdateAlbumDto dto)
        {
            _musicAlbumService.Update(bandId, dto);
            return NoContent();
        }
        [HttpDelete("{albumId}")]
        public IActionResult DeleteAlbum([FromRoute] int bandId, [FromRoute] int albumId)
        {
            _musicAlbumService.Delete(bandId, albumId);
            return NoContent();
        }
    }   
}
