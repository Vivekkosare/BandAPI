using AutoMapper;
using BandsAPI.Entities;
using BandsAPI.Models;
using BandsAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BandsAPI.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController:ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult <IEnumerable<AlbumDto>> GetAlbumsFromBands(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumsRepo = _bandAlbumRepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albumsRepo));
        }

        [HttpGet("{albumId}", Name = "GetAlbumForBand")]
        public ActionResult <AlbumDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumRepo == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumDto>(albumRepo));
        }

        [HttpPost]
        public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)
        {
            var albumEntity = _mapper.Map<Entities.Album>(album);
            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            var albumToReturn = _mapper.Map<AlbumDto>(albumEntity);
            return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn);
        }

        [HttpPut("{albumId}")]
        public ActionResult UpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] AlbumForUpdatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
                return NotFound();
            _mapper.Map(album, albumFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpPatch("{albumId}")]
        public ActionResult PartiallyUpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] JsonPatchDocument<AlbumForUpdatingDto> patchDocument)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
                return NotFound();

            var albumToPatch = _mapper.Map<AlbumForUpdatingDto>(albumFromRepo);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            if (!TryValidateModel(albumToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(albumToPatch, albumFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbumForBand(Guid albumId, Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
                return NotFound();

            _bandAlbumRepository.DeleteAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }
    }
}
