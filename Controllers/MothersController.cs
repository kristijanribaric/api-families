using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamiliesApi.Data;
using FamiliesApi.DTOs;
using FamiliesApi.Models;


namespace FamiliesApi.Controllers {

    public class MothersController : FamilyMembersController<Mother, MotherDto> {
        public MothersController( DataContext context, IMapper mapper ) : base(context, mapper) {
        }

        [HttpPut]
        public override async Task<ActionResult<MotherDto>> UpdateMember( [FromBody] MotherDto receivedMother ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == receivedMother.Id).FirstOrDefaultAsync();
            if ( familyMember == null) {
                return BadRequest("Family Member not found.");
            }
            
            if ( receivedMother.Type != familyMember.GetType().Name ) {

                FamilyMember? newMember = null;
                foreach ( FamilyMember memberType in FamilyMember.FamilyMemberTypes ) {
                    if ( receivedMother.Type == memberType.GetType().Name ) {
                        
                        newMember = _mapper.Map(receivedMother, memberType);
                    }
                }
                if ( newMember == null ) {
                    return BadRequest("Family member type not found.");
                }
                newMember.FamilyId = familyMember.FamilyId;
                newMember.Id = familyMember.Id;
                _context.FamilyMembers.Remove(familyMember);
                _context.FamilyMembers.Add(newMember);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<FamilyMemberDto>(newMember));
            }
            var mother = familyMember as Mother;
            mother.Age = receivedMother.Age;
            mother.FirstName = receivedMother.FirstName;
            mother.LastName = receivedMother.LastName;
            mother.FavoriteDish = receivedMother.FavoriteDish;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<MotherDto>(mother));
        }
    }
}
