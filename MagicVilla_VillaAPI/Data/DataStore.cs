using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public class DataStore
    {
        public static new List<VillaDTO> VillaList = new List<VillaDTO>
                {
                new VillaDTO {id=1 , name="Pool View",Occupancy=4 , Sqft=100},
                new VillaDTO { id=2 ,name= "Beach View",Occupancy=3,Sqft=300}
            };

    }
}
