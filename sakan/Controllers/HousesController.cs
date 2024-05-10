using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sakan.DTOs;
using sakan.Models;

namespace sakan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]

    public class HousesController : ControllerBase
    {
        private readonly SakanDbContext _context;
        private readonly IMapper _mapper;
        public HousesController(SakanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //get all houses("اعلانات البيوت كلها")
        // https://localhost:7221/api/Houses/get all houses
        [HttpGet("{sex}/GetallHouses")]
        public async Task<IActionResult> GetAllHouses(string sex)
        {
            var house = await _context.Houses.Where(x=>x.Sex==sex|| x.Sex==null).ToListAsync();
            return Ok(_mapper.Map<List<GetAllHousesDTO>>(house));
        }


        //get all houses("اعلانات البيوت بالموقع")
        [HttpGet("{location}/GetallHousesWithLocation")]
        public async Task<IActionResult> GetallHousesWithLocation(string location)
        {
            var house = await _context.Houses.Where(x => x.Location == location).ToListAsync();
            if(house == null)
              return BadRequest();
           
            return Ok(_mapper.Map<List<GetAllHousesDTO>>(house));
        }


            //get selected house by id("اعلان البيت اللي هيدوس عليه") 
            //https://localhost:7221/api/Houses/11 get selected house by id
            [HttpGet("{id}/GetSelectedHouseById")]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await _context.Houses.Where(x => x.Id == id).ToListAsync();
            return Ok(_mapper.Map<List<GetHousesDTO>>(house));
        }


        //get owner's Ads("اعلانات البيوت بتاعت صاحب البيت") 
        //https://localhost:7221/api/Houses/5 get owner's Ads
        [HttpGet("{OwnerId}/GetOwnerAds")]
        public async Task<IActionResult> GetOwnerAds(int OwnerId)
        {
            var house = await _context.Houses.Where(x => x.HouseOwnerID == OwnerId).ToListAsync();
            return Ok(_mapper.Map<List<GetAllHousesDTO>>(house));
        }


        //// get related Ads("اعلانات البيوت اللي فيها الطلبه ف نفس الكليه والمحافظه بتاعت الطالب")
        //[HttpGet("{college}/{city}")]
        //public async Task<IActionResult> GetRelatedAds(string college, string city)
        //{
        //    var house = await _context.Houses.Where(x => x.Student.faculty == college)
        //        .Where(x => x.Student.Governorate == city).ToListAsync();
        //    if (house == null)
        //        return BadRequest("No Ads");
        //    return Ok(_mapper.Map<List<GetHousesDTO>>(house));
        //}


        //create house advertisement
        // ("https://localhost:7221/api/Houses/create house advertisement")
        [HttpPost("{ownerId}/CreateHouseAdvertisement")]
        public async Task<IActionResult> CreateAd(int ownerId,string time,[FromForm] CreateHouseDTO dto)
        {
            var house = _mapper.Map<House>(dto);
            if (dto.Photo != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo.CopyToAsync(dataStream);
                house.Photo = dataStream.ToArray();
            }
            house.HouseOwnerID = ownerId;
            house.Time = time;

            await _context.AddAsync(house);
            await _context.SaveChangesAsync();

            return Created();
        }

        ////update account info("")
        ///https://localhost:7221/api/Houses/4 update account info
        [HttpPut("{id}/UpdateAccountInfo")]
        public async Task<IActionResult> updateHouse(int id, [FromForm] UpdateHouseDTO dto)
        {
            var house = await _context.Houses.SingleOrDefaultAsync(x => x.Id == id);

            house.Location = dto.Location ?? house.Location;
            house.Description = dto.Description ?? house.Description;
            house.NumOfRooms = dto.NumOfRooms ?? house.NumOfRooms;
            house.NumOfBeds = dto.NumOfBeds ?? house.NumOfBeds;
            house.Floor = dto.Floor ?? house.Floor;
            house.Price = dto.Price ?? house.Price;
            if (dto.Photo != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo.CopyToAsync(dataStream);
                house.Photo = dataStream.ToArray();
            }
            else
                house.Photo = house.Photo;

            await _context.SaveChangesAsync();
            return Created();
        }

        //Delete ads("")
        //https://localhost:7221/api/Houses/4 Delete ads
        [HttpDelete("{id}/DeleteAds")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var house = await _context.Houses.FindAsync(id);

            _context.Remove(house);
            await _context.SaveChangesAsync();
            return NoContent();

        }



    }
}
