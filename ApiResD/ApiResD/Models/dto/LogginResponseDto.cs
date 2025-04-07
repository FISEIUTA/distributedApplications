namespace ApiResD.Models.dto
{
    public class LogginResponseDto
    {
        public required string UserName { get; set; }
        public required string Accesstoken { get; set; }
        public required int ExpirationIn { get; set; }
    }
}
