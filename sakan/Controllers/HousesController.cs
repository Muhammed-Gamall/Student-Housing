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
    [Authorize]

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
        // http://localhost:5160/api/Houses/male/GetallHouses
        [HttpGet("{sex}/GetallHouses")]
        public async Task<IActionResult> GetAllHouses(string sex)
        {
            var house = await _context.Houses.Where(x => x.Sex == sex || x.Sex == null).Select(b=>new GetAllHousesDTO
            {
                Photo1= b.Photo1==null?null: Convert.ToBase64String(b.Photo1),
                Photo2 = b.Photo2 == null ? null : Convert.ToBase64String(b.Photo2),
                Photo3 = b.Photo3 == null ? null : Convert.ToBase64String(b.Photo3),
                Photo4 = b.Photo4 == null ? null : Convert.ToBase64String(b.Photo4),
                Location = b.Location,
                Price = b.Price,
                Time = b.Time

            }).ToListAsync();
            return Ok(house);
        }


       
        //get all houses("اعلانات البيوت بالموقع")
        // http://localhost:5160/api/Houses/shalaby/GetallHousesWithLocation
        [HttpGet("{location}/GetallHousesWithLocation")]
        public async Task<IActionResult> GetallHousesWithLocation(string location)
        {
            var house = await _context.Houses.Where(x => x.Location ==location).Select(b => new GetAllHousesDTO
            {
                Photo1 = b.Photo1 == null ? null : Convert.ToBase64String(b.Photo1),
                Photo2 = b.Photo2 == null ? null : Convert.ToBase64String(b.Photo2),
                Photo3 = b.Photo3 == null ? null : Convert.ToBase64String(b.Photo3),
                Photo4 = b.Photo4 == null ? null : Convert.ToBase64String(b.Photo4),
                Location = b.Location,
                Price = b.Price,
                Time = b.Time

            }).ToListAsync();
            return Ok(house);

        }


        //get selected house by id("اعلان البيت اللي هيدوس عليه") 
        // http://localhost:5160/api/Houses/3/GetSelectedHouseById
        [HttpGet("{id}/GetSelectedHouseById")]
        public async Task<IActionResult> GetById(int id)
        {

            var house = await _context.Houses.Where(x => x.Id == id).Select(b => new GetHousesDTO
            {
                Photo1 = b.Photo1 == null ? null : Convert.ToBase64String(b.Photo1),
                Photo2 = b.Photo2 == null ? null : Convert.ToBase64String(b.Photo2),
                Photo3 = b.Photo3 == null ? null : Convert.ToBase64String(b.Photo3),
                Photo4 = b.Photo4 == null ? null : Convert.ToBase64String(b.Photo4),
                Location = b.Location,
                Price = b.Price,
                Floor = b.Floor,
                NmOfRooms = b.NumOfRooms,
                NumOfBeds = b.NumOfBeds,
                Description = b.Description,
                phone= b.phone
             })
                .ToListAsync();
          
            return Ok(house);
        }



        //get owner's Ads("اعلانات البيوت بتاعت صاحب البيت") 
        // http://localhost:5160/api/Houses/3/GetOwnerAds
        [HttpGet("{OwnerId}/GetOwnerAds")]
        public async Task<IActionResult> GetOwnerAds(int OwnerId)
        {
            var house = await _context.Houses.Where(x => x.HouseOwnerID == OwnerId).Select(b => new GetAllHousesDTO
            {
                Photo1 = b.Photo1 == null ? null : Convert.ToBase64String(b.Photo1),
                Photo2 = b.Photo2 == null ? null : Convert.ToBase64String(b.Photo2),
                Photo3 = b.Photo3 == null ? null : Convert.ToBase64String(b.Photo3),
                Photo4 = b.Photo4 == null ? null : Convert.ToBase64String(b.Photo4),
                Location = b.Location,
                Price = b.Price,
                Time = b.Time

            }).ToListAsync();
            return Ok(house);
        }


        ////// get related Ads("اعلانات البيوت اللي فيها الطلبه ف نفس الكليه والمحافظه بتاعت الطالب")
        ///http://localhost:5160/api/Houses/computer%20science/cairo
        [HttpGet("{college}/{city}")]
        public async Task<IActionResult> GetRelatedAds(string college, string city)
        {
            var house = await _context.Students.Include(h=>h.House).Where(x => x.faculty == college && x.Governorate == city)
                .Select(m=>new GetAllHousesDTO
                {
                    Id = m.House.Id,
                    Photo1 = m.House.Photo1 == null ? null : Convert.ToBase64String(m.House.Photo1),
                    Photo2 = m.House.Photo2 == null ? null : Convert.ToBase64String(m.House.Photo2),
                    Photo3 = m.House.Photo3 == null ? null : Convert.ToBase64String(m.House.Photo3),
                    Photo4 = m.House.Photo4 == null ? null : Convert.ToBase64String(m.House.Photo4),
                    Location = m.House.Location,
                    Price = m.House.Price,
                })
                .ToListAsync();
            if (house == null)
                return BadRequest("No Ads");
            return Ok(house);
        }


        //create house advertisement
        // http://localhost:5160/api/Houses/2/CreateHouseAdvertisement
        [HttpPost("{ownerId}/CreateHouseAdvertisement")]
        public async Task<IActionResult> CreateAd(int ownerId , [FromForm] CreateHouseDTO dto)
        {
            var house = _mapper.Map<AdminHouse>(dto);
            if (dto.Photo1 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo1.CopyToAsync(dataStream);
                house.Photo1 = dataStream.ToArray();
            }
            if (dto.Photo2 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo2.CopyToAsync(dataStream);
                house.Photo2 = dataStream.ToArray();
            }
            if (dto.Photo3 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo3.CopyToAsync(dataStream);
                house.Photo3 = dataStream.ToArray();
            }
            if (dto.Photo4 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo4.CopyToAsync(dataStream);
                house.Photo4 = dataStream.ToArray();
            }
            house.HouseOwnerID = ownerId;

            await _context.AddAsync(house);
            await _context.SaveChangesAsync();

            return Created();
        }





        ////update ads
        /// http://localhost:5160/api/Houses/3/UpdateAccountInfo
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
            if (dto.Photo1 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo1.CopyToAsync(dataStream);
                house.Photo1 = dataStream.ToArray();
            }
            else
                house.Photo1 = house.Photo1;

            if (dto.Photo2 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo2.CopyToAsync(dataStream);
                house.Photo2 = dataStream.ToArray();
            }
            else
                house.Photo2 = house.Photo2;

            if (dto.Photo3 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo3.CopyToAsync(dataStream);
                house.Photo3 = dataStream.ToArray();
            }
            else
                house.Photo3 = house.Photo3;

            if (dto.Photo4 != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo4.CopyToAsync(dataStream);
                house.Photo4 = dataStream.ToArray();
            }
            else
                house.Photo4 = house.Photo4;

            await _context.SaveChangesAsync();
            return Created();
        }

        //Delete ads("")
        // http://localhost:5160/api/Houses/4/DeleteAds
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
