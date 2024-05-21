using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public ActionResult <IEnumerable<VillaDTO>> GetVillas() // Ienmerarble la kieu dem duoc
        {
            
            return Ok (_db.Villas.ToList());
        }
        [HttpGet ("{id:int}",Name ="Getvilla")]
        // product response type sẽ trả về kiểu phản hồi
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult <VillaDTO> GetVillaId(int id)
        {
            if(id < 0 || id==0)
            {
               
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok (villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_db.Villas.FirstOrDefault(u => u.name.ToLower() == villa.name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa is alredy exist !!!");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if (villa.id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
            _db.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("Getvilla",new {id=villa.id},villa);
        }
        [HttpDelete ("{id:int}", Name = "DeleteVilla") ]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult  DeleteVilla(int id)
        {         
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=_db.Villas.FirstOrDefault(u=>u.id==id);
            if(villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id , [FromBody] VillaDTO villa )
        {
            if (villa.id == null || id != villa.id)
            {
                return BadRequest();
            }
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
            _db.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPatch ("{id:int}",Name ="UpdatepartialVilla")]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatepartialVilla (int id , JsonPatchDocument <VillaDTO> patchDTO)
        {
            if(patchDTO == null || id==0)
            {
                return BadRequest();
            }
            var patch=_db.Villas.FirstOrDefault(u=>u.id==id);
            VillaDTO vilaDTO = new()
            {
                amenity = patch.amenity,
                details = patch.details,
                imageUrl = patch.imageUrl,
                name = patch.name,
                sqft = patch.sqft,
                occupancy = patch.occupancy,
                rate = patch.rate
            };
            _db.Update(vilaDTO);
            _db.SaveChanges ();
            if (patch == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(vilaDTO,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
