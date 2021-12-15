using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylAPI.Models;
using VinylAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VinylAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SongController : ControllerBase
    {
        private readonly ISongService _service;
        public SongController(ISongService service)
        {
            _service = service;
        }
        [HttpGet("bands/{bandId}/album/{albumId}/song")]
        public IActionResult GetAllSong([FromRoute] int bandId, [FromRoute] int albumId)
        {
            var albums = _service.GetAll(bandId, albumId);
            return Ok(albums);
        }
        [HttpGet("query")]
        public IActionResult GetAllSongsWithQuuery([FromQuery] SongQuery query)
        {
            return Ok(_service.GetAllWithQuery(query));
        }
        [HttpGet("bands/{bandId}/album/{albumId}/song/{songId}")]
        public IActionResult GetSongbyId([FromRoute] int bandId, [FromRoute] int albumId, [FromRoute] int songId)
        {
            var albums = _service.GetById(bandId, albumId, songId);
            return Ok(albums);
        }
        [HttpPost("bands/{bandId}/album/{albumId}/song/create")]
        [Authorize]
        public IActionResult CreateSongForAlbum([FromRoute] int bandId, [FromRoute] int albumId, [FromBody] CreateSongDto dto)
        {
            var songId = _service.CreateSong(bandId, albumId, dto);

            return Created($"api/bands/{bandId}/album/{albumId}/song/{songId}", null);
}
        [HttpPut("bands/{bandId}/album/{albumId}/song/update")]
        [Authorize]
        public IActionResult UpdateSong([FromRoute] int bandId, [FromRoute] int albumId, [FromBody] UpdateSongDto dto)
        {
            _service.UpdateSong(bandId, albumId, dto);
            return NoContent();
        }
        [HttpDelete("bands/{bandId}/album/{albumId}/song/{songId}/delete")]
        [Authorize]
        public IActionResult DeleteSong([FromRoute] int bandId, [FromRoute] int albumId, [FromRoute] int songId)
        {
            _service.DeleteSong(bandId, albumId, songId);
            return NoContent();
        }
    }
}
