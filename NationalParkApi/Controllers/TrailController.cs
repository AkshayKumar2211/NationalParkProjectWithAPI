using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkApi.Models;
using NationalParkApi.Models.DTOs;
using NationalParkApi.Repository.IRepository;

namespace NationalParkApi.Controllers
{
    [Route("api/trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository= trailRepository;
            _mapper= mapper;
        }

        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().Select(_mapper.Map<TrailDto>));
        }

        [HttpGet("{trailId:int}",Name ="GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail=_trailRepository.GetTrail(trailId);

            if (trail == null) return NotFound();

            var trailDto=_mapper.Map<TrailDto>(trail);

            return Ok(trailDto);
        }

        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDto trailDto)
        {

           if(trailDto == null) return BadRequest();

           

           if(_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

           var trail=_mapper.Map<Trail>(trailDto);

            if(!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (!ModelState.IsValid) return BadRequest();

            return CreatedAtRoute("GetTrail", new {trailId=trail.Id},trail);
        }

        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDto trailDto)
        {
            if(trailDto==null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var trail=_mapper.Map<Trail>(trailDto);

            if(!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete("trailId:int")]
        public IActionResult DeleteTrail(int trailId)
        {
            if(!_trailRepository.TrailExist(trailId)) return NotFound();

            var trail=_trailRepository.GetTrail(trailId);

            if(trail==null) return NotFound();  

            if(!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
