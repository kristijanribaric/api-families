using FamiliesApi.DTOs;
using System.Text.RegularExpressions;

namespace FamiliesApi.Models
{
    public class Child : FamilyMember
    {
        public string FavoriteToy { get; set; } = string.Empty;
        public override bool CheckIfMember( string memberString ) {
            return Regex.IsMatch(memberString, "^([A-Z]|[a-z]|[^\\x00-\\x7F])+\\s+([A-Z]|[a-z]|[^\\x00-\\x7F])+$");
        }

        public override Child CreateMember( string memberString ) {
            var memberArr = memberString.Trim().Split(' ');
            var FirstName = memberArr [0];
            var LastName = memberArr [1];
            return new Child { FirstName = FirstName, LastName = LastName };
        }

        public override Child CreateMember( FamilyMemberDto member ) {
            return new Child { FirstName = member.FirstName, LastName = member.LastName, Age = member.Age };
        }
    }
}
