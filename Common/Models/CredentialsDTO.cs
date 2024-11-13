using System.ComponentModel.DataAnnotations;


namespace Common.Models
{
    public class CredentialsDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
