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
    public class FamilyMembersController<T, D> : ControllerBase where T : FamilyMember where D : FamilyMemberDto {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        public FamilyMembersController(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<FamilyMembersController>
        [HttpGet]
        public async Task<ActionResult<List<D>>> GetFamilyMembers() {
            return Ok(await _context.FamilyMembers.Where(f => f is T).Select(f => _mapper.Map<D>(f)).ToListAsync());
        }
        
        // GET api/<FamilyMembersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<D>> GetFamilyMember( int id ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == id && m is T)
                                                           .Select(m => _mapper.Map<D>(m))
                                                           .FirstOrDefaultAsync();
            if ( familyMember == null ) {
                BadRequest("Family Member not found");
            }
            return Ok(familyMember);
        }
        

        // PUT api/<FamilyMembersController>/5
        [HttpPut]
        public virtual async Task<ActionResult<D>> UpdateMember(  [FromBody] D receivedMember ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == receivedMember.Id).FirstOrDefaultAsync();
            if ( familyMember == null ) {
                return BadRequest("Family Member not found");
            }
            if ( receivedMember.Type != familyMember.GetType().Name ) {
                //delete the old one and create a new one
                
                //await _context.SaveChangesAsync();
                FamilyMember? newMember = null;
                foreach ( FamilyMember memberType in FamilyMember.FamilyMemberTypes ) {
                    if ( receivedMember.Type == memberType.GetType().Name ) {

                        newMember = memberType.CreateMember(receivedMember);
                        _context.FamilyMembers.Update(familyMember);
                    }
                }
                if ( newMember == null ) {
                    return BadRequest("Family member type not found.");
                }
                newMember.Family = familyMember.Family;
                newMember.Id = familyMember.Id;
                _context.FamilyMembers.Remove(familyMember);
                _context.FamilyMembers.Add(newMember);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<D>(newMember));
            }
           
            familyMember.Age = receivedMember.Age;
            familyMember.FirstName = receivedMember.FirstName;
            familyMember.LastName = receivedMember.LastName;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<D>(familyMember));
        }

        // DELETE api/<FamilyMembersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<D>> DeleteFamilyMember( int id ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == id).FirstOrDefaultAsync();
            if ( familyMember == null ) {
                return BadRequest("Family Member not found");
            }
            _context.FamilyMembers.Remove(familyMember);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<D>(familyMember));
        }
    }
}
