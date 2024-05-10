using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sakan.DTOs;
using sakan.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace sakan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly SakanDbContext _context;
        private readonly JwtToken Token;
        private readonly IMapper _mapper;

        public TokensController(SakanDbContext context, JwtToken token, IMapper mapper)
        {
            _context = context;
            Token = token;
            _mapper = mapper;
        }

        [HttpPost("StudentLogin")]
        public async Task<ActionResult> StudentLogin(AuthDTO userAuth)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Email == userAuth.Email && x.Password == userAuth.Password);
            if (student != null)
            {
                // Assuming 'student' has 'Id' and 'Gender' properties
                var id = student.Id.ToString();
                var gender = student.Sex;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = Token.Issuer,
                    Audience = Token.Audiance,
                    Expires = DateTime.UtcNow.AddHours(3), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token.SigningKey)), SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Gender, gender)
            })
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);

                return Ok(new { token = accessToken, Id = id, Gender = gender });
            }

            return Unauthorized();
        }



        [HttpPost("StudentSignUp")]
        public async Task<IActionResult> createStudent([FromForm] CreateStudentDTO dto)
        {

            var stud = _mapper.Map<Student>(dto);
            if (dto.Photo != null)
            {
                using var dataStream = new MemoryStream();
                await dto.Photo.CopyToAsync(dataStream);
                stud.Photo = dataStream.ToArray();
            }
            await _context.AddAsync(stud);
            await _context.SaveChangesAsync();

            var id = stud.Id.ToString();
            var gender = stud.Sex;

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Token.Issuer,
                Audience = Token.Audiance,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token.SigningKey)),
                SecurityAlgorithms.HmacSha256),

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, id),
                    new(ClaimTypes.Gender, gender)

                })

            };
            var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
            var accessToken = TokenHandler.WriteToken(SecurityToken);
            return Ok(new { token = accessToken, Id = id, Gender = gender });
        }


        [HttpPost("OwnerLogin")]
        public async Task<ActionResult> OwnerLogin(AuthDTO userAuth)
        {

            var owner = await _context.HouseOwners.SingleOrDefaultAsync(x => x.Email == userAuth.Email && x.Password == userAuth.Password);
            if (owner != null)
            {
                var id = owner.Id.ToString();
              
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = Token.Issuer,
                    Audience = Token.Audiance,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token.SigningKey)),
                    SecurityAlgorithms.HmacSha256),

                    

                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new(ClaimTypes.NameIdentifier,id )
                    })
                   
                };
                var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
                var accessToken = TokenHandler.WriteToken(SecurityToken);
                return Ok(new { token=accessToken, Id=id });
            }
            return Unauthorized();
        }


        [HttpPost("OwnerSignUp")]
        public async Task<IActionResult> createOwner([FromForm] CreateOwnerDTO dto)
        {
            var owner = _mapper.Map<HouseOwner>(dto);

            await _context.AddAsync(owner);
            await _context.SaveChangesAsync();

            var id = owner.Id.ToString();

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Token.Issuer,
                Audience = Token.Audiance,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token.SigningKey)),
                SecurityAlgorithms.HmacSha256),

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, id)
                })

            };
            var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
            var accessToken = TokenHandler.WriteToken(SecurityToken);
            return Ok(accessToken);
        }
    }
}
