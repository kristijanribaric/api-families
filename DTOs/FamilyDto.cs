namespace FamiliesApi.DTOs {
    public class FamilyDto {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public List<FamilyMemberDto>? Members { get; set; } 
    }
}
