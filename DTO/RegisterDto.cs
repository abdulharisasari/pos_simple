namespace pos_simple.DTO
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int Role { get; set; } // 0 = Admin, 1 = User
    }
}
