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

        //veiw Owner profile("https://localhost:7221/api/HouseOwners/5 Veiw Owner Profile")
        [HttpGet("{id}/VeiwOwnerProfile")]
        public async Task<IActionResult> GetOwnerInfo(int id)
        {

            var owner = await _context.HouseOwners.Where(x => x.Id == id).Select(w => new CreateOwnerDTO
            {
                Name = w.Name,
                Email = w.Email,
                Password = w.Password,
                phone = w.phone
            })
              .ToListAsync();
            return Ok(owner);

            //var owner = _context.HouseOwners.Where(x=>x.Id == id).ToListAsync();
            //return Ok(_mapper.Map<List<CreateOwnerDTO>>(owner));
        }



        //sign up ("https://localhost:7221/api/HouseOwners")
        //[HttpPost("SignUp")]
        //public async Task<IActionResult> createOwner([FromForm] CreateOwnerDTO dto)
        //{
        //    var owner = _mapper.Map<HouseOwner>(dto);

        //    await _context.AddAsync(owner);
        //    await _context.SaveChangesAsync();

        //    return Created();
        //}

        ////update account info("https://localhost:7221/api/HouseOwners/id?id=1")
        [HttpPut("{id}/UpdateAccountInfo")]
        public async Task<IActionResult> updateOwner(int id, [FromForm] CreateOwnerDTO dto)
        {
            var owner = await _context.HouseOwners.SingleOrDefaultAsync(x => x.Id == id);

            owner.Name = dto.Name ?? owner.Name;
            owner.Email = dto.Email ?? owner.Email;
            owner.Password = dto.Password ?? owner.Password;
            owner.phone = dto.phone ?? owner.phone;

            await _context.SaveChangesAsync();
            return Created();
        }

        //Delete account("https://localhost:7221/api/HouseOwners/id?id=2")
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
