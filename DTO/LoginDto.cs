using pos_simple.Model;

namespace pos_simple.Model
{
    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}


public class LoginResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string Token { get; set; } = string.Empty;
    public User Data { get; set; } = null!;
}