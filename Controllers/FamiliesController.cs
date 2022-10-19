using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOs;
using TestApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FamiliesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FamilyDto>>> GetFamilies()
        {
            return Ok(await _context.Families.Select(f => _mapper.Map<FamilyDto>(f)).ToListAsync());
        }

        [HttpGet("{lastName}")]
        public async Task<ActionResult<FamilyDto>> GetFamily(string lastName)
        {
            var family = await _context.Families.Where(f => f.LastName == lastName).Select(f => _mapper.Map<FamilyDto>(f)).FirstOrDefaultAsync();
            if (family == null)
            {
                return BadRequest("Family not found");
            }
            return Ok(family);
        }

        [HttpDelete("{lastName}")]
        public async Task<ActionResult<List<FamilyDto>>> DeleteFamilies( string lastName ) {
            var families = await _context.Families.Where(f => f.LastName == lastName).ToListAsync();
            if ( families == null ) {
                return BadRequest("Family not found");
            }
            _context.Families.RemoveRange(families);
            await _context.SaveChangesAsync();
            return Ok(families.Select(f => _mapper.Map<FamilyDto>(f)));
        }

        [HttpPost("FamilyMembers")]
        public async Task<ActionResult<List<FamilyDto>>> CreateFamilyMembers( [FromBody] string membersString ) {
            var membersArr = membersString.Split(',').Select(p => p.Trim()).ToList();
            var FamilyMembers = new List<FamilyMember>();
            foreach ( var memberString in membersArr ) {
           
            
                foreach (FamilyMember memberType in FamilyMember.FamilyMemberTypes)
                {
                    if (memberType.CheckIfMember(memberString))
                    {
                        var familyMember = memberType.CreateMember(memberString);
                       
                        var family = await _context.Families.Where(f => f.LastName == familyMember.LastName).FirstOrDefaultAsync();
                        if ( family == null ) {
                            _context.Families.Add(new Family { LastName = familyMember.LastName, Members = new List<FamilyMember> { familyMember } });
                            FamilyMembers.Add(familyMember);
                        } else {
                            family.Members.Add(familyMember);
                            
                            FamilyMembers.Add(familyMember);
                        }
                     }
                }
            }
            await _context.SaveChangesAsync();
            if ( FamilyMembers.Count == 0 ) {
                return BadRequest("No family members found");
            }

            return Ok(await _context.Families.Where(fm => FamilyMembers.Select(f => f.LastName).Contains(fm.LastName)).Select(f => _mapper.Map<FamilyDto>(f)).ToListAsync());

        }
    }
}
