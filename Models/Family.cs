


namespace TestApi.Models
{
    public class Family
    {
        public int Id { get; set; } = 0;
        public string LastName { get; set; } = string.Empty;
        public List<FamilyMember> Members { get; set; }

       
    }
}
