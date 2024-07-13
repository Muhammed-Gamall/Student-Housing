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
   // [Authorize]

    public class AdminsController : ControllerBase
    {
        private readonly SakanDbContext _context;
        private readonly IMapper _mapper;

        public AdminsController(SakanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // http://localhost:5160/api/Admins/2/AcceptAds
        [HttpPut("{id}/AcceptAds")]
        public async Task<IActionResult> AcceptAds(int id)
        {
            var adm = await _context.AdminHouses.Where(x => x.Id == id).ToListAsync();

            foreach (var i in adm)
            {
                var newItem = new House
                {
                    Location = i.Location,
                    Description = i.Description,
                    Photo1 = i.Photo1,
                    Photo2 = i.Photo2,
                    Photo3 = i.Photo3,
                    Photo4 = i.Photo4,
                    NumOfBeds = i.NumOfBeds,
                    NumOfRooms = i.NumOfRooms,
                    phone = i.phone,
                    Floor = i.Floor,
                    Price = i.Price,
                    Time = i.Time,
                    Sex = i.Sex,
                    HouseOwnerID = i.HouseOwnerID
                };
                _context.Add(newItem);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Delete account("http://localhost:5160/api/Admins/1/DeleteAds")
        [HttpDelete("{id}/DeleteAds")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var owner = await _context.AdminHouses.FindAsync(id);

            _context.Remove(owner);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}
