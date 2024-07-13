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
    public class HouseOwnersController : ControllerBase
    {
        private readonly SakanDbContext _context;
        private readonly IMapper _mapper;
        public HouseOwnersController(SakanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //veiw Owner profile("http://localhost:5160/api/HouseOwners/1/VeiwOwnerProfile")
        [HttpGet("{id}/VeiwOwnerProfile")]
        public async Task<IActionResult> GetOwnerInfo(int id)
        {
            var owner = await _context.HouseOwners.Where(x => x.Id == id).Select(w => new CreateOwnerDTO
            {
                Name = w.Name,
                Email = w.Email,
                Password = w.Password,
              
            })
              .ToListAsync();
            return Ok(owner);

        }


        ////update account info("http://localhost:5160/api/HouseOwners/1/UpdateAccountInfo")
        [HttpPut("{id}/UpdateAccountInfo")]
        public async Task<IActionResult> updateOwner(int id, [FromForm] CreateOwnerDTO dto)
        {
            var owner = await _context.HouseOwners.SingleOrDefaultAsync(x => x.Id == id);

            owner.Name = dto.Name ?? owner.Name;
            owner.Email = dto.Email ?? owner.Email;
            owner.Password = dto.Password ?? owner.Password;
            await _context.SaveChangesAsync();
            return Created();
        }

        //Delete account("http://localhost:5160/api/HouseOwners/1/DeleteAccount")
        [HttpDelete("{id}/DeleteAccount")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var owner = await _context.HouseOwners.FindAsync(id);

            _context.Remove(owner);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
