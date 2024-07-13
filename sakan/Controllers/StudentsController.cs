using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sakan.DTOs;
using sakan.Models;
using System.Collections;
using System.IO;

namespace sakan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize]

    public class StudentsController : ControllerBase
    {
        private readonly SakanDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(SakanDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        ////veiw student profile(" https://localhost:7221/api/Students/5 Veiw Student Profile")
        //[HttpGet("{id}/VeiwStudentProfile")]
        //public async Task<IActionResult> VeiwStudentProfile(int id)
        //{
        //    var stud = await _context.Students.Where(x => x.Id == id).ToListAsync();
        //    return Ok(_mapper.Map<List<RoommatesDTO>>(stud));

        //}



        //veiw student profile(" http://localhost:5160/api/Students/2/VeiwStudentProfile")
        [HttpGet("{id}/VeiwStudentProfile")]
        public async Task<IActionResult> VeiwStudentProfile(int id)
        {
            var stud = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            string base64Image = null; // Initialize with null

            if (stud.Photo != null)
            {
                base64Image = Convert.ToBase64String(stud.Photo);
            }
            var x = new RoommatesDTO
            {
                Name = stud.Name,
                Governorate = stud.Governorate,
                Level = stud.Level,
                faculty = stud.faculty,
                Photo = base64Image
            };

            return Ok(x);
        }


        ////return saved Ads
        ////https://localhost:7221/api/Students/1 Return Saved Ads
        //[HttpGet("{studId}/ReturnSavedAds")]
        //public async Task<IActionResult> VeiwSavedAds(int studId)
        //{
        //    var stud = await _context.Students.SingleOrDefaultAsync(x => x.Id == studId);
        //    return Ok(stud.Saved);

        //}

        ////saved ads
        ////https://localhost:7221/api/Students/3 Saved Ads
        //[HttpPut("{studID}/SavedAds")]
        //public async Task<IActionResult> love(int studID, [FromForm] SavedDTO housIdDto)
        //{
        //    var stud = await _context.Students.SingleOrDefaultAsync(x => x.Id == studID);
        //    stud.Saved.Add(housIdDto.Saved);

        //    await _context.SaveChangesAsync();

        //    return Ok(stud.Saved);

        //}



        ////update account info("http://localhost:5160/api/Students/2/UpdateAccountInfo")
        [HttpPut("{id}/UpdateAccountInfo")]
        public async Task<IActionResult> updateStudent(int id, [FromForm] CreateStudentDTO dto)
        {

            var stud = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);

            stud.Name = dto.Name ?? stud.Name;
            stud.Email = dto.Email ?? stud.Email;
            stud.Password = dto.Password ?? stud.Password;
            stud.faculty = dto.faculty ?? stud.faculty;
            stud.Governorate = dto.Governorate ?? stud.Governorate;
            stud.Level = dto.Level ?? stud.Level;
            if (dto.Photo != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo.CopyToAsync(dataStream);
                stud.Photo = dataStream.ToArray();
            }
            else
                stud.Photo = stud.Photo;

            await _context.SaveChangesAsync();

            return Created();
        }

        //Delete account("http://localhost:5160/api/Students/3/DeleteAccount")
        [HttpDelete("{id}/DeleteAccount")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var stud = await _context.Students.FindAsync(id);
            
            _context.Remove(stud);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        //http://localhost:5160/api/Students/6/Reserve
        [HttpPut("{studId}/Reserve")]
        public async Task<IActionResult> Reserve(int studId,[FromForm]int houseId)
        {
            var stud = await _context.Students.SingleOrDefaultAsync(x => x.Id == studId);
            if(stud == null)
                return NoContent();
            stud.HouseID = houseId;

          //  await _context.AddAsync(stud);
            await _context.SaveChangesAsync();

            return Created();
        }

        // http://localhost:5160/api/Students/5/GetRoommates
        //get roommates
        [HttpGet("{houseId}/GetRoommates")]
        public async Task<IActionResult> GetRoommates(byte houseId)
        {
            var stud = await _context.Students.Where(x => x.HouseID == houseId).ToListAsync();
            return Ok(_mapper.Map<List<RoommatesDTO>>(stud));

        }
    }
}
