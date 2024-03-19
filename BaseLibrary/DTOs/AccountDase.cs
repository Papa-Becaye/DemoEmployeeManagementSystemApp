using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class AccountDase
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]

        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]

        public string? Password { get; set; }
    }
}
