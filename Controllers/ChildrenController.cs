using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamiliesApi.Data;
using FamiliesApi.DTOs;
using FamiliesApi.Models;

namespace FamiliesApi.Controllers {

    public class ChildrenController : FamilyMembersController<Child, ChildDto> {
        public ChildrenController( DataContext context, IMapper mapper ) : base(context, mapper) {
        }

        [HttpPut]
        public override async Task<ActionResult<ChildDto>> UpdateMember( [FromBody] ChildDto receivedChild ) {
            var familyMember = await _context.FamilyMembers.Where(m => m.Id == receivedChild.Id).FirstOrDefaultAsync();
            if ( familyMember == null ) {
                return BadRequest("Family Member not found.");
            }
            
            if ( receivedChild.Type != familyMember.GetType().Name ) {

                FamilyMember? newMember = null;
                foreach ( FamilyMember memberType in FamilyMember.FamilyMemberTypes ) {
                    if ( receivedChild.Type == memberType.GetType().Name ) {

                        newMember = _mapper.Map(receivedChild, memberType);
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
            var child = familyMember as Child;
            child.Age = receivedChild.Age;
            child.FirstName = receivedChild.FirstName;
            child.LastName = receivedChild.LastName;
            child.FavoriteToy = receivedChild.FavoriteToy;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ChildDto>(child));
        }
    }
}
