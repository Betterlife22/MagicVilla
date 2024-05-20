using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
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
        
        private readonly ILogging _logger;
        public VillaApiController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet] //endpoint to get data
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas() // Ienmerarble la kieu dem duoc
        {
            _logger.Log("Get All Villas","");
            return Ok (DataStore.VillaList);
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
                _logger.Log("Get villas Error with id" + id,"error");
                return BadRequest();
            }
            var villa = DataStore.VillaList.FirstOrDefault(u => u.id == id);
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
        public ActionResult <VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(DataStore.VillaList.FirstOrDefault(u=>u.name.ToLower() == villa.name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError" ,"Villa is alredy exist !!!");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if(villa.id > 0) {
                return StatusCode (StatusCodes.Status500InternalServerError);
            } 
            villa.id = DataStore.VillaList.OrderByDescending(u=>u.id).FirstOrDefault().id +1;
            DataStore.VillaList.Add(villa);
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
            var villa=DataStore.VillaList.FirstOrDefault(u=>u.id==id);
            if(villa == null)
            {
                return NotFound();
            }
            DataStore.VillaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id , [FromBody] VillaDTO villa )
        {
            if (villa.id == null || id != villa.id)
            {
                return BadRequest();
            }
            var vill=DataStore.VillaList.FirstOrDefault(v => v.id==id);
            vill.name=villa.name;
            vill.Sqft=villa.Sqft;
            vill.Occupancy=villa.Occupancy;
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
            var patch=DataStore.VillaList.FirstOrDefault(u=>u.id==id);
            if(patch == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(patch,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
