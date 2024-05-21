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
        public VillaApiController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet] //endpoint to get data
        [ProducesResponseType(StatusCodes.Status200OK)]
        //using async
        public async Task <ActionResult<IEnumerable<VillaDTO>>> GetVillas() // Ienmerarble la kieu dem duoc
        {

            return Ok(await _db.Villas.ToListAsync());
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
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villa)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (await _db.Villas.FirstOrDefaultAsync(u => u.name.ToLower() == villa.name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa is alredy exist !!!");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            //if (villa.id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Villa model = new()
            {
                amenity = villa.amenity,
                details = villa.details,
                imageUrl = villa.imageUrl,
                name = villa.name,
                sqft = villa.Sqft,
                occupancy = villa.Occupancy,
                rate = villa.rate
            };
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villa)
        {
            if (villa.id == null || id != villa.id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Id =villa.id,
                amenity = villa.amenity,
                details = villa.details,
                imageUrl = villa.imageUrl,
                name = villa.name,
                sqft = villa.Sqft,
                occupancy = villa.Occupancy,
                rate = villa.rate
            };
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
            VillaUpdateDTO vilaDTO = new()
            {
                id  = patch.Id,
                amenity = patch.amenity,
                details = patch.details,
                imageUrl = patch.imageUrl,
                name = patch.name,
                Sqft = patch.sqft,
                Occupancy = patch.occupancy,
                rate = patch.rate
            };
            
            if (patch == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(vilaDTO, ModelState);
        
        Villa model = new Villa()
        {
            Id = vilaDTO.id,
            amenity = vilaDTO.amenity,
            details = vilaDTO.details,
            imageUrl = vilaDTO.imageUrl,
            name = vilaDTO.name,
            sqft = vilaDTO.Sqft,
            occupancy = vilaDTO.Occupancy,
            rate = vilaDTO.rate
        };
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
