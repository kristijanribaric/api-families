namespace TestApi.DTOs {
    public class FamilyMemberDto {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Age { get; set; } 
        public string Type { get; set; } = string.Empty;
    }
}
