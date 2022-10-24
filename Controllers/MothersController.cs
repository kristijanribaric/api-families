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
            if ( familyMember == null || familyMember is not Mother ) {
                return BadRequest("Family Member not found or is not a mother");
            }
            var mother = familyMember as Mother;
            if ( receivedMother.Type != mother.GetType().Name ) {

                FamilyMember? newMember = null;
                foreach ( FamilyMember memberType in FamilyMember.FamilyMemberTypes ) {
                    if ( receivedMother.Type == memberType.GetType().Name ) {
                        
                        newMember = _mapper.Map(receivedMother, memberType);
                    }
                }
                if ( newMember == null ) {
                    return BadRequest("Family member type not found.");
                }
                newMember.FamilyId = mother.FamilyId;
                newMember.Id = mother.Id;
                _context.FamilyMembers.Remove(mother);
                _context.FamilyMembers.Add(newMember);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<FamilyMemberDto>(newMember));
            }

            mother.Age = receivedMother.Age;
            mother.FirstName = receivedMother.FirstName;
            mother.LastName = receivedMother.LastName;
            mother.FavoriteDish = receivedMother.FavoriteDish;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<MotherDto>(mother));
        }
    }
}
