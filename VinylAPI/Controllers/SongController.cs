using Microsoft.AspNetCore.Mvc;
using VinylAPI.Models;
using VinylAPI.Services;

namespace VinylAPI.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/album/{albumId}/song")]

    public class SongController : ControllerBase
    {
        private readonly ISongService _service;
        public SongController(ISongService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAllSong([FromRoute] int bandId, [FromRoute] int albumId)
        {
            var albums = _service.GetAll(bandId, albumId);
            return Ok(albums);
        }
        [HttpGet("{songId}")]
        public IActionResult GetSongbyId([FromRoute] int bandId, [FromRoute] int albumId, [FromRoute] int songId)
        {
            var albums = _service.GetById(bandId, albumId, songId);
            return Ok(albums);
        }
        [HttpPost("create")]
        public IActionResult CreateSongForAlbum([FromRoute] int bandId, [FromRoute] int albumId, [FromBody] CreateSongDto dto)
        {
            var songId = _service.CreateSong(bandId, albumId, dto);

            return Created($"api/bands/{bandId}/album/{albumId}/song/{songId}", null);
        }
        [HttpPut("update")]
        public IActionResult UpdateSong([FromRoute] int bandId, [FromRoute] int albumId, [FromBody] UpdateSongDto dto)
        {
            _service.UpdateSong(bandId, albumId, dto);
            return NoContent();
        }
    }
}
