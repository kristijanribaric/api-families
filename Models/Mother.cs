using System.Text.RegularExpressions;

namespace FamiliesApi.Models
{
    public class Mother : FamilyMember
    {

        public override bool CheckIfMember( string memberString ) {
            return Regex.IsMatch(memberString, "^Mrs\\.\\s+[A-Z]([a-z]|[^\\x00-\\x7F])+\\s+[A-Z]([a-z]|[^\\x00-\\x7F])+$");
        }


        public override Mother CreateMember( string memberString ) {
            var memberArr = memberString.Trim().Split(' ');
            List<string> memberList = new List<string>(memberArr);
            memberList.RemoveAt(0);
            var FirstName = memberList [0];
            var LastName = memberList [1];
            return new Mother { FirstName = FirstName, LastName = LastName };
        }
    }
}
