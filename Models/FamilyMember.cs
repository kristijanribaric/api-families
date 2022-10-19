
using System.Text.Json.Serialization;

namespace FamiliesApi.Models
{
    public  class FamilyMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Age { get; set; }
        public Family Family { get; set; }

        public virtual bool CheckIfMember( string memberString ) {
            throw new NotImplementedException();
        }

        public virtual FamilyMember CreateMember( string memberString ) {
            throw new NotImplementedException();
        }

        public static List<FamilyMember> FamilyMemberTypes = new List<FamilyMember>()
            { new Father(), new Child(), new Mother() };

        public static implicit operator List<object>( FamilyMember v ) {
            throw new NotImplementedException();
        }
    }
}
