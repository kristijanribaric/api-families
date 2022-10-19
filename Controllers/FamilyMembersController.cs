using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamiliesApi.Data;
using FamiliesApi.DTOs;
using FamiliesApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FamiliesApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FamilyMembersController(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<FamilyMembersController>
        [HttpGet]
        public async Task<ActionResult<List<FamilyMemberDto>>> GetFamilyMembers() {
            return Ok(await _context.FamilyMembers.Select(m => _mapper.Map<FamilyMemberDto>(m)).ToListAsync());
        }

        // GET api/<FamilyMembersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyMemberDto>> GetFamilyMember( int id ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == id)
                                                           .Select(m => _mapper.Map<FamilyMemberDto>(m))
                                                           .FirstOrDefaultAsync();
            if ( familyMember == null ) {
                BadRequest("Family Member not found");
            }
            return Ok(familyMember);
        }
        

        // PUT api/<FamilyMembersController>/5
        [HttpPut]
        public async Task<ActionResult<FamilyMemberDto>> PutAge(  [FromBody] FamilyMemberDto receivedMember ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == receivedMember.Id).FirstOrDefaultAsync();
            if ( familyMember == null ) {
                return BadRequest("Family Member not found");
            }
            familyMember.Age = receivedMember.Age;
            familyMember.FirstName = receivedMember.FirstName;
            familyMember.LastName = receivedMember.LastName;
            
            Console.WriteLine(familyMember);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<FamilyMemberDto>(familyMember));
        }

        // DELETE api/<FamilyMembersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FamilyMemberDto>> DeleteFamilyMember( int id ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == id).FirstOrDefaultAsync();
            if ( familyMember == null ) {
                return BadRequest("Family Member not found");
            }
            _context.FamilyMembers.Remove(familyMember);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<FamilyMemberDto>(familyMember));
        }
    }
}
