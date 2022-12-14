using System.Text.RegularExpressions;
using System.Xml.Linq;
using FamiliesApi.DTOs;
using FamiliesApi.Models;

namespace FamiliesApi.Models
{
    public class Father : FamilyMember
    {
        public string FavoriteCar { get; set; } = string.Empty;
        public override bool CheckIfMember( string memberString ) {
            return Regex.IsMatch(memberString, "^Mr\\.\\s+([A-Z]|[a-z]|[^\\x00-\\x7F])+\\s+([A-Z]|[a-z]|[^\\x00-\\x7F])+$");
        }

        public override Father CreateMember( string memberString)
        {
            var memberArr = memberString.Trim().Split(' ');
            List<string> memberList = new List<string>(memberArr);
            memberList.RemoveAt(0);
            var FirstName = memberList[0];
            var LastName = memberList[1];
            return new Father { FirstName = FirstName, LastName = LastName };
        }

        public override Father CreateMember(  FamilyMemberDto member ) {
            {
                return new Father { FirstName = member.FirstName, LastName = member.LastName, Age = member.Age };
            }
        }
    }

}

