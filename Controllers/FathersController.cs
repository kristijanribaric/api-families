using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamiliesApi.Data;
using FamiliesApi.DTOs;
using FamiliesApi.Models;

namespace FamiliesApi.Controllers {
 
    public class FathersController : FamilyMembersController<Father,FatherDto> {
        public FathersController( DataContext context, IMapper mapper ) : base(context, mapper) {
        }

        [HttpPut]
        public override async Task<ActionResult<FatherDto>> UpdateMember( [FromBody] FatherDto receivedFather ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == receivedFather.Id).FirstOrDefaultAsync();
            if ( familyMember == null || familyMember is not Father ) {
                return BadRequest("Family Member not found or is not a father");
            }
            var father = familyMember as Father;
            if ( receivedFather.Type != father.GetType().Name ) {

                FamilyMember? newMember = null;
                foreach ( FamilyMember memberType in FamilyMember.FamilyMemberTypes ) {
                    if ( receivedFather.Type == memberType.GetType().Name ) {
                        
                        newMember = _mapper.Map(receivedFather, memberType);
                    }
                }
                if ( newMember == null ) {
                    return BadRequest("Family member type not found.");
                }
                newMember.FamilyId = father.FamilyId;
                newMember.Id = father.Id;
                _context.FamilyMembers.Remove(father);
                _context.FamilyMembers.Add(newMember);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<FamilyMemberDto>(newMember));
            }

            father.Age = receivedFather.Age;
            father.FirstName = receivedFather.FirstName;
            father.LastName = receivedFather.LastName;
            father.FavoriteCar = receivedFather.FavoriteCar;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<FatherDto>(father));
        }
    }
}
