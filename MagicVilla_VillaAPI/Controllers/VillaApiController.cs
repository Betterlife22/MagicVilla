﻿using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[Controller]")] 
    [Route("api/VillaApi")]
    [ApiController] //add apicontroller

    public class VillaApiController : ControllerBase    //implement controllerBase


    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaApiController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpGet] //endpoint to get data
        [ProducesResponseType(StatusCodes.Status200OK)]
        //using async
        public async Task <ActionResult<IEnumerable<VillaDTO>>> GetVillas() // Ienmerarble la kieu dem duoc
        {
            IEnumerable<Villa> VillaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map <List<VillaDTO>> (VillaList));
        }
        [HttpGet("{id:int}", Name = "Getvilla")]
        // product response type sẽ trả về kiểu phản hồi
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task <ActionResult<VillaDTO>> GetVillaId(int id)
        {
            if (id < 0 || id == 0)
            {

                return BadRequest();
            }
            var villa=await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO> (villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (await _db.Villas.FirstOrDefaultAsync(u => u.name.ToLower() == villaDTO.name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa is alredy exist !!!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            
            Villa model = _mapper.Map<Villa>(villaDTO);

            //Villa model = new()
            //{
            //    amenity = createDTO.amenity,
            //    details = createDTO.details,
            //    imageUrl = createDTO.imageUrl,
            //    name = createDTO.name,
            //    sqft = createDTO.Sqft,
            //    occupancy =     createDTO.Occupancy,
            //    rate = createDTO.rate
            //};
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("Getvilla", new { id = model.Id }, model);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task <IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (updateDTO.id == null || id != updateDTO.id)
            {
                return BadRequest();
            }
            Villa model=_mapper.Map<Villa>(updateDTO);
            //Villa model = new()
            //{
            //    Id =updateDTO.id,
            //    amenity = updateDTO.amenity,
            //    details = updateDTO.details,
            //    imageUrl = updateDTO.imageUrl,
            //    name = updateDTO.name,
            //    sqft = updateDTO.Sqft,
            //    occupancy = updateDTO.Occupancy,
            //    rate = updateDTO.rate
            //};
            _db.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdatepartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatepartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var patch = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            VillaUpdateDTO vilaDTO = _mapper.Map <VillaUpdateDTO> (patch);
            //VillaUpdateDTO vilaDTO = new()
            //{
            //    id  = patch.Id,
            //    amenity = patch.amenity,
            //    details = patch.details,
            //    imageUrl = patch.imageUrl,
            //    name = patch.name,
            //    Sqft = patch.sqft,
            //    Occupancy = patch.occupancy,
            //    rate = patch.rate
            //};
            
            if (patch == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(vilaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(vilaDTO);
            
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
