using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NationalParkApi.Models;
using NationalParkApi.Models.DTOs;
using NationalParkApi.Repository.IRepository;

namespace NationalParkApi.Controllers
{
    [Route("api/nationalPark")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository=nationalParkRepository;
            _mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetNationalPark()
        {
            var nationalParkList = _nationalParkRepository.GetNationalParks().Select(_mapper.Map<NationalParkDto>);
            return Ok(nationalParkList);
        }

        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark=_nationalParkRepository.GetNationalPark(nationalParkId);
            if (nationalPark==null) return NotFound();  //404

            var nationalParkDto = _mapper.Map<NationalPark, NationalParkDto>(nationalPark);

            return Ok(nationalParkDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto==null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "NationalPark in DB");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

           

            var nationalPark=_mapper.Map<NationalParkDto,NationalPark>(nationalParkDto);

            if(!_nationalParkRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);
        }

        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto== null) return BadRequest();
            if (ModelState.IsValid) return BadRequest();

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if(!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if(!_nationalParkRepository.NationalParkExists(nationalParkId)) return NotFound();

            var nationalPark=_nationalParkRepository.GetNationalPark(nationalParkId);

            if(nationalPark == null) return NotFound();

            if(!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong while Deleting");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
