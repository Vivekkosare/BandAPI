using AutoMapper;
using BandsAPI.Entities;
using BandsAPI.Helpers;
using BandsAPI.Models;
using BandsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandsAPI.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandsResourceParameters bandsResourceParameters)
        {
            var bandRepo = _bandAlbumRepository.GetBands(bandsResourceParameters);
            var bandsDto = new List<BandDto>();
            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandRepo));
        }

        [HttpGet("{bandId}", Name ="GetBand")]
        public IActionResult GetBand(Guid bandId)
        {
            var bandRepo = _bandAlbumRepository.GetBand(bandId);
            if (bandRepo == null)
                return NotFound();
            return Ok(bandRepo);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody]BandForCreatingDto band)
        {
            var bandEntity = _mapper.Map<Band>(band);
            _bandAlbumRepository.AddBand(bandEntity);
            _bandAlbumRepository.Save();

            var bandToReturn = _mapper.Map<BandDto>(bandEntity);
            return CreatedAtRoute("GetBand", new { bandId = bandToReturn.Id }, bandToReturn);
        }

        [HttpOptions]
        public IActionResult GetBandOptions()
        {
            Response.Headers.Add("Allow", "POST,GET,HEADER,DELETE,OPTIONS");
            return Ok();
        }
    }
}
